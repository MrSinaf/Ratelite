use crate::engine::scene::Scene;
use crate::vault::Vault;
use crate::{SceneAction, VecI2};
use std::rc::Rc;
use std::time::Instant;
use winit::window::Window;

pub struct GameWindow {
    window: Window,

    ctx: GameContext,
    scenes: Vec<Box<dyn Scene>>,
    current_scene: usize,
    last_frame_time: Instant,
}

impl GameWindow {
    pub fn new(gl: Rc<glow::Context>, window: Window) -> Self {
        Self {
            ctx: GameContext::new(gl, Vault::new(), {
                let size = window.inner_size();
                VecI2::new(size.width as i32, size.height as i32)
            }),
            window,
            scenes: Vec::with_capacity(3),
            current_scene: 0,
            last_frame_time: Instant::now(),
        }
    }

    pub fn add_scene(&mut self, scene: Box<dyn Scene>) {
        self.scenes.push(scene);
        self.current_scene = self.scenes.len() - 1;
        self.scenes[self.current_scene].start(&mut self.ctx);
    }

    pub fn update(&mut self) {
        let now = Instant::now();
        self.ctx.delta_time = now.duration_since(self.last_frame_time).as_secs_f32();
        self.last_frame_time = now;
        self.ctx.time += self.ctx.delta_time;

        let scene = self.scenes.get_mut(self.current_scene).unwrap();
        match scene.update(&mut self.ctx) {
            SceneAction::None => (),
        }
    }

    pub fn resized(&mut self, size: VecI2) {
        let ctx = &mut self.ctx;
        ctx.window_size = size;

        let scene = self.scenes.get_mut(self.current_scene).unwrap();
        scene.resized(ctx);
    }

    pub fn render(&mut self) {
        let scene = self.scenes.get_mut(self.current_scene).unwrap();
        scene.render(&self.ctx);
    }

    pub fn get_window(&self) -> &Window {
        &self.window
    }
}

pub struct GameContext {
    window_size: VecI2,
    gl: Rc<glow::Context>,
    vault: Vault,

    time: f32,
    delta_time: f32,
}

impl GameContext {
    pub fn new(gl: Rc<glow::Context>, vault: Vault, window_size: VecI2) -> Self {
        Self {
            gl,
            window_size,
            vault,
            delta_time: 0.0,
            time: 0.0,
        }
    }

    pub fn get_gl(&self) -> &glow::Context {
        &self.gl
    }
    pub fn get_window_size(&self) -> VecI2 {
        self.window_size
    }

    pub fn get_vault(&self) -> &Vault {
        &self.vault
    }
    pub fn get_vault_mut(&mut self) -> &mut Vault {
        &mut self.vault
    }

    pub fn get_delta_time(&self) -> f32 {
        self.delta_time
    }
    pub fn get_time(&self) -> f32 {
        self.time
    }
}
