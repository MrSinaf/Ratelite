#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum PathStringFormat : int
{
	[Obsolete("Deprecated in favour of \"SvgNV\"")]
	PathFormatSvgNV = 0x9070,
	[Obsolete("Deprecated in favour of \"PSNV\"")]
	PathFormatPSNV = 0x9071,
	
	SvgNV = 0x9070,
	
	PSNV = 0x9071
}