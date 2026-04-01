#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum OcclusionQueryParameterNameNV : int
{
	[Obsolete("Deprecated in favour of \"NV\"")]
	PixelCountNV = 0x8866,
	[Obsolete("Deprecated in favour of \"AvailableNV\"")]
	PixelCountAvailableNV = 0x8867,
	
	NV = 0x8866,
	
	AvailableNV = 0x8867
}