#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum TextureWrapMode : int
{
	Repeat = 0x2901,
	
	ClampToBorder = 0x812D,
	
	ClampToBorderArb = 0x812D,
	
	ClampToBorderExt = 0x812D,
	
	ClampToBorderNV = 0x812D,
	
	ClampToBorderSgis = 0x812D,
	
	ClampToBorderOes = 0x812D,
	
	ClampToEdge = 0x812F,
	
	ClampToEdgeSgis = 0x812F,
	
	MirroredRepeat = 0x8370,
	
	MirroredRepeatArb = 0x8370,
	
	MirroredRepeatIbm = 0x8370,
	
	MirroredRepeatOes = 0x8370
}