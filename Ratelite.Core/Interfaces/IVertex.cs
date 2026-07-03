using Ratelite.Rendering;

namespace Ratelite;

public interface IVertex
{
	public Vector2 position { get; }
	public static abstract GVertexArrayObject GetVAO();
}