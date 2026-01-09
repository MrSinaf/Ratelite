use crate::Material;
use crate::Matrix3x3;
use crate::Mesh;
use crate::Rect;
use crate::VecF2;
use crate::window::GameContext;

pub struct Element {
    name: String,

    transform: Transform,
    mesh: Option<Mesh>,
    material: Option<Material>,

    children: Vec<Box<dyn Widget>>,

    is_dirty: bool,
}

#[allow(unused)]
pub struct Transform {
    matrix: Matrix3x3,
    real_position: VecF2,
    real_size: VecF2,

    position: VecF2,
    size: VecF2,
    scale: VecF2,
    pivot: VecF2,
    anchor_min: VecF2,
    anchor_max: VecF2,

    scale_with_size: bool,
    preserve_mesh_origin: bool,
}

impl Transform {
    fn new() -> Self {
        Self {
            real_position: VecF2::zero(),
            position: VecF2::zero(),
            size: VecF2::zero(),
            scale: VecF2::new(1.0, 1.0),
            pivot: VecF2::zero(),
            real_size: VecF2::zero(),
            anchor_min: VecF2::zero(),
            anchor_max: VecF2::zero(),
            matrix: Matrix3x3::identity(),
            scale_with_size: true,
            preserve_mesh_origin: true,
        }
    }

    pub(super) fn by_canvas(size: VecF2) -> Self {
        let mut transform = Self::new();
        transform.size = size;
        transform
    }
}

pub trait Widget {
    fn begin_render(&self, _ctx: &GameContext) {}
    fn render(&self, ctx: &GameContext) {
        self.begin_render(ctx);
        self.get_element().internal_render(ctx);
        self.end_render(ctx);
    }
    fn end_render(&self, _ctx: &GameContext) {}

    fn begin_update(&mut self, _ctx: &GameContext, _parent: &Transform) {}
    fn update(&mut self, ctx: &GameContext, parent: &Transform) {
        self.begin_update(ctx, parent);
        self.get_element_mut().internal_update(ctx, parent);
        self.end_update(ctx, parent);
    }
    fn end_update(&mut self, _ctx: &GameContext, _parent: &Transform) {}

    fn get_element(&self) -> &Element;
    fn get_element_mut(&mut self) -> &mut Element;

    fn with_element(mut self, f: impl FnOnce(&mut Element)) -> Self
    where
        Self: Sized,
    {
        f(self.get_element_mut());
        self
    }
}

#[allow(unused)]
impl Element {
    pub fn new(mesh: Mesh, material: Material) -> Self {
        Self {
            name: String::new(),
            transform: Transform::new(),
            mesh: Some(mesh),
            material: Some(material),

            children: Vec::new(),

            is_dirty: true,
        }
    }

    pub fn empty() -> Self {
        Self {
            name: String::new(),
            transform: Transform::new(),
            mesh: None,
            material: None,
            children: Vec::new(),

            is_dirty: true,
        }
    }

    pub fn internal_update(&mut self, ctx: &GameContext, parent: &Transform) {
        if self.is_dirty {
            Self::calcule_matrix(self, &parent);
            for e in self.children.iter_mut() {
                e.get_element_mut().is_dirty = true;
                e.update(ctx, &self.transform);
            }
            if let Some(material) = &mut self.material {
                material.set_property(ctx, "u_model", self.transform.matrix)
            }
            self.is_dirty = false;
        } else {
            for e in self.children.iter_mut() {
                e.update(ctx, &self.transform);
            }
        }
    }

    pub fn internal_render(&self, ctx: &GameContext) {
        if let (Some(mesh), Some(material)) = (&self.mesh, &self.material) {
            material.apply_properties(ctx.get_gl(), ctx.get_vault());
            mesh.draw(ctx.get_gl());
        }

        for c in self.children.iter() {
            c.render(ctx);
        }
    }

