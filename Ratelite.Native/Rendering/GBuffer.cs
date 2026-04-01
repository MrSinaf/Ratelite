using Ratelite.Bindings;

namespace Ratelite.Rendering;

public class GBuffer<T> : IDisposable where T : unmanaged
{
	public readonly uint handle;
	public readonly uint sizeInBytes;
	public BufferType type => (BufferType)target;
	
	private readonly BufferTargetARB target;
	
	public bool isDisposed { get; protected set; }
	
	public static unsafe GBuffer<T> Create(BufferType type, T[] data, bool dynamic = false)
	{
		fixed (void* ptr = data)
		{
			return new GBuffer<T>(type, (uint)(data.Length * sizeof(T)), ptr, dynamic);
		}
	}
	
	public unsafe GBuffer(BufferType type, uint sizeInBytes, void* data, bool dynamic)
	{
		this.sizeInBytes = sizeInBytes;
		target = (BufferTargetARB)type;
		handle = GL.GenBuffer();
		
		Bind();
		GL.BufferData(
			target,
			sizeInBytes,
			data,
			dynamic ? BufferUsageARB.DynamicDraw : BufferUsageARB.StaticDraw
		);
	}
	
	public unsafe void Set(uint offsetInBytes, T[] data)
	{
		var dataSize = (uint)(data.Length * sizeof(T));
		if (dataSize > sizeInBytes - offsetInBytes)
			throw new ArgumentException("Data is too big for the buffer (ㆆ_ㆆ)");
		
		Bind();
		fixed (void* ptr = data)
		{
			GL.BufferSubData(target, (nint)offsetInBytes, (uint)(data.Length * sizeof(T)), ptr);
		}
	}
	
	public unsafe void Set(uint offsetInBytes, void* data, uint dataSizeInBytes)
	{
		if (dataSizeInBytes > sizeInBytes - offsetInBytes)
			throw new ArgumentException("Data is too big for the buffer (ㆆ_ㆆ)");
		
		Bind();
		GL.BufferSubData(target, (nint)offsetInBytes, dataSizeInBytes, data);
	}
	
	public void Bind()
	{
		GL.BindBuffer(target, handle);
	}
	
	public void Dispose()
	{
		if (isDisposed)
			return;
		
		GL.DeleteBuffer(handle);
		isDisposed = true;
		
		GC.SuppressFinalize(this);
	}
}