#pragma warning disable 1591

namespace Ratelite.Bindings;

[Flags]
public enum MapBufferAccessMask : int
{
	None = 0,
	[Obsolete("Deprecated in favour of \"ReadBit\"")]
	MapReadBit = 0x1,
	[Obsolete("Deprecated in favour of \"ReadBitExt\"")]
	MapReadBitExt = 0x1,
	[Obsolete("Deprecated in favour of \"WriteBit\"")]
	MapWriteBit = 0x2,
	[Obsolete("Deprecated in favour of \"WriteBitExt\"")]
	MapWriteBitExt = 0x2,
	[Obsolete("Deprecated in favour of \"InvalidateRangeBit\"")]
	MapInvalidateRangeBit = 0x4,
	[Obsolete("Deprecated in favour of \"InvalidateRangeBitExt\"")]
	MapInvalidateRangeBitExt = 0x4,
	[Obsolete("Deprecated in favour of \"InvalidateBufferBit\"")]
	MapInvalidateBufferBit = 0x8,
	[Obsolete("Deprecated in favour of \"InvalidateBufferBitExt\"")]
	MapInvalidateBufferBitExt = 0x8,
	[Obsolete("Deprecated in favour of \"FlushExplicitBit\"")]
	MapFlushExplicitBit = 0x10,
	[Obsolete("Deprecated in favour of \"FlushExplicitBitExt\"")]
	MapFlushExplicitBitExt = 0x10,
	[Obsolete("Deprecated in favour of \"UnsynchronizedBit\"")]
	MapUnsynchronizedBit = 0x20,
	[Obsolete("Deprecated in favour of \"UnsynchronizedBitExt\"")]
	MapUnsynchronizedBitExt = 0x20,
	[Obsolete("Deprecated in favour of \"PersistentBit\"")]
	MapPersistentBit = 0x40,
	[Obsolete("Deprecated in favour of \"PersistentBitExt\"")]
	MapPersistentBitExt = 0x40,
	[Obsolete("Deprecated in favour of \"CoherentBit\"")]
	MapCoherentBit = 0x80,
	[Obsolete("Deprecated in favour of \"CoherentBitExt\"")]
	MapCoherentBitExt = 0x80,
	
	ReadBit = 0x1,
	
	ReadBitExt = 0x1,
	
	WriteBit = 0x2,
	
	WriteBitExt = 0x2,
	
	InvalidateRangeBit = 0x4,
	
	InvalidateRangeBitExt = 0x4,
	
	InvalidateBufferBit = 0x8,
	
	InvalidateBufferBitExt = 0x8,
	
	FlushExplicitBit = 0x10,
	
	FlushExplicitBitExt = 0x10,
	
	UnsynchronizedBit = 0x20,
	
	UnsynchronizedBitExt = 0x20,
	
	PersistentBit = 0x40,
	
	PersistentBitExt = 0x40,
	
	CoherentBit = 0x80,
	
	CoherentBitExt = 0x80
}