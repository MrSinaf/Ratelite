use crate::GameContext;
use crate::rendering::native::program::{GProgram, UniformLocation};
use crate::rendering::shader::Shader;
use crate::vault::{Vault, AssetId};
use std::collections::HashMap;

pub struct Material {
    shader_id: AssetId,
    properties: HashMap<String, MaterialProperty>,
    textures: HashMap<String, MaterialTexture>,
}

struct MaterialProperty {
    location: Option<UniformLocation>,
    value: Box<dyn Property>,
}

struct MaterialTexture {
    location: Option<UniformLocation>,
    active_unit: u32,
    value: AssetId,
}

pub trait Property {
    fn set_uniform(&self, prg: &GProgram, gl: &glow::Context, location: &UniformLocation);
}

impl Material {
    pub fn new(shader_id: AssetId) -> Self {
        Self {
            shader_id,
            properties: HashMap::new(),
            textures: HashMap::new(),
        }
    }

    pub fn set_property<T: Property + 'static>(&mut self, ctx: &GameContext, name: &str, value: T) {
        if let Some(shader) = ctx.get_vault().get::<Shader>(self.shader_id) {
            match self.properties.get_mut(name) {
                None => {
                    self.properties.insert(
                        name.to_string(),
                        MaterialProperty {
                            location: shader
                                .get_program()
                                .get_uniform_location(ctx.get_gl(), name),
                            value: Box::new(value),
                        },
                    );
                }
                Some(stored) => {
                    stored.value = Box::new(value);
                }
            }
        }
    }

    pub fn set_texture2d(
        &mut self,
        ctx: &GameContext,
        name: &str,
        texture_id: AssetId,
        active_unit: u32,
    ) {
        let vault = ctx.get_vault();
        if let Some(shader) = vault.get::<Shader>(self.shader_id) {
            match self.textures.get_mut(name) {
                None => {
                    self.textures.insert(
                        name.to_string(),
                        MaterialTexture {
                            location: shader
                                .get_program()
                                .get_uniform_location(ctx.get_gl(), name),
                            active_unit,
                            value: texture_id,
                        },
                    );
                }
                Some(stored) => {
                    stored.value = texture_id;
                }
            }
        }
    }

    pub fn apply_properties(&self, gl: &glow::Context, vault: &Vault) {
        if let Some(shader) = vault.get::<Shader>(self.shader_id) {
            let prg = shader.get_program();
            shader.bind(gl);

            for (_, property) in &self.properties {
                if let Some(location) = property.location {
                    property.value.set_uniform(prg, gl, &location);
                }
            }

            for (_, texture) in &self.textures {
                if let (Some(location), Some(tex)) = (texture.location, vault.get(texture.value)) {
                    prg.set_uniform_texture(gl, &location, tex, texture.active_unit)
                }
            }
        }
    }

    pub fn get_shader_id(&self) -> AssetId {
        self.shader_id
    }
}
