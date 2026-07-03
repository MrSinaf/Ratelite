using JetBrains.Annotations;

namespace Ratelite;

[UsedImplicitly]
public interface IComponent
{
	public bool enable { get; set; }
}

[UsedImplicitly]
public interface IUpdatableComponent : IComponent
{
	public void Update();
}

[UsedImplicitly]
public interface IRenderableComponent : IComponent
{
	public void Render();
}

[UsedImplicitly]
public interface IDisposableComponent : IComponent
{
	public void Dispose();
}