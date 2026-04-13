namespace Ratelite.Sounds.Bindings.StbVorbis.Hebron.Runtime;

internal static unsafe class Utility
{
	public static byte* ToBytePointer(this IntPtr ptr)
		=> (byte*)ptr.ToPointer();
}