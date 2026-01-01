mod material;
mod mesh;
pub mod native;
mod shader;
mod texture_2d;

pub use material::*;
pub use mesh::*;
pub use shader::*;
pub use texture_2d::*;

#[derive(Debug)]
#[allow(unused)]
pub enum RenderingError {
    BufferOverflow,
    ShaderCompilation(String),
    ProgramLink(String),
    SetImage2D(String),
}
