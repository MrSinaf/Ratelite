#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ClampColorTargetARB : int
{
	[Obsolete("Deprecated in favour of \"VertexColorArb\"")]
	ClampVertexColorArb = 0x891A,
	[Obsolete("Deprecated in favour of \"FragmentColorArb\"")]
	ClampFragmentColorArb = 0x891B,
	[Obsolete("Deprecated in favour of \"ReadColor\"")]
	ClampReadColor = 0x891C,
	[Obsolete("Deprecated in favour of \"ReadColorArb\"")]
	ClampReadColorArb = 0x891C,
	
	VertexColorArb = 0x891A,
	
	FragmentColorArb = 0x891B,
	
	ReadColor = 0x891C,
	
	ReadColorArb = 0x891C
}