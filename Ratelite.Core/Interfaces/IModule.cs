using JetBrains.Annotations;

namespace Ratelite;

[UsedImplicitly]
public interface IModule
{
	public int priority { get; }
	
	public void Init();
}

[UsedImplicitly]
public interface IDisposableModule : IModule
{
	public void Dispose();
}

[UsedImplicitly]
public interface ILoadableModule : IModule
{
	public Task Load();
}

[UsedImplicitly]
public interface IRenderableModule : IModule
{
	public void Render();
}

[UsedImplicitly]
public interface IUpdatableModule : IModule
{
	public void Update();
}