using Ratelite.Bindings;

namespace Ratelite.Rendering;

public class GVertexArrayObject : IDisposable
{
	public readonly uint handle;
	public readonly uint strideInBytes;
	
	public bool isDisposed { get; protected set; }
	
	public GVertexArrayObject(uint strideInBytes)
	{
		this.strideInBytes = strideInBytes;
		handle = GL.GenVertexArray();
		if (handle == 0)
			throw new Exception("Failed to generate (○｀ 3′○) Vertex Array Object (VAO)");
	}
	
	public void VertexAttribPointer(uint index, int size, VertexType type, int offsetInBytes)
	{
		Bind();
		GL.EnableVertexAttribArray(index);
		
		unsafe
		{
			GL.VertexAttribPointer(
				index,
				size,
				(VertexAttribPointerType)type,
				false,
				strideInBytes,
				(void*)offsetInBytes
			);
		}
	}
	
	public void Bind()
	{
		GL.BindVertexArray(handle);
	}
	
	public void Dispose()
	{
		if (isDisposed)
			return;
		
		GL.DeleteVertexArray(handle);
		isDisposed = true;
		
		GC.SuppressFinalize(this);
	}
}