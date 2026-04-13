using System.Runtime.InteropServices;

namespace Ratelite.Sounds.Bindings.StbVorbis.Hebron.Runtime;

internal static unsafe class CRuntime
{
	public delegate int QSortComparer(void* a, void* b);
	
	public const long DBL_EXP_MASK = 0x7ff0000000000000L;
	public const int DBL_MANT_BITS = 52;
	public const long DBL_SGN_MASK = -1 - 0x7fffffffffffffffL;
	public const long DBL_MANT_MASK = 0x000fffffffffffffL;
	public const long DBL_EXP_CLR_MASK = DBL_SGN_MASK | DBL_MANT_MASK;
	
	public static void* Malloc(ulong size)
		=> Malloc((long)size);
	
	public static void* Malloc(long size)
	{
		var ptr = Marshal.AllocHGlobal((int)size);
		MemoryStats.Allocated();
		
		return ptr.ToPointer();
	}
	
	public static void MemCopy(void* a, void* b, long size)
	{
		var ap = (byte*)a;
		var bp = (byte*)b;
		for (long i = 0; i < size; ++i) *ap++ = *bp++;
	}
	
	public static void MemCopy(void* a, void* b, ulong size)
	{
		MemCopy(a, b, (long)size);
	}
	
	public static void MemMove(void* a, void* b, long size)
	{
		void* temp = null;
		
		try
		{
			temp = Malloc(size);
			MemCopy(temp, b, size);
			MemCopy(a, temp, size);
		}
		
		finally
		{
			if (temp != null) Free(temp);
		}
	}
	
	public static void MemMove(void* a, void* b, ulong size)
	{
		MemMove(a, b, (long)size);
	}
	
	public static int MemCompare(void* a, void* b, long size)
	{
		var result = 0;
		var ap = (byte*)a;
		var bp = (byte*)b;
		for (long i = 0; i < size; ++i)
		{
			if (*ap != *bp) result += 1;
			
			ap++;
			bp++;
		}
		
		return result;
	}
	
	public static int MemCompare(void* a, void* b, ulong size)
		=> MemCompare(a, b, (long)size);
	
	public static int MemCompare(byte* a, byte[] b, ulong size)
	{
		fixed (void* bptr = b)
		{
			return MemCompare(a, bptr, (long)size);
		}
	}
	
	public static void Free(void* a)
	{
		if (a == null) return;
		
		var ptr = new IntPtr(a);
		Marshal.FreeHGlobal(ptr);
		MemoryStats.Freed();
	}
	
	public static void MemSet(void* ptr, int value, long size)
	{
		var bptr = (byte*)ptr;
		var bval = (byte)value;
		for (long i = 0; i < size; ++i) *bptr++ = bval;
	}
	
	public static void MemSet(void* ptr, int value, ulong size)
	{
		MemSet(ptr, value, (long)size);
	}
	
	public static uint _lrotl(uint x, int y)
		=> (x << y) | (x >> (32 - y));
	
	public static void* Realloc(void* a, long newSize)
	{
		if (a == null) return Malloc(newSize);
		
		var ptr = new IntPtr(a);
		var result = Marshal.ReAllocHGlobal(ptr, new IntPtr(newSize));
		
		return result.ToPointer();
	}
	
	public static void* Realloc(void* a, ulong newSize)
		=> Realloc(a, (long)newSize);
	
	public static int Abs(int v)
		=> Math.Abs(v);
	
	/// <summary>
	///     This code had been borrowed from here: https://github.com/MachineCognitis/C.math.NET
	/// </summary>
	/// <param name="number"></param>
	/// <param name="exponent"></param>
	/// <returns></returns>
	public static double CalculateExponent(double number, int* exponent)
	{
		var bits = BitConverter.DoubleToInt64Bits(number);
		var exp = (int)((bits & DBL_EXP_MASK) >> DBL_MANT_BITS);
		*exponent = 0;
		
		if (exp == 0x7ff || number == 0D)
		{
			number += number;
		}
		else
		{
			// Not zero and finite.
			*exponent = exp - 1022;
			if (exp == 0)
			{
				// Subnormal, scale number so that it is in [1, 2).
				number *= BitConverter.Int64BitsToDouble(0x4350000000000000L); // 2^54
				bits = BitConverter.DoubleToInt64Bits(number);
				exp = (int)((bits & DBL_EXP_MASK) >> DBL_MANT_BITS);
				*exponent = exp - 1022 - 54;
			}
			
			// Set exponent to -1 so that number is in [0.5, 1).
			number = BitConverter.Int64BitsToDouble(
				(bits & DBL_EXP_CLR_MASK) | 0x3fe0000000000000L
			);
		}
		
		return number;
	}
	
	public static double Pow(double a, double b)
		=> Math.Pow(a, b);
	
	public static float Fabs(double a)
		=> (float)Math.Abs(a);
	
	public static double Ceil(double a)
		=> Math.Ceiling(a);
	
	
	public static double Floor(double a)
		=> Math.Floor(a);
	
	public static double Log(double value)
		=> Math.Log(value);
	
	public static double Exp(double value)
		=> Math.Exp(value);
	
	public static double Cos(double value)
		=> Math.Cos(value);
	
	public static double Acos(double value)
		=> Math.Acos(value);
	
	public static double Sin(double value)
		=> Math.Sin(value);
	
	public static double Ldexp(double number, int exponent)
		=> number * Math.Pow(2, exponent);
	
	private static void QsortSwap(byte* data, long size, long pos1, long pos2)
	{
		var a = data + size * pos1;
		var b = data + size * pos2;
		
		for (long k = 0; k < size; ++k)
		{
			var tmp = *a;
			*a = *b;
			*b = tmp;
			
			a++;
			b++;
		}
	}
	
	private static long QsortPartition(
		byte* data,
		long size,
		QSortComparer comparer,
		long left,
		long right
	)
	{
		void* pivot = data + size * left;
		var i = left - 1;
		var j = right + 1;
		for (;;)
		{
			do
			{
				++i;
			} while (comparer(data + size * i, pivot) < 0);
			
			do
			{
				--j;
			} while (comparer(data + size * j, pivot) > 0);
			
			if (i >= j) return j;
			
			QsortSwap(data, size, i, j);
		}
	}
	
	
	private static void QsortInternal(
		byte* data,
		long size,
		QSortComparer comparer,
		long left,
		long right
	)
	{
		if (left < right)
		{
			var p = QsortPartition(data, size, comparer, left, right);
			
			QsortInternal(data, size, comparer, left, p);
			QsortInternal(data, size, comparer, p + 1, right);
		}
	}
	
	public static void Qsort(void* data, ulong count, ulong size, QSortComparer comparer)
	{
		QsortInternal((byte*)data, (long)size, comparer, 0, (long)count - 1);
	}
	
	public static double Sqrt(double val)
		=> Math.Sqrt(val);
	
	public static double Fmod(double x, double y)
		=> x % y;
	
	public static ulong Strlen(sbyte* str)
	{
		var ptr = str;
		
		while (*ptr != '\0') ptr++;
		
		return (ulong)ptr - (ulong)str - 1;
	}
}