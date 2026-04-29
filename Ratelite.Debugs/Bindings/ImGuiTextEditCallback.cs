using System.Runtime.InteropServices;

namespace Ratelite.Debugs;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate int ImGuiInputTextCallback(ImGuiInputTextCallbackData* data);