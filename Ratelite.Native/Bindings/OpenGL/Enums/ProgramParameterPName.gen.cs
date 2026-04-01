#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ProgramParameterPName : int
{
	[Obsolete("Deprecated in favour of \"BinaryRetrievableHint\"")]
	ProgramBinaryRetrievableHint = 0x8257,
	[Obsolete("Deprecated in favour of \"Separable\"")]
	ProgramSeparable = 0x8258,
	
	BinaryRetrievableHint = 0x8257,
	
	Separable = 0x8258
}