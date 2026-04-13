using Ratelite.Rendering;
using Ratelite.Utils;

namespace Ratelite.Resources;

public class Shader : IResource<Shader>
{
	private const string OPENGL_VERSION = "#version 330 core";
	
	public readonly IReadOnlyDictionary<string, object> defaultUniforms;
	public GProgram gProgram { get; private set; } = null!;
	
	public Shader(
		string vertexShader,
		string fragmentShader,
		IReadOnlyDictionary<string, object>? defaultUniforms = null
	)
	{
		this.defaultUniforms = defaultUniforms ?? new Dictionary<string, object>();
		MainThreadQueue.EnqueueRenderer(() =>
			{
				gProgram = new GProgram();
				gProgram.Compile(
					OPENGL_VERSION + "\n" + vertexShader,
					OPENGL_VERSION + "\n" + fragmentShader
				);
			}
		);
	}
	
	public static Shader Load(VaultRessource ress)
	{
		using var reader = new StreamReader(ress.stream);
		var shadxy = reader.ReadToEnd();
		
		var (vertexShader, fragmentShader) = ShaderFactory.Build(shadxy);
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
			ShaderFactory.ExtractUniformsWithDefaultValue(shadxy)
		);
	}
	
	public static bool ValidateExtension(string extension)
		=> extension == ".rshad";
}