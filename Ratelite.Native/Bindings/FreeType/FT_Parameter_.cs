using System.Runtime.InteropServices;

namespace Ratelite.Bindings
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FT_Parameter_
    {
        public CULong tag;
        public void* data;
    }
}