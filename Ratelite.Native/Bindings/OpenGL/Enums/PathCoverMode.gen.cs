#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum PathCoverMode : int
{
	PathFillCoverModeNV = 0x9082,
	
	ConvexHullNV = 0x908B,
	
	BoundingBoxNV = 0x908D
}