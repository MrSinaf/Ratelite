#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum PathParameter : int
{
	[Obsolete("Deprecated in favour of \"StrokeWidthNV\"")]
	PathStrokeWidthNV = 0x9075,
	[Obsolete("Deprecated in favour of \"EndCapsNV\"")]
	PathEndCapsNV = 0x9076,
	[Obsolete("Deprecated in favour of \"InitialEndCapNV\"")]
	PathInitialEndCapNV = 0x9077,
	[Obsolete("Deprecated in favour of \"TerminalEndCapNV\"")]
	PathTerminalEndCapNV = 0x9078,
	[Obsolete("Deprecated in favour of \"JoinStyleNV\"")]
	PathJoinStyleNV = 0x9079,
	[Obsolete("Deprecated in favour of \"MiterLimitNV\"")]
	PathMiterLimitNV = 0x907A,
	[Obsolete("Deprecated in favour of \"DashCapsNV\"")]
	PathDashCapsNV = 0x907B,
	[Obsolete("Deprecated in favour of \"InitialDashCapNV\"")]
	PathInitialDashCapNV = 0x907C,
	[Obsolete("Deprecated in favour of \"TerminalDashCapNV\"")]
	PathTerminalDashCapNV = 0x907D,
	[Obsolete("Deprecated in favour of \"DashOffsetNV\"")]
	PathDashOffsetNV = 0x907E,
	[Obsolete("Deprecated in favour of \"ClientLengthNV\"")]
	PathClientLengthNV = 0x907F,
	[Obsolete("Deprecated in favour of \"FillModeNV\"")]
	PathFillModeNV = 0x9080,
	[Obsolete("Deprecated in favour of \"FillMaskNV\"")]
	PathFillMaskNV = 0x9081,
	[Obsolete("Deprecated in favour of \"FillCoverModeNV\"")]
	PathFillCoverModeNV = 0x9082,
	[Obsolete("Deprecated in favour of \"StrokeCoverModeNV\"")]
	PathStrokeCoverModeNV = 0x9083,
	[Obsolete("Deprecated in favour of \"StrokeMaskNV\"")]
	PathStrokeMaskNV = 0x9084,
	[Obsolete("Deprecated in favour of \"ObjectBoundingBoxNV\"")]
	PathObjectBoundingBoxNV = 0x908A,
	[Obsolete("Deprecated in favour of \"CommandCountNV\"")]
	PathCommandCountNV = 0x909D,
	[Obsolete("Deprecated in favour of \"CoordCountNV\"")]
	PathCoordCountNV = 0x909E,
	[Obsolete("Deprecated in favour of \"DashArrayCountNV\"")]
	PathDashArrayCountNV = 0x909F,
	[Obsolete("Deprecated in favour of \"ComputedLengthNV\"")]
	PathComputedLengthNV = 0x90A0,
	[Obsolete("Deprecated in favour of \"FillBoundingBoxNV\"")]
	PathFillBoundingBoxNV = 0x90A1,
	[Obsolete("Deprecated in favour of \"StrokeBoundingBoxNV\"")]
	PathStrokeBoundingBoxNV = 0x90A2,
	[Obsolete("Deprecated in favour of \"DashOffsetResetNV\"")]
	PathDashOffsetResetNV = 0x90B4,
	
	StrokeWidthNV = 0x9075,
	
	EndCapsNV = 0x9076,
	
	InitialEndCapNV = 0x9077,
	
	TerminalEndCapNV = 0x9078,
	
	JoinStyleNV = 0x9079,
	
	MiterLimitNV = 0x907A,
	
	DashCapsNV = 0x907B,
	
	InitialDashCapNV = 0x907C,
	
	TerminalDashCapNV = 0x907D,
	
	DashOffsetNV = 0x907E,
	
	ClientLengthNV = 0x907F,
	
	FillModeNV = 0x9080,
	
	FillMaskNV = 0x9081,
	
	FillCoverModeNV = 0x9082,
	
	StrokeCoverModeNV = 0x9083,
	
	StrokeMaskNV = 0x9084,
	
	ObjectBoundingBoxNV = 0x908A,
	
	CommandCountNV = 0x909D,
	
	CoordCountNV = 0x909E,
	
	DashArrayCountNV = 0x909F,
	
	ComputedLengthNV = 0x90A0,
	
	FillBoundingBoxNV = 0x90A1,
	
	StrokeBoundingBoxNV = 0x90A2,
	
	DashOffsetResetNV = 0x90B4
}