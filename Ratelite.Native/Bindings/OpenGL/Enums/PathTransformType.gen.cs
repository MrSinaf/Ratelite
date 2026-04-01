#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum PathTransformType : int
{
	None = 0x0,
	
	TranslateXNV = 0x908E,
	
	TranslateYNV = 0x908F,
	
	Translate2DNV = 0x9090,
	
	Translate3DNV = 0x9091,
	
	Affine2DNV = 0x9092,
	
	Affine3DNV = 0x9094,
	
	TransposeAffine2DNV = 0x9096,
	
	TransposeAffine3DNV = 0x9098
}