#[derive(Clone, Copy, Debug, PartialEq, Eq, Hash)]
pub struct ArenaId {
    pub index: usize,
    pub version: u32,
}

struct ArenaEntry<T> {
    version: u32,
    value: Option<T>,
}

pub struct Arena<T> {
    items: Vec<ArenaEntry<T>>,
    free_indices: Vec<usize>,
}

impl<T> Arena<T> {
    pub fn new() -> Self {
        Self {
            items: Vec::new(),
            free_indices: Vec::new(),
        }
    }

    pub fn insert(&mut self, value: T) -> ArenaId {
        if let Some(index) = self.free_indices.pop() {
            let entry = &mut self.items[index];
            entry.value = Some(value);
            ArenaId {
                index,
                version: entry.version,
            }
        } else {
            let index = self.items.len();
            let id = ArenaId { index, version: 0 };
            self.items.push(ArenaEntry {
                version: 0,
                value: Some(value),
            });
            id
        }
    }

    pub fn remove(&mut self, id: ArenaId) -> Option<T> {
        let entry = self.items.get_mut(id.index)?;
        if entry.version == id.version && entry.value.is_some() {
            let value = entry.value.take();
            entry.version += 1;
            self.free_indices.push(id.index);
            value
        } else {
            None
        }
    }

    pub fn get(&self, id: ArenaId) -> Option<&T> {
        let entry = self.items.get(id.index)?;
        if entry.version == id.version {
            entry.value.as_ref()
        } else {
            None
        }
    }

    pub fn get_mut(&mut self, id: ArenaId) -> Option<&mut T> {
        let entry = self.items.get_mut(id.index)?;
        if entry.version == id.version {
            entry.value.as_mut()
        } else {
            None
        }
    }

    #[allow(unused)]
    pub fn iter(&self) -> impl Iterator<Item = (ArenaId, &T)> {
        self.items.iter().enumerate().filter_map(|(i, entry)| {
            entry.value.as_ref().map(|v| {
                (
                    ArenaId {
                        index: i,
                        version: entry.version,
                    },
                    v,
                )
            })
        })
    }
}
