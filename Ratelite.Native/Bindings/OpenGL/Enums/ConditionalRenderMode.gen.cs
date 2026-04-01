#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ConditionalRenderMode : int
{
	[Obsolete("Deprecated in favour of \"Wait\"")]
	QueryWait = 0x8E13,
	[Obsolete("Deprecated in favour of \"NoWait\"")]
	QueryNoWait = 0x8E14,
	[Obsolete("Deprecated in favour of \"ByRegionWait\"")]
	QueryByRegionWait = 0x8E15,
	[Obsolete("Deprecated in favour of \"ByRegionNoWait\"")]
	QueryByRegionNoWait = 0x8E16,
	[Obsolete("Deprecated in favour of \"WaitInverted\"")]
	QueryWaitInverted = 0x8E17,
	[Obsolete("Deprecated in favour of \"NoWaitInverted\"")]
	QueryNoWaitInverted = 0x8E18,
	[Obsolete("Deprecated in favour of \"ByRegionWaitInverted\"")]
	QueryByRegionWaitInverted = 0x8E19,
	[Obsolete("Deprecated in favour of \"ByRegionNoWaitInverted\"")]
	QueryByRegionNoWaitInverted = 0x8E1A,
	Wait = 0x8E13,
	
	NoWait = 0x8E14,
	
	ByRegionWait = 0x8E15,
	
	ByRegionNoWait = 0x8E16,
	
	WaitInverted = 0x8E17,
	
	NoWaitInverted = 0x8E18,
	
	ByRegionWaitInverted = 0x8E19,
	
	ByRegionNoWaitInverted = 0x8E1A
}