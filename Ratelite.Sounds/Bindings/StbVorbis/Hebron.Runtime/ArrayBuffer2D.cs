namespace Ratelite.Sounds.Bindings.StbVorbis.Hebron.Runtime;

internal class ArrayBuffer2D<T>(int capacity1, int capacity2)
{
	public T[] array { get; private set; } = new T[capacity1 * capacity2];
	public int capacity1 { get; private set; } = capacity1;
	public int capacity2 { get; private set; } = capacity2;
	
	public T this[int index1, int index2]
	{
		get => array[index1 * capacity2 + index2];
		set => array[index1 * capacity2 + index2] = value;
	}
	
	public void EnsureSize(int capacity1, int capacity2)
	{
		this.capacity1 = capacity1;
		this.capacity2 = capacity2;
		
		var required = capacity1 * capacity2;
		if (array.Length >= required) return;
		
		var oldData = array;
		
		var newSize = array.Length;
		while (newSize < required) newSize *= 2;
		
		array = new T[newSize];
		Array.Copy(oldData, array, oldData.Length);
	}
}