use ratelite::ui::Canvas;
use ratelite::ui::element::Widget;
use ratelite::ui::widgets::Image;
use ratelite::{GameContext, Ratelite, Scene, SceneAction, Shader, Texture2D, VecF2};

fn main() {
    Ratelite::launch(
        "Hello Ratelite",
        Home {
            canvas: Canvas::new(),
        },
    );
}

struct Home {
    canvas: Canvas,
}

impl Scene for Home {
    fn start(&mut self, ctx: &mut GameContext) {
        let logo_id = {
            let asset = Texture2D::load(ctx.get_gl(), "examples/assets/ratelite.png");
            ctx.get_vault_mut().add("ratelite", asset)
        };
        let shader_id = {
            let asset = Shader::default(ctx.get_gl());
            ctx.get_vault_mut().add("shader", asset)
        };

        self.canvas
            .add_child(Image::new(ctx, shader_id, logo_id).with_element(|e| {
                e.set_pivot_and_anchors(VecF2::new(0.5, 0.5));
                e.set_scale(VecF2::new(10.0, 10.0));
            }));

        self.canvas.start(ctx);
    }

    fn update(&mut self, ctx: &mut GameContext) -> SceneAction {
        self.canvas.update(ctx);
        SceneAction::None
    }

    fn render(&self, ctx: &GameContext) {
        self.canvas.render(ctx);
    }

    fn resized(&mut self, ctx: &mut GameContext) {
        self.canvas.resize(ctx);
    }
}
