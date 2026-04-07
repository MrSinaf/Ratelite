using Ratelite.Rendering;
using Ratelite.Utils;

namespace Ratelite.Resources;

public class Shader : IResource<Shader>
{
	private const string OPENGL_VERSION = "#version 330 core";
	
	public GProgram gProgram = null!;
	
	public Shader(string vertexShader, string fragmentShader)
	{
		MainThreadQueue.EnqueueRenderer(() =>
		{
			gProgram = new GProgram();
			gProgram.Compile(
				OPENGL_VERSION + "\n" + vertexShader,
				OPENGL_VERSION + "\n" + fragmentShader
			);
		});
	}
	
	public static Shader Load(Stream stream)
	{
		// TODO Ajouter la gestion des variables par défaut (soit ici, soit via le material).
		using var reader = new StreamReader(stream);
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
		return new Shader(vertexShader, fragmentShader);
	}
	
	public static bool ValidateExtension(string extension)
		=> extension == ".rshad";
}