#pragma warning disable 1591

namespace Ratelite.Bindings;

[Flags]
public enum SubgroupSupportedFeatures : int
{
	None = 0,
	[Obsolete("Deprecated in favour of \"BasicBitKhr\"")]
	SubgroupFeatureBasicBitKhr = 0x1,
	[Obsolete("Deprecated in favour of \"VoteBitKhr\"")]
	SubgroupFeatureVoteBitKhr = 0x2,
	[Obsolete("Deprecated in favour of \"ArithmeticBitKhr\"")]
	SubgroupFeatureArithmeticBitKhr = 0x4,
	[Obsolete("Deprecated in favour of \"BallotBitKhr\"")]
	SubgroupFeatureBallotBitKhr = 0x8,
	[Obsolete("Deprecated in favour of \"ShuffleBitKhr\"")]
	SubgroupFeatureShuffleBitKhr = 0x10,
	[Obsolete("Deprecated in favour of \"ShuffleRelativeBitKhr\"")]
	SubgroupFeatureShuffleRelativeBitKhr = 0x20,
	[Obsolete("Deprecated in favour of \"ClusteredBitKhr\"")]
	SubgroupFeatureClusteredBitKhr = 0x40,
	[Obsolete("Deprecated in favour of \"QuadBitKhr\"")]
	SubgroupFeatureQuadBitKhr = 0x80,
	[Obsolete("Deprecated in favour of \"PartitionedBitNV\"")]
	SubgroupFeaturePartitionedBitNV = 0x100,
	
	BasicBitKhr = 0x1,
	
	VoteBitKhr = 0x2,
	
	ArithmeticBitKhr = 0x4,
	
	BallotBitKhr = 0x8,
	
	ShuffleBitKhr = 0x10,
	
	ShuffleRelativeBitKhr = 0x20,
	
	ClusteredBitKhr = 0x40,
	
	QuadBitKhr = 0x80,
	
	PartitionedBitNV = 0x100
}