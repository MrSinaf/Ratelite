use crate::VecF2;

#[derive(Debug, Copy, Clone)]
pub struct Rect {
    pub x: f32,
    pub y: f32,
    pub width: f32,
    pub height: f32,
}

impl Rect {
    pub fn new(x: f32, y: f32, width: f32, height: f32) -> Rect {
        Rect {
            x,
            y,
            width,
            height,
        }
    }

    pub fn empty() -> Rect {
        Rect {
            x: 0.0,
            y: 0.0,
            width: 0.0,
            height: 0.0,
        }
    }

    pub fn get_size(&self) -> VecF2 {
        VecF2::new(self.width, self.height)
    }
    pub fn get_position(&self) -> VecF2 {
        VecF2::new(self.x, self.y)
    }
}
