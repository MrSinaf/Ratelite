#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ImageTransformPNameHP : int
{
	[Obsolete("Deprecated in favour of \"ScaleXHP\"")]
	ImageScaleXHP = 0x8155,
	[Obsolete("Deprecated in favour of \"ScaleYHP\"")]
	ImageScaleYHP = 0x8156,
	[Obsolete("Deprecated in favour of \"TranslateXHP\"")]
	ImageTranslateXHP = 0x8157,
	[Obsolete("Deprecated in favour of \"TranslateYHP\"")]
	ImageTranslateYHP = 0x8158,
	[Obsolete("Deprecated in favour of \"RotateAngleHP\"")]
	ImageRotateAngleHP = 0x8159,
	[Obsolete("Deprecated in favour of \"RotateOriginXHP\"")]
	ImageRotateOriginXHP = 0x815A,
	[Obsolete("Deprecated in favour of \"RotateOriginYHP\"")]
	ImageRotateOriginYHP = 0x815B,
	[Obsolete("Deprecated in favour of \"MagFilterHP\"")]
	ImageMagFilterHP = 0x815C,
	[Obsolete("Deprecated in favour of \"MinFilterHP\"")]
	ImageMinFilterHP = 0x815D,
	[Obsolete("Deprecated in favour of \"CubicWeightHP\"")]
	ImageCubicWeightHP = 0x815E,
	
	ScaleXHP = 0x8155,
	
	ScaleYHP = 0x8156,
	
	TranslateXHP = 0x8157,
	
	TranslateYHP = 0x8158,
	
	RotateAngleHP = 0x8159,
	
	RotateOriginXHP = 0x815A,
	
	RotateOriginYHP = 0x815B,
	
	MagFilterHP = 0x815C,
	
	MinFilterHP = 0x815D,
	
	CubicWeightHP = 0x815E
}