    pub fn add_child<W: Widget + 'static>(&mut self, child: W) {
        self.children.push(Box::new(child));
    }

    pub fn get_child(&self, index: usize) -> Option<&dyn Widget> {
        match self.children.get(index) {
            Some(c) => Some(c.as_ref()),
            None => None,
        }
    }
    pub fn get_child_mut(&mut self, index: usize) -> Option<&mut dyn Widget> {
        match self.children.get_mut(index) {
            Some(c) => Some(c.as_mut()),
            None => None,
        }
    }

    pub fn get_name(&self) -> &str {
        &self.name
    }
    pub fn set_name<T: Into<String>>(&mut self, name: T) {
        self.name = name.into();
    }

    pub fn get_mesh(&self) -> Option<&Mesh> {
        self.mesh.as_ref()
    }
    pub fn get_mesh_mut(&mut self) -> Option<&mut Mesh> {
        self.mesh.as_mut()
    }
    pub fn set_mesh(&mut self, mesh: Mesh) {
        self.mesh = Some(mesh);
    }

    pub fn get_position(&self) -> &VecF2 {
        &self.transform.position
    }
    pub fn get_position_mut(&mut self) -> &mut VecF2 {
        self.is_dirty = true;
        &mut self.transform.position
    }
    pub fn set_position(&mut self, position: VecF2) {
        self.is_dirty = true;
        self.transform.position = position;
    }

    pub fn get_scale(&self) -> &VecF2 {
        &self.transform.scale
    }
    pub fn get_scale_mut(&mut self) -> &mut VecF2 {
        self.is_dirty = true;
        &mut self.transform.scale
    }
    pub fn set_scale(&mut self, scale: VecF2) {
        self.is_dirty = true;
        self.transform.scale = scale;
    }

    pub fn get_pivot(&self) -> &VecF2 {
        &self.transform.pivot
    }
    pub fn get_pivot_mut(&mut self) -> &mut VecF2 {
        self.is_dirty = true;
        &mut self.transform.pivot
    }
    pub fn set_pivot(&mut self, pivot: VecF2) {
        self.is_dirty = true;
        self.transform.pivot = pivot;
    }

    pub fn get_size(&self) -> &VecF2 {
        &self.transform.size
    }
    pub fn get_size_mut(&mut self) -> &mut VecF2 {
        self.is_dirty = true;
        &mut self.transform.size
    }
    pub fn set_size(&mut self, size: VecF2) {
        self.is_dirty = true;
        self.transform.size = size;
    }

    pub fn get_anchor_min(&self) -> &VecF2 {
        &self.transform.anchor_min
    }
    pub fn get_anchor_min_mut(&mut self) -> &mut VecF2 {
        self.is_dirty = true;
        &mut self.transform.anchor_min
    }
    pub fn set_anchor_min(&mut self, anchor: VecF2) {
        self.is_dirty = true;
        self.transform.anchor_min = anchor;
    }

    pub fn get_anchor_max(&self) -> &VecF2 {
        &self.transform.anchor_max
    }
    pub fn get_anchor_max_mut(&mut self) -> &mut VecF2 {
        self.is_dirty = true;
        &mut self.transform.anchor_max
    }
    pub fn set_anchor_max(&mut self, anchor: VecF2) {
        self.is_dirty = true;
        self.transform.anchor_max = anchor;
    }

    pub fn set_anchors(&mut self, anchors: VecF2) {
        self.is_dirty = true;
        self.transform.anchor_min = anchors;
        self.transform.anchor_max = anchors;
    }

    pub fn set_pivot_and_anchors(&mut self, value: VecF2) {
        self.is_dirty = true;
        self.transform.anchor_min = value;
        self.transform.anchor_max = value;
        self.transform.pivot = value;
    }

    pub fn is_scale_with_size(&self) -> bool {
        self.transform.scale_with_size
    }
    pub fn set_scale_with_size(&mut self, value: bool) {
        self.is_dirty = true;
        self.transform.scale_with_size = value;
    }

    pub fn is_preserve_mesh_origin(&self) -> bool {
        self.transform.preserve_mesh_origin
    }
    pub fn set_preserve_mesh_origin(&mut self, value: bool) {
        self.is_dirty = true;
        self.transform.preserve_mesh_origin = value;
    }

    fn calcule_matrix(&mut self, tfm_parent: &Transform) {
        let tfm = &mut self.transform;
        let bounds = if let Some(mesh) = &self.mesh {
            let mut mesh_bounds = mesh.get_bounds();
            if tfm.scale_with_size {
                mesh_bounds.width = tfm.size.x;
                mesh_bounds.height = tfm.size.y;
            }
            if !tfm.preserve_mesh_origin {
                mesh_bounds.x = 0.0;
                mesh_bounds.y = 0.0;
            }
            mesh_bounds
        } else {
            Rect::new(0.0, 0.0, tfm.size.x, tfm.size.y)
        };

        let anchor_min_pos = tfm_parent.real_size * tfm.anchor_min;
        let anchor_max_pos = tfm_parent.real_size * tfm.anchor_max;
        let anchor_area_size = anchor_max_pos - anchor_min_pos;

        let mut final_size = bounds.get_size() * tfm.scale;
        if tfm.anchor_min.x != tfm.anchor_max.x {
            final_size.x = anchor_area_size.x + tfm.size.x;
        }
        if tfm.anchor_min.y != tfm.anchor_max.y {
            final_size.y = anchor_area_size.y + tfm.size.y;
        }

        let pivot_offset = tfm.pivot * final_size + bounds.get_position() * final_size;
        tfm.real_position = tfm_parent.real_position + anchor_min_pos + tfm.position - pivot_offset;
        tfm.real_size = final_size;

        tfm.matrix = if tfm.scale_with_size {
            Matrix3x3::create_scale(tfm.real_size.into())
        } else {
            Matrix3x3::create_scale(tfm.scale.into())
        } * Matrix3x3::create_translation(tfm.real_position.into());
    }
}

impl Widget for Element {
    fn get_element(&self) -> &Element {
        &self
    }

    fn get_element_mut(&mut self) -> &mut Element {
        self
    }
}
