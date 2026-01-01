use crate::window::GameContext;

pub enum SceneAction {
    None,
}

pub trait Scene {
    fn start(&mut self, _ctx: &mut GameContext) { }

    fn update(&mut self, _ctx: &mut GameContext) -> SceneAction {
        SceneAction::None
    }

    fn render(&self, _ctx: &GameContext) {  }

    fn resized(&mut self, _ctx: &mut GameContext) { }
}

