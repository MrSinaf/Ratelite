#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum FramebufferParameterName : int
{
	[Obsolete("Deprecated in favour of \"Width\"")]
	FramebufferDefaultWidth = 0x9310,
	[Obsolete("Deprecated in favour of \"Height\"")]
	FramebufferDefaultHeight = 0x9311,
	[Obsolete("Deprecated in favour of \"Layers\"")]
	FramebufferDefaultLayers = 0x9312,
	[Obsolete("Deprecated in favour of \"Samples\"")]
	FramebufferDefaultSamples = 0x9313,
	[Obsolete("Deprecated in favour of \"FixedSampleLocations\"")]
	FramebufferDefaultFixedSampleLocations = 0x9314,
	
	Width = 0x9310,
	
	Height = 0x9311,
	
	Layers = 0x9312,
	
	Samples = 0x9313,
	
	FixedSampleLocations = 0x9314
}