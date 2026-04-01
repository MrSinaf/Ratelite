#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum QueryTarget : int
{
	TransformFeedbackOverflow = 0x82EC,
	
	VerticesSubmitted = 0x82EE,
	
	PrimitivesSubmitted = 0x82EF,
	
	VertexShaderInvocations = 0x82F0,
	
	TimeElapsed = 0x88BF,
	
	SamplesPassed = 0x8914,
	
	AnySamplesPassed = 0x8C2F,
	
	PrimitivesGenerated = 0x8C87,
	
	TransformFeedbackPrimitivesWritten = 0x8C88,
	
	AnySamplesPassedConservative = 0x8D6A,
	
	TaskShaderInvocationsExt = 0x9753,
	
	MeshShaderInvocationsExt = 0x9754,
	
	MeshPrimitivesGeneratedExt = 0x9755
}