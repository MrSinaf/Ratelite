using System.Text.RegularExpressions;
using Ratelite.Bindings;

namespace Ratelite.Rendering;

public class GProgram : IDisposable
{
	private static uint? currentHandle;
	
	private readonly Dictionary<string, int> uniformLocations = [];
	private readonly Dictionary<string, int> attribLocations = [];
	public readonly uint handle = GL.CreateProgram();
	
	public bool isDisposed { get; protected set; }
	private uint vertexHandle;
	private uint fragmentHandle;
	
	public void Bind()
	{
		if (currentHandle == handle)
			return;
		
		GL.UseProgram(handle);
		currentHandle = handle;
	}
	
	public void Compile(string vertexSource, string fragmentSource)
	{
		if (!string.IsNullOrEmpty(vertexSource))
			CompileShader(ShaderType.VertexShader, vertexSource);
		
		if (!string.IsNullOrEmpty(fragmentSource))
			CompileShader(ShaderType.FragmentShader, fragmentSource);
		
		GL.LinkProgram(handle);
		GL.GetProgramInfoLog(handle, out var statusInfo);
		GL.GetProgram(handle, ProgramPropertyARB.LinkStatus, out var statusCode);
		
		// Vérifie son statut :
		if (statusCode != 1)
		{
			Decompile();
			throw new InvalidOperationException(
				$"Failed to Link Shader.\n{statusInfo}\n\nStatus Code: {statusCode}"
			);
		}
		
		GL.Flush();
		
		void CompileShader(ShaderType type, string source)
		{
			var shader = GL.CreateShader(type);
			GL.ShaderSource(shader, source);
			GL.CompileShader(shader);
			
			GL.GetShaderInfoLog(shader, out var statusInfo);
			GL.GetShader(shader, ShaderParameterName.CompileStatus, out var statusCode);
			
			// Vérifie son statut :
			if (statusCode != 1)
			{
				Decompile();
				var match = Regex.Match(statusInfo, @"\((\d+)\)");
				if (!match.Success)
					throw new InvalidOperationException(
						$"Failed to Compile {type} Source.\n{statusInfo}\nStatus Code: {statusCode}"
					);
				
				var lineIndex = int.Parse(match.Groups[1].Value) - 1;
				var lines = fragmentSource.Replace("\r\n", "\n").Split('\n');
				
				throw new InvalidOperationException(
					$"Failed to Compile {type} Source.\n{statusInfo}```\n{lines[lineIndex]}\n```" +
					$"\nStatus Code: {statusCode}"
				);
			}
			
			GL.AttachShader(handle, shader);
			
			if (type == ShaderType.VertexShader)
				vertexHandle = shader;
			else if (type == ShaderType.FragmentShader)
				fragmentHandle = shader;
		}
	}
	
	public void Decompile()
	{
		GL.DetachShader(handle, vertexHandle);
		GL.DeleteShader(vertexHandle);
		GL.DetachShader(handle, fragmentHandle);
		GL.DeleteShader(fragmentHandle);
		
		GL.Flush();
	}
	
	public void SetUniform(string name, bool value)
	{
		Bind();
		
		if (GetUniformLocation(name, out var location))
			GL.Uniform1(location, value ? 1 : 0);
	}
	
	public void GetUniform(string name, Type type, out object? obj)
	{
		if (type == typeof(bool))
		{
			GetUniform(name, out bool value);
			obj = value;
		}
		else if (type == typeof(float))
		{
			GetUniform(name, out float value);
			obj = value;
		}
		else if (type == typeof(int))
		{
			GetUniform(name, out int value);
			obj = value;
		}
		else if (type == typeof(Vector2))
		{
			GetUniform(name, out Vector2 value);
			obj = value;
		}
		else if (type == typeof(Vector3))
		{
			GetUniform(name, out Vector3 value);
			obj = value;
		}
		else if (type == typeof(Vector4))
		{
			GetUniform(name, out Vector4 value);
			obj = value;
		}
		else if (type == typeof(Region))
		{
			GetUniform(name, out Region value);
			obj = value;
		}
		else if (type == typeof(Rect))
		{
			GetUniform(name, out Rect value);
			obj = value;
		}
		else if (type == typeof(Matrix3X3))
		{
			GetUniform(name, out Matrix3X3 value);
			obj = value;
		}
		else if (type == typeof(Color))
		{
			GetUniform(name, out Color value);
			obj = value;
		}
		else
		{
			obj = null;
		}
	}
	
