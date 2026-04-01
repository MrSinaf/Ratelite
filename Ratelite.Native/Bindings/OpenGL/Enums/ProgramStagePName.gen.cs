#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ProgramStagePName : int
{
	[Obsolete("Deprecated in favour of \"Subroutines\"")]
	ActiveSubroutines = 0x8DE5,
	[Obsolete("Deprecated in favour of \"SubroutineUniforms\"")]
	ActiveSubroutineUniforms = 0x8DE6,
	[Obsolete("Deprecated in favour of \"SubroutineUniformLocations\"")]
	ActiveSubroutineUniformLocations = 0x8E47,
	[Obsolete("Deprecated in favour of \"SubroutineMaxLength\"")]
	ActiveSubroutineMaxLength = 0x8E48,
	[Obsolete("Deprecated in favour of \"SubroutineUniformMaxLength\"")]
	ActiveSubroutineUniformMaxLength = 0x8E49,
	
	Subroutines = 0x8DE5,
	
	SubroutineUniforms = 0x8DE6,
	
	SubroutineUniformLocations = 0x8E47,
	
	SubroutineMaxLength = 0x8E48,
	
	SubroutineUniformMaxLength = 0x8E49
}