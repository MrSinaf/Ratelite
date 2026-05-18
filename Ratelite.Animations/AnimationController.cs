namespace Ratelite.Animations;

public class AnimationController<T> : IAsset
{
	private readonly Dictionary<string, AnimationBlock<T>> animationBlocks = [];
	private readonly Dictionary<string, ConditionBlock<T>> conditionBlocks = [];
	public IBlock? firstBlock;
	
	public void AddBlock(
		string name,
		Animation<T> animation,
		Action<Animator<T>, T>? onUpdate =
				null
	)
	{
		var newBlock = new AnimationBlock<T>(name, animation, onUpdate ?? delegate { });
		animationBlocks[name] = newBlock;
		firstBlock ??= newBlock;
	}
	
	
	public void AddBlock(string name, Action<Animator<T>, T> onUpdate)
	{
		var newBlock = new ConditionBlock<T>(name, onUpdate);
		conditionBlocks[name] = newBlock;
		firstBlock ??= newBlock;
	}
	
	public IBlock? GetAnimationBlock(string name)
		=> (IBlock?)animationBlocks.GetValueOrDefault(name) ??
		   conditionBlocks.GetValueOrDefault(name);
	
	public void Destroy() => animationBlocks.Clear();
}

public interface IBlock
{
	public string name { get; }
}

public class AnimationBlock<T>(string name, Animation<T> animation, Action<Animator<T>, T> onUpdate)
		: IBlock
{
	public string name { get; } = name;
	public readonly Animation<T> animation = animation;
	
	public readonly Action<Animator<T>, T> onUpdate = onUpdate;
}

public class ConditionBlock<T>(string name, Action<Animator<T>, T> onUpdate) : IBlock
{
	public string name { get; } = name;
	public readonly Action<Animator<T>, T> onUpdate = onUpdate;
}