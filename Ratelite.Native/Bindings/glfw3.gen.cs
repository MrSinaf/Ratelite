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
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwInit();
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwTerminate();
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwInitHint(int hint, int value);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwInitAllocator(GLFWallocator* allocator);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwGetVersion(int* major, int* minor, int* rev);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern sbyte* glfwGetVersionString();
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwGetError(sbyte** description);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<int, sbyte*, void>
				glfwSetErrorCallback(delegate* unmanaged[Cdecl]<int, sbyte*, void> callback);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwGetPlatform();
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwPlatformSupported(int platform);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern GLFWmonitor** glfwGetMonitors(int* count);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern GLFWmonitor* glfwGetPrimaryMonitor();
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwGetMonitorPos(GLFWmonitor* monitor, int* xpos, int* ypos);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwGetMonitorWorkarea(
			GLFWmonitor* monitor,
			int* xpos,
			int* ypos,
			int* width,
			int* height
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwGetMonitorPhysicalSize(
			GLFWmonitor* monitor,
			int* widthMM,
			int* heightMM
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwGetMonitorContentScale(
			GLFWmonitor* monitor,
			float* xscale,
			float* yscale
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern sbyte* glfwGetMonitorName(GLFWmonitor* monitor);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetMonitorUserPointer(GLFWmonitor* monitor, void* pointer);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void* glfwGetMonitorUserPointer(GLFWmonitor* monitor);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWmonitor*, int, void>
				glfwSetMonitorCallback(
					delegate* unmanaged[Cdecl]<GLFWmonitor*, int, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern GLFWvidmode* glfwGetVideoModes(GLFWmonitor* monitor, int* count);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern GLFWvidmode* glfwGetVideoMode(GLFWmonitor* monitor);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetGamma(GLFWmonitor* monitor, float gamma);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern GLFWgammaramp* glfwGetGammaRamp(GLFWmonitor* monitor);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetGammaRamp(GLFWmonitor* monitor, GLFWgammaramp* ramp);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwDefaultWindowHints();
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwWindowHint(int hint, int value);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwWindowHintString(int hint, sbyte* value);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern GLFWwindow* glfwCreateWindow(
			int width,
			int height,
			sbyte* title,
			GLFWmonitor* monitor,
			GLFWwindow* share
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwDestroyWindow(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwWindowShouldClose(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetWindowShouldClose(GLFWwindow* window, int value);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern sbyte* glfwGetWindowTitle(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetWindowTitle(GLFWwindow* window, sbyte* title);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetWindowIcon(
			GLFWwindow* window,
			int count,
			GLFWimage* images
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwGetWindowPos(GLFWwindow* window, int* xpos, int* ypos);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetWindowPos(GLFWwindow* window, int xpos, int ypos);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwGetWindowSize(GLFWwindow* window, int* width, int* height);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetWindowSizeLimits(
			GLFWwindow* window,
			int minwidth,
			int minheight,
			int maxwidth,
			int maxheight
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetWindowAspectRatio(
			GLFWwindow* window,
			int numer,
			int denom
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetWindowSize(GLFWwindow* window, int width, int height);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwGetFramebufferSize(
			GLFWwindow* window,
			int* width,
			int* height
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwGetWindowFrameSize(
			GLFWwindow* window,
			int* left,
			int* top,
			int* right,
			int* bottom
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwGetWindowContentScale(
			GLFWwindow* window,
			float* xscale,
			float* yscale
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern float glfwGetWindowOpacity(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetWindowOpacity(GLFWwindow* window, float opacity);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwIconifyWindow(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwRestoreWindow(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwMaximizeWindow(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwShowWindow(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwHideWindow(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwFocusWindow(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwRequestWindowAttention(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern GLFWmonitor* glfwGetWindowMonitor(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetWindowMonitor(
			GLFWwindow* window,
			GLFWmonitor* monitor,
			int xpos,
			int ypos,
			int width,
			int height,
			int refreshRate
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwGetWindowAttrib(GLFWwindow* window, int attrib);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetWindowAttrib(GLFWwindow* window, int attrib, int value);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetWindowUserPointer(GLFWwindow* window, void* pointer);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void* glfwGetWindowUserPointer(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, void>
				glfwSetWindowPosCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, void>
				glfwSetWindowSizeCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, void>
				glfwSetWindowCloseCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, void>
				glfwSetWindowRefreshCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, int, void>
				glfwSetWindowFocusCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, int, void>
				glfwSetWindowIconifyCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, int, void>
				glfwSetWindowMaximizeCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, void>
				glfwSetFramebufferSizeCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, float, float, void>
				glfwSetWindowContentScaleCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, float, float, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwPollEvents();
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwWaitEvents();
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwWaitEventsTimeout(double timeout);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwPostEmptyEvent();
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwGetInputMode(GLFWwindow* window, int mode);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetInputMode(GLFWwindow* window, int mode, int value);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwRawMouseMotionSupported();
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern sbyte* glfwGetKeyName(int key, int scancode);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwGetKeyScancode(int key);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwGetKey(GLFWwindow* window, int key);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwGetMouseButton(GLFWwindow* window, int button);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwGetCursorPos(GLFWwindow* window, double* xpos, double* ypos);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetCursorPos(GLFWwindow* window, double xpos, double ypos);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern GLFWcursor* glfwCreateCursor(
			GLFWimage* image,
			int xhot,
			int yhot
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern GLFWcursor* glfwCreateStandardCursor(int shape);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwDestroyCursor(GLFWcursor* cursor);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetCursor(GLFWwindow* window, GLFWcursor* cursor);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, int, int, void>
				glfwSetKeyCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, int, int, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, uint, void>
				glfwSetCharCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, uint, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, uint, int, void>
				glfwSetCharModsCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, uint, int, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, int, void>
				glfwSetMouseButtonCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, int, int, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, double, double, void>
				glfwSetCursorPosCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, double, double, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, int, void>
				glfwSetCursorEnterCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, double, double, void>
				glfwSetScrollCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, double, double, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<GLFWwindow*, int, sbyte**, void>
				glfwSetDropCallback(
					GLFWwindow* window,
					delegate* unmanaged[Cdecl]<GLFWwindow*, int, sbyte**, void> callback
				);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwJoystickPresent(int jid);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern float* glfwGetJoystickAxes(int jid, int* count);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern byte* glfwGetJoystickButtons(int jid, int* count);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern byte* glfwGetJoystickHats(int jid, int* count);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern sbyte* glfwGetJoystickName(int jid);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern sbyte* glfwGetJoystickGUID(int jid);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetJoystickUserPointer(int jid, void* pointer);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void* glfwGetJoystickUserPointer(int jid);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwJoystickIsGamepad(int jid);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<int, int, void> glfwSetJoystickCallback(
			delegate* unmanaged[Cdecl]<int, int, void> callback
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwUpdateGamepadMappings(
			sbyte* @string
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern sbyte* glfwGetGamepadName(int jid);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwGetGamepadState(int jid, GLFWgamepadstate* state);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetClipboardString(
			GLFWwindow* window,
			sbyte* @string
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern sbyte* glfwGetClipboardString(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern double glfwGetTime();
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSetTime(double time);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern ulong glfwGetTimerValue();
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern ulong glfwGetTimerFrequency();
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwMakeContextCurrent(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern GLFWwindow* glfwGetCurrentContext();
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSwapBuffers(GLFWwindow* window);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern void glfwSwapInterval(int interval);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwExtensionSupported(sbyte* extension);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern delegate* unmanaged[Cdecl]<void> glfwGetProcAddress(
			sbyte* procname
		);
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern int glfwVulkanSupported();
		
		[DllImport("glfw3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		public static extern sbyte** glfwGetRequiredInstanceExtensions(uint* count);
	}
}