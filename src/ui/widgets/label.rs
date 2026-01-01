use crate::Material;
use crate::native::font::FontBitmap;
use crate::ui::element::{Element, Transform, Widget};
use crate::vault::AssetId;
use crate::{GameContext, VecF2};
use crate::{Mesh, VertexPosUv};

pub struct Label {
    element: Element,
    font_id: AssetId,
    text: String,
    is_dirty: bool,
}

impl Label {
    pub fn new(ctx: &GameContext, font_id: AssetId, shader_id: AssetId) -> Label {
        if let Some(font) = ctx.get_vault().get::<FontBitmap>(font_id) {
            let mut material = Material::new(shader_id);
            material.set_texture2d(ctx, "u_texture", font.get_texture_id(), 0);
            let mesh = Mesh::new::<VertexPosUv>(ctx.get_gl(), &[], &[]);
            let mut element = Element::new(mesh, material);
            element.set_scale_with_size(false);
            element.set_preserve_mesh_origin(false);

            Label {
                element,
                font_id,
                text: String::new(),
                is_dirty: false,
            }
        } else {
            Label {
                element: Element::empty(),
                text: String::new(),
                font_id,
                is_dirty: false,
            }
        }
    }

    pub fn set_text(&mut self, text: &str) {
        self.text = text.to_string();
        self.is_dirty = true;
    }

    fn update_mesh(&mut self, ctx: &GameContext) {
        let text = &self.text;
        if let Some(font) = ctx.get_vault().get::<FontBitmap>(self.font_id) {
            let mut vertices: Vec<VertexPosUv> = Vec::with_capacity(text.len() * 4);
            let mut indices: Vec<u32> = Vec::with_capacity(text.len() * 6);

            let mut cursor_x = 0.0;
            let mut min_y = 0.0f32;
            let mut max_y = 0.0f32;

            for (i, c) in text.chars().enumerate() {
                if let Some(glyph) = font.get_glyph_metrics(c) {
                    let x0 = cursor_x + glyph.offset.x;
                    let y0 = glyph.offset.y;
                    let x1 = x0 + glyph.size.x;
                    let y1 = y0 + glyph.size.y;

                    min_y = min_y.min(y0);
                    max_y = max_y.max(y1);

                    let u0 = glyph.uv.pos00.x;
                    let v0 = glyph.uv.pos11.y;
                    let u1 = glyph.uv.pos11.x;
                    let v1 = glyph.uv.pos00.y;

                    let base_idx = (i * 4) as u32;

                    vertices.push(VertexPosUv {
                        pos: VecF2::new(x0, y0),
                        uv: VecF2::new(u0, v0),
                    });
                    vertices.push(VertexPosUv {
                        pos: VecF2::new(x1, y0),
                        uv: VecF2::new(u1, v0),
                    });
                    vertices.push(VertexPosUv {
                        pos: VecF2::new(x1, y1),
                        uv: VecF2::new(u1, v1),
                    });
                    vertices.push(VertexPosUv {
                        pos: VecF2::new(x0, y1),
                        uv: VecF2::new(u0, v1),
                    });

                    indices.extend_from_slice(&[
                        base_idx,
                        base_idx + 1,
                        base_idx + 2,
                        base_idx,
                        base_idx + 2,
                        base_idx + 3,
                    ]);

                    cursor_x += glyph.advance;
                }
            }

            self.element
                .set_mesh(Mesh::new(ctx.get_gl(), &vertices, &indices));
        }
    }
}

impl Widget for Label {
    fn begin_update(&mut self, ctx: &GameContext, _parent: &Transform) {
        if self.is_dirty {
            self.update_mesh(ctx);
            self.is_dirty = false;
        }
    }

    fn get_element(&self) -> &Element {
        &self.element
    }

    fn get_element_mut(&mut self) -> &mut Element {
        &mut self.element
    }
}
