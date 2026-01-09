use crate::Downcast;
use crate::engine::arena::{Arena, ArenaId};
use std::collections::HashMap;

pub type AssetId = ArenaId;

struct VaultBundle {
    name: String,
    asset: Box<dyn Asset>,
}

pub struct Vault {
    storage: Arena<VaultBundle>,
    lookup: HashMap<String, AssetId>,
}

impl Vault {
    pub fn new() -> Self {
        Self {
            storage: Arena::new(),
            lookup: HashMap::new(),
        }
    }

    pub fn add<A: Asset>(&mut self, name: &str, value: A) -> AssetId {
        let id = self.storage.insert(VaultBundle {
            name: name.to_string(),
            asset: Box::new(value),
        });
        self.lookup.insert(name.to_string(), id);
        id
    }

    pub fn remove(&mut self, id: AssetId) -> bool {
        if let Some(bundle) = self.storage.remove(id) {
            self.lookup.remove(&bundle.name);
            bundle.asset.on_remove(self);
            return true;
        }
        false
    }

    pub fn search(&self, name: &str) -> Option<AssetId> {
        self.lookup.get(name).copied()
    }

    pub fn get<A: Asset>(&self, id: AssetId) -> Option<&A> {
        self.storage
            .get(id)
            .and_then(|bundle| bundle.asset.as_ref().as_any().downcast_ref::<A>())
    }

    pub fn get_mut<A: Asset>(&mut self, id: AssetId) -> Option<&mut A> {
        self.storage
            .get_mut(id)
            .and_then(|bundle| bundle.asset.as_mut().as_any_mut().downcast_mut::<A>())
    }
}

pub trait Asset: Downcast {
    fn on_remove(&self, _ctx: &mut Vault) {}
}
