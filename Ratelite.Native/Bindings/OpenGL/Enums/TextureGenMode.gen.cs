#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum TextureGenMode : int
{
	EyeDistanceToPointSgis = 0x81F0,
	
	ObjectDistanceToPointSgis = 0x81F1,
	
	EyeDistanceToLineSgis = 0x81F2,
	
	ObjectDistanceToLineSgis = 0x81F3
}