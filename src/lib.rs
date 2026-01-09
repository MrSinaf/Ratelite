mod engine;
mod numerics;
mod rendering;
pub mod ui;

pub use engine::*;
pub use numerics::*;
pub use rendering::*;
use std::any::Any;

pub trait Downcast: Any {
    fn as_any(&self) -> &dyn Any;
    fn as_any_mut(&mut self) -> &mut dyn Any;
}

impl<T: Any> Downcast for T {
    fn as_any(&self) -> &dyn Any {
        self
    }
    fn as_any_mut(&mut self) -> &mut dyn Any {
        self
    }
}
