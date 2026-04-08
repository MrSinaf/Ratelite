using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ratelite.Bindings
{
	public partial struct GLFWmonitor { }
	
	public partial struct GLFWwindow { }
	
	public partial struct GLFWcursor { }
	
	public partial struct GLFWvidmode
	{
		public int width;
		public int height;
		public int redBits;
		public int greenBits;
		public int blueBits;
		public int refreshRate;
	}
	
	public unsafe partial struct GLFWgammaramp
	{
		public ushort* red;
		public ushort* green;
		public ushort* blue;
		public uint size;
	}
	
	public unsafe partial struct GLFWimage
	{
		public int width;
		public int height;
		public byte* pixels;
	}
	
	public partial struct GLFWgamepadstate
	{
		public _buttons_e__FixedBuffer buttons;
		public _axes_e__FixedBuffer axes;
		
		[InlineArray(15)]
		public partial struct _buttons_e__FixedBuffer
		{
			public byte e0;
		}
		
		[InlineArray(6)]
		public partial struct _axes_e__FixedBuffer
		{
			public float e0;
		}
	}
	
	public unsafe partial struct GLFWallocator
	{
		public delegate* unmanaged[Cdecl]<nuint, void*, void*> allocate;
		public delegate* unmanaged[Cdecl]<void*, nuint, void*, void*> reallocate;
		public delegate* unmanaged[Cdecl]<void*, void*, void> deallocate;
		public void* user;
	}
	
	public static unsafe partial class GlfwNative
	{
		[LibraryImport("glfw3")]
		public static partial int glfwInit();
		
		[LibraryImport("glfw3")]
		public static partial void glfwTerminate();
		
		[LibraryImport("glfw3")]
		public static partial void glfwInitHint(int hint, int value);
		
		[LibraryImport("glfw3")]
		public static partial void glfwInitAllocator(GLFWallocator* allocator);
		
		[LibraryImport("glfw3")]
		public static partial void glfwGetVersion(int* major, int* minor, int* rev);
		
		[LibraryImport("glfw3")]
		public static partial sbyte* glfwGetVersionString();
		
		[LibraryImport("glfw3")]
		public static partial int glfwGetError(sbyte** description);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<int, sbyte*, void>
				glfwSetErrorCallback(delegate* unmanaged[Cdecl]<int, sbyte*, void> callback);
		
		[LibraryImport("glfw3")]
		public static partial int glfwGetPlatform();
		
		[LibraryImport("glfw3")]
		public static partial int glfwPlatformSupported(int platform);
		
		[LibraryImport("glfw3")]
		public static partial GLFWmonitor** glfwGetMonitors(int* count);
		
		[LibraryImport("glfw3")]
		public static partial GLFWmonitor* glfwGetPrimaryMonitor();
		
		[LibraryImport("glfw3")]
		public static partial void glfwGetMonitorPos(GLFWmonitor* monitor, int* xpos, int* ypos);
		
		[LibraryImport("glfw3")]
		public static partial void glfwGetMonitorWorkarea(
			GLFWmonitor* monitor,
			int* xpos,
			int* ypos,
			int* width,
			int* height
		);
		
		[LibraryImport("glfw3")]
		public static partial void glfwGetMonitorPhysicalSize(
			GLFWmonitor* monitor,
			int* widthMM,
			int* heightMM
		);
		
		[LibraryImport("glfw3")]
		public static partial void glfwGetMonitorContentScale(
			GLFWmonitor* monitor,
			float* xscale,
			float* yscale
		);
		
		[LibraryImport("glfw3")]
		public static partial sbyte* glfwGetMonitorName(GLFWmonitor* monitor);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetMonitorUserPointer(GLFWmonitor* monitor, void* pointer);
		
		[LibraryImport("glfw3")]
		public static partial void* glfwGetMonitorUserPointer(GLFWmonitor* monitor);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWmonitor*, int, void>
				glfwSetMonitorCallback(
					delegate* unmanaged[Cdecl]<GLFWmonitor*, int, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial GLFWvidmode* glfwGetVideoModes(GLFWmonitor* monitor, int* count);
		
		[LibraryImport("glfw3")]
		public static partial GLFWvidmode* glfwGetVideoMode(GLFWmonitor* monitor);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetGamma(GLFWmonitor* monitor, float gamma);
		
		[LibraryImport("glfw3")]
		public static partial GLFWgammaramp* glfwGetGammaRamp(GLFWmonitor* monitor);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetGammaRamp(GLFWmonitor* monitor, GLFWgammaramp* ramp);
		
		[LibraryImport("glfw3")]
		public static partial void glfwDefaultWindowHints();
		
		[LibraryImport("glfw3")]
		public static partial void glfwWindowHint(int hint, int value);
		
		[LibraryImport("glfw3")]
		public static partial void glfwWindowHintString(int hint, sbyte* value);
		
		[LibraryImport("glfw3")]
		public static partial GLFWwindow* glfwCreateWindow(
			int width,
			int height,
			sbyte* title,
			GLFWmonitor* monitor,
			GLFWwindow* share
		);
		
		[LibraryImport("glfw3")]
		public static partial void glfwDestroyWindow(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial int glfwWindowShouldClose(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetWindowShouldClose(GLFWwindow* window, int value);
		
		[LibraryImport("glfw3")]
		public static partial sbyte* glfwGetWindowTitle(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetWindowTitle(GLFWwindow* window, sbyte* title);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetWindowIcon(
			GLFWwindow* window,
			int count,
			GLFWimage* images
		);
		
		[LibraryImport("glfw3")]
		public static partial void glfwGetWindowPos(GLFWwindow* window, int* xpos, int* ypos);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetWindowPos(GLFWwindow* window, int xpos, int ypos);
		
		[LibraryImport("glfw3")]
		public static partial void glfwGetWindowSize(GLFWwindow* window, int* width, int* height);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetWindowSizeLimits(
			GLFWwindow* window,
			int minwidth,
			int minheight,
			int maxwidth,
			int maxheight
		);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetWindowAspectRatio(
			GLFWwindow* window,
			int numer,
			int denom
		);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetWindowSize(GLFWwindow* window, int width, int height);
		
		[LibraryImport("glfw3")]
		public static partial void glfwGetFramebufferSize(
			GLFWwindow* window,
			int* width,
			int* height
		);
		
		[LibraryImport("glfw3")]
		public static partial void glfwGetWindowFrameSize(
			GLFWwindow* window,
			int* left,
			int* top,
			int* right,
			int* bottom
		);
		
		[LibraryImport("glfw3")]
		public static partial void glfwGetWindowContentScale(
			GLFWwindow* window,
			float* xscale,
			float* yscale
		);
		
		[LibraryImport("glfw3")]
		public static partial float glfwGetWindowOpacity(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetWindowOpacity(GLFWwindow* window, float opacity);
		
		[LibraryImport("glfw3")]
		public static partial void glfwIconifyWindow(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial void glfwRestoreWindow(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial void glfwMaximizeWindow(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial void glfwShowWindow(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial void glfwHideWindow(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial void glfwFocusWindow(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial void glfwRequestWindowAttention(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial GLFWmonitor* glfwGetWindowMonitor(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetWindowMonitor(
			GLFWwindow* window,
			GLFWmonitor* monitor,
			int xpos,
			int ypos,
			int width,
			int height,
			int refreshRate
		);
		
		[LibraryImport("glfw3")]
		public static partial int glfwGetWindowAttrib(GLFWwindow* window, int attrib);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetWindowAttrib(GLFWwindow* window, int attrib, int value);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetWindowUserPointer(GLFWwindow* window, void* pointer);
		
		[LibraryImport("glfw3")]
		public static partial void* glfwGetWindowUserPointer(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, void>
				glfwSetWindowPosCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, void>
				glfwSetWindowSizeCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, void>
				glfwSetWindowCloseCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, void>
				glfwSetWindowRefreshCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, int, void>
				glfwSetWindowFocusCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, int, void>
				glfwSetWindowIconifyCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, int, void>
				glfwSetWindowMaximizeCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, void>
				glfwSetFramebufferSizeCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, float, float, void>
				glfwSetWindowContentScaleCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, float, float, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial void glfwPollEvents();
		
		[LibraryImport("glfw3")]
		public static partial void glfwWaitEvents();
		
		[LibraryImport("glfw3")]
		public static partial void glfwWaitEventsTimeout(double timeout);
		
		[LibraryImport("glfw3")]
		public static partial void glfwPostEmptyEvent();
		
		[LibraryImport("glfw3")]
		public static partial int glfwGetInputMode(GLFWwindow* window, int mode);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetInputMode(GLFWwindow* window, int mode, int value);
		
		[LibraryImport("glfw3")]
		public static partial int glfwRawMouseMotionSupported();
		
		[LibraryImport("glfw3")]
		public static partial sbyte* glfwGetKeyName(int key, int scancode);
		
		[LibraryImport("glfw3")]
		public static partial int glfwGetKeyScancode(int key);
		
		[LibraryImport("glfw3")]
		public static partial int glfwGetKey(GLFWwindow* window, int key);
		
		[LibraryImport("glfw3")]
		public static partial int glfwGetMouseButton(GLFWwindow* window, int button);
		
		[LibraryImport("glfw3")]
		public static partial void glfwGetCursorPos(GLFWwindow* window, double* xpos, double* ypos);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetCursorPos(GLFWwindow* window, double xpos, double ypos);
		
		[LibraryImport("glfw3")]
		public static partial GLFWcursor* glfwCreateCursor(
			GLFWimage* image,
			int xhot,
			int yhot
		);
		
		[LibraryImport("glfw3")]
		public static partial GLFWcursor* glfwCreateStandardCursor(int shape);
		
		[LibraryImport("glfw3")]
		public static partial void glfwDestroyCursor(GLFWcursor* cursor);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetCursor(GLFWwindow* window, GLFWcursor* cursor);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, int, int, void>
				glfwSetKeyCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, int, int, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, uint, void>
				glfwSetCharCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, uint, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, uint, int, void>
				glfwSetCharModsCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, uint, int, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, int, void>
				glfwSetMouseButtonCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, int, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, double, double, void>
				glfwSetCursorPosCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, double, double, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, int, void>
				glfwSetCursorEnterCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, double, double, void>
				glfwSetScrollCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, double, double, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<GLFWwindow*, int, sbyte**, void>
				glfwSetDropCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, sbyte**, void> callback
				);
		
		[LibraryImport("glfw3")]
		public static partial int glfwJoystickPresent(int jid);
		
		[LibraryImport("glfw3")]
		public static partial float* glfwGetJoystickAxes(int jid, int* count);
		
		[LibraryImport("glfw3")]
		public static partial byte* glfwGetJoystickButtons(int jid, int* count);
		
		[LibraryImport("glfw3")]
		public static partial byte* glfwGetJoystickHats(int jid, int* count);
		
		[LibraryImport("glfw3")]
		public static partial sbyte* glfwGetJoystickName(int jid);
		
		[LibraryImport("glfw3")]
		public static partial sbyte* glfwGetJoystickGUID(int jid);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetJoystickUserPointer(int jid, void* pointer);
		
		[LibraryImport("glfw3")]
		public static partial void* glfwGetJoystickUserPointer(int jid);
		
		[LibraryImport("glfw3")]
		public static partial int glfwJoystickIsGamepad(int jid);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<int, int, void> glfwSetJoystickCallback(
			delegate* unmanaged[Cdecl]<int, int, void> callback
		);
		
		[LibraryImport("glfw3")]
		public static partial int glfwUpdateGamepadMappings(
			sbyte* @string
		);
		
		[LibraryImport("glfw3")]
		public static partial sbyte* glfwGetGamepadName(int jid);
		
		[LibraryImport("glfw3")]
		public static partial int glfwGetGamepadState(int jid, GLFWgamepadstate* state);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetClipboardString(
			GLFWwindow* window,
			sbyte* @string
		);
		
		[LibraryImport("glfw3")]
		public static partial sbyte* glfwGetClipboardString(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial double glfwGetTime();
		
		[LibraryImport("glfw3")]
		public static partial void glfwSetTime(double time);
		
		[LibraryImport("glfw3")]
		public static partial ulong glfwGetTimerValue();
		
		[LibraryImport("glfw3")]
		public static partial ulong glfwGetTimerFrequency();
		
		[LibraryImport("glfw3")]
		public static partial void glfwMakeContextCurrent(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial GLFWwindow* glfwGetCurrentContext();
		
		[LibraryImport("glfw3")]
		public static partial void glfwSwapBuffers(GLFWwindow* window);
		
		[LibraryImport("glfw3")]
		public static partial void glfwSwapInterval(int interval);
		
		[LibraryImport("glfw3")]
		public static partial int glfwExtensionSupported(sbyte* extension);
		
		[LibraryImport("glfw3")]
		public static partial delegate* unmanaged[Cdecl]<void> glfwGetProcAddress(
			sbyte* procname
		);
		
		[LibraryImport("glfw3")]
		public static partial int glfwVulkanSupported();
		
		[LibraryImport("glfw3")]
		public static partial sbyte** glfwGetRequiredInstanceExtensions(uint* count);
	}
}