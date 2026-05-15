using Ratelite.Rendering;

namespace Ratelite.Resources;

public class Shader : IResourceAsync<Shader>
{
	private const string OPENGL_VERSION = "#version 330 core";
	
	public readonly IReadOnlyDictionary<string, object> defaultUniforms;
	public readonly GProgram gProgram;
	
	public Shader(
		string vertexShader,
		string fragmentShader,
		IReadOnlyDictionary<string, object>? defaultUniforms = null
	)
	{
		this.defaultUniforms = defaultUniforms ?? new Dictionary<string, object>();
		gProgram = new GProgram();
		gProgram.Compile(
			OPENGL_VERSION + "\n" + vertexShader,
			OPENGL_VERSION + "\n" + fragmentShader
		);
	}
	
	public static Shader Load(VaultRessource ress)
	{
		using var reader = new StreamReader(ress.stream);
		var shad = reader.ReadToEnd();
		
		var (vertexShader, fragmentShader) = Utils.ShaderFactory.Build(shad);
		var layout = """
					 layout(std140) uniform Default {
					     float time;
					     float delta_time;
					     vec2 resolution;
					     mat3 projection;
					 };
					 """;
		
		vertexShader = layout + vertexShader;
		fragmentShader = layout + fragmentShader;
		
		return new Shader(
			vertexShader,
			fragmentShader,
			Utils.ShaderFactory.ExtractUniformsWithDefaultValue(shad)
		);
	}
	
	public static async Task<Shader> LoadAsync(VaultRessource ress)
	{
		using var reader = new StreamReader(ress.stream);
		var shad = await reader.ReadToEndAsync();
		
		var (vertexShader, fragmentShader) = Utils.ShaderFactory.Build(shad);
		var layout = """
					 layout(std140) uniform Default {
					     float time;
					     float delta_time;
					     vec2 resolution;
					     mat3 projection;
					 };
					 """;
		
		vertexShader = layout + vertexShader;
		fragmentShader = layout + fragmentShader;
		Shader? shader = null;
		MainThread.Enqueue(() => shader = new Shader(
			vertexShader, fragmentShader,
			Utils.ShaderFactory.ExtractUniformsWithDefaultValue(shad)
		));
		await MainThread.Wait();
		return shader ?? throw new NullReferenceException();
	}
	
	public static bool ValidateExtension(string extension)
		=> extension == ".rshad";
}