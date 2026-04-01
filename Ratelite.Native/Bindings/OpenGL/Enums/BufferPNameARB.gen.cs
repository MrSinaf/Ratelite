#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum BufferPNameARB : int
{
	[Obsolete("Deprecated in favour of \"ImmutableStorage\"")]
	BufferImmutableStorage = 0x821F,
	[Obsolete("Deprecated in favour of \"StorageFlags\"")]
	BufferStorageFlags = 0x8220,
	[Obsolete("Deprecated in favour of \"Size\"")]
	BufferSize = 0x8764,
	[Obsolete("Deprecated in favour of \"SizeArb\"")]
	BufferSizeArb = 0x8764,
	[Obsolete("Deprecated in favour of \"Usage\"")]
	BufferUsage = 0x8765,
	[Obsolete("Deprecated in favour of \"UsageArb\"")]
	BufferUsageArb = 0x8765,
	[Obsolete("Deprecated in favour of \"Access\"")]
	BufferAccess = 0x88BB,
	[Obsolete("Deprecated in favour of \"AccessArb\"")]
	BufferAccessArb = 0x88BB,
	[Obsolete("Deprecated in favour of \"Mapped\"")]
	BufferMapped = 0x88BC,
	[Obsolete("Deprecated in favour of \"MappedArb\"")]
	BufferMappedArb = 0x88BC,
	[Obsolete("Deprecated in favour of \"AccessFlags\"")]
	BufferAccessFlags = 0x911F,
	[Obsolete("Deprecated in favour of \"MapLength\"")]
	BufferMapLength = 0x9120,
	[Obsolete("Deprecated in favour of \"MapOffset\"")]
	BufferMapOffset = 0x9121,
	
	ImmutableStorage = 0x821F,
	
	StorageFlags = 0x8220,
	Size = 0x8764,
	
	SizeArb = 0x8764,
	
	Usage = 0x8765,
	
	UsageArb = 0x8765,
	
	Access = 0x88BB,
	
	AccessArb = 0x88BB,
	
	Mapped = 0x88BC,
	
	MappedArb = 0x88BC,
	
	AccessFlags = 0x911F,
	
	MapLength = 0x9120,
	
	MapOffset = 0x9121
}