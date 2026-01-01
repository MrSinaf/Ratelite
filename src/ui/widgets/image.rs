use crate::GameContext;
use crate::Material;
use crate::Mesh;
use crate::Texture2D;
use crate::ui::element::{Element, Widget};
use crate::vault::AssetId;
use crate::VecF2;

pub struct Image {
    element: Element,
}

impl Image {
    pub fn new(ctx: &mut GameContext, shader_id: AssetId, texture_id: AssetId) -> Self {
        if let Some(texture) = ctx.get_vault_mut().get::<Texture2D>(texture_id) {
            let (width, height) = texture.get_size();
            let gl = ctx.get_gl();
            let mesh = Mesh::new_quad(gl, VecF2::zero(), VecF2::one());
            let mut material = Material::new(shader_id);
            material.set_texture2d(ctx, "u_texture", texture_id, 0);
            let mut element = Element::new(mesh, material);
            element.set_size(VecF2::new(width as f32, height as f32));

            Self {
                element,
            }
        } else {
            Self {
                element: Element::empty(),
            }
        }
    }
}

impl Widget for Image {
    fn get_element(&self) -> &Element {
        &self.element
    }
    fn get_element_mut(&mut self) -> &mut Element {
        &mut self.element
    }
}
