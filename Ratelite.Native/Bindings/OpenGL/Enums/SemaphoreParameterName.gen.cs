#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum SemaphoreParameterName : int
{
	D3D12FenceValueExt = 0x9595,
	
	TimelineSemaphoreValueNV = 0x9595,
	
	SemaphoreTypeNV = 0x95B3,
	
	SemaphoreTypeBinaryNV = 0x95B4,
	
	SemaphoreTypeTimelineNV = 0x95B5
}