	public void GetUniform(string name, out bool value)
	{
		value = false;
		
		if (GetUniformLocation(name, out var location))
		{
			GL.GetUniform(handle, location, out int data);
			value = data != 0;
		}
	}
	
	public void SetUniform(string name, float value)
	{
		Bind();
		
		if (GetUniformLocation(name, out var location))
			GL.Uniform1(location, value);
	}
	
	public void GetUniform(string name, out float value)
	{
		value = 0;
		if (GetUniformLocation(name, out var location))
			GL.GetUniform(handle, location, out value);
	}
	
	public void SetUniform(string name, int value)
	{
		Bind();
		
		if (GetUniformLocation(name, out var location))
			GL.Uniform1(location, value);
	}
	
	public void GetUniform(string name, out int value)
	{
		value = 0;
		if (GetUniformLocation(name, out var location))
			GL.GetUniform(handle, location, out value);
	}
	
	public void SetUniform(string name, Vector2 value)
	{
		Bind();
		
		if (GetUniformLocation(name, out var location))
			GL.Uniform2(location, value.x, value.y);
	}
	
	public void GetUniform(string name, out Vector2 value)
	{
		value = default;
		
		if (GetUniformLocation(name, out var location))
		{
			var data = new float[2];
			GL.GetUniform(handle, location, data);
			value = new Vector2(data[0], data[1]);
		}
	}
	
	public void SetUniform(string name, Vector3 value)
	{
		Bind();
		
		if (GetUniformLocation(name, out var location))
			GL.Uniform3(location, value.x, value.y, value.z);
	}
	
	public void GetUniform(string name, out Vector3 value)
	{
		value = default;
		
		if (GetUniformLocation(name, out var location))
		{
			var data = new float[3];
			GL.GetUniform(handle, location, data);
			value = new Vector3(data[0], data[1], data[2]);
		}
	}
	
	public void SetUniform(string name, Vector4 value)
	{
		Bind();
		
		if (GetUniformLocation(name, out var location))
			GL.Uniform4(location, value.x, value.y, value.z, value.w);
	}
	
	public void GetUniform(string name, out Vector4 value)
	{
		value = default;
		
		if (GetUniformLocation(name, out var location))
		{
			var data = new float[4];
			GL.GetUniform(handle, location, data);
			value = new Vector4(data[0], data[1], data[2], data[3]);
		}
	}
	
	public void SetUniform(string name, Region value)
	{
		Bind();
		
		if (GetUniformLocation(name, out var location))
			GL.Uniform4(
				location,
				value.position00.x,
				value.position00.y,
				value.position11.x,
				value.position11.y
			);
	}
	
	public void GetUniform(string name, out Region value)
	{
		value = default;
		
		if (GetUniformLocation(name, out var location))
		{
			var data = new float[4];
			GL.GetUniform(handle, location, data);
			value = new Region(data[0], data[1], data[2], data[3]);
		}
	}
	
	public void SetUniform(string name, Rect value)
	{
		Bind();
		
		if (GetUniformLocation(name, out var location))
			GL.Uniform4(location, value.position.x, value.position.y, value.size.x, value.size.y);
	}
	
	public void GetUniform(string name, out Rect value)
	{
		value = default;
		
		if (GetUniformLocation(name, out var location))
		{
			var data = new float[4];
			GL.GetUniform(handle, location, data);
			value = new Rect(data[0], data[1], data[2], data[3]);
		}
	}
	
	public void SetUniform(string name, Matrix3X3 value)
	{
		Bind();
		
		if (GetUniformLocation(name, out var location))
		{
			unsafe
			{
				GL.UniformMatrix3(location, 1, false, (float*)&value);
			}
		}
	}
	
