using Ratelite.Rendering;
using Ratelite.Utils;

namespace Ratelite.Resources;

public class Shader : IResource<Shader>
{
	private const string OPENGL_VERSION = "#version 330 core";
	
	public GProgram gProgram { get; private set; } = null!;
	public IReadOnlyDictionary<string, object> defaultUniforms { get; private set; } = null!;
	
	public Shader(string vertexShader, string fragmentShader, string[]? defaultUniforms = null)
	{
		MainThreadQueue.EnqueueRenderer(() =>
			{
				gProgram = new GProgram();
				gProgram.Compile(
					OPENGL_VERSION + "\n" + vertexShader,
					OPENGL_VERSION + "\n" + fragmentShader
				);
				
				if (defaultUniforms != null)
				{
					var programUniforms = gProgram.GetUniforms();
					var uniforms = new Dictionary<string, object>();
					foreach (var uniform in programUniforms)
					{
						if (defaultUniforms.Contains(uniform.name))
						{
							gProgram.GetUniform(uniform.name, uniform.type, out var obj);
							if (obj != null)
								uniforms.Add(uniform.name, obj);
						}
					}
					
					this.defaultUniforms = uniforms;
				}
				else this.defaultUniforms = new Dictionary<string, object>();
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
			ShaderFactory.ExtractUniformNamesWithDefaultValue(shadxy)
		);
	}
	
	public static bool ValidateExtension(string extension)
		=> extension == ".rshad";
}