#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum PixelTransformPNameEXT : int
{
	[Obsolete("Deprecated in favour of \"MagFilterExt\"")]
	PixelMagFilterExt = 0x8331,
	[Obsolete("Deprecated in favour of \"MinFilterExt\"")]
	PixelMinFilterExt = 0x8332,
	[Obsolete("Deprecated in favour of \"CubicWeightExt\"")]
	PixelCubicWeightExt = 0x8333,
	
	MagFilterExt = 0x8331,
	
	MinFilterExt = 0x8332,
	
	CubicWeightExt = 0x8333
}