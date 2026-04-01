using Ratelite.Resources;

namespace Ratelite.Utils;

public static class MeshFactory
{
	public static Mesh CreateQuad(Vector2 size, Vector2? origin = null, Region? uvs = null)
	{
		var originValue = origin ?? Vector2.zero;
		var meshUv = uvs ?? new Region(Vector2.zero, Vector2.one);
		var mesh = Mesh.Create(
			[
				new VertexPositionUV
				{
					position = new Vector2(0, size.y) - originValue,
					uv = meshUv.position00
				},
				new VertexPositionUV
				{
					position = size - originValue,
					uv = new Vector2(meshUv.position11.x, meshUv.position00.y)
				},
				new VertexPositionUV
				{
					position = new Vector2(size.x, 0) - originValue,
					uv = meshUv.position11
				},
				new VertexPositionUV
				{
					position = -originValue,
					uv = new Vector2(meshUv.position00.x, meshUv.position11.y)
				}
			],
			[0, 3, 1, 3, 2, 1]
		);
		
		return mesh;
	}
	
	public static Mesh CreateQuads(ReadOnlySpan<(Rect vertices, Region uvs)> quads)
	{
		var vertices = new VertexPositionUV[quads.Length * 4];
		var indices = new uint[quads.Length * 6];
		
		for (var i = 0; i < quads.Length; i++)
		{
			var quad = quads[i];
			var pos00 = quad.vertices.position;
			var pos11 = quad.vertices.position + quad.vertices.size;
			var iV = i * 4;
			
			vertices[iV].position = new Vector2(pos00.x, pos11.y);
			vertices[iV + 1].position = pos11;
			vertices[iV + 2].position = new Vector2(pos11.x, pos00.y);
			vertices[iV + 3].position = pos00;
			
			vertices[iV].uv = quad.uvs.position00;
			vertices[iV + 1].uv = new Vector2(quad.uvs.position11.x, quad.uvs.position00.y);
			vertices[iV + 2].uv = quad.uvs.position11;
			vertices[iV + 3].uv = new Vector2(quad.uvs.position00.x, quad.uvs.position11.y);
			
			var jV = (uint)iV;
			var iI = i * 6;
			indices[iI] = jV;
			indices[iI + 1] = jV + 3;
			indices[iI + 2] = jV + 1;
			indices[iI + 3] = jV + 3;
			indices[iI + 4] = jV + 2;
			indices[iI + 5] = jV + 1;
		}
		
		return Mesh.Create(vertices, indices);
	}
	
	public static Mesh SetQuadsVertices(Mesh mesh, ReadOnlySpan<(Rect vertices, Region uvs)> quads)
	{
		var vertices = new VertexPositionUV[quads.Length * 4];
		
		for (var i = 0; i < quads.Length; i++)
		{
			var quad = quads[i];
			var pos00 = quad.vertices.position;
			var pos11 = quad.vertices.position + quad.vertices.size;
			var iV = i * 4;
			
			vertices[iV].position = new Vector2(pos00.x, pos11.y);
			vertices[iV + 1].position = pos11;
			vertices[iV + 2].position = new Vector2(pos11.x, pos00.y);
			vertices[iV + 3].position = pos00;
			
			vertices[iV].uv = quad.uvs.position00;
			vertices[iV + 1].uv = new Vector2(quad.uvs.position11.x, quad.uvs.position00.y);
			vertices[iV + 2].uv = quad.uvs.position11;
			vertices[iV + 3].uv = new Vector2(quad.uvs.position00.x, quad.uvs.position11.y);
		}
		
		mesh.vertices = vertices;
		mesh.ApplyVertex();
		return mesh;
	}
}