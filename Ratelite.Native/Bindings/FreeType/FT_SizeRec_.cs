using System.Runtime.InteropServices;

namespace Ratelite.Bindings
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FT_SizeRec_
    {
        public FT_FaceRec_* face;
        public FT_Generic_ generic;
        public FT_Size_Metrics_ metrics;
        public FT_Size_InternalRec_* _internal;
    }
}