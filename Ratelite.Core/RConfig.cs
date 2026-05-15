using Ratelite.Bindings;
using Ratelite.Rendering;

namespace Ratelite;

public class RConfig
{
	public required WindowOptions windowOptions;
	public RawImage? icon;
	public Type? startingScene;
	public event Func<IProgress<float>, Task> action = delegate { return Task.CompletedTask; };
	
	internal readonly List<IModule> modules = [];
	
	public RConfig AddModule<T>() where T : IModule
	{
		modules.Add(Activator.CreateInstance<T>());
		return this;
	}
	
	public RConfig SetIcon(string path)
	{
		if (!File.Exists(path))
			Log.Warning("Icon file not found: " + path);
		else
			SetIcon(File.OpenRead(path));
		return this;
	}
	
	public RConfig SetWindowOptions(WindowOptions options)
	{
		windowOptions = options;
		return this;
	}
	
	public RConfig SetIcon(Stream stream)
	{
		var icon = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
		this.icon = new RawImage(icon.width, icon.height, icon.data);
		return this;
	}
	
	/// <summary>
	/// Définit la scène de démarrage pour le jeu en spécifiant
	/// une classe de type <see cref="Scene"/>. Cette méthode utilise un type générique
	/// qui sera instancié comme nouvelle scène. (＾▽＾)
	/// </summary>
	/// <typeparam name="T">
	/// Le type de la scène de démarrage. Ce type doit hériter de <see cref="Scene"/>
	/// et disposer d'un constructeur sans aucun paramètre (*_*). 
	/// </typeparam>
	/// <return>
	///	Retourne une instance mise à jour de <see cref="RConfig"/> pour permettre un chaînage
	///	fluide. :3
	/// </return>
	public RConfig SetStartingScene<T>() where T : Scene, new()
	{
		startingScene = typeof(T);
		return this;
	}
	
	/// <summary>
	///	Définit une action asynchrone à exécuter pour le chargement des ressources.
	///	Permet de rapporter la progression à l'aide d'un objet <see cref="IProgress{T}"/>.
	/// </summary>
	/// <param name="action">
	///	Une fonction asynchrone prenant un paramètre de type <see cref="IProgress{float}"/> et
	///	retournant une <see cref="Task"/>. Cette fonction représente l'action à effectuer pour
	///	charger les ressources. ヽ(✿ﾟ▽ﾟ)ノ
	/// </param>
	/// <return>
	///	Retourne une instance mise à jour de <see cref="RConfig"/> pour permettre un chaînage
	///	fluide. :3
	/// </return>
	public RConfig LoadingAssets(Func<IProgress<float>, Task> action)
	{
		this.action = action;
		return this;
	}
	
	/// <summary>
	/// Lance le jeu (≧▽≦) à l'aide de la configuration actuelle contenue dans
	/// <see cref="RConfig"/>.
	/// </summary>
	/// <remarks>
	/// Assurez-vous ( •̀ ω •́ )✧ que toutes les dépendances et modules nécessaires ont été
	/// correctement ajoutés à la configuration avant d'appeler cette méthode.
	/// </remarks>
	public void Run() => R.RunGame(this);
	
	internal async Task Action(IProgress<float> progress) => await action(progress);
}