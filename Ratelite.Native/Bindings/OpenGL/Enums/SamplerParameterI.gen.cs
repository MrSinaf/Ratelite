#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum SamplerParameterI : int
{
	[Obsolete("Deprecated in favour of \"MagFilter\"")]
	TextureMagFilter = 0x2800,
	[Obsolete("Deprecated in favour of \"MinFilter\"")]
	TextureMinFilter = 0x2801,
	[Obsolete("Deprecated in favour of \"WrapS\"")]
	TextureWrapS = 0x2802,
	[Obsolete("Deprecated in favour of \"WrapT\"")]
	TextureWrapT = 0x2803,
	[Obsolete("Deprecated in favour of \"WrapR\"")]
	TextureWrapR = 0x8072,
	[Obsolete("Deprecated in favour of \"CompareMode\"")]
	TextureCompareMode = 0x884C,
	[Obsolete("Deprecated in favour of \"CompareFunc\"")]
	TextureCompareFunc = 0x884D,
	[Obsolete("Deprecated in favour of \"UnnormalizedCoordinatesArm\"")]
	TextureUnnormalizedCoordinatesArm = 0x8F6A,
	
	MagFilter = 0x2800,
	
	MinFilter = 0x2801,
	
	WrapS = 0x2802,
	
	WrapT = 0x2803,
	
	WrapR = 0x8072,
	
	CompareMode = 0x884C,
	
	CompareFunc = 0x884D,
	
	UnnormalizedCoordinatesArm = 0x8F6A
}