	public void GetUniform(string name, out Matrix3X3 value)
	{
		value = default;
		
		if (GetUniformLocation(name, out var location))
		{
			var data = new float[9];
			GL.GetUniform(handle, location, data);
			value = new Matrix3X3(
				data[0],
				data[1],
				data[2],
				data[3],
				data[4],
				data[5],
				data[6],
				data[7],
				data[8]
			);
		}
	}
	
	public void SetUniform(string name, Color color)
	{
		Bind();
		
		if (GetUniformLocation(name, out var location))
		{
			var (r, g, b, a) = color.ToFloats();
			GL.Uniform4(location, r, g, b, a);
		}
	}
	
	public void GetUniform(string name, out Color value)
	{
		value = default;
		
		if (GetUniformLocation(name, out var location))
		{
			var data = new float[4];
			GL.GetUniform(handle, location, data);
			value = new Color(data[0], data[1], data[2], data[3]);
		}
	}
	
	public void SetUniform(string name, Texture texture, ushort unit = 0)
	{
		Bind();
		GL.ActiveTexture((TextureUnit)((uint)TextureUnit.Texture0 + unit));
		texture.gTexture.Bind();
		SetUniform(name, 0);
	}
	
	public string[] GetUniformNames()
	{
		var list = new List<string>();
		
		GL.GetProgram(handle, ProgramPropertyARB.ActiveUniforms, out var uCount);
		
		for (var i = 0u; i < uCount; i++)
		{
			var name = GL.GetActiveUniform(handle, i, out _, out _);
			list.Add(name);
		}
		
		return list.ToArray();
	}
	
	public (string name, Type type, int arraySize)[] GetUniforms()
	{
		var list = new List<(string, Type, int)>();
		
		GL.GetProgram(handle, ProgramPropertyARB.ActiveUniforms, out var uCount);
		
		for (var i = 0u; i < uCount; i++)
		{
			var name = GL.GetActiveUniform(handle, i, out _, out var uniformType);
			var (type, arraySize) = UnitformTypeToType(uniformType);
			if (type != null)
				list.Add((name, type, arraySize));
		}
		
		return list.ToArray();
		
		(Type?, int) UnitformTypeToType(UniformType type) => type switch
		{
			UniformType.Int             => (typeof(int), 1),
			UniformType.UnsignedInt     => (typeof(uint), 1),
			UniformType.Float           => (typeof(float), 1),
			UniformType.Double          => (typeof(double), 1),
			UniformType.FloatVec2       => (typeof(Vector2), 1),
			UniformType.FloatVec3       => (typeof(Vector3), 1),
			UniformType.FloatVec4       => (typeof(Color), 1),
			UniformType.IntVec2         => (typeof(Vector2Int), 1),
			UniformType.IntVec3         => (typeof(int), 3),
			UniformType.IntVec4         => (typeof(int), 4),
			UniformType.UnsignedIntVec2 => (typeof(Vector2Int), 1),
			UniformType.UnsignedIntVec3 => (typeof(uint), 3),
			UniformType.UnsignedIntVec4 => (typeof(uint), 3),
			UniformType.Bool            => (typeof(bool), 1),
			UniformType.BoolVec2        => (typeof(bool), 2),
			UniformType.BoolVec3        => (typeof(bool), 3),
			UniformType.BoolVec4        => (typeof(bool), 4),
			UniformType.FloatMat3       => (typeof(Matrix3X3), 1),
			_                           => (null, 0)
		};
	}
	
	public bool GetUniformLocation(string name, out int location)
	{
		if (uniformLocations.TryGetValue(name, out location))
			return location != -1;
		
		location = GL.GetUniformLocation(handle, name);
		uniformLocations[name] = location;
		return location != -1;
	}
	
	public bool GetAttribLocation(string name, out int location)
	{
		if (attribLocations.TryGetValue(name, out location))
			return location != -1;
		
		location = GL.GetAttribLocation(handle, name);
		attribLocations[name] = location;
		return location != -1;
	}
	
	public void Dispose()
	{
		if (isDisposed)
			return;
		
		// if (currentHandle == handle)
		// 	currentHandle = null;
		
		GL.DeleteProgram(handle);
		isDisposed = true;
		GC.SuppressFinalize(this);
	}
}