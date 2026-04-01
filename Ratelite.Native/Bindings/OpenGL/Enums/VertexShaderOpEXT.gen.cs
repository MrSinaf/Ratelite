#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum VertexShaderOpEXT : int
{
	[Obsolete("Deprecated in favour of \"IndexExt\"")]
	OpIndexExt = 0x8782,
	[Obsolete("Deprecated in favour of \"NegateExt\"")]
	OpNegateExt = 0x8783,
	[Obsolete("Deprecated in favour of \"Dot3Ext\"")]
	OpDot3Ext = 0x8784,
	[Obsolete("Deprecated in favour of \"Dot4Ext\"")]
	OpDot4Ext = 0x8785,
	[Obsolete("Deprecated in favour of \"MulExt\"")]
	OpMulExt = 0x8786,
	[Obsolete("Deprecated in favour of \"AddExt\"")]
	OpAddExt = 0x8787,
	[Obsolete("Deprecated in favour of \"MaddExt\"")]
	OpMaddExt = 0x8788,
	[Obsolete("Deprecated in favour of \"FracExt\"")]
	OpFracExt = 0x8789,
	[Obsolete("Deprecated in favour of \"MaxExt\"")]
	OpMaxExt = 0x878A,
	[Obsolete("Deprecated in favour of \"MinExt\"")]
	OpMinExt = 0x878B,
	[Obsolete("Deprecated in favour of \"SetGEExt\"")]
	OpSetGEExt = 0x878C,
	[Obsolete("Deprecated in favour of \"SetLTExt\"")]
	OpSetLTExt = 0x878D,
	[Obsolete("Deprecated in favour of \"ClampExt\"")]
	OpClampExt = 0x878E,
	[Obsolete("Deprecated in favour of \"FloorExt\"")]
	OpFloorExt = 0x878F,
	[Obsolete("Deprecated in favour of \"RoundExt\"")]
	OpRoundExt = 0x8790,
	[Obsolete("Deprecated in favour of \"ExpBase2Ext\"")]
	OpExpBase2Ext = 0x8791,
	[Obsolete("Deprecated in favour of \"LogBase2Ext\"")]
	OpLogBase2Ext = 0x8792,
	[Obsolete("Deprecated in favour of \"PowerExt\"")]
	OpPowerExt = 0x8793,
	[Obsolete("Deprecated in favour of \"RecipExt\"")]
	OpRecipExt = 0x8794,
	[Obsolete("Deprecated in favour of \"RecipSqrtExt\"")]
	OpRecipSqrtExt = 0x8795,
	[Obsolete("Deprecated in favour of \"SubExt\"")]
	OpSubExt = 0x8796,
	[Obsolete("Deprecated in favour of \"CrossProductExt\"")]
	OpCrossProductExt = 0x8797,
	[Obsolete("Deprecated in favour of \"MultiplyMatrixExt\"")]
	OpMultiplyMatrixExt = 0x8798,
	[Obsolete("Deprecated in favour of \"MovExt\"")]
	OpMovExt = 0x8799,
	
	IndexExt = 0x8782,
	
	NegateExt = 0x8783,
	Dot3Ext = 0x8784,
	Dot4Ext = 0x8785,
	MulExt = 0x8786,
	AddExt = 0x8787,
	MaddExt = 0x8788,
	FracExt = 0x8789,
	MaxExt = 0x878A,
	MinExt = 0x878B,
	
	SetGEExt = 0x878C,
	
	SetLTExt = 0x878D,
	
	ClampExt = 0x878E,
	
	FloorExt = 0x878F,
	
	RoundExt = 0x8790,
	
	ExpBase2Ext = 0x8791,
	
	LogBase2Ext = 0x8792,
	
	PowerExt = 0x8793,
	
	RecipExt = 0x8794,
	
	RecipSqrtExt = 0x8795,
	SubExt = 0x8796,
	
	CrossProductExt = 0x8797,
	
	MultiplyMatrixExt = 0x8798,
	MovExt = 0x8799
}