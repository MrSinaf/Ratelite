use glow::{Context, HasContext};

#[derive(Debug)]
pub struct GVertexArray {
    handle: glow::NativeVertexArray,
    stride: i32,
}

impl GVertexArray {
    pub fn new(gl: &Context, stride_in_bytes: i32) -> Self {
        let handle = unsafe {
            gl.create_vertex_array()
                .expect("Can't create vertex array !")
        };

        Self { handle, stride: stride_in_bytes }
    }

    pub fn bind_buffer(
        &self,
        gl: &Context,
        index: u32,
        size: i32,
        offset_in_bytes: i32,
    ) {
        unsafe {
            self.bind(gl);

            gl.enable_vertex_attrib_array(index);
            gl.vertex_attrib_pointer_f32(
                index,
                size,
                glow::FLOAT,
                false,
                self.stride,
                offset_in_bytes,
            );
        }
    }

    pub fn bind(&self, gl: &Context) {
        unsafe { gl.bind_vertex_array(Some(self.handle)) }
    }
}
