namespace Ratelite.Bindings;

internal static class Hints
{
	// ReSharper disable UnusedMember.Local
	public const int FOCUSED = 0x00020001;
	public const int RESIZABLE = 0x00020003;
	public const int VISIBLE = 0x00020004;
	public const int DECORATED = 0x00020005;
	public const int AUTO_ICONIFY = 0x00020006;
	public const int FLOATING = 0x00020007;
	public const int MAXIMIZED = 0x00020008;
	public const int CENTER_CURSOR = 0x00020009;
	public const int TRANSPARENT_FRAMEBUFFER = 0x0002000A;
	public const int HOVERED = 0x0002000B;
	public const int FOCUS_ON_SHOW = 0x0002000C;
	public const int RED_BITS = 0x00021001;
	public const int GREEN_BITS = 0x00021002;
	public const int BLUE_BITS = 0x00021003;
	public const int ALPHA_BITS = 0x00021004;
	public const int DEPTH_BITS = 0x00021005;
	public const int STENCIL_BITS = 0x00021006;
	public const int ACCUM_RED_BITS = 0x00021007;
	public const int ACCUM_GREEN_BITS = 0x00021008;
	public const int ACCUM_BLUE_BITS = 0x00021009;
	public const int ACCUM_ALPHA_BITS = 0x0002100A;
	public const int AUX_BUFFERS = 0x0002100B;
	public const int STEREO = 0x0002100C;
	public const int SAMPLES = 0x0002100D;
	public const int SRGB_CAPABLE = 0x0002100E;
	public const int REFRESH_RATE = 0x0002100F;
	public const int DOUBLEBUFFER = 0x00021010;
	public const int CLIENT_API = 0x00022001;
	public const int CONTEXT_VERSION_MAJOR = 0x00022002;
	public const int CONTEXT_VERSION_MINOR = 0x00022003;
	public const int CONTEXT_REVISION = 0x00022004;
	public const int CONTEXT_ROBUSTNESS = 0x00022005;
	public const int OPENGL_FORWARD_COMPAT = 0x00022006;
	public const int OPENGL_DEBUG_CONTEXT = 0x00022007;
	public const int OPENGL_PROFILE = 0x00022008;
	public const int CONTEXT_RELEASE_BEHAVIOR = 0x00022009;
	public const int SCALE_TO_MONITOR = 0x0002200C;

	
	public const int CURSOR = 0x00033001;
	public const int STICKY_KEYS = 0x00033002;
	public const int STICKY_MOUSE_BUTTONS = 0x00033003;
	public const int LOCK_KEY_MODS = 0x00033004;
	public const int RAW_MOUSE_MOTION = 0x00033005;
	
	public const int CURSOR_NORMAL = 0x00034001;
	public const int CURSOR_HIDDEN = 0x00034002;
	public const int CURSOR_DISABLED = 0x00034003;
	
	public const int TRUE = 1;
	public const int FALSE = 0;
	
	public const int ARROW_CURSOR = 0x00036001;
	public const int IBEAM_CURSOR = 0x00036002;
	public const int CROSSHAIR_CURSOR = 0x00036003;
	public const int POINTING_HAND_CURSOR = 0x00036004;
	public const int RESIZE_EW_CURSOR = 0x00036005;
	public const int RESIZE_NS_CURSOR = 0x00036006;
	public const int RESIZE_NWSE_CURSOR = 0x00036007;
	public const int RESIZE_NESW_CURSOR = 0x00036008;
	public const int RESIZE_ALL_CURSOR = 0x00036009;
	public const int NOT_ALLOWED_CURSOR = 0x0003600A;
}