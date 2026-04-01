#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum PNTrianglesPNameATI : int
{
	[Obsolete("Deprecated in favour of \"PointModeAti\"")]
	PNTrianglesPointModeAti = 0x87F2,
	[Obsolete("Deprecated in favour of \"NormalModeAti\"")]
	PNTrianglesNormalModeAti = 0x87F3,
	[Obsolete("Deprecated in favour of \"TesselationLevelAti\"")]
	PNTrianglesTesselationLevelAti = 0x87F4,
	
	PointModeAti = 0x87F2,
	
	NormalModeAti = 0x87F3,
	
	TesselationLevelAti = 0x87F4
}