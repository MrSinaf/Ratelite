#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ColorTableParameterPName : int
{
	[Obsolete("Deprecated in favour of \"Scale\"")]
	ColorTableScale = 0x80D6,
	[Obsolete("Deprecated in favour of \"ScaleSgi\"")]
	ColorTableScaleSgi = 0x80D6,
	[Obsolete("Deprecated in favour of \"Bias\"")]
	ColorTableBias = 0x80D7,
	[Obsolete("Deprecated in favour of \"BiasSgi\"")]
	ColorTableBiasSgi = 0x80D7,
	[Obsolete("Deprecated in favour of \"Format\"")]
	ColorTableFormat = 0x80D8,
	[Obsolete("Deprecated in favour of \"FormatSgi\"")]
	ColorTableFormatSgi = 0x80D8,
	[Obsolete("Deprecated in favour of \"Width\"")]
	ColorTableWidth = 0x80D9,
	[Obsolete("Deprecated in favour of \"WidthSgi\"")]
	ColorTableWidthSgi = 0x80D9,
	[Obsolete("Deprecated in favour of \"RedSize\"")]
	ColorTableRedSize = 0x80DA,
	[Obsolete("Deprecated in favour of \"RedSizeSgi\"")]
	ColorTableRedSizeSgi = 0x80DA,
	[Obsolete("Deprecated in favour of \"GreenSize\"")]
	ColorTableGreenSize = 0x80DB,
	[Obsolete("Deprecated in favour of \"GreenSizeSgi\"")]
	ColorTableGreenSizeSgi = 0x80DB,
	[Obsolete("Deprecated in favour of \"BlueSize\"")]
	ColorTableBlueSize = 0x80DC,
	[Obsolete("Deprecated in favour of \"BlueSizeSgi\"")]
	ColorTableBlueSizeSgi = 0x80DC,
	[Obsolete("Deprecated in favour of \"AlphaSize\"")]
	ColorTableAlphaSize = 0x80DD,
	[Obsolete("Deprecated in favour of \"AlphaSizeSgi\"")]
	ColorTableAlphaSizeSgi = 0x80DD,
	[Obsolete("Deprecated in favour of \"LuminanceSize\"")]
	ColorTableLuminanceSize = 0x80DE,
	[Obsolete("Deprecated in favour of \"LuminanceSizeSgi\"")]
	ColorTableLuminanceSizeSgi = 0x80DE,
	[Obsolete("Deprecated in favour of \"IntensitySize\"")]
	ColorTableIntensitySize = 0x80DF,
	[Obsolete("Deprecated in favour of \"IntensitySizeSgi\"")]
	ColorTableIntensitySizeSgi = 0x80DF,
	
	Scale = 0x80D6,
	
	ScaleSgi = 0x80D6,
	
	Bias = 0x80D7,
	
	BiasSgi = 0x80D7,
	
	Format = 0x80D8,
	
	FormatSgi = 0x80D8,
	
	Width = 0x80D9,
	
	WidthSgi = 0x80D9,
	
	RedSize = 0x80DA,
	
	RedSizeSgi = 0x80DA,
	
	GreenSize = 0x80DB,
	
	GreenSizeSgi = 0x80DB,
	
	BlueSize = 0x80DC,
	
	BlueSizeSgi = 0x80DC,
	
	AlphaSize = 0x80DD,
	
	AlphaSizeSgi = 0x80DD,
	
	LuminanceSize = 0x80DE,
	
	LuminanceSizeSgi = 0x80DE,
	
	IntensitySize = 0x80DF,
	
	IntensitySizeSgi = 0x80DF
}