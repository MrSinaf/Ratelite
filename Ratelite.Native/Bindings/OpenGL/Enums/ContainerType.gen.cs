#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ContainerType : int
{
	[Obsolete("Deprecated in favour of \"Arb\"")]
	ProgramObjectArb = 0x8B40,
	[Obsolete("Deprecated in favour of \"Ext\"")]
	ProgramObjectExt = 0x8B40,
	
	Arb = 0x8B40,
	
	Ext = 0x8B40
}