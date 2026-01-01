use crate::rendering::texture_2d::Texture2D;
use crate::vault::{Asset, AssetId, Vault};
use crate::{Color, GameContext, Region, VecF2};
use std::collections::HashMap;
use std::path::Path;

#[allow(unused)]
pub struct GlyphMetrics {
    pub uv: Region,
    pub size: VecF2,
    pub offset: VecF2,
    pub advance: f32,
}

#[allow(unused)]
pub struct FontBitmap {
    glyphs: HashMap<char, GlyphMetrics>,
    texture_id: AssetId,
    line_height: f32,
}

impl FontBitmap {
    #[allow(unused)]
    pub fn load<P: AsRef<Path>>(ctx: &mut GameContext, path: P, size: f32) -> Self {
        let font_data = std::fs::read(path).unwrap();
        let font = match fontdue::Font::from_bytes(font_data, fontdue::FontSettings::default()) {
            Ok(x) => x,
            Err(_) => todo!(),
        };

        let atlas_size = 528;
        let atlas_texel = 1.0 / atlas_size as f32;
        let mut atlas_data = vec![0u8; atlas_size * atlas_size];
        let mut glyphs = HashMap::new();

        let mut cur_x = 3;
        let mut cur_y = 3;
        let mut max_row_height = 0;

        let chars_to_load: Vec<char> = (32..127u8).chain(160..255u8).map(|i| i as char).collect();

        for c in chars_to_load {
            let (metrics, bitmap) = font.rasterize(c, size);

            if cur_x + metrics.width > atlas_size {
                cur_x = 3;
                cur_y += max_row_height + 3;
                max_row_height = 0;
            }

            for y in 0..metrics.height {
                for x in 0..metrics.width {
                    let dest_idx = (cur_y + y) * atlas_size + (cur_x + x);
                    let src_idx = y * metrics.width + x;
                    atlas_data[dest_idx] = bitmap[src_idx];
                }
            }

            glyphs.insert(
                c,
                GlyphMetrics {
                    uv: Region::new(
                        VecF2::new(cur_x as f32 * atlas_texel, cur_y as f32 * atlas_texel),
                        VecF2::new(
                            (cur_x + metrics.width) as f32 * atlas_texel,
                            (cur_y + metrics.height) as f32 * atlas_texel,
                        ),
                    ),
                    size: VecF2::new(metrics.width as f32, metrics.height as f32),
                    offset: VecF2::new(metrics.xmin as f32, metrics.ymin as f32),
                    advance: metrics.advance_width,
                },
            );

            cur_x += metrics.width + 1;
            max_row_height = max_row_height.max(metrics.height);
        }

        let rgba_data: Vec<Color> = atlas_data
            .iter()
            .map(|&a| Color::new(255, 255, 255, a))
            .collect();
        let texture_id = {
            let texture = Texture2D::new(
                ctx.get_gl(),
                atlas_size as u32,
                atlas_size as u32,
                &rgba_data,
            );
            ctx.get_vault_mut().add("font.texture.dep", texture)
        };

        Self {
            texture_id,
            glyphs,
            line_height: size, // Simplifié
        }
    }

    pub fn get_texture_id(&self) -> AssetId {
        self.texture_id
    }

    pub fn get_glyph_metrics(&self, c: char) -> Option<&GlyphMetrics> {
        self.glyphs.get(&c)
    }
}

impl Asset for FontBitmap {
    fn on_remove(&self, vault: &mut Vault) {
        vault.remove(self.texture_id);
    }
}
