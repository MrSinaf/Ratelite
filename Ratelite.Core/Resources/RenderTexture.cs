using Ratelite.Bindings;
using Ratelite.Rendering;

namespace Ratelite.Resources;

public class RenderTexture : Texture
{
	public Vector2Int size { get; private set; }
	public uint width { get; set; }
	public uint height { get; set; }
	public Vector2 texel { get; private set; }
	
	private readonly uint frameBufferHandle;
	private readonly uint rbHandle;
	
	public RenderTexture(uint width, uint height)
	{
		this.width = width;
		this.height = height;
		size = new Vector2Int((int)width, (int)height);
		texel = Vector2.one / size;
		
		gTexture.SetImage2D(this.width, this.height, new Color[width * height]);
		
		frameBufferHandle = GL.GenFramebuffer();
		GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBufferHandle);
		GL.FramebufferTexture2D(
			FramebufferTarget.Framebuffer,
			FramebufferAttachment.ColorAttachment0,
			TextureTarget.Texture2D,
			gTexture.handle,
			0
		);
		
		rbHandle = GL.GenRenderbuffer();
		GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, rbHandle);
		GL.RenderbufferStorage(
			RenderbufferTarget.Renderbuffer,
			InternalFormat.DepthComponent,
			this.width,
			this.height
		);
		GL.FramebufferRenderbuffer(
			FramebufferTarget.Framebuffer,
			FramebufferAttachment.DepthAttachment,
			RenderbufferTarget.Renderbuffer,
			rbHandle
		);
		
		if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferStatus.Complete)
			throw new Exception("Framebuffer incomplete!");
		
		GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
	}
	
	public void Bind()
	{
		GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBufferHandle);
		GL.Viewport(0, 0, width, height);
		GL.ClearColor(Color.transparent);
		GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
	}
	
	public void Unbind()
	{
		GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
	}
	
	public void Dispose()
	{
		GL.DeleteFramebuffer(frameBufferHandle);
		GL.DeleteRenderbuffer(rbHandle);
		gTexture.Dispose();
	}
}