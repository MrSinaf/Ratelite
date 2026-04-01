#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum QueryObjectParameterName : int
{
	[Obsolete("Deprecated in favour of \"Target\"")]
	QueryTarget = 0x82EA,
	[Obsolete("Deprecated in favour of \"Result\"")]
	QueryResult = 0x8866,
	[Obsolete("Deprecated in favour of \"ResultAvailable\"")]
	QueryResultAvailable = 0x8867,
	[Obsolete("Deprecated in favour of \"ResultNoWait\"")]
	QueryResultNoWait = 0x9194,
	
	Target = 0x82EA,
	
	Result = 0x8866,
	
	ResultAvailable = 0x8867,
	
	ResultNoWait = 0x9194
}