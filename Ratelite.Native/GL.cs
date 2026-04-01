/*			 /!\ ATTENTION /!\
 * Je fais ici une chose assez stupide (>_<。)...
 * Car, pour le moment j'écris les fonctions à la main, c'est juste que j'ai pas trop d'idée
 * comment m'y prendre pour avoir une solution automatique mais donnant le même résultat-
 *
 * Pour le moment, je crée simplement les fonctions dont j'ai besoin... DONC il risque d'avoir
 * beaucoup de mise à jour avant que je trouve un moyen de tout générer automatiquement!
 */
using System.Text;
using Ratelite.Bindings;
using static Ratelite.Bindings.GLNative;

namespace Ratelite;

public static unsafe class GL
{
	#region Buffer
	
	public static uint GenBuffer()
	{
		uint id = 0;
		glGenBuffers(1, &id);
		return id;
	}
	
	public static void BindBuffer(BufferTargetARB target, uint buffer)
		=> glBindBuffer(target, buffer);
	
	public static void BufferData(
		BufferTargetARB target,
		uint sizeInBytes,
		void* data,
		BufferUsageARB usage
	)
		=> glBufferData(target, sizeInBytes, data, usage);
	
	
	public static void BufferSubData(
		BufferTargetARB target,
		nint offsetInBytes,
		uint sizeInBytes,
		void* data
	)
		=> glBufferSubData(target, offsetInBytes, sizeInBytes, data);
	
	public static void BindBufferBase(BufferTargetARB target, uint index, uint buffer)
		=> glBindBufferBase(target, index, buffer);
	
	
	public static void DeleteBuffer(uint buffer)
		=> glDeleteBuffers(1, &buffer);
	
	#endregion
	
	#region Programs
	
	public static uint CreateProgram()
		=> glCreateProgram();
	
	public static void DeleteProgram(uint program)
		=> glDeleteProgram(program);
	
	public static void UseProgram(uint program)
		=> glUseProgram(program);
	
	public static void LinkProgram(uint program)
		=> glLinkProgram(program);
	
	public static void GetProgram(uint program, ProgramPropertyARB pname, out int value)
	{
		int data;
		glGetProgramiv(program, pname, &data);
		value = data;
	}
	
	public static void GetProgramInfoLog(uint program, out string info)
	{
		info = string.Empty;
		var length = 0;
		
		glGetProgramiv(program, ProgramPropertyARB.InfoLogLength, &length);
		if (length <= 1)
			return;
		
		var buffer = stackalloc byte[length];
		var written = 0;
		glGetProgramInfoLog(program, length, &written, (sbyte*)buffer);
		
		var count = written > 0 ? written : length - 1;
		info = Encoding.UTF8.GetString(buffer, count);
	}
	
	#endregion
	
	#region Uniforms
	
	public static int GetUniformLocation(uint program, string name)
	{
		fixed (byte* bName = Encoding.UTF8.GetBytes(name + "\0"))
		{
			return glGetUniformLocation(program, (sbyte*)bName);
		}
	}
	
	public static int GetAttribLocation(uint program, string name)
	{
		fixed (byte* bName = Encoding.UTF8.GetBytes(name + "\0"))
		{
			return glGetAttribLocation(program, (sbyte*)bName);
		}
	}
	
	public static void GetUniform(uint program, int location, out int value)
	{
		var result = 0;
		glGetUniformiv(program, location, &result);
		value = result;
	}
	
	public static void GetUniform(uint program, int location, out float value)
	{
		var result = 0F;
		glGetUniformfv(program, location, &result);
		value = result;
	}
	
	public static void GetUniform(uint program, int location, float[] values)
	{
		fixed (float* p = values)
		{
			glGetUniformfv(program, location, p);
		}
	}
	
	public static void Uniform1(int location, int value)
		=> glUniform1I(location, value);
	
	public static void Uniform1(int location, float value)
		=> glUniform1F(location, value);
	
	public static void Uniform2(int location, float x, float y)
		=> glUniform2F(location, x, y);
	
	public static void Uniform3(int location, float x, float y, float z)
		=> glUniform3F(location, x, y, z);
	
	public static void Uniform4(int location, float x, float y, float z, float w)
		=> glUniform4F(location, x, y, z, w);
	
	public static void UniformMatrix3(int location, int count, bool transpose, float* value)
		=> glUniformMatrix3Fv(location, count, transpose, value);
	
