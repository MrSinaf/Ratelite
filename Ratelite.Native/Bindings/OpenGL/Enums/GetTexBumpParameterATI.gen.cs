#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum GetTexBumpParameterATI : int
{
	[Obsolete("Deprecated in favour of \"RotMatrixAti\"")]
	BumpRotMatrixAti = 0x8775,
	[Obsolete("Deprecated in favour of \"RotMatrixSizeAti\"")]
	BumpRotMatrixSizeAti = 0x8776,
	[Obsolete("Deprecated in favour of \"NumTexUnitsAti\"")]
	BumpNumTexUnitsAti = 0x8777,
	[Obsolete("Deprecated in favour of \"TexUnitsAti\"")]
	BumpTexUnitsAti = 0x8778,
	
	RotMatrixAti = 0x8775,
	
	RotMatrixSizeAti = 0x8776,
	
	NumTexUnitsAti = 0x8777,
	
	TexUnitsAti = 0x8778
}