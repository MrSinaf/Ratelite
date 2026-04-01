#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ClipPlaneName : int
{
	[Obsolete("Deprecated in favour of \"Distance0\"")]
	ClipDistance0 = 0x3000,
	[Obsolete("Deprecated in favour of \"Distance1\"")]
	ClipDistance1 = 0x3001,
	[Obsolete("Deprecated in favour of \"Distance2\"")]
	ClipDistance2 = 0x3002,
	[Obsolete("Deprecated in favour of \"Distance3\"")]
	ClipDistance3 = 0x3003,
	[Obsolete("Deprecated in favour of \"Distance4\"")]
	ClipDistance4 = 0x3004,
	[Obsolete("Deprecated in favour of \"Distance5\"")]
	ClipDistance5 = 0x3005,
	[Obsolete("Deprecated in favour of \"Distance6\"")]
	ClipDistance6 = 0x3006,
	[Obsolete("Deprecated in favour of \"Distance7\"")]
	ClipDistance7 = 0x3007,
	
	Distance0 = 0x3000,
	
	Distance1 = 0x3001,
	
	Distance2 = 0x3002,
	
	Distance3 = 0x3003,
	
	Distance4 = 0x3004,
	
	Distance5 = 0x3005,
	
	Distance6 = 0x3006,
	
	Distance7 = 0x3007
}