	public static string GetActiveUniform(
		uint program,
		uint index,
		out uint size,
		out UniformType type
	)
	{
		GetProgram(program, (ProgramPropertyARB)GLEnum.ActiveUniformMaxLength, out var maxLen);
		if (maxLen <= 0)
		{
			size = 0;
			type = default;
			return string.Empty;
		}
		
		var buffer = stackalloc byte[maxLen];
		var length = 0;
		uint rSize;
		UniformType rType;
		glGetActiveUniform(program, index, maxLen, &length, &rSize, &rType, (sbyte*)buffer);
		size = rSize;
		type = rType;
		
		if (length <= 0)
			return string.Empty;
		
		return Encoding.UTF8.GetString(buffer, length);
	}
	
	public static void UniformMatrix4(int location, int count, bool transpose, float* value)
		=> glUniformMatrix4Fv(location, count, transpose, value);
	
	#endregion
	
	#region Shaders
	
	public static void Flush()
		=> glFlush();
	
	public static uint CreateShader(ShaderType type)
		=> glCreateShader(type);
	
	public static void ShaderSource(uint shader, string source)
	{
		fixed (byte* utf8 = Encoding.UTF8.GetBytes(source))
		{
			var src = (sbyte*)utf8;
			glShaderSource(shader, 1, &src, null);
		}
	}
	
	public static void CompileShader(uint shader)
		=> glCompileShader(shader);
	
	public static void GetShader(uint shader, ShaderParameterName pname, out int value)
	{
		var result = 0;
		glGetShaderiv(shader, pname, &result);
		value = result;
	}
	
	public static void GetShaderInfoLog(uint shader, out string info)
	{
		info = string.Empty;
		int length = 0;
		
		glGetShaderiv(shader, ShaderParameterName.InfoLogLength, &length);
		if (length <= 1)
			return;
		
		var buffer = stackalloc byte[length];
		int written = 0;
		glGetShaderInfoLog(shader, length, &written, (sbyte*)buffer);
		
		var count = written > 0 ? written : length - 1;
		info = Encoding.UTF8.GetString(buffer, count);
	}
	
	public static void AttachShader(uint program, uint shader)
		=> glAttachShader(program, shader);
	
	public static void DetachShader(uint program, uint shader)
		=> glDetachShader(program, shader);
	
	public static void DeleteShader(uint shader)
		=> glDeleteShader(shader);
	
	#endregion
	
	#region Textures
	
	public static void ActiveTexture(TextureUnit unit)
		=> glActiveTexture(unit);
	
	public static uint GenTexture()
	{
		uint id = 0;
		glGenTextures(1, &id);
		return id;
	}
	
	public static void BindTexture(TextureTarget target, uint texture)
		=> glBindTexture(target, texture);
	
	public static void TexParameter(TextureTarget target, GLEnum pname, int param)
		=> glTexParameteri(target, pname, param);
	
	public static void GenerateMipmap(TextureTarget target)
		=> glGenerateMipmap(target);
	
	public static void TexImage2D(
		TextureTarget target,
		int level,
		InternalFormat internalFormat,
		uint width,
		uint height,
		int border,
		PixelFormat format,
		PixelType type,
		void* pixels
	)
		=> glTexImage2D(target, level, internalFormat, width, height, border, format, type, pixels);
	
	public static void TexSubImage2D(
		TextureTarget target,
		int level,
		int xoffset,
		int yoffset,
		uint width,
		uint height,
		PixelFormat format,
		PixelType type,
		void* pixels
	)
		=> glTexSubImage2D(target, level, xoffset, yoffset, width, height, format, type, pixels);
	
	public static void DeleteTexture(uint texture)
		=> glDeleteTextures(1, &texture);
	
	#endregion
	
	#region Vertex Arrays
	
	public static uint GenVertexArray()
	{
		uint id = 0;
		glGenVertexArrays(1, &id);
		return id;
	}
	
	public static void EnableVertexAttribArray(uint index)
		=> glEnableVertexAttribArray(index);
	
	public static void VertexAttribPointer(
		uint index,
		int size,
		VertexAttribPointerType type,
		bool normalized,
		uint stride,
		void* pointer
	)
		=> glVertexAttribPointer(index, size, type, normalized, stride, pointer);
	
	public static void BindVertexArray(uint array)
		=> glBindVertexArray(array);
	
	public static void DeleteVertexArray(uint array)
		=> glDeleteVertexArrays(1, &array);
	
	#endregion
	
