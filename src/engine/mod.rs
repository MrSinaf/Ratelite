use crate::VecI2;
use crate::engine::window::GameWindow;
use glow::{BLEND, HasContext, ONE_MINUS_SRC_ALPHA, SRC_ALPHA};
use glutin::context::PossiblyCurrentContext;
use glutin::surface::Surface;
use glutin::{
    config::{ConfigTemplateBuilder, GlConfig},
    context::{ContextAttributesBuilder, NotCurrentGlContext},
    display::{GetGlDisplay, GlDisplay},
    prelude::GlSurface,
    surface::{SurfaceAttributesBuilder, SwapInterval, WindowSurface},
};
use raw_window_handle::HasWindowHandle;
use std::ffi::CString;
use std::num::NonZeroU32;
use std::rc::Rc;
use winit::{
    application::ApplicationHandler,
    dpi::LogicalSize,
    event::WindowEvent,
    event_loop::{ActiveEventLoop, ControlFlow, EventLoop},
    platform::windows::{Color, WindowAttributesExtWindows},
    window::{Icon, Window, WindowId},
};

mod arena;
pub mod scene;
pub mod vault;
pub mod window;

pub use scene::Scene;
pub use scene::SceneAction;
pub use window::GameContext;

pub struct Ratelite {
    gl: Option<Rc<glow::Context>>,
    gl_surface: Option<Surface<WindowSurface>>,
    gl_context: Option<PossiblyCurrentContext>,

    game_name: String,
    game_window: Option<GameWindow>,
    first_scene: Option<Box<dyn Scene>>,
}

impl Ratelite {
    pub fn launch<S: Scene + 'static>(game_name: &str, scene: S) {
        let event_loop = EventLoop::new().unwrap();
        event_loop.set_control_flow(ControlFlow::Poll);
        event_loop
            .run_app(&mut Self {
                gl: None,
                gl_surface: None,
                gl_context: None,

                game_name: game_name.into(),
                game_window: None,
                first_scene: Some(Box::new(scene)),
            })
            .unwrap();
    }
}

impl ApplicationHandler for Ratelite {
    fn resumed(&mut self, event_loop: &ActiveEventLoop) {
        // Récupération de l'icône par défaut du moteur :
        let icon = match image::load_from_memory(include_bytes!("../../icon.png")) {
            Ok(result) => {
                let image = result.into_rgba8();
                let (width, height) = image.dimensions();
                let rgba = image.into_raw();
                match Icon::from_rgba(rgba, width, height) {
                    Ok(icon) => Some(icon),
                    Err(_) => None,
                }
            }
            Err(_) => None,
        };

        let color = Color::from_rgb(33, 33, 44);
        let win_attributes = Window::default_attributes()
            .with_title(&self.game_name)
            .with_window_icon(icon)
            .with_min_inner_size(LogicalSize::new(600.0, 300.0))
            .with_inner_size(LogicalSize::new(1200.0, 600.0))
            .with_title_text_color(Color::from_rgb(53, 119, 92))
            .with_title_background_color(Some(color))
            .with_border_color(Some(color));

        let display_builder =
            glutin_winit::DisplayBuilder::new().with_window_attributes(Some(win_attributes));

        let (window, gl_config) = display_builder
            .build(event_loop, ConfigTemplateBuilder::new(), |configs| {
                configs
                    .reduce(|accum, config| {
                        if config.num_samples() > accum.num_samples() {
                            config
                        } else {
                            accum
                        }
                    })
                    .unwrap()
            })
            .unwrap();
        let window = window.unwrap();
        let raw_window_handle = window.window_handle().unwrap().as_raw();

        let context_attributes = ContextAttributesBuilder::new().build(Some(raw_window_handle));
        let not_current_gl_context = unsafe {
            gl_config
                .display()
                .create_context(&gl_config, &context_attributes)
                .unwrap()
        };

        let win_size = window.inner_size();
        let surface_attributes = SurfaceAttributesBuilder::<WindowSurface>::new().build(
            raw_window_handle,
            NonZeroU32::new(win_size.width).unwrap(),
            NonZeroU32::new(win_size.height).unwrap(),
        );
        let gl_surface = unsafe {
            gl_config
                .display()
                .create_window_surface(&gl_config, &surface_attributes)
                .unwrap()
        };

        let gl_context = not_current_gl_context.make_current(&gl_surface).unwrap();
        let gl = unsafe {
            glow::Context::from_loader_function(|s| {
                let s = CString::new(s).unwrap();
                gl_config.display().get_proc_address(&s) as *const _
            })
        };

        unsafe {
            gl.enable(BLEND);
            gl.blend_func(SRC_ALPHA, ONE_MINUS_SRC_ALPHA);
        }

        gl_surface
            .set_swap_interval(&gl_context, SwapInterval::Wait(NonZeroU32::new(1).unwrap()))
            .unwrap();

        let gl = Rc::new(gl);
        self.gl = Some(gl.clone());
        self.gl_surface = Some(gl_surface);
        self.gl_context = Some(gl_context);
        self.game_window = Some(GameWindow::new(gl.clone(), window));

        self.game_window
            .as_mut()
            .unwrap()
            .add_scene(self.first_scene.take().unwrap())
    }

    fn window_event(&mut self, event_loop: &ActiveEventLoop, _id: WindowId, event: WindowEvent) {
        match event {
            WindowEvent::CloseRequested => {
                event_loop.exit();
            }
            WindowEvent::RedrawRequested => {
                if let (Some(gl), Some(surface), Some(context), Some(game)) = (
                    &self.gl,
                    &self.gl_surface,
                    &self.gl_context,
                    &mut self.game_window,
                ) {
                    unsafe {
                        gl.clear_color(33.0 / 255.0, 33.0 / 255.0, 44.0 / 255.0, 1.0);
                        gl.clear(glow::COLOR_BUFFER_BIT);
                    }

                    game.update();
                    game.render();

                    surface.swap_buffers(context).unwrap();
                    game.get_window().request_redraw();
                }
            }
            WindowEvent::Resized(size) => {
                if let (Some(gl), Some(surface), Some(context), Some(game)) = (
                    &self.gl,
                    &self.gl_surface,
                    &self.gl_context,
                    &mut self.game_window,
                ) {
                    if let (Some(width), Some(height)) = (
                        NonZeroU32::new(size.width / 2),
                        NonZeroU32::new(size.height),
                    ) {
                        surface.resize(context, width, height);
                        let size = VecI2::new(size.width as i32, size.height as i32);
                        unsafe {
                            gl.viewport(0, 0, size.x, size.y);
                        }
                        game.resized(size);
                    }
                }
            }
            _ => {}
        }
    }
}
