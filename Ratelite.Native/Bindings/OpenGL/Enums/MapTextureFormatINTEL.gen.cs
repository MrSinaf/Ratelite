#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum MapTextureFormatINTEL : int
{
	[Obsolete("Deprecated in favour of \"DefaultIntel\"")]
	LayoutDefaultIntel = 0x0,
	[Obsolete("Deprecated in favour of \"LinearIntel\"")]
	LayoutLinearIntel = 0x1,
	[Obsolete("Deprecated in favour of \"LinearCpuCachedIntel\"")]
	LayoutLinearCpuCachedIntel = 0x2,
	
	DefaultIntel = 0x0,
	
	LinearIntel = 0x1,
	
	LinearCpuCachedIntel = 0x2
}