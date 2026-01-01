use crate::rendering::native::program::GProgram;
use glow::Context;
use crate::vault::Asset;

pub struct Shader {
    program: GProgram
}

impl Shader {
    #[allow(unused)]
    pub fn new(gl: &Context, vertex_shader: &str, fragment_shader: &str) -> Self {
        let program = GProgram::new(gl);
        program.compile(gl, vertex_shader, fragment_shader).unwrap();

        Self { program }
    }

    #[allow(unused)]
    pub fn default(gl: &Context) -> Self {
        Self::new(
            gl,
            r#"
                #version 330 core
                layout(std140) uniform Canvas {
                    float time;
                    float delta_time;
                    vec2 resolution;
                    mat3 projection;
                };

                layout (location = 0) in vec2 a_pos;
                layout (location = 1) in vec2 a_texCoord;

                uniform mat3 u_model;

                out vec2 v_texCoord;
                void main() {
                    gl_Position = vec4(projection * u_model * vec3(a_pos, 1.0), 1.0);
                    v_texCoord = a_texCoord;
                }
            "#,
            r#"
                #version 330 core
                layout(std140) uniform Canvas {
                    float time;
                    float delta_time;
                    vec2 resolution;
                    mat3 projection;
                };
                in vec2 v_texCoord;

                uniform sampler2D u_texture;

                out vec4 o_fragColor;
                void main() {
                    vec4 color = texture(u_texture, v_texCoord);
                    o_fragColor = color;
                }
            "#,
        )
    }

    pub fn get_program(&self) -> &GProgram { &self.program }

    pub fn bind(&self, gl: &Context) {
        self.program.bind(gl);
    }
}

impl Asset for Shader { }