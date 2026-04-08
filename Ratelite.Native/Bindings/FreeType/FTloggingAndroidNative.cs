using System.Runtime.InteropServices;

namespace Ratelite.Bindings
{
    internal static unsafe partial class FTloggingAndroidNative
    {
        [LibraryImport("libfreetype.so")]
        public static partial void FT_Trace_Set_Level(byte* tracing_level);
        [LibraryImport("libfreetype.so")]
        public static partial void FT_Trace_Set_Default_Level();
        [LibraryImport("libfreetype.so")]
        public static partial void FT_Set_Log_Handler(delegate* unmanaged<byte*, byte*, byte*, void> * handler);
        [LibraryImport("libfreetype.so")]
        public static partial void FT_Set_Default_Log_Handler();
    }
}