	#region Framebuffers
	
	public static uint GenFramebuffer()
	{
		uint id = 0;
		glGenFramebuffers(1, &id);
		return id;
	}
	
	public static void BindFramebuffer(FramebufferTarget target, uint framebuffer)
		=> glBindFramebuffer(target, framebuffer);
	
	public static void FramebufferTexture2D(
		FramebufferTarget target,
		FramebufferAttachment attachment,
		TextureTarget textarget,
		uint texture,
		int level
	)
		=> glFramebufferTexture2D(target, attachment, textarget, texture, level);
	
	public static FramebufferStatus CheckFramebufferStatus(FramebufferTarget target)
		=> glCheckFramebufferStatus(target);
	
	public static void DeleteFramebuffer(uint framebuffer)
		=> glDeleteFramebuffers(1, &framebuffer);
	
	#endregion
	
	#region Renderbuffers
	
	public static uint GenRenderbuffer()
	{
		uint id = 0;
		glGenRenderbuffers(1, &id);
		return id;
	}
	
	public static void BindRenderbuffer(RenderbufferTarget target, uint renderbuffer)
		=> glBindRenderbuffer(target, renderbuffer);
	
	public static void RenderbufferStorage(
		RenderbufferTarget target,
		InternalFormat internalformat,
		uint width,
		uint height
	)
		=> glRenderbufferStorage(target, internalformat, width, height);
	
	public static void FramebufferRenderbuffer(
		FramebufferTarget target,
		FramebufferAttachment attachment,
		RenderbufferTarget renderbuffertarget,
		uint renderbuffer
	)
		=> glFramebufferRenderbuffer(target, attachment, renderbuffertarget, renderbuffer);
	
	public static void DeleteRenderbuffer(uint renderbuffer)
		=> glDeleteRenderbuffers(1, &renderbuffer);
	
	#endregion
	
	#region Draws
	
	public static void DrawElements(
		PrimitiveType mode,
		uint count,
		DrawElementsType type,
		nint indices
	)
		=> glDrawElements(mode, count, type, (void*)indices);
	
	public static void DrawElementsBaseVertex(
		PrimitiveType mode,
		uint count,
		DrawElementsType type,
		void* indices,
		int baseVertex
	)
		=> glDrawElementsBaseVertex(mode, count, type, indices, baseVertex);
	
	#endregion
	
	#region Blend
	
	public static void BlendEquationSeparate(
		BlendEquationModeEXT modeRGB,
		BlendEquationModeEXT modeAlpha
	)
		=> glBlendEquationSeparate(modeRGB, modeAlpha);
	
	public static void BlendFuncSeparate(
		BlendingFactor srcRGB,
		BlendingFactor dstRGB,
		BlendingFactor srcAlpha,
		BlendingFactor dstAlpha
	)
		=> glBlendFuncSeparate(srcRGB, dstRGB, srcAlpha, dstAlpha);
	
	public static void BlendEquation(BlendEquationModeEXT mode)
		=> glBlendEquation(mode);
	#endregion
	
	public static void BindSampler(uint unit, uint sampler)
		=> glBindSampler(unit, sampler);
	
	public static void PolygonMode(TriangleFace face, PolygonMode mode)
		=> glPolygonMode(face, mode);
	
	public static void ClearColor(Color color)
	{
		var (r, g, b, a) = color.ToFloats();
		glClearColor(r, g, b, a);
	}
	
	public static void Clear(ClearBufferMask mask)
		=> glClear(mask);
	
	public static void Viewport(int x, int y, uint width, uint height)
		=> glViewport(x, y, width, height);
	
	public static void Enable(EnableCap cap)
		=> glEnable(cap);
	
	public static void Disable(EnableCap cap)
		=> glDisable(cap);
	
	public static bool IsEnabled(EnableCap cap)
		=> glIsEnabled(cap) != 0;
	
	public static void BlendFunc(BlendingFactor sfactor, BlendingFactor dfactor)
		=> glBlendFunc(sfactor, dfactor);
	
	public static void GetInteger(GetPName pname, Span<int> values)
	{
		fixed (int* p = values)
		{
			glGetIntegerv(pname, p);
		}
	}
	
	public static void GetInteger(GetPName pname, out int value)
	{
		var data = 0;
		glGetIntegerv(pname, &data);
		value = data;
	}
	
	public static void Scissor(int x, int y, uint width, uint height)
		=> glScissor(x, y, (int)width, (int)height);
}