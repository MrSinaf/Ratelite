use crate::rendering::RenderingError;
use glow::{Context, HasContext};

#[derive(Debug, Clone, Copy)]
#[allow(unused)]
pub enum Target {
    ParameterBuffer,
    ArrayBuffer,
    ElementArrayBuffer,
    PixelPackBuffer,
    PixelUnpackBuffer,
    UniformBuffer,
    TextureBuffer,
    TransformFeedbackBuffer,
    CopyReadBuffer,
    CopyWriteBuffer,
    DrawIndirectBuffer,
    ShaderStorageBuffer,
    DispatchIndirectBuffer,
    QueryBuffer,
    AtomicCounterBuffer,
}

impl Target {
    fn to_gl(self) -> u32 {
        match self {
            Target::ParameterBuffer => glow::PARAMETER_BUFFER,
            Target::ArrayBuffer => glow::ARRAY_BUFFER,
            Target::ElementArrayBuffer => glow::ELEMENT_ARRAY_BUFFER,
            Target::PixelPackBuffer => glow::PIXEL_PACK_BUFFER,
            Target::PixelUnpackBuffer => glow::PIXEL_UNPACK_BUFFER,
            Target::UniformBuffer => glow::UNIFORM_BUFFER,
            Target::TextureBuffer => glow::TEXTURE_BUFFER,
            Target::TransformFeedbackBuffer => glow::TRANSFORM_FEEDBACK_BUFFER,
            Target::CopyReadBuffer => glow::COPY_READ_BUFFER,
            Target::CopyWriteBuffer => glow::COPY_WRITE_BUFFER,
            Target::DrawIndirectBuffer => glow::DRAW_INDIRECT_BUFFER,
            Target::ShaderStorageBuffer => glow::SHADER_STORAGE_BUFFER,
            Target::DispatchIndirectBuffer => glow::DISPATCH_INDIRECT_BUFFER,
            Target::QueryBuffer => glow::QUERY_BUFFER,
            Target::AtomicCounterBuffer => glow::ATOMIC_COUNTER_BUFFER,
        }
    }
}

#[derive(Debug)]
pub struct GBuffer {
    handle: glow::NativeBuffer,
    target: Target,
    size_in_bytes: usize,
}

impl GBuffer {
    pub fn handle(&self) -> glow::NativeBuffer {
        self.handle
    }

    pub fn from_array<T: Sized>(gl: &Context, target: Target, data: &[T], dynamic: bool) -> Self {
        let size_in_bytes = data.len() * size_of::<T>();
        let bytes =
            unsafe { std::slice::from_raw_parts(data.as_ptr() as *const u8, size_in_bytes) };

        Self::from_bytes(gl, target, bytes, dynamic)
    }

    pub fn from_bytes(gl: &Context, target: Target, bytes: &[u8], dynamic: bool) -> Self {
        let handle = unsafe { gl.create_buffer().expect("Can't create buffer !") };
        let size_in_bytes = bytes.len();

        unsafe {
            gl.bind_buffer(target.to_gl(), Some(handle));
            gl.buffer_data_u8_slice(
                target.to_gl(),
                bytes,
                if dynamic {
                    glow::DYNAMIC_DRAW
                } else {
                    glow::STATIC_DRAW
                },
            );
        }

        Self {
            handle,
            target,
            size_in_bytes,
        }
    }

    pub fn set_array<T: Sized>(
        &self,
        gl: &Context,
        offset: usize,
        data: &[T],
    ) -> Result<(), RenderingError> {
        let data_size_in_bytes = data.len() * size_of::<T>();
        let offset_in_bytes = offset * size_of::<T>();
        let bytes =
            unsafe { std::slice::from_raw_parts(data.as_ptr() as *const u8, data_size_in_bytes) };

        self.set_bytes(gl, offset_in_bytes, bytes)
    }

    pub fn set_bytes(
        &self,
        gl: &Context,
        offset_in_bytes: usize,
        bytes: &[u8],
    ) -> Result<(), RenderingError> {
        self.bind(gl);
        if offset_in_bytes + bytes.len() > self.size_in_bytes {
            return Err(RenderingError::BufferOverflow);
        }
        unsafe {
            gl.buffer_sub_data_u8_slice(self.target.to_gl(), offset_in_bytes as i32, bytes);
        }

        Ok(())
    }

    pub fn bind(&self, gl: &Context) {
        unsafe { gl.bind_buffer(self.target.to_gl(), Some(self.handle)) }
    }
}
