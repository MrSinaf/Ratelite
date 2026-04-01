#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum PixelStoreResampleMode : int
{
	[Obsolete("Deprecated in favour of \"DecimateSgix\"")]
	ResampleDecimateSgix = 0x8430,
	[Obsolete("Deprecated in favour of \"ReplicateSgix\"")]
	ResampleReplicateSgix = 0x8433,
	[Obsolete("Deprecated in favour of \"ZeroFillSgix\"")]
	ResampleZeroFillSgix = 0x8434,
	
	DecimateSgix = 0x8430,
	
	ReplicateSgix = 0x8433,
	
	ZeroFillSgix = 0x8434
}