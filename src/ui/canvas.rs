use crate::Matrix3x3;
use crate::rendering::native::buffer::{GBuffer, Target};
use crate::ui::element::{Element, Transform, Widget};
use crate::VecF2;
use crate::window::GameContext;

pub struct Canvas {
    root: Element,
    transform: Transform,
    uniform: CanvasUniform,
}

impl Canvas {
    pub fn new() -> Self {
        Self {
            root: Element::empty().with_element(|e| e.set_size(VecF2::new(1200.0, 600.0))),
            transform: Transform::by_canvas(VecF2::new(0.0, 0.0)),
            uniform: CanvasUniform {
                buffer: None,
                delta_time: 0.0,
                time: 0.0,
                resolution: VecF2::new(1200.0, 600.0),
                projection: Matrix3x3::create_orthographic(1200.0, 600.0, false),
            }
        }
    }

    pub fn start(&mut self, ctx: &mut GameContext) {
        self.uniform.create_buffer(ctx);
    }

    pub fn update(&mut self, ctx: &mut GameContext) {
        let uniform = &mut self.uniform;
        uniform.delta_time = ctx.get_delta_time();
        uniform.time = ctx.get_time();

        self.uniform.update_buffer(ctx);
        self.root.update(ctx, &self.transform);
    }

    pub fn render(&self, ctx: &GameContext) {
        self.root.render(ctx);
    }

    pub fn resize(&mut self, ctx: &GameContext) {
        let size: VecF2 = ctx.get_window_size().into();
        self.uniform.resolution = size;
        self.uniform.projection = Matrix3x3::create_orthographic(size.x, size.y, false);
        self.root.set_size(size);
    }

    pub fn add_child<W: Widget + 'static>(&mut self, child: W) {
        self.root.add_child(child);
    }

    #[allow(unused)]
    pub fn get_child(&self, index: usize) -> Option<&dyn Widget> {
        self.root.get_child(index)
    }

    #[allow(unused)]
    pub fn get_child_mut(&mut self, index: usize) -> Option<&mut dyn Widget> {
        self.root.get_child_mut(index)
    }
}

struct CanvasUniform {
    buffer: Option<GBuffer>,

    delta_time: f32,
    time: f32,
    resolution: VecF2,
    projection: Matrix3x3,
}

impl CanvasUniform {
    fn create_buffer(&mut self, ctx: &GameContext) {
        let proj_array = self.to_array();
        self.buffer = Some(GBuffer::from_array::<f32>(
            ctx.get_gl(),
            Target::UniformBuffer,
            &proj_array,
            true,
        ));

        if let Some(buffer) = &self.buffer {
            use glow::HasContext;
            unsafe {
                ctx.get_gl().bind_buffer_base(glow::UNIFORM_BUFFER, 0, Some(buffer.handle()));
            }
        }
    }

    fn update_buffer(&mut self, ctx: &GameContext) {
        if let Some(buffer) = &self.buffer {
            let array = &self.to_array();
            buffer.set_array::<f32>(ctx.get_gl(), 0, array).unwrap()
        }
    }

    fn to_array(&self) -> [f32; 16] {
        let proj_array = self.projection.as_std140();
        [
            self.time, self.delta_time, self.resolution.x, self.resolution.y,
            proj_array[0], proj_array[1], proj_array[2], proj_array[3],
            proj_array[4], proj_array[5], proj_array[6], proj_array[7],
            proj_array[8], proj_array[9], proj_array[10], proj_array[11],
        ]
    }
}