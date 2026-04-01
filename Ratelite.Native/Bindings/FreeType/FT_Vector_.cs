using System.Runtime.InteropServices;

namespace Ratelite.Bindings
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FT_Vector_
    {
        public CLong x;
        public CLong y;
    }
}