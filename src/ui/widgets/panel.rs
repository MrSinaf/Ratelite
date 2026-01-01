use crate::GameContext;
use crate::ui::element::{Element, Widget};

pub struct Panel {
    element: Element,
}

impl Panel {
    pub fn new(_ctx: &GameContext) -> Self {
        Self {
            element: Element::empty(),
        }
    }
}

impl Widget for Panel {
    fn get_element(&self) -> &Element {
        &self.element
    }

    fn get_element_mut(&mut self) -> &mut Element {
        &mut self.element
    }
}