#pragma warning disable 1591

namespace Ratelite.Bindings;

[Flags]
public enum OcclusionQueryEventMaskAMD : int
{
	None = 0,
	[Obsolete("Deprecated in favour of \"DepthPassEventBitAmd\"")]
	QueryDepthPassEventBitAmd = 0x1,
	[Obsolete("Deprecated in favour of \"DepthFailEventBitAmd\"")]
	QueryDepthFailEventBitAmd = 0x2,
	[Obsolete("Deprecated in favour of \"StencilFailEventBitAmd\"")]
	QueryStencilFailEventBitAmd = 0x4,
	[Obsolete("Deprecated in favour of \"DepthBoundsFailEventBitAmd\"")]
	QueryDepthBoundsFailEventBitAmd = 0x8,
	[Obsolete("Deprecated in favour of \"AllEventBitsAmd\"")]
	QueryAllEventBitsAmd = unchecked((int)0xFFFFFFFF),
	
	DepthPassEventBitAmd = 0x1,
	
	DepthFailEventBitAmd = 0x2,
	
	StencilFailEventBitAmd = 0x4,
	
	DepthBoundsFailEventBitAmd = 0x8,
	
	AllEventBitsAmd = unchecked((int)0xFFFFFFFF)
}