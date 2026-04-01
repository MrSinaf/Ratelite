#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum BufferPointerNameARB : int
{
	[Obsolete("Deprecated in favour of \"Pointer\"")]
	BufferMapPointer = 0x88BD,
	[Obsolete("Deprecated in favour of \"PointerArb\"")]
	BufferMapPointerArb = 0x88BD,
	
	Pointer = 0x88BD,
	
	PointerArb = 0x88BD
}