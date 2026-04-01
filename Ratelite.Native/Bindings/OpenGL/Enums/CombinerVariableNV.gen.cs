#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum CombinerVariableNV : int
{
	[Obsolete("Deprecated in favour of \"ANV\"")]
	VariableANV = 0x8523,
	[Obsolete("Deprecated in favour of \"BNV\"")]
	VariableBNV = 0x8524,
	[Obsolete("Deprecated in favour of \"CNV\"")]
	VariableCNV = 0x8525,
	[Obsolete("Deprecated in favour of \"DNV\"")]
	VariableDNV = 0x8526,
	[Obsolete("Deprecated in favour of \"ENV\"")]
	VariableENV = 0x8527,
	[Obsolete("Deprecated in favour of \"FNV\"")]
	VariableFNV = 0x8528,
	[Obsolete("Deprecated in favour of \"GNV\"")]
	VariableGNV = 0x8529,
	
	ANV = 0x8523,
	
	BNV = 0x8524,
	
	CNV = 0x8525,
	
	DNV = 0x8526,
	
	ENV = 0x8527,
	
	FNV = 0x8528,
	
	GNV = 0x8529
}