#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum FragmentLightModelParameterSGIX : int
{
	[Obsolete("Deprecated in favour of \"LocalViewerSgix\"")]
	FragmentLightModelLocalViewerSgix = 0x8408,
	[Obsolete("Deprecated in favour of \"TwoSideSgix\"")]
	FragmentLightModelTwoSideSgix = 0x8409,
	[Obsolete("Deprecated in favour of \"AmbientSgix\"")]
	FragmentLightModelAmbientSgix = 0x840A,
	[Obsolete("Deprecated in favour of \"NormalInterpolationSgix\"")]
	FragmentLightModelNormalInterpolationSgix = 0x840B,
	
	LocalViewerSgix = 0x8408,
	
	TwoSideSgix = 0x8409,
	
	AmbientSgix = 0x840A,
	
	NormalInterpolationSgix = 0x840B
}