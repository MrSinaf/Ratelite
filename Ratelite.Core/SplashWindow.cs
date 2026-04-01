using System.Reflection;
using Ratelite.Rendering;
using Ratelite.Resources;
using Ratelite.Utils;

namespace Ratelite;

public class SplashWindow
{
	internal readonly Window window;
	
	public bool isLoaded { get; private set; }
	
	private Progress<float> progress = new ();
	private RConfig config;
	
	public static Mesh mesh = null!;
	public static Material material = null!;
	
	public SplashWindow(RConfig config)
	{
		this.config = config;
		window = Window.Create(
			new WindowOptions(config.windowOptions.title, 256, 256)
			{
				transparent = true,
				decorated = false
			}
		);
		window.SetIcon(config.icon ?? R.icon);
		window.Center();
		
		window.start += Start;
		window.render += Render;
	}
	
	private void Start()
	{
		GL.ClearColor(Color.transparent);
		Loading(progress).ContinueWith(_ => window.Close());
		
		using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
			"Ratelite.assets.textures.icon-rat.png"
		)!;
		var image = RawImage.Load(stream);
		var texture = new Texture2D(
			image.size.x,
			image.size.y,
			Color.AsColors(image.pixels).ToArray()
		);
		var shader = new Shader(
			"""
			uniform mat3 u_projection;
			layout (location = 0) in vec2 a_pos;
			layout (location = 1) in vec2 a_texCoord;
			uniform mat3 u_model;
			out vec2 v_texCoord;
			void main() {
				gl_Position = vec4(u_projection * u_model * vec3(a_pos, 1.0), 1.0);
				v_texCoord = a_texCoord;
			}
			""",
			"""
			in vec2 v_texCoord;
			uniform sampler2D u_texture;
			out vec4 o_fragColor;
			void main() {
			    vec4 color = texture(u_texture, v_texCoord);
			    o_fragColor = color;
			}
			"""
		);
		material = new Material(
			shader,
			("u_texture", texture),
			("u_projection", Matrix3X3.CreateOrthographic(256, 256)),
			("u_model", Matrix3X3.CreateScale(new Vector2(2)))
		);
		mesh = MeshFactory.CreateQuad(texture.size, texture.size * 0.5F);
	}
	
	private void Render()
	{
		material.ApplyProperties();
		mesh.Draw();
	}
	
	public void Destroy()
	{
		window.Destroy();
		config = null!;
		progress = null!;
	}
	
	private async Task Loading(IProgress<float> progress)
	{
		var delta = config.modules.Count + 1 / 1;
		var currentProgress = 0F;
		foreach (var module in config.modules)
		{
			var moduleName = module.GetType().Name.Replace("Module", "");
			if (module is ILoadableModule loadable)
			{
				try
				{
					await loadable.Load();
					Log.Write(
						$"Module '{moduleName}' loaded \\^o^/",
						Log.Level.Info
					);
				}
				catch (Exception e)
				{
					Log.Write(
						$"Error intializing module (ㆆ_ㆆ) : {moduleName}\n{e.Message}",
						Log.Level.Error
					);
				}
			}
			else
				Log.Write($"Module '{moduleName}' ready \\^o^/", Log.Level.Info);
			
			currentProgress += delta;
			progress.Report(currentProgress);
		}
		
		var progressAssets = new Progress<float>();
		try
		{
			await config.Action(progressAssets);
		}
		catch (Exception e)
		{
			Log.Write("Failed to load assets ;-;", e);
			await Task.Delay(-1);
		}
		
		try
		{
			await Stage.Load(
				(Scene)Activator.CreateInstance(config.startingScene ?? typeof(Scene))!
			);
		}
		catch (Exception e)
		{
			Log.Write(
				$"Failed to load the starting scene ( ˘︹˘ ): {config.startingScene}\n{e.Message}",
				Log.Level.Error
			);
			throw;
		}
		await Task.Delay(2000);
		isLoaded = true;
	}
}