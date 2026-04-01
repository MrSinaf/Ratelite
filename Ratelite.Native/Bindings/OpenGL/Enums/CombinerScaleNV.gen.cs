#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum CombinerScaleNV : int
{
	None = 0x0,
	
	ScaleByTwoNV = 0x853E,
	
	ScaleByFourNV = 0x853F,
	
	ScaleByOneHalfNV = 0x8540
}