use crate::rendering::RenderingError;
use glow::{Context, HasContext, NativeTexture, PixelUnpackData};

const TEXTURE_TARGET: u32 = glow::TEXTURE_2D;

#[derive(Debug)]
pub struct GTexture {
    handle: NativeTexture,
}

#[derive(Debug, Copy, Clone)]
#[allow(unused)]
pub enum Wrap {
    Repeat,
    ClampToEdge,
    ClampToBorder,
    MirroredRepeat,
}

impl Wrap {
    #[allow(unused)]
    fn to_gl(self) -> i32 {
        (match self {
            Wrap::Repeat => glow::REPEAT,
            Wrap::ClampToEdge => glow::CLAMP_TO_EDGE,
            Wrap::ClampToBorder => glow::CLAMP_TO_BORDER,
            Wrap::MirroredRepeat => glow::MIRRORED_REPEAT,
        }) as i32
    }
}

#[derive(Debug, Copy, Clone)]
#[allow(unused)]
pub enum MinFilter {
    Nearest,
    Linear,
    NearestMipmapNearest,
    NearestMipmapLinear,
    LinearMipmapNearest,
    LinearMipmapLinear,
}

impl MinFilter {
    fn to_gl(self) -> i32 {
        (match self {
            MinFilter::Nearest => glow::NEAREST,
            MinFilter::Linear => glow::LINEAR,
            MinFilter::NearestMipmapNearest => glow::NEAREST_MIPMAP_NEAREST,
            MinFilter::NearestMipmapLinear => glow::NEAREST_MIPMAP_LINEAR,
            MinFilter::LinearMipmapNearest => glow::LINEAR_MIPMAP_NEAREST,
            MinFilter::LinearMipmapLinear => glow::LINEAR_MIPMAP_LINEAR,
        }) as i32
    }
}

#[derive(Debug, Copy, Clone)]
#[allow(unused)]
pub enum MagFilter {
    Nearest,
    Linear,
}

impl MagFilter {
    fn to_gl(self) -> i32 {
        (match self {
            MagFilter::Nearest => glow::NEAREST,
            MagFilter::Linear => glow::LINEAR,
        }) as i32
    }
}

impl GTexture {
    pub fn new(gl: &Context) -> Self {
        let handle = unsafe { gl.create_texture().expect("Can't create texture !") };
        Self { handle }
    }

    #[allow(unused)]
    pub fn set_wrap_s(&self, gl: &Context, wrap: Wrap) {
        self.bind(gl);
        unsafe {
            gl.tex_parameter_i32(TEXTURE_TARGET, glow::TEXTURE_WRAP_S, wrap.to_gl())
        }
    }

    #[allow(unused)]
    pub fn set_wrap_t(&self, gl: &Context, wrap: Wrap) {
        self.bind(gl);
        unsafe {
            gl.tex_parameter_i32(TEXTURE_TARGET, glow::TEXTURE_WRAP_T, wrap.to_gl())
        }
    }

    #[allow(unused)]
    pub fn set_wrap_r(&self, gl: &Context, wrap: Wrap) {
        self.bind(gl);
        unsafe {
            gl.tex_parameter_i32(TEXTURE_TARGET, glow::TEXTURE_WRAP_R, wrap.to_gl())
        }
    }

    pub fn set_min_filter(&self, gl: &Context, filter: MinFilter) {
        self.bind(gl);
        unsafe {
            gl.tex_parameter_i32(TEXTURE_TARGET, glow::TEXTURE_MIN_FILTER, filter.to_gl())
        }
    }

    pub fn set_mag_filter(&self, gl: &Context, filter: MagFilter) {
        self.bind(gl);
        unsafe {
            gl.tex_parameter_i32(TEXTURE_TARGET, glow::TEXTURE_MAG_FILTER, filter.to_gl())
        }
    }

    #[allow(unused)]
    pub fn generate_mipmap(&self, gl: &Context) {
        self.bind(gl);
        unsafe {
            gl.generate_mipmap(TEXTURE_TARGET);
        }
    }

    pub fn set_image_2d(
        &self,
        gl: &Context,
        width: i32,
        height: i32,
        pixels_rgba: &[u8],
    ) -> Result<(), RenderingError> {
        self.bind(gl);
        unsafe {
            gl.tex_image_2d(
                TEXTURE_TARGET,
                0,
                glow::RGBA8 as i32,
                width,
                height,
                0,
                glow::RGBA,
                glow::UNSIGNED_BYTE,
                PixelUnpackData::Slice(Some(pixels_rgba)),
            );

            let error = gl.get_error();
            if error != glow::NO_ERROR {
                return Err(RenderingError::SetImage2D(
                    match error {
                        glow::INVALID_ENUM => "INVALID_ENUM",
                        glow::INVALID_VALUE => "INVALID_VALUE",
                        glow::INVALID_OPERATION => "INVALID_OPERATION",
                        glow::INVALID_FRAMEBUFFER_OPERATION => "INVALID_FRAMEBUFFER_OPERATION",
                        glow::OUT_OF_MEMORY => "OUT_OF_MEMORY",
                        glow::STACK_UNDERFLOW => "STACK_UNDERFLOW",
                        glow::STACK_OVERFLOW => "STACK_OVERFLOW",
                        _ => "UNKNOWN_ERROR",
                    }
                        .into(),
                ));
            }
        }
        Ok(())
    }

    pub fn bind(&self, gl: &Context) {
        unsafe {
            gl.bind_texture(TEXTURE_TARGET, Some(self.handle));
        }
    }
}
