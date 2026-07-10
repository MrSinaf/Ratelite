namespace Ratelite.Debugs.Windows;

public class LogDebugWindow : IDebugWindow
{
	public bool show { get; set; }
	
	private readonly Queue<LogInfos> logs = new();
	private float time;
	
	public LogDebugWindow()
	{
		Log.onLog += infos =>
		{
			logs.Enqueue(infos);
			while (logs.Count > Log.nLogs)
				logs.Dequeue();
		};
	}
	
	public void Draw()
	{
		ListingLogEpheral();
		
		if (!show) return;
		
		var refShow = show;
		if (show && ImGui.Begin("Logs", ref refShow))
		{
			foreach (var log in Log.logs)
				ImGui.Text($"[{log.date:HH:mm:ss}] {log.level}: {log.content}");
			
			ImGui.End();
		}
		show = refShow;
	}
	
	private void ListingLogEpheral()
	{
		if (logs.Count == 0) return;

		time += Time.delta;
		if (time > 2)
		{
			logs.Dequeue();
			time = 0;
		}
		
		if (logs.Count == 0) return;
		
		var viewport = ImGui.GetMainViewport();
		ImGui.SetNextWindowViewport(viewport.ID);
		ImGui.SetNextWindowPos(
			new Vector2(
				viewport.WorkPos.x + 5,
				viewport.WorkPos.y + viewport.WorkSize.y - 5
			),
			ImGuiCond.Always,
			new Vector2(0, 1)
		);
		
		ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0);
		ImGui.SetNextWindowBgAlpha(0.35f);
		
		if (ImGui.Begin("##EphemeralLogsOverlay", ImGuiWindowFlags.NoDecoration |
												  ImGuiWindowFlags.AlwaysAutoResize |
												  ImGuiWindowFlags.NoSavedSettings |
												  ImGuiWindowFlags.NoFocusOnAppearing |
												  ImGuiWindowFlags.NoNav |
												  ImGuiWindowFlags.NoMove |
												  ImGuiWindowFlags.NoInputs))
		{
			foreach (var log in logs)
			{
				var color = log.level switch
				{
					Log.Level.Verbose => new Vector4(0.45f, 0.65f, 1.00f, 1.00f),
					Log.Level.Debug   => new Vector4(0.35f, 0.90f, 1.00f, 1.00f),
					Log.Level.Info    => new Vector4(0.45f, 1.00f, 0.45f, 1.00f),
					Log.Level.Warning => new Vector4(1.00f, 0.85f, 0.25f, 1.00f),
					Log.Level.Error   => new Vector4(1.00f, 0.35f, 0.35f, 1.00f),
					Log.Level.Fatal   => new Vector4(1.00f, 0.35f, 1.00f, 1.00f),
					_                 => new Vector4(1, 1, 1, 1)
				};
				
				var level = log.level switch
				{
					Log.Level.Verbose => "VER",
					Log.Level.Debug   => "DEB",
					Log.Level.Info    => "INF",
					Log.Level.Warning => "WAR",
					Log.Level.Error   => "ERR",
					Log.Level.Fatal   => "FAT",
					_                 => "???"
				};
				
				ImGui.TextColored(
					color,
					$"[{level}] {log.content}"
				);
			}
			ImGui.PopStyleVar();
			ImGui.End();
		}
		
	}
}