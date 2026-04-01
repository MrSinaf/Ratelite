#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum PixelTexGenModeSGIX : int
{
	None = 0x0,
	Alpha = 0x1906,
	Rgb = 0x1907,
	Rgba = 0x1908,
	
	PixelTexGenQCeilingSgix = 0x8184,
	
	PixelTexGenQRoundSgix = 0x8185,
	
	PixelTexGenQFloorSgix = 0x8186,
	
	PixelTexGenAlphaLSSgix = 0x8189,
	
	PixelTexGenAlphaMSSgix = 0x818A
}