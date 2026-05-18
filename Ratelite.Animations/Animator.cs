namespace Ratelite.Animations;

public class Animator<T> : IUpdatableComponent
{
	public int priority => 0;
	public bool enable { get; set; }
	
	public AnimationController<T> controller { get; private set; }
	public IBlock? block { get; private set; }
	
	public float currentTime { get; private set; }
	public bool isRunning { get; private set; }
	public T obj { get; private set; }
	
	public void Play()
	{
		currentTime = 0;
		isRunning = true;
	}
	
	public void Resume() => isRunning = true;
	
	public void Pause() => isRunning = false;
	
	public void Stop()
	{
		currentTime = 0;
		isRunning = false;
	}
	
	public Animator<T> SetController(T obj, AnimationController<T> controller, bool autoPlay = true)
	{
		this.obj = obj;
		this.controller = controller;
		block = controller.firstBlock;
		
		if (autoPlay)
			Play();
		
		return this;
	}
	
	public void SetBlock(string name)
	{
		Stop();
		block = controller.GetAnimationBlock(name);
		Play();
	}
	
	public void Update()
	{
		if (!isRunning)
			return;
		
		switch (block)
		{
			case AnimationBlock<T> animationBlock:
			{
				currentTime += Time.delta;
				animationBlock.animation.Sample(obj, currentTime);
				
				if (currentTime > animationBlock.animation.duration)
				{
					if (animationBlock.animation.loop)
					{
						currentTime = 0;
						animationBlock.animation.ResetTracks();
					}
					else
						isRunning = false;
				}
				
				animationBlock.onUpdate(this, obj);
				break;
			}
			case ConditionBlock<T> conditionBlock:
				conditionBlock.onUpdate(this, obj);
				break;
		}
	}
}