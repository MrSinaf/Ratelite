#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum SamplerParameterF : int
{
	[Obsolete("Deprecated in favour of \"BorderColor\"")]
	TextureBorderColor = 0x1004,
	[Obsolete("Deprecated in favour of \"MinLod\"")]
	TextureMinLod = 0x813A,
	[Obsolete("Deprecated in favour of \"MaxLod\"")]
	TextureMaxLod = 0x813B,
	[Obsolete("Deprecated in favour of \"MaxAnisotropy\"")]
	TextureMaxAnisotropy = 0x84FE,
	[Obsolete("Deprecated in favour of \"LodBias\"")]
	TextureLodBias = 0x8501,
	[Obsolete("Deprecated in favour of \"UnnormalizedCoordinatesArm\"")]
	TextureUnnormalizedCoordinatesArm = 0x8F6A,
	
	BorderColor = 0x1004,
	
	MinLod = 0x813A,
	
	MaxLod = 0x813B,
	
	MaxAnisotropy = 0x84FE,
	
	LodBias = 0x8501,
	
	UnnormalizedCoordinatesArm = 0x8F6A
}