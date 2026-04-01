using System.Runtime.InteropServices;
using Ratelite.Bindings;

namespace Ratelite.Platforms;

public static unsafe partial class WindowsUtils
{
	private const int DWMWA_CAPTION_COLOR = 35;
	private const int DWMWA_BORDER_COLOR = 34;
	
	[LibraryImport("glfw3")]
	private static partial IntPtr glfwGetWin32Window(GLFWwindow* window);
	
	[LibraryImport("dwmapi.dll")]
	private static partial void DwmSetWindowAttribute(
		IntPtr hwnd,
		int attr,
		ref int attrValue,
		int attrSize
	);
	
	public static void SetWindowColor(this Window window, Color color)
	{
		if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
		
		var hwnd = glfwGetWin32Window(window.handle);
		SetColorAttribute(hwnd, DWMWA_CAPTION_COLOR, color);
		SetColorAttribute(hwnd, DWMWA_BORDER_COLOR, color);
	}
	
	private static void SetColorAttribute(IntPtr hwnd, int attribute, Color color)
	{
		var bgrColor = color.r | (color.g << 8) | (color.b << 16);
		DwmSetWindowAttribute(hwnd, attribute, ref bgrColor, sizeof(int));
	}
}