use glow::Context;
use crate::rendering::Property;
use crate::rendering::native::program::{GProgram, UniformLocation};

#[derive(Debug, Copy, Clone)]
#[rustfmt::skip]
pub struct Matrix3x3 {
    pub m11: f32, pub m21: f32, pub m31: f32,
    pub m12: f32, pub m22: f32, pub m32: f32,
    pub m13: f32, pub m23: f32, pub m33: f32,
}

#[rustfmt::skip]
impl Matrix3x3 {
    pub fn new(
        m11: f32, m21: f32, m31: f32,
        m12: f32, m22: f32, m32: f32,
        m13: f32, m23: f32, m33: f32,
    ) -> Self {
        Self {
            m11, m21, m31,
            m12, m22, m32,
            m13, m23, m33,
        }
    }

    pub fn identity() -> Self {
        Self::new(
            1.0, 0.0, 0.0,
            0.0, 1.0, 0.0,
            0.0, 0.0, 1.0,
        )
    }

    pub fn inverse(&self) -> Option<Self> {
        let det = self.determinant();
        if det.abs() < f32::EPSILON {
            return None;
        }
        let inv_det = 1.0 / det;
        Some(Self::new(
            (self.m22 * self.m33 - self.m32 * self.m23) * inv_det,
            (self.m31 * self.m23 - self.m21 * self.m33) * inv_det,
            (self.m21 * self.m32 - self.m31 * self.m22) * inv_det,
            (self.m32 * self.m13 - self.m12 * self.m33) * inv_det,
            (self.m11 * self.m33 - self.m31 * self.m13) * inv_det,
            (self.m31 * self.m12 - self.m11 * self.m32) * inv_det,
            (self.m12 * self.m23 - self.m22 * self.m13) * inv_det,
            (self.m21 * self.m13 - self.m11 * self.m23) * inv_det,
            (self.m11 * self.m22 - self.m21 * self.m12) * inv_det,
        ))
    }

    pub fn determinant(&self) -> f32 {
        self.m11 * (self.m22 * self.m33 - self.m32 * self.m23) -
        self.m21 * (self.m12 * self.m33 - self.m32 * self.m13) +
        self.m31 * (self.m12 * self.m23 - self.m22 * self.m13)
    }

    pub fn transpose(&self) -> Self {
        Self::new(
            self.m11, self.m12, self.m13,
            self.m21, self.m22, self.m23,
            self.m31, self.m32, self.m33,
        )
    }

    pub fn create_translation(translation: (f32, f32)) -> Self {
        Self::new(
            1.0, 0.0, translation.0,
            0.0, 1.0, translation.1,
            0.0, 0.0, 1.0,
        )
    }

    pub fn create_scale(scale: (f32, f32)) -> Self {
        Self::new(
            scale.0, 0.0, 0.0,
            0.0, scale.1, 0.0,
            0.0, 0.0, 1.0,
        )
    }

    pub fn create_rotation(radians: f32) -> Self {
        let cos = radians.cos();
        let sin = radians.sin();
        Self::new(
            cos, -sin, 0.0,
            sin, cos, 0.0,
            0.0, 0.0, 1.0,
        )
    }

    pub fn create_orthographic(width: f32, height: f32, center: bool) -> Self {
        if width <= 0.0 || height <= 0.0 {
            panic!("Width and height must be strictly positive.");
        }
        let offset = if center { 0.0 } else { -1.0 };
        Self::new(
            2.0 / width, 0.0, offset,
            0.0, 2.0 / height, offset,
            0.0, 0.0, 1.0,
        )
    }

    pub fn transform_point(&self, point: (f32, f32)) -> (f32, f32) {
        (
            self.m11 * point.0 + self.m21 * point.1 + self.m31,
            self.m12 * point.0 + self.m22 * point.1 + self.m32,
        )
    }

    pub fn as_raw(&self) -> [f32; 9] {
        [
            self.m11, self.m21, self.m31,
            self.m12, self.m22, self.m32,
            self.m13, self.m23, self.m33,
        ]
    }

    pub fn as_std140(&self) -> [f32; 12] {
        [
            self.m11, self.m12, self.m13, 0.0,
            self.m21, self.m22, self.m23, 0.0,
            self.m31, self.m32, self.m33, 0.0,
        ]
    }
}

impl PartialEq for Matrix3x3 {
    fn eq(&self, other: &Self) -> bool {
        self.m11 == other.m11 &&
        self.m12 == other.m12 &&
        self.m13 == other.m13 &&
        self.m21 == other.m21 &&
        self.m22 == other.m22 &&
        self.m23 == other.m23 &&
        self.m31 == other.m31 &&
        self.m32 == other.m32 &&
        self.m33 == other.m33
    }
}

impl Eq for Matrix3x3 {}

impl std::ops::Mul for Matrix3x3 {
    type Output = Self;
    fn mul(self, other: Self) -> Self {
        Self::new(
            self.m11 * other.m11 + self.m12 * other.m21 + self.m13 * other.m31,
            self.m21 * other.m11 + self.m22 * other.m21 + self.m23 * other.m31,
            self.m31 * other.m11 + self.m32 * other.m21 + self.m33 * other.m31,
            self.m11 * other.m12 + self.m12 * other.m22 + self.m13 * other.m32,
            self.m21 * other.m12 + self.m22 * other.m22 + self.m23 * other.m32,
            self.m31 * other.m12 + self.m32 * other.m22 + self.m33 * other.m32,
            self.m11 * other.m13 + self.m12 * other.m23 + self.m13 * other.m33,
            self.m21 * other.m13 + self.m22 * other.m23 + self.m23 * other.m33,
            self.m31 * other.m13 + self.m32 * other.m23 + self.m33 * other.m33,
        )
    }
}

impl Property for Matrix3x3 {
    fn set_uniform(&self, prg: &GProgram, gl: &Context, location: &UniformLocation) {
        prg.set_uniform_matrix3x3(gl, location, self);
    }
}