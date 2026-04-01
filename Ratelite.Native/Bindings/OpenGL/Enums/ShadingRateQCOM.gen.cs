#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ShadingRateQCOM : int
{
	[Obsolete("Deprecated in favour of \"Rate1X1PixelsQCom\"")]
	ShadingRate1X1PixelsQCom = 0x96A6,
	[Obsolete("Deprecated in favour of \"Rate1X2PixelsQCom\"")]
	ShadingRate1X2PixelsQCom = 0x96A7,
	[Obsolete("Deprecated in favour of \"Rate2X1PixelsQCom\"")]
	ShadingRate2X1PixelsQCom = 0x96A8,
	[Obsolete("Deprecated in favour of \"Rate2X2PixelsQCom\"")]
	ShadingRate2X2PixelsQCom = 0x96A9,
	[Obsolete("Deprecated in favour of \"Rate1X4PixelsQCom\"")]
	ShadingRate1X4PixelsQCom = 0x96AA,
	[Obsolete("Deprecated in favour of \"Rate4X1PixelsQCom\"")]
	ShadingRate4X1PixelsQCom = 0x96AB,
	[Obsolete("Deprecated in favour of \"Rate4X2PixelsQCom\"")]
	ShadingRate4X2PixelsQCom = 0x96AC,
	[Obsolete("Deprecated in favour of \"Rate2X4PixelsQCom\"")]
	ShadingRate2X4PixelsQCom = 0x96AD,
	[Obsolete("Deprecated in favour of \"Rate4X4PixelsQCom\"")]
	ShadingRate4X4PixelsQCom = 0x96AE,
	
	Rate1X1PixelsQCom = 0x96A6,
	
	Rate1X2PixelsQCom = 0x96A7,
	
	Rate2X1PixelsQCom = 0x96A8,
	
	Rate2X2PixelsQCom = 0x96A9,
	
	Rate1X4PixelsQCom = 0x96AA,
	
	Rate4X1PixelsQCom = 0x96AB,
	
	Rate4X2PixelsQCom = 0x96AC,
	
	Rate2X4PixelsQCom = 0x96AD,
	
	Rate4X4PixelsQCom = 0x96AE
}