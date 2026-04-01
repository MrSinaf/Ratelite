#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum FramebufferStatus : int
{
	[Obsolete("Deprecated in favour of \"Undefined\"")]
	FramebufferUndefined = 0x8219,
	[Obsolete("Deprecated in favour of \"Complete\"")]
	FramebufferComplete = 0x8CD5,
	[Obsolete("Deprecated in favour of \"IncompleteAttachment\"")]
	FramebufferIncompleteAttachment = 0x8CD6,
	[Obsolete("Deprecated in favour of \"IncompleteMissingAttachment\"")]
	FramebufferIncompleteMissingAttachment = 0x8CD7,
	[Obsolete("Deprecated in favour of \"IncompleteDrawBuffer\"")]
	FramebufferIncompleteDrawBuffer = 0x8CDB,
	[Obsolete("Deprecated in favour of \"IncompleteReadBuffer\"")]
	FramebufferIncompleteReadBuffer = 0x8CDC,
	[Obsolete("Deprecated in favour of \"Unsupported\"")]
	FramebufferUnsupported = 0x8CDD,
	[Obsolete("Deprecated in favour of \"IncompleteMultisample\"")]
	FramebufferIncompleteMultisample = 0x8D56,
	[Obsolete("Deprecated in favour of \"IncompleteLayerTargets\"")]
	FramebufferIncompleteLayerTargets = 0x8DA8,
	
	Undefined = 0x8219,
	
	Complete = 0x8CD5,
	
	IncompleteAttachment = 0x8CD6,
	
	IncompleteMissingAttachment = 0x8CD7,
	
	IncompleteDrawBuffer = 0x8CDB,
	
	IncompleteReadBuffer = 0x8CDC,
	
	Unsupported = 0x8CDD,
	
	IncompleteMultisample = 0x8D56,
	
	IncompleteLayerTargets = 0x8DA8
}