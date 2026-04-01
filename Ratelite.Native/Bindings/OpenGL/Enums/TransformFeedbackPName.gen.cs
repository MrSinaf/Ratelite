#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum TransformFeedbackPName : int
{
	[Obsolete("Deprecated in favour of \"BufferStart\"")]
	TransformFeedbackBufferStart = 0x8C84,
	[Obsolete("Deprecated in favour of \"BufferSize\"")]
	TransformFeedbackBufferSize = 0x8C85,
	[Obsolete("Deprecated in favour of \"BufferBinding\"")]
	TransformFeedbackBufferBinding = 0x8C8F,
	[Obsolete("Deprecated in favour of \"Paused\"")]
	TransformFeedbackPaused = 0x8E23,
	[Obsolete("Deprecated in favour of \"Active\"")]
	TransformFeedbackActive = 0x8E24,
	
	BufferStart = 0x8C84,
	
	BufferSize = 0x8C85,
	
	BufferBinding = 0x8C8F,
	
	Paused = 0x8E23,
	
	Active = 0x8E24
}