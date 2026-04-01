using Ratelite.Bindings;
using Ratelite.Rendering;

namespace Ratelite.Resources;

// TODO > Le rendre pénétrable... (⊙x⊙;) par n'importe quel type de Vertex !
public class Mesh : IAsset, IDisposable
{
	public GVertexArrayObject vao { get; private set; } = null!;
	private GBuffer<byte> vertexBuffer = null!;
	private GBuffer<uint> indexBuffer = null!;
	
	public required uint[] indices;
	public required VertexPositionUV[] vertices;
	
	public Region bounds { get; private set; }
	public bool isValid => !isDisposed;
	
	private bool isDisposed;
	
	private Mesh() { }
	
	public static Mesh Create(VertexPositionUV[] vertices, uint[] indices)
	{
		var mesh = new Mesh
		{
			vertices = vertices,
			indices = indices
		};
		
		mesh.CreateBuffer();
		return mesh;
	}
	
	/* TODO
	 * Quand on passe du SplashWindow au GameWindow si un mesh avait été chargé il doit rebindé
	 * son VAO car il n'est pas partagé dans le OpenGL context (っ °Д °;)っ...
	 * Évidemment faut voir comment automatiser ceci efficacement!
	 */
	public void RebindVAO()
	{
		if (!isValid)
			return;
		
		vao.Dispose();
		
		vertexBuffer.Bind();
		vao = VertexPositionUV.GetVAO();
		indexBuffer.Bind();
	}
	
	public void Draw()
	{
		vao.Bind();
		GL.DrawElements(
			PrimitiveType.Triangles,
			(uint)indices.Length,
			DrawElementsType.UnsignedInt,
			0
		);
	}
	
	public void ApplyVertex() => ApplyVertex(0, vertices.Length);
	
	public void ApplyVertex(int offset, int length)
	{
		unsafe
		{
			fixed (VertexPositionUV* ptr = vertices.AsSpan(offset, length))
			{
				vertexBuffer.Set(
					(uint)(offset * sizeof(VertexPositionUV)),
					(byte*)ptr,
					(uint)(length * sizeof(VertexPositionUV))
				);
			}
		}
		UpdateBounds();
	}
	
	public void ApplyIndices() => ApplyIndices(0, indices.Length);
	
	public void ApplyIndices(int offset, int length)
	{
		unsafe
		{
			fixed (uint* ptr = indices.AsSpan(offset, length))
			{
				indexBuffer.Set((uint)offset, ptr, (uint)(length * sizeof(uint)));
			}
		}
	}
	
	private unsafe void CreateBuffer()
	{
		fixed (VertexPositionUV* ptr = vertices.AsSpan())
		{
			vertexBuffer = new GBuffer<byte>(
				BufferType.VertexBuffer,
				(uint)(vertices.Length * sizeof(VertexPositionUV)),
				ptr,
				true
			);
		}
		vao = VertexPositionUV.GetVAO();
		fixed (uint* ptr = indices.AsSpan())
		{
			indexBuffer = new GBuffer<uint>(
				BufferType.ElementsBuffer,
				(uint)(indices.Length * sizeof(uint)),
				ptr,
				true
			);
		}
		
		UpdateBounds();
	}
	
	private void UpdateBounds()
	{
		if (vertices.Length == 0)
		{
			bounds = Region.zero;
			return;
		}
		
		var min = Vector2.max;
		var max = Vector2.min;
		
		foreach (var vertex in vertices)
		{
			var position = vertex.position;
			if (position.x < min.x) min.x = position.x;
			if (position.y < min.y) min.y = position.y;
			
			if (position.x > max.x) max.x = position.x;
			if (position.y > max.y) max.y = position.y;
		}
		
		bounds = new Region(min, max);
	}
	
	public void Dispose()
	{
		if (isDisposed)
			return;
		
		vao.Dispose();
		vertexBuffer.Dispose();
		indexBuffer.Dispose();
		
		isDisposed = true;
		GC.SuppressFinalize(this);
	}
}

public struct VertexPositionUV
{
	public Vector2 position { get; set; }
	public Vector2 uv { get; set; }
	
	public static GVertexArrayObject GetVAO()
	{
		var vao = new GVertexArrayObject(16);
		vao.VertexAttribPointer(0, 2, VertexType.Float, 0);
		vao.VertexAttribPointer(1, 2, VertexType.Float, 8);
		return vao;
	}
}