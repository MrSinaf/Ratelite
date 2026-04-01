#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ContextProfileMask : int
{
	[Obsolete("Deprecated in favour of \"CoreProfileBit\"")]
	ContextCoreProfileBit = 0x1,
	[Obsolete("Deprecated in favour of \"CompatibilityProfileBit\"")]
	ContextCompatibilityProfileBit = 0x2,
	
	CoreProfileBit = 0x1,
	
	CompatibilityProfileBit = 0x2
}