#[derive(Debug, Copy, Clone)]
pub struct VecF2 {
    pub x: f32,
    pub y: f32,
}

impl VecF2 {
    pub fn new(x: f32, y: f32) -> VecF2 {
        Self { x, y }
    }
    pub fn zero() -> VecF2 {
        Self::new(0.0, 0.0)
    }
    pub fn one() -> VecF2 {
        Self::new(1.0, 1.0)
    }
}

impl Into<(f32, f32)> for VecF2 {
    fn into(self) -> (f32, f32) {
        (self.x, self.y)
    }
}

impl Into<VecF2> for VecI2 {
    fn into(self) -> VecF2 {
        VecF2::new(self.x as f32, self.y as f32)
    }
}

impl std::ops::Mul for VecF2 {
    type Output = Self;
    fn mul(self, other: Self) -> Self {
        Self::new(self.x * other.x, self.y * other.y)
    }
}
impl std::ops::Div for VecF2 {
    type Output = Self;
    fn div(self, other: Self) -> Self {
        Self::new(self.x / other.x, self.y / other.y)
    }
}
impl std::ops::Add for VecF2 {
    type Output = Self;
    fn add(self, other: Self) -> Self::Output {
        Self::new(self.x + other.x, self.y + other.y)
    }
}
impl std::ops::AddAssign for VecF2 {
    fn add_assign(&mut self, other: Self) {
        self.x += other.x;
        self.y += other.y;
    }
}
impl std::ops::Sub for VecF2 {
    type Output = Self;
    fn sub(self, other: Self) -> Self::Output {
        Self::new(self.x - other.x, self.y - other.y)
    }
}
impl std::ops::SubAssign for VecF2 {
    fn sub_assign(&mut self, other: Self) {
        self.x -= other.x;
        self.y -= other.y;
    }
}
impl std::ops::Mul<f32> for VecF2 {
    type Output = Self;
    fn mul(self, other: f32) -> Self::Output {
        Self::new(self.x * other, self.y * other)
    }
}
impl std::ops::Div<f32> for VecF2 {
    type Output = Self;
    fn div(self, other: f32) -> Self::Output {
        Self::new(self.x / other, self.y / other)
    }
}
impl std::ops::Neg for VecF2 {
    type Output = Self;
    fn neg(self) -> Self::Output {
        Self::new(-self.x, -self.y)
    }
}
impl PartialEq for VecF2 {
    fn eq(&self, other: &Self) -> bool {
        (self.x - other.x).abs() <= 1e-5 && (self.y - other.y).abs() <= 1e-5
    }
}

#[derive(Debug, Copy, Clone)]
pub struct VecI2 {
    pub x: i32,
    pub y: i32,
}

impl VecI2 {
    pub fn new(x: i32, y: i32) -> VecI2 {
        Self { x, y }
    }
    pub fn zero() -> VecI2 {
        Self::new(0, 0)
    }
    pub fn one() -> VecI2 {
        Self::new(1, 1)
    }
}

impl Into<(i32, i32)> for VecI2 {
    fn into(self) -> (i32, i32) {
        (self.x, self.y)
    }
}

impl Into<VecI2> for VecF2 {
    fn into(self) -> VecI2 {
        VecI2::new(self.x as i32, self.y as i32)
    }
}

impl std::ops::Mul for VecI2 {
    type Output = Self;
    fn mul(self, other: Self) -> Self {
        Self::new(self.x * other.x, self.y * other.y)
    }
}
impl std::ops::Div for VecI2 {
    type Output = Self;
    fn div(self, other: Self) -> Self {
        Self::new(self.x / other.x, self.y / other.y)
    }
}
impl std::ops::Add for VecI2 {
    type Output = Self;
    fn add(self, other: Self) -> Self::Output {
        Self::new(self.x + other.x, self.y + other.y)
    }
}
impl std::ops::Sub for VecI2 {
    type Output = Self;
    fn sub(self, other: Self) -> Self::Output {
        Self::new(self.x - other.x, self.y - other.y)
    }
}
impl std::ops::Mul<i32> for VecI2 {
    type Output = Self;
    fn mul(self, other: i32) -> Self::Output {
        Self::new(self.x * other, self.y * other)
    }
}
impl std::ops::Div<i32> for VecI2 {
    type Output = Self;
    fn div(self, other: i32) -> Self::Output {
        Self::new(self.x / other, self.y / other)
    }
}
