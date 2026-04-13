using System.Reflection;
using Ratelite.Rendering;
using Ratelite.Resources;
using Ratelite.Utils;

namespace Ratelite;

public class SplashWindow
{
	internal readonly Window window;
	private readonly List<(Mesh mesh, Material material)> objects = [];
	
	public bool isLoaded { get; private set; }
	
	private float progress;
	private float currentProgress;
	private RConfig config;
	
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
		Loading().ContinueWith(_ => window.Close());
		
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
			layout (location = 0) in vec2 a_pos;
			layout (location = 1) in vec2 a_texCoord;
			uniform mat3 u_projection;
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
			uniform vec4 u_tint;
			out vec4 o_fragColor;
			void main() {
			    vec4 color = texture(u_texture, v_texCoord);
			    o_fragColor = color * u_tint;
			}
			"""
		);
		MainThreadQueue.EnqueueRenderer(() =>
		{
			shader.gProgram.SetUniform(
				"u_projection",
				Matrix3X3.CreateOrthographic(256, 266, false)
			);
			objects.Add(
				(
					MeshFactory.CreateQuad(texture.size * 2, new Vector2(0, -20)),
					new Material(
						shader,
						("u_texture", texture),
						("u_tint", Color.white),
						("u_model", Matrix3X3.Identity())
					)
				)
			);
			objects.Add(
				(
					MeshFactory.CreateQuad(new Vector2(256, 20), Vector2.zero),
					new Material(
						shader,
						("u_texture", new Texture2D(1, 1, [Color.white])),
						("u_tint", Color.black),
						("u_model", Matrix3X3.Identity())
					)
				)
			);
			objects.Add(
				(
					MeshFactory.CreateQuad(new Vector2(246, 10), Vector2.zero),
					new Material(
						shader,
						("u_texture", new Texture2D(1, 1, [Color.white])),
						("u_tint", new Color(0x35775C)),
						("u_model", Matrix3X3.CreateScale(new Vector2(0.5F, 1)) *
									Matrix3X3.CreateTranslation(new Vector2(5)))
					)
				)
			);
		});
	}
	
	private void Render()
	{
		MainThreadQueue.ExecuteAllRenderer();
		currentProgress = float.Lerp(currentProgress, progress, Time.delta * 15);
		objects[2].material.SetProperty(
			"u_model",
			Matrix3X3.CreateScale(new Vector2(currentProgress, 1)) *
			Matrix3X3.CreateTranslation(new Vector2(5))
		);
		
		foreach (var obj in objects)
		{
			obj.material.ApplyProperties();
			obj.mesh.Draw();
		}
	}
	
	public void Destroy()
	{
		window.Destroy();
		config = null!;
	}
	
	private async Task Loading()
	{
		await Task.Delay(500);
		var delta = 1F / config.modules.Count;
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
					Log.Write($"Error intializing module (ㆆ_ㆆ) : {moduleName}", e);
					await Task.Delay(-1);
				}
			}
			else
				Log.Write($"Module '{moduleName}' ready \\^o^/", Log.Level.Info);
			
			
			progress += delta;
		}
		
		await Task.Delay(500);
		progress = currentProgress = 0;
		
		var progressAssets = new Progress<float>();
		progressAssets.ProgressChanged += (_, value) => progress = value;
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
		await Task.Delay(1000);
		isLoaded = true;
	}
}