using Ratelite.Bindings;
using Ratelite.Rendering;

namespace Ratelite.GO;

public class CameraUniform
{
	private readonly GBuffer<float> buffer;
	
	public float deltaTime;
	public float time;
	public Vector2 resolution;
	public Matrix3X3 projection;
	
	public CameraUniform()
	{
		buffer = GBuffer<float>.Create(BufferType.UniformBuffer, ToArray(), true);
	}
	
	public void UpdateBuffer()
	{
		GL.BindBufferBase(BufferTargetARB.UniformBuffer, 0, buffer.handle);
		buffer.Set(0, ToArray());
	}
	
	private float[] ToArray() =>
	[
		time, deltaTime, resolution.x, resolution.y,
		projection.m11, projection.m12, projection.m13, 0,
		projection.m21, projection.m22, projection.m23, 0,
		projection.m31, projection.m32, projection.m33, 0,
	];
}