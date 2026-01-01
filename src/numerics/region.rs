use crate::VecF2;

#[derive(Debug, Copy, Clone)]
pub struct Region {
    pub pos00: VecF2,
    pub pos11: VecF2,
}

impl Region {
    pub fn new(pos00: VecF2, pos11: VecF2) -> Region {
        Region {
            pos00,
            pos11,
        }
    }

    pub fn empty() -> Region {
        Region {
            pos00: VecF2::zero(),
            pos11: VecF2::zero(),
        }
    }
}
