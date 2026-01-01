use crate::Color;
use crate::numerics::VecF2;
use crate::rendering::native::texture;
use crate::rendering::native::texture::GTexture;
use crate::vault::Asset;
use glow::Context;
use std::path::Path;

#[allow(unused)]
pub struct Texture2D {
    texture: GTexture,
    width: u32,
    height: u32,
    texel: VecF2,
}

impl Texture2D {
    pub fn new(gl: &Context, width: u32, height: u32, pixels: &[Color]) -> Self {
        let texture = GTexture::new(gl);
        let data = unsafe {
            std::slice::from_raw_parts(
                pixels.as_ptr() as *const u8,
                pixels.len() * size_of::<Color>(),
            )
        };
        let texel = VecF2::new(1.0 / width as f32, 1.0 / height as f32);
        texture
            .set_image_2d(gl, width as i32, height as i32, data)
            .unwrap();
        texture.set_min_filter(gl, texture::MinFilter::Nearest);
        texture.set_mag_filter(gl, texture::MagFilter::Nearest);

        Self {
            width,
            height,
            texel,
            texture,
        }
    }

    pub fn load<P: AsRef<Path>>(gl: &Context, path: P) -> Texture2D {
        let path_ref = path.as_ref();
        if let Ok(img) = image::open(path_ref) {
            let img = img.into_rgba8();
            let (width, height) = img.dimensions();
            let raw_pixels = img.into_raw();
            let colors: &[Color] = unsafe {
                std::slice::from_raw_parts(
                    raw_pixels.as_ptr() as *const Color,
                    raw_pixels.len() / size_of::<Color>(),
                )
            };
            Texture2D::new(&gl, width, height, colors)
        } else {
            Texture2D::new(
                gl,
                2,
                2,
                &[
                    Color::new(0, 255, 77, 255),
                    Color::new(255, 255, 255, 255),
                    Color::new(255, 255, 255, 255),
                    Color::new(0, 255, 77, 255),
                ],
            )
        }
    }

    pub fn get_size(&self) -> (u32, u32) {
        (self.width, self.height)
    }

    pub fn bind(&self, gl: &Context) {
        self.texture.bind(gl);
    }
}

impl Asset for Texture2D {}
