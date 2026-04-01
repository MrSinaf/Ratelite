using System.Runtime.InteropServices;

namespace Ratelite.Bindings;

internal static unsafe partial class StbImage
{
	public static string gFailurReason = string.Empty;
	public static readonly char[] parsePngFileInvalidChunk = new char[25];
	
	public static int nativeAllocations => MemoryStats.Allocations;
	
	private static int Error(string str)
	{
		gFailurReason = str;
		return 0;
	}
	
	public static byte Get8(StbiContext s)
	{
		var b = s.stream.ReadByte();
		if (b == -1) return 0;
		
		return (byte)b;
	}
	
	public static void Skip(StbiContext s, int skip)
	{
		s.stream.Seek(skip, SeekOrigin.Current);
	}
	
	public static void Rewind(StbiContext s)
	{
		s.stream.Seek(0, SeekOrigin.Begin);
	}
	
	public static int AtEndOfStream(StbiContext s)
		=> s.stream.Position == s.stream.Length ? 1 : 0;
	
	public static int GetN(StbiContext s, byte* buf, int size)
	{
		if (s.tempBuffer == null ||
			s.tempBuffer.Length < size)
			s.tempBuffer = new byte[size * 2];
		
		var result = s.stream.Read(s.tempBuffer, 0, size);
		Marshal.Copy(s.tempBuffer, 0, new IntPtr(buf), result);
		
		return result;
	}
	
	public class StbiContext(Stream stream)
	{
		public readonly Stream stream = stream ?? throw new ArgumentNullException(nameof(stream));
		
		public byte[]? tempBuffer;
		public int imgN = 0;
		public int imgOutR = 0;
		public uint imgX = 0;
		public uint imgY = 0;
	}
}