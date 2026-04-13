namespace Ratelite.Sounds.Bindings.StbVorbis.Hebron.Runtime;

internal class ArrayBuffer<T>(int capacity)
{
	public T[] array { get; private set; } = new T[capacity];
	
	public int Capacity => array.Length;
	
	public T this[int index] { get => array[index]; set => array[index] = value; }
	
	public T this[ulong index] { get => array[index]; set => array[index] = value; }
	
	public void EnsureSize(int required)
	{
		if (array.Length >= required) return;
		
		var oldData = array;
		
		var newSize = array.Length;
		while (newSize < required) newSize *= 2;
		
		array = new T[newSize];
		
		Array.Copy(oldData, array, oldData.Length);
	}
}