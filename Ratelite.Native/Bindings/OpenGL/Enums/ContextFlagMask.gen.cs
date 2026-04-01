#pragma warning disable 1591

namespace Ratelite.Bindings;

[Flags]
public enum ContextFlagMask : int
{
	None = 0,
	[Obsolete("Deprecated in favour of \"ForwardCompatibleBit\"")]
	ContextFlagForwardCompatibleBit = 0x1,
	[Obsolete("Deprecated in favour of \"DebugBit\"")]
	ContextFlagDebugBit = 0x2,
	[Obsolete("Deprecated in favour of \"DebugBitKhr\"")]
	ContextFlagDebugBitKhr = 0x2,
	[Obsolete("Deprecated in favour of \"RobustAccessBit\"")]
	ContextFlagRobustAccessBit = 0x4,
	[Obsolete("Deprecated in favour of \"RobustAccessBitArb\"")]
	ContextFlagRobustAccessBitArb = 0x4,
	[Obsolete("Deprecated in favour of \"NoErrorBit\"")]
	ContextFlagNoErrorBit = 0x8,
	[Obsolete("Deprecated in favour of \"NoErrorBitKhr\"")]
	ContextFlagNoErrorBitKhr = 0x8,
	[Obsolete("Deprecated in favour of \"ProtectedContentBitExt\"")]
	ContextFlagProtectedContentBitExt = 0x10,
	
	ForwardCompatibleBit = 0x1,
	
	DebugBit = 0x2,
	
	DebugBitKhr = 0x2,
	
	RobustAccessBit = 0x4,
	
	RobustAccessBitArb = 0x4,
	
	NoErrorBit = 0x8,
	
	NoErrorBitKhr = 0x8,
	
	ProtectedContentBitExt = 0x10
}