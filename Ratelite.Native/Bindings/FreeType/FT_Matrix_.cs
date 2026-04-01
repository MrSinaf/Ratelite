using System.Runtime.InteropServices;

namespace Ratelite.Bindings
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FT_Matrix_
    {
        public CLong xx;
        public CLong xy;
        public CLong yx;
        public CLong yy;
    }
}