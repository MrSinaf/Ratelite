use std::any::Any;
use std::collections::HashMap;

pub struct Vault {
    items: Vec<VaultEntry>,
    free_indices: Vec<usize>,
    lookup: HashMap<String, AssetId>,
}

#[derive(Clone, Copy, Debug, PartialEq, Eq, Hash)]
pub struct AssetId {
    index: usize,
    version: u32,
}

struct VaultEntry {
    version: u32,
    name: String,
    value: Option<Box<dyn Asset>>,
}

impl Vault {
    pub fn new() -> Self {
        Self {
            items: Vec::new(),
            free_indices: Vec::new(),
            lookup: HashMap::new(),
        }
    }

    pub fn add<A: Asset>(&mut self, name: &str, value: A) -> AssetId {
        let id = if let Some(index) = self.free_indices.pop() {
            let entry = &mut self.items[index];
            entry.value = Some(Box::new(value));
            entry.name = name.to_string();

            return AssetId {
                index,
                version: entry.version,
            };
        } else {
            let id = AssetId {
                index: self.items.len(),
                version: 0,
            };
            self.items.push(VaultEntry {
                value: Some(Box::new(value)),
                name: name.to_string(),
                version: 0,
            });
            id
        };
        self.lookup.insert(name.to_string(), id);
        id
    }

    pub fn remove(&mut self, id: AssetId) -> bool {
        if let Some(entry) = self.items.get_mut(id.index) {
            if entry.version == id.version && entry.value.is_some() {
                entry.version += 1;
                self.free_indices.push(id.index);
                self.lookup.remove(&entry.name);
                entry.value.take().unwrap().on_remove(self);

                return true;
            }
        }
        false
    }

    pub fn search(&self, name: &str) -> Option<AssetId> {
        self.lookup.get(name).copied()
    }

    pub fn get<A: Asset>(&self, id: AssetId) -> Option<&A> {
        self.items.get(id.index).and_then(|entry| {
            if entry.version == id.version {
                entry.value.as_ref()?.as_any().downcast_ref::<A>()
            } else {
                None
            }
        })
    }

    pub fn get_mut<A: Asset>(&mut self, id: AssetId) -> Option<&mut A> {
        self.items.get_mut(id.index).and_then(|entry| {
            if entry.version == id.version {
                entry.value.as_mut()?.as_any_mut().downcast_mut::<A>()
            } else {
                None
            }
        })
    }
}

pub trait Asset: Any + Downcast {
    fn on_remove(&self, _ctx: &mut Vault) {}
}

pub trait Downcast: Any {
    fn as_any(&self) -> &dyn Any;
    fn as_any_mut(&mut self) -> &mut dyn Any;
}

impl<T: Asset> Downcast for T {
    fn as_any(&self) -> &dyn Any {
        self
    }
    fn as_any_mut(&mut self) -> &mut dyn Any {
        self
    }
}
