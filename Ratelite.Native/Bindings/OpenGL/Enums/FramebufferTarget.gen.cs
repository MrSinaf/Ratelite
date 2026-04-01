#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum FramebufferTarget : int
{
	ReadFramebuffer = 0x8CA8,
	
	DrawFramebuffer = 0x8CA9,
	Framebuffer = 0x8D40,
	
	FramebufferOes = 0x8D40
}