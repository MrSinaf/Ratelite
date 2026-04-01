#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum VertexAttribPointerPropertyARB : int
{
	[Obsolete("Deprecated in favour of \"Pointer\"")]
	VertexAttribArrayPointer = 0x8645,
	[Obsolete("Deprecated in favour of \"PointerArb\"")]
	VertexAttribArrayPointerArb = 0x8645,
	
	Pointer = 0x8645,
	
	PointerArb = 0x8645
}