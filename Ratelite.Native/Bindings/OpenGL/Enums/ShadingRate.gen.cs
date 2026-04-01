#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ShadingRate : int
{
	[Obsolete("Deprecated in favour of \"Rate1X1PixelsExt\"")]
	ShadingRate1X1PixelsExt = 0x96A6,
	[Obsolete("Deprecated in favour of \"Rate1X2PixelsExt\"")]
	ShadingRate1X2PixelsExt = 0x96A7,
	[Obsolete("Deprecated in favour of \"Rate2X1PixelsExt\"")]
	ShadingRate2X1PixelsExt = 0x96A8,
	[Obsolete("Deprecated in favour of \"Rate2X2PixelsExt\"")]
	ShadingRate2X2PixelsExt = 0x96A9,
	[Obsolete("Deprecated in favour of \"Rate1X4PixelsExt\"")]
	ShadingRate1X4PixelsExt = 0x96AA,
	[Obsolete("Deprecated in favour of \"Rate4X1PixelsExt\"")]
	ShadingRate4X1PixelsExt = 0x96AB,
	[Obsolete("Deprecated in favour of \"Rate4X2PixelsExt\"")]
	ShadingRate4X2PixelsExt = 0x96AC,
	[Obsolete("Deprecated in favour of \"Rate2X4PixelsExt\"")]
	ShadingRate2X4PixelsExt = 0x96AD,
	[Obsolete("Deprecated in favour of \"Rate4X4PixelsExt\"")]
	ShadingRate4X4PixelsExt = 0x96AE,
	
	Rate1X1PixelsExt = 0x96A6,
	
	Rate1X2PixelsExt = 0x96A7,
	
	Rate2X1PixelsExt = 0x96A8,
	
	Rate2X2PixelsExt = 0x96A9,
	
	Rate1X4PixelsExt = 0x96AA,
	
	Rate4X1PixelsExt = 0x96AB,
	
	Rate4X2PixelsExt = 0x96AC,
	
	Rate2X4PixelsExt = 0x96AD,
	
	Rate4X4PixelsExt = 0x96AE
}