using Ratelite.Bindings;
using Ratelite.Rendering;

namespace Ratelite.Resources;

public abstract class Mesh : IAsset, IDisposable
{
	public required int[] indices;
	
	public GVertexArrayObject vao { get; protected set; } = null!;
	public Region bounds { get; protected set; }
	public bool isValid => !isDisposed;
	public List<SubMesh> subMeshes { get; protected set; } = [];
	
	public abstract int nVertices { get; }
	
	protected GBuffer<byte> vertexBuffer = null!;
	protected GBuffer<uint> indexBuffer = null!;
	private bool isDisposed;
	
	public static Mesh Create<T>(T[] vertices, int[] indices) where T : unmanaged, IVertex
	{
		var mesh = new Mesh<T>
		{
			vertices = vertices,
			indices = indices
		};
		
		mesh.CreateBuffer();
		return mesh;
	}
	
	public abstract void ApplyVertex();
	public abstract void ApplyVertex(int offset, int length, bool updateBounds = true);
	public abstract void ApplyIndices();
	public abstract void ApplyIndices(int offset, int length);
	protected abstract void CreateBuffer();
	protected abstract void UpdateBounds();
	
	public Mesh AddSubMesh(SubMesh subMesh)
	{
		subMeshes.Add(subMesh);
		return this;
	}
	
	public void Draw()
	{
		ObjectDisposedException.ThrowIf(!isValid, nameof(Mesh));
		
		vao.Bind();
		GL.DrawElements(
			PrimitiveType.Triangles,
			(uint)indices.Length,
			DrawElementsType.UnsignedInt,
			0
		);
	}
	
	public void DrawSubMeshes(SubMesh subMesh)
	{
		ObjectDisposedException.ThrowIf(!isValid, nameof(Mesh));

		vao.Bind();
		GL.DrawElements(
			PrimitiveType.Triangles,
			subMesh.indexCount,
			DrawElementsType.UnsignedInt,
			subMesh.indexOffsetInOctets
		);
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

public class Mesh<T> : Mesh where T : unmanaged, IVertex
{
	public required T[] vertices;
	
	public override int nVertices => vertices.Length;
	
	public override void ApplyVertex() => ApplyVertex(0, vertices.Length);
	
	public override void ApplyVertex(int offset, int length, bool updateBounds = true)
	{
		unsafe
		{
			fixed (T* ptr = vertices.AsSpan(offset, length))
			{
				vertexBuffer.Set(
					(uint)(offset * sizeof(T)),
					(byte*)ptr,
					(uint)(length * sizeof(T))
				);
			}
		}
		
		if (updateBounds)
			UpdateBounds();
	}
	
	public override void ApplyIndices() => ApplyIndices(0, indices.Length);
	
	public override void ApplyIndices(int offset, int length)
	{
		unsafe
		{
			vao.Bind();
			fixed (int* ptr = indices.AsSpan(offset, length))
			{
				indexBuffer.Set((uint)(offset * sizeof(uint)), ptr, (uint)(length * sizeof(uint)));
			}
		}
	}
	
	protected override unsafe void CreateBuffer()
	{
		fixed (T* ptr = vertices.AsSpan())
		{
			vertexBuffer = new GBuffer<byte>(
				BufferType.VertexBuffer,
				(uint)(vertices.Length * sizeof(T)),
				ptr,
				true
			);
		}
		vao = T.GetVAO();
		fixed (int* ptr = indices.AsSpan())
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
	
	protected override void UpdateBounds()
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
	
	/* TODO
	 * Quand on passe du SplashWindow au GameWindow si un mesh avait été chargé il doit rebindé
	 * son VAO car il n'est pas partagé dans le OpenGL context (っ °Д °;)っ...
	 * Évidemment faut voir comment automatiser ceci efficacement!
	 */
	public void RebindVAO()
	{
		if (!isValid)
			return;
		
		MainThread.Assert();
		vao.Dispose();
		
		vertexBuffer.Bind();
		vao = T.GetVAO();
		indexBuffer.Bind();
	}
}

public record struct SubMesh(uint indexOffset, uint indexCount, uint materialIndex)
{
	public readonly uint indexOffset = indexOffset;
	public readonly uint indexCount = indexCount;
	public readonly uint materialIndex = materialIndex;
	public readonly nint indexOffsetInOctets = (nint)(indexOffset * sizeof(uint));
}

public struct VertexPositionUV : IVertex
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