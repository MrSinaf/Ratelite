using System.Runtime.InteropServices;

namespace Ratelite.Bindings
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FT_StreamDesc_
    {
        public CLong value;
        public void* pointer;
    }
}