#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum GetVariantValueEXT : int
{
	[Obsolete("Deprecated in favour of \"ValueExt\"")]
	VariantValueExt = 0x87E4,
	[Obsolete("Deprecated in favour of \"DatatypeExt\"")]
	VariantDatatypeExt = 0x87E5,
	[Obsolete("Deprecated in favour of \"ArrayStrideExt\"")]
	VariantArrayStrideExt = 0x87E6,
	[Obsolete("Deprecated in favour of \"ArrayTypeExt\"")]
	VariantArrayTypeExt = 0x87E7,
	
	ValueExt = 0x87E4,
	
	DatatypeExt = 0x87E5,
	
	ArrayStrideExt = 0x87E6,
	
	ArrayTypeExt = 0x87E7
}