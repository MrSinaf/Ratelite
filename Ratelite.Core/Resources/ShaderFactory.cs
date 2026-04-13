using System.Text;
using System.Text.RegularExpressions;

namespace Ratelite.Resources;

public static partial class ShaderFactory
{
	public static (string vertex, string fragment) Build(string shadxySource)
	{
		var preamble = ExtractPreambleDedup(shadxySource);
		var functions = ExtractFunctions(shadxySource);
		
		var vertexSb = new StringBuilder();
		var fragmentSb = new StringBuilder();
		
		vertexSb.Append('\n').Append(preamble).Append('\n');
		fragmentSb.Append('\n').Append(preamble).Append('\n');
		
		var vertexParams = new StringBuilder();
		var fragmentParams = new StringBuilder();
		
		foreach (var f in functions)
		{
			switch (f.name)
			{
				case "mainVertex":
				{
					vertexParams.Insert(0, f.globalParams);
					var method = f.body.Replace(f.name, "main");
					vertexSb.Append(method).Append('\n');
					break;
				}
				case "mainFragment":
				{
					fragmentParams.Insert(0, f.globalParams);
					var method = f.body.Replace(f.name, "main");
					fragmentSb.Append(method).Append('\n');
					break;
				}
				default:
					switch (f.stage)
					{
						case Stage.VertexOnly:
							vertexSb.Append(f.body).Append('\n');
							break;
						case Stage.FragmentOnly:
							fragmentSb.Append(f.body).Append('\n');
							break;
						case Stage.Both:
							vertexSb.Append(f.body).Append('\n');
							fragmentSb.Append(f.body).Append('\n');
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
					break;
			}
		}
		
		vertexSb.Insert(0, vertexParams.ToString());
		fragmentSb.Insert(0, fragmentParams.ToString());
		
		return (vertexSb.ToString(), fragmentSb.ToString());
	}
	
	public static string[] ExtractUniformNamesWithDefaultValue(string shadxySource)
	{
		var names = new List<string>();
		
		foreach (Match m in UniformWithDefaultRegex().Matches(shadxySource))
			names.Add(m.Groups[1].Value);
		
		return names.ToArray();
	}
	
	private enum Stage { Both, VertexOnly, FragmentOnly }
	
	private sealed record FunctionInfo(string name, string body, string globalParams, Stage stage);
	
	private static List<FunctionInfo> ExtractFunctions(string src)
	{
		var list = new List<FunctionInfo>();
		var matches = FunctionHeaderRegex().Matches(src);
		foreach (Match m in matches)
		{
			var startIndex = m.Index;
			var functionName = m.Groups[1].Value;
			var parametersText = m.Groups[2].Value.Trim();
			
			var endIndex = startIndex + m.Length;
			for (var count = 1; endIndex < src.Length; endIndex++)
			{
				var c = src[endIndex];
				if (c == '{') count++;
				else if (c == '}' && --count == 0) break;
			}
			if (endIndex <= startIndex) continue;
			
			var stage = Stage.Both;
			{
				var prevLineStart = src.LastIndexOf('\n', Math.Max(0, startIndex - 1));
				if (prevLineStart >= 0)
				{
					var prevPrevLineStart = src.LastIndexOf('\n', Math.Max(0, prevLineStart - 1));
					var prevLine = src.Substring(
						prevLineStart + 1,
						startIndex - (prevLineStart + 1)
					);
					var prevPrevLine = prevPrevLineStart >= 0
							? src.Substring(
								prevPrevLineStart + 1,
								prevLineStart - (prevPrevLineStart + 1)
							)
							: string.Empty;
					if (prevLine.Contains("@fragment-only") ||
						prevPrevLine.Contains("@fragment-only"))
						stage = Stage.FragmentOnly;
					else if (prevLine.Contains("@vertex-only") ||
							 prevPrevLine.Contains("@vertex-only"))
						stage = Stage.VertexOnly;
				}
			}
			
			var globalParams = string.Empty;
			string body;
			if (!string.IsNullOrWhiteSpace(parametersText))
			{
				var parameters = parametersText.Split(',');
				var formattedParamsList = new List<string>();
				var cleanParamsList = new List<string>();
				foreach (var p in parameters)
				{
					var trimmed = p.Trim();
					if (trimmed.StartsWith("in ") || trimmed.StartsWith("out "))
						formattedParamsList.Add(trimmed + ";\n");
					else
						cleanParamsList.Add(trimmed);
				}
				globalParams = string.Join(string.Empty, formattedParamsList);
				var originalSig = $"{functionName}({parametersText})";
				var cleanSig = $"{functionName}({string.Join(", ", cleanParamsList)})";
				body = src.Substring(startIndex, endIndex - startIndex + 1)
						  .Replace(originalSig, cleanSig);
			}
			else
				body = src.Substring(startIndex, endIndex - startIndex + 1);
			
			list.Add(new FunctionInfo(functionName, body, globalParams, stage));
		}
		return list;
	}
	
	private static string ExtractPreambleDedup(string src)
	{
		var layouts = new HashSet<string>();
		var uniforms = new HashSet<string>();
		
		foreach (Match m in LayoutRegex().Matches(src))
			layouts.Add(m.Value.Trim());
		
		foreach (Match m in UniformRegex().Matches(src))
			uniforms.Add(m.Value.Trim());
		
		var strBuilder = new StringBuilder();
		foreach (var l in layouts)
			strBuilder.AppendLine(l);
		
		if (layouts.Count > 0)
			strBuilder.AppendLine();
		
		foreach (var u in uniforms)
			strBuilder.AppendLine(u);
		
		return strBuilder.ToString();
	}
	
	[GeneratedRegex(
		@"(?:void|bool|int|uint|float|double|vec[234]|ivec[234]|uvec[234]|dvec[234]|mat[234](?:x[234])?)\s+(\w+)\s*\(([^\)]*)\)\s*{"
	)]
	private static partial Regex FunctionHeaderRegex();
	
	[GeneratedRegex(@"layout\s*\(.*?\).*?;")]
	private static partial Regex LayoutRegex();
	
	[GeneratedRegex(@"uniform\s+.*?;")]
	private static partial Regex UniformRegex();
	
	[GeneratedRegex(@"uniform\s+.*?\b(\w+)\s*=\s*.*?;")]
	private static partial Regex UniformWithDefaultRegex();
}