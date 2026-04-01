using Ratelite.Bindings;

namespace Ratelite.Rendering;

public class GTexture : IDisposable
{
	/*
	 * TODO > Ajouter différent type de TextureTarget O(∩_∩)O
	 * Pour me simplifier la vie, je n'ai pas ajouter différents types de Texture,
	 * mais c'est naturellement nécessaire !
	 */
	private const TextureTarget TARGET = TextureTarget.Texture2D;
	
	private static uint? currentBound;
	public readonly uint handle = GL.GenTexture();
	
	public bool isDisposed { get; protected set; }
	
	public void SetWrapS(TextureWrap wrap)
	{
		Bind();
		GL.TexParameter(TARGET, GLEnum.TextureWrapS, (int)wrap);
	}
	
	public void SetWrapT(TextureWrap wrap)
	{
		Bind();
		GL.TexParameter(TARGET, GLEnum.TextureWrapT, (int)wrap);
	}
	
	public void SetWrapR(TextureWrap wrap)
	{
		Bind();
		GL.TexParameter(TARGET, GLEnum.TextureWrapR, (int)wrap);
	}
	
	public void SetMinFilter(TextureMin filter)
	{
		Bind();
		GL.TexParameter(TARGET, GLEnum.TextureMinFilter, (int)filter);
	}
	
	public void SetMagFilter(TextureMag filter)
	{
		Bind();
		GL.TexParameter(TARGET, GLEnum.TextureMagFilter, (int)filter);
	}
	
	public void GenerateMipmap()
	{
		Bind();
		GL.GenerateMipmap(TARGET);
	}
	
	public unsafe void SetImage2D(uint width, uint height, byte[] pixels)
	{
		Bind();
		
		fixed (byte* ptr = pixels)
		{
			GL.TexImage2D(
				TARGET,
				0,
				InternalFormat.Rgba8,
				width,
				height,
				0,
				PixelFormat.Rgba,
				PixelType.UnsignedByte,
				ptr
			);
		}
	}
	
	public unsafe void SetImage2D(uint width, uint height, Color[] pixels)
	{
		Bind();
		
		fixed (Color* ptr = pixels)
		{
			GL.TexImage2D(
				TARGET,
				0,
				InternalFormat.Rgba8,
				width,
				height,
				0,
				PixelFormat.Rgba,
				PixelType.UnsignedByte,
				ptr
			);
		}
	}
	
	public unsafe void SetSubImage2D(int x, int y, byte r, byte g, byte b, byte a)
	{
		Bind();
		byte[] pixels = { r, g, b, a };
		
		fixed (byte* ptr = pixels)
		{
			GL.TexSubImage2D(TARGET, 0, x, y, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
		}
	}
	
	public unsafe void SetSubImage2D(int x, int y, Color color)
	{
		Bind();
		GL.TexSubImage2D(TARGET, 0, x, y, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, &color);
	}
	
	public unsafe void SetSubImage2D(int x, int y, uint width, uint height, Color[] colors)
	{
		Bind();
		
		var bytes = new List<byte>();
		
		foreach (var color in colors)
		{
			bytes.Add(color.r);
			bytes.Add(color.g);
			bytes.Add(color.b);
			bytes.Add(color.a);
		}
		
		fixed (byte* ptr = bytes.ToArray())
		{
			GL.TexSubImage2D(
				TARGET,
				0,
				x,
				y,
				width,
				height,
				PixelFormat.Rgba,
				PixelType.UnsignedByte,
				ptr
			);
		}
	}
	
	public void Bind()
	{
		// if (currentBound == handle)
		// 	return;
		
		GL.BindTexture(TextureTarget.Texture2D, handle);
		currentBound = handle;
	}
	
	public void Dispose()
	{
		if (isDisposed)
			return;
		
		// if (currentBound == handle)
		// 	currentBound = null;
		
		GL.DeleteTexture(handle);
		isDisposed = true;
		GC.SuppressFinalize(this);
	}
}