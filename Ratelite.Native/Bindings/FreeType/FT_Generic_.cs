using System.Runtime.InteropServices;

namespace Ratelite.Bindings
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FT_Generic_
    {
        public void* data;
        public delegate* unmanaged<void*, void> * finalizer;
    }
}