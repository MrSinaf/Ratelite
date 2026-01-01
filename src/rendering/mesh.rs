use crate::Rect;
use crate::rendering::native::buffer;
use crate::rendering::native::buffer::GBuffer;
use crate::rendering::native::vertex_array::GVertexArray;
use crate::VecF2;
use glow::{Context, HasContext};

#[derive(Debug)]
#[allow(unused)]
pub struct Mesh {
    vao: GVertexArray,
    vbo: GBuffer,
    ibo: GBuffer,
    bounds: Rect,
    indices_count: i32,
}

impl Mesh {
    pub fn new_quad(gl: &Context, position: VecF2, size: VecF2) -> Self {
        let half_width = size.x * 0.5;
        let half_height = size.y * 0.5;
        Self::new(
            gl,
            &[
                VertexPosUv {
                    pos: VecF2::new(position.x + half_width, position.y + half_height),
                    uv: VecF2::new(1.0, 0.0),
                },
                VertexPosUv {
                    pos: VecF2::new(position.x + half_width, position.y - half_height),
                    uv: VecF2::new(1.0, 1.0),
                },
                VertexPosUv {
                    pos: VecF2::new(position.x - half_width, position.y - half_height),
                    uv: VecF2::new(0.0, 1.0),
                },
                VertexPosUv {
                    pos: VecF2::new(position.x - half_width, position.y + half_height),
                    uv: VecF2::new(0.0, 0.0),
                },
            ],
            &[0, 1, 3, 1, 2, 3],
        )
    }

    pub fn new<V: Vertex>(gl: &Context, vertices: &[V], indices: &[u32]) -> Self {
        let vbo = GBuffer::from_array(gl, buffer::Target::ArrayBuffer, &vertices, true);
        let vao = V::get_vao(gl);
        let ibo = GBuffer::from_array(gl, buffer::Target::ElementArrayBuffer, &indices, true);

        Self {
            vao, vbo, ibo,
            bounds: Self::calcule_bounds(&vertices),
            indices_count: indices.len() as i32,
        }
    }

    fn calcule_bounds<V: Vertex>(vertices: &[V]) -> Rect {
        let mut min = VecF2::zero();
        let mut max = VecF2::zero();
        for i in 0..vertices.len() {
            let (x, y) = vertices[i].get_position();

            if x > max.x {
                max.x = x;
            }
            if y > max.y {
                max.y = y;
            }

            if x < min.x {
                min.x = x;
            }
            if y < min.y {
                min.y = y;
            }
        }

        Rect::new(min.x, min.y, max.x - min.x, max.y - min.y)
    }

    pub fn get_bounds(&self) -> Rect {
        self.bounds
    }

    pub fn draw(&self, gl: &Context) {
        unsafe {
            self.vao.bind(gl);
            gl.active_texture(glow::TEXTURE0);
            gl.draw_elements(glow::TRIANGLES, self.indices_count, glow::UNSIGNED_INT, 0);
        }
    }
}

#[allow(unused)]
pub trait Vertex {
    fn get_position(&self) -> (f32, f32);
    fn get_vao(gl: &Context) -> GVertexArray;
}

#[allow(unused)]
pub struct VertexPosUv {
    pub pos: VecF2,
    pub uv: VecF2,
}

impl VertexPosUv {
    #[allow(unused)]
    pub fn new(position: VecF2, uv: VecF2) -> Self {
        Self { pos: position, uv }
    }
}

impl Vertex for VertexPosUv {
    fn get_position(&self) -> (f32, f32) {
        (self.pos.x, self.pos.y)
    }

    fn get_vao(gl: &Context) -> GVertexArray {
        let vao = GVertexArray::new(gl, 16);
        vao.bind_buffer(&gl, 0, 2, 0);
        vao.bind_buffer(&gl, 1, 2, 8);
        vao
    }
}
