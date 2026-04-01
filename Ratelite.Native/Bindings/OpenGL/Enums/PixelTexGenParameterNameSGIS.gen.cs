#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum PixelTexGenParameterNameSGIS : int
{
	[Obsolete("Deprecated in favour of \"RgbSourceSgis\"")]
	PixelFragmentRgbSourceSgis = 0x8354,
	[Obsolete("Deprecated in favour of \"AlphaSourceSgis\"")]
	PixelFragmentAlphaSourceSgis = 0x8355,
	
	RgbSourceSgis = 0x8354,
	
	AlphaSourceSgis = 0x8355
}