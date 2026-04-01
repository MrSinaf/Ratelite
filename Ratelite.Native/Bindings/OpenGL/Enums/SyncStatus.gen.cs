#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum SyncStatus : int
{
	AlreadySignaled = 0x911A,
	
	TimeoutExpired = 0x911B,
	
	ConditionSatisfied = 0x911C,
	WaitFailed = 0x911D
}