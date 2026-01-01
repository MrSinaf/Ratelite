use crate::numerics::Matrix3x3;
use crate::rendering;
use crate::rendering::texture_2d::Texture2D;
use glow::{Context, HasContext, NativeUniformLocation};
use rendering::RenderingError;

pub type UniformLocation = NativeUniformLocation;

pub struct GProgram {
    handle: glow::NativeProgram,
}

impl GProgram {
    pub fn new(gl: &Context) -> Self {
        unsafe {
            let handle = gl.create_program().expect("Cannot create program");
            Self { handle }
        }
    }

    pub fn compile(
        &self,
        gl: &Context,
        vertex_src: &str,
        fragment_src: &str,
    ) -> Result<(), RenderingError> {
        unsafe {
            let handle = self.handle;
            let v_handle = Self::compile_shader(gl, glow::VERTEX_SHADER, vertex_src)?;
            let f_handle = Self::compile_shader(gl, glow::FRAGMENT_SHADER, fragment_src)?;

            gl.attach_shader(handle, v_handle);
            gl.attach_shader(handle, f_handle);
            gl.link_program(handle);

            if !gl.get_program_link_status(handle) {
                return Err(RenderingError::ProgramLink(gl.get_program_info_log(handle)));
            }

            gl.delete_shader(v_handle);
            gl.delete_shader(f_handle);
            gl.flush();
        }
        Ok(())
    }

    pub fn bind(&self, gl: &Context) {
        unsafe {
            gl.use_program(Some(self.handle));
        }
    }

    pub fn set_uniform_texture(
        &self,
        gl: &Context,
        location: &UniformLocation,
        texture: &Texture2D,
        active_texture: u32,
    ) {
        unsafe {
            gl.active_texture(glow::TEXTURE0 + active_texture);
            texture.bind(gl);
            gl.uniform_1_i32(Some(location), active_texture as i32);
        }
    }

    pub fn set_uniform_matrix3x3_old(&self, gl: &Context, name: &str, matrix: &Matrix3x3) {
        self.bind(gl);
        if let Some(location) = self.get_uniform_location(gl, name) {
            unsafe { gl.uniform_matrix_3_f32_slice(Some(&location), true, &matrix.as_raw()) }
        }
    }

    pub fn set_uniform_matrix3x3(
        &self,
        gl: &Context,
        location: &UniformLocation,
        matrix: &Matrix3x3,
    ) {
        unsafe { gl.uniform_matrix_3_f32_slice(Some(&location), true, &matrix.as_raw()) }
    }

    pub fn get_uniform_location(&self, gl: &Context, name: &str) -> Option<UniformLocation> {
        unsafe { gl.get_uniform_location(self.handle, name) }
    }

    unsafe fn compile_shader(
        gl: &Context,
        shader_type: u32,
        source: &str,
    ) -> Result<glow::NativeShader, RenderingError> {
        unsafe {
            let shader = gl.create_shader(shader_type).expect("Cannot create shader");
            gl.shader_source(shader, source);
            gl.compile_shader(shader);
            if !gl.get_shader_compile_status(shader) {
                return Err(RenderingError::ShaderCompilation(
                    gl.get_shader_info_log(shader),
                ));
            }
            Ok(shader)
        }
    }
}
