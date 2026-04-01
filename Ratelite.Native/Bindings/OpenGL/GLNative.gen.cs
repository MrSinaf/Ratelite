using System.Text;

namespace Ratelite.Bindings;

internal static unsafe class GLNative
{
	internal static delegate*
			unmanaged[Cdecl]<float, float, float, float, void> glClearColor;
	internal static delegate*
			unmanaged[Cdecl]<ClearBufferMask, void> glClear;
	internal static delegate*
			unmanaged[Cdecl]<int, int, uint, uint, void> glViewport;
	internal static delegate*
			unmanaged[Cdecl]<EnableCap, void> glEnable;
	internal static delegate*
			unmanaged[Cdecl]<EnableCap, void> glDisable;
	internal static delegate*
			unmanaged[Cdecl]<BlendingFactor, BlendingFactor, void> glBlendFunc;
	internal static delegate*
			unmanaged[Cdecl]<GetPName, int*, void> glGetIntegerv;
	internal static delegate*
			unmanaged[Cdecl]<EnableCap, byte> glIsEnabled;
	internal static delegate*
			unmanaged[Cdecl]<int, int, int, int, void> glScissor;
	internal static delegate*
			unmanaged[Cdecl]<uint, uint, void> glBindSampler;
	internal static delegate*
			unmanaged[Cdecl]<TriangleFace, PolygonMode, void> glPolygonMode;
	
	#region Blend
	
	internal static delegate*
			unmanaged[Cdecl]<BlendEquationModeEXT, BlendEquationModeEXT, void>
			glBlendEquationSeparate;
	internal static delegate*
			unmanaged[Cdecl]<BlendingFactor, BlendingFactor, BlendingFactor, BlendingFactor, void>
			glBlendFuncSeparate;
	internal static delegate*
			unmanaged[Cdecl]<BlendEquationModeEXT, void> glBlendEquation;

	
	#endregion
	
	#region Buffers
	
	internal static delegate*
			unmanaged[Cdecl]<uint, uint*, void> glGenBuffers;
	internal static delegate*
			unmanaged[Cdecl]<BufferTargetARB, uint, void> glBindBuffer;
	internal static delegate*
			unmanaged[Cdecl]<BufferTargetARB, nuint, void*, BufferUsageARB, void> glBufferData;
	internal static delegate* unmanaged[Cdecl]<BufferTargetARB, nint, nuint, void*, void>
			glBufferSubData;
	internal static delegate*
			unmanaged[Cdecl]<uint, uint*, void> glDeleteBuffers;
	internal static delegate*
			unmanaged[Cdecl]<BufferTargetARB, uint, uint, void> glBindBufferBase;
	
	#endregion
	
	#region Programs
	
	internal static delegate*
			unmanaged[Cdecl]<uint> glCreateProgram;
	internal static delegate*
			unmanaged[Cdecl]<uint, void> glDeleteProgram;
	internal static delegate*
			unmanaged[Cdecl]<uint, void> glUseProgram;
	internal static delegate*
			unmanaged[Cdecl]<uint, void> glLinkProgram;
	internal static delegate*
			unmanaged[Cdecl]<uint, ProgramPropertyARB, int*, void> glGetProgramiv;
	internal static delegate*
			unmanaged[Cdecl]<uint, int, int*, sbyte*, void> glGetProgramInfoLog;
	
	#endregion
	
	#region Shaders
	
	internal static delegate*
			unmanaged[Cdecl]<void> glFlush;
	internal static delegate*
			unmanaged[Cdecl]<ShaderType, uint> glCreateShader;
	internal static delegate*
			unmanaged[Cdecl]<uint, int, sbyte**, int*, void> glShaderSource;
	internal static delegate*
			unmanaged[Cdecl]<uint, void> glCompileShader;
	internal static delegate*
			unmanaged[Cdecl]<uint, ShaderParameterName, int*, void> glGetShaderiv;
	internal static delegate*
			unmanaged[Cdecl]<uint, int, int*, sbyte*, void> glGetShaderInfoLog;
	internal static delegate*
			unmanaged[Cdecl]<uint, uint, void> glAttachShader;
	internal static delegate*
			unmanaged[Cdecl]<uint, uint, void> glDetachShader;
	internal static delegate*
			unmanaged[Cdecl]<uint, void> glDeleteShader;
	
	#endregion
	
	#region Uniforms
	
	internal static delegate*
			unmanaged[Cdecl]<uint, sbyte*, int> glGetUniformLocation;
	internal static delegate*
			unmanaged[Cdecl]<uint, sbyte*, int> glGetAttribLocation;
	internal static delegate*
			unmanaged[Cdecl]<uint, int, float*, void> glGetUniformfv;
	internal static delegate*
			unmanaged[Cdecl]<uint, int, int*, void> glGetUniformiv;
	internal static delegate*
			unmanaged[Cdecl]<int, int, void> glUniform1I;
	internal static delegate*
			unmanaged[Cdecl]<int, float, void> glUniform1F;
	internal static delegate*
			unmanaged[Cdecl]<int, float, float, void> glUniform2F;
	internal static delegate*
			unmanaged[Cdecl]<int, float, float, float, void> glUniform3F;
	internal static delegate*
			unmanaged[Cdecl]<int, float, float, float, float, void> glUniform4F;
	internal static delegate*
			unmanaged[Cdecl]<int, int, bool, float*, void> glUniformMatrix3Fv;
	internal static delegate*
			unmanaged[Cdecl]<uint, uint, int, int*, uint*, UniformType*, sbyte*, void>
			glGetActiveUniform;
	internal static delegate*
			unmanaged[Cdecl]<int, int, bool, float*, void> glUniformMatrix4Fv;
	
	#endregion
	
	#region Textures
	
	internal static delegate*
			unmanaged[Cdecl]<TextureUnit, void> glActiveTexture;
	internal static delegate*
			unmanaged[Cdecl]<uint, uint*, void> glGenTextures;
	internal static delegate*
			unmanaged[Cdecl]<TextureTarget, uint, void> glBindTexture;
	internal static delegate*
			unmanaged[Cdecl]<TextureTarget, GLEnum, int, void> glTexParameteri;
	internal static delegate*
			unmanaged[Cdecl]<TextureTarget, void> glGenerateMipmap;
	internal static delegate*
			unmanaged[Cdecl]<TextureTarget, int, InternalFormat, uint, uint, int, PixelFormat,
			PixelType, void*, void> glTexImage2D;
	internal static delegate*
			unmanaged[Cdecl]<TextureTarget, int, int, int, uint, uint, PixelFormat, PixelType, void*
			, void> glTexSubImage2D;
	internal static delegate*
			unmanaged[Cdecl]<uint, uint*, void> glDeleteTextures;
	
	#endregion
	
	#region Vertex Arrays
	
	internal static delegate*
			unmanaged[Cdecl]<uint, uint*, void> glGenVertexArrays;
	internal static delegate*
			unmanaged[Cdecl]<uint, void> glEnableVertexAttribArray;
	internal static delegate*
			unmanaged[Cdecl]<uint, int, VertexAttribPointerType, bool, uint, void*, void>
			glVertexAttribPointer;
	internal static delegate*
			unmanaged[Cdecl]<uint, void> glBindVertexArray;
	internal static delegate*
			unmanaged[Cdecl]<uint, uint*, void> glDeleteVertexArrays;
	
	#endregion
	
	#region Framebuffers
	
	internal static delegate*
			unmanaged[Cdecl]<uint, uint*, void> glGenFramebuffers;
	internal static delegate*
			unmanaged[Cdecl]<FramebufferTarget, uint, void> glBindFramebuffer;
	internal static delegate*
			unmanaged[Cdecl]<FramebufferTarget, FramebufferAttachment, TextureTarget, uint, int,
			void> glFramebufferTexture2D;
	internal static delegate*
			unmanaged[Cdecl]<FramebufferTarget, FramebufferStatus> glCheckFramebufferStatus;
	internal static delegate*
			unmanaged[Cdecl]<uint, uint*, void> glDeleteFramebuffers;
	
	#endregion
	
	#region RenderBuffers
	
	internal static delegate*
			unmanaged[Cdecl]<uint, uint*, void> glGenRenderbuffers;
	internal static delegate*
			unmanaged[Cdecl]<RenderbufferTarget, uint, void> glBindRenderbuffer;
	internal static delegate*
			unmanaged[Cdecl]<RenderbufferTarget, InternalFormat, uint, uint, void>
			glRenderbufferStorage;
	internal static delegate*
			unmanaged[Cdecl]<FramebufferTarget, FramebufferAttachment, RenderbufferTarget, uint,
			void> glFramebufferRenderbuffer;
	internal static delegate*
			unmanaged[Cdecl]<uint, uint*, void> glDeleteRenderbuffers;
	
	#endregion
	
	#region Draws
	
	internal static delegate*
			unmanaged[Cdecl]<PrimitiveType, uint, DrawElementsType, void*, void> glDrawElements;
	internal static delegate*
			unmanaged[Cdecl]<PrimitiveType, uint, DrawElementsType, void*, int, void>
			glDrawElementsBaseVertex;
	
	#endregion
	
	private static bool isLoaded;
	
	internal static void Load()
	{
		if (isLoaded)
			return;
		
		isLoaded = true;
		glClearColor = (delegate* unmanaged[Cdecl]<float, float, float, float, void>)
				LoadProc("glClearColor");
		glClear = (delegate* unmanaged[Cdecl]<ClearBufferMask, void>)
				LoadProc("glClear");
		glViewport = (delegate* unmanaged[Cdecl]<int, int, uint, uint, void>)
				LoadProc("glViewport");
		glEnable = (delegate* unmanaged[Cdecl]<EnableCap, void>)
				LoadProc("glEnable");
		glDisable = (delegate* unmanaged[Cdecl]<EnableCap, void>)
				LoadProc("glDisable");
		glBlendFunc = (delegate* unmanaged[Cdecl]<BlendingFactor, BlendingFactor, void>)
				LoadProc("glBlendFunc");
		glGetIntegerv = (delegate* unmanaged[Cdecl]<GetPName, int*, void>)
				LoadProc("glGetIntegerv");
		glIsEnabled = (delegate* unmanaged[Cdecl]<EnableCap, byte>)
				LoadProc("glIsEnabled");
		glScissor = (delegate* unmanaged[Cdecl]<int, int, int, int, void>)
				LoadProc("glScissor");
		glPolygonMode = (delegate* unmanaged[Cdecl]<TriangleFace, PolygonMode, void>)
				LoadProc("glPolygonMode");
		glBindSampler = (delegate* unmanaged[Cdecl]<uint, uint, void>)
				LoadProc("glBindSampler");
		// Blend
		glBlendEquationSeparate =
				(delegate* unmanaged[Cdecl]<BlendEquationModeEXT, BlendEquationModeEXT, void>)
				LoadProc("glBlendEquationSeparate");
		glBlendFuncSeparate =
				(delegate* unmanaged[Cdecl]<BlendingFactor, BlendingFactor, BlendingFactor,
					BlendingFactor, void>)
				LoadProc("glBlendFuncSeparate");
		glBlendEquation = (delegate* unmanaged[Cdecl]<BlendEquationModeEXT, void>)
				LoadProc("glBlendEquation");
		// Draws
		glDrawElements = (delegate* unmanaged[Cdecl]<PrimitiveType, uint, DrawElementsType, void*,
					void>)
				LoadProc("glDrawElements");
		glDrawElementsBaseVertex =
				(delegate* unmanaged[Cdecl]<PrimitiveType, uint, DrawElementsType, void*, int, void
					>)
				LoadProc("glDrawElementsBaseVertex");
		// Buffers
		glGenBuffers = (delegate* unmanaged[Cdecl]<uint, uint*, void>)
				LoadProc("glGenBuffers");
		glBindBuffer = (delegate* unmanaged[Cdecl]<BufferTargetARB, uint, void>)
				LoadProc("glBindBuffer");
		glBufferData =
				(delegate* unmanaged[Cdecl]<BufferTargetARB, nuint, void*, BufferUsageARB, void>)
				LoadProc("glBufferData");
		glBufferSubData = (delegate* unmanaged[Cdecl]<BufferTargetARB, nint, nuint, void*, void>)
				LoadProc("glBufferSubData");
		glDeleteBuffers = (delegate* unmanaged[Cdecl]<uint, uint*, void>)
				LoadProc("glDeleteBuffers");
		glBindBufferBase = (delegate* unmanaged[Cdecl]<BufferTargetARB, uint, uint, void>)
				LoadProc("glBindBufferBase");
		// Programs
		glCreateProgram = (delegate* unmanaged[Cdecl]<uint>)
				LoadProc("glCreateProgram");
		glDeleteProgram = (delegate* unmanaged[Cdecl]<uint, void>)
				LoadProc("glDeleteProgram");
		glUseProgram = (delegate* unmanaged[Cdecl]<uint, void>)
				LoadProc("glUseProgram");
		glLinkProgram = (delegate* unmanaged[Cdecl]<uint, void>)
				LoadProc("glLinkProgram");
		glGetProgramiv = (delegate* unmanaged[Cdecl]<uint, ProgramPropertyARB, int*, void>)
				LoadProc("glGetProgramiv");
		glGetProgramInfoLog = (delegate* unmanaged[Cdecl]<uint, int, int*, sbyte*, void>)
				LoadProc("glGetProgramInfoLog");
		// Shaders
		glFlush = (delegate* unmanaged[Cdecl]<void>)
				LoadProc("glFlush");
		glCreateShader = (delegate* unmanaged[Cdecl]<ShaderType, uint>)
				LoadProc("glCreateShader");
		glShaderSource = (delegate* unmanaged[Cdecl]<uint, int, sbyte**, int*, void>)
				LoadProc("glShaderSource");
		glCompileShader = (delegate* unmanaged[Cdecl]<uint, void>)
				LoadProc("glCompileShader");
		glGetShaderiv = (delegate* unmanaged[Cdecl]<uint, ShaderParameterName, int*, void>)
				LoadProc("glGetShaderiv");
		glGetShaderInfoLog = (delegate* unmanaged[Cdecl]<uint, int, int*, sbyte*, void>)
				LoadProc("glGetShaderInfoLog");
		glAttachShader = (delegate* unmanaged[Cdecl]<uint, uint, void>)
				LoadProc("glAttachShader");
		glDetachShader = (delegate* unmanaged[Cdecl]<uint, uint, void>)
				LoadProc("glDetachShader");
		glDeleteShader = (delegate* unmanaged[Cdecl]<uint, void>)
				LoadProc("glDeleteShader");
		// Uniforms
		glGetUniformLocation = (delegate* unmanaged[Cdecl]<uint, sbyte*, int>)
				LoadProc("glGetUniformLocation");
		glGetAttribLocation = (delegate* unmanaged[Cdecl]<uint, sbyte*, int>)
				LoadProc("glGetAttribLocation");
		glGetUniformfv = (delegate* unmanaged[Cdecl]<uint, int, float*, void>)
				LoadProc("glGetUniformfv");
		glGetUniformiv = (delegate* unmanaged[Cdecl]<uint, int, int*, void>)
				LoadProc("glGetUniformiv");
		glUniform1I = (delegate* unmanaged[Cdecl]<int, int, void>)
				LoadProc("glUniform1i");
		glUniform1F = (delegate* unmanaged[Cdecl]<int, float, void>)
				LoadProc("glUniform1f");
		glUniform2F = (delegate* unmanaged[Cdecl]<int, float, float, void>)
				LoadProc("glUniform2f");
		glUniform3F = (delegate* unmanaged[Cdecl]<int, float, float, float, void>)
				LoadProc("glUniform3f");
		glUniform4F = (delegate* unmanaged[Cdecl]<int, float, float, float, float, void>)
				LoadProc("glUniform4f");
		glUniformMatrix3Fv = (delegate* unmanaged[Cdecl]<int, int, bool, float*, void>)
				LoadProc("glUniformMatrix3fv");
		glGetActiveUniform =
				(delegate* unmanaged[Cdecl]
					<uint, uint, int, int*, uint*, UniformType*, sbyte*, void>)
				LoadProc("glGetActiveUniform");
		glUniformMatrix4Fv = (delegate* unmanaged[Cdecl]<int, int, bool, float*, void>)
				LoadProc("glUniformMatrix4fv");
		// Textures
		glActiveTexture = (delegate* unmanaged[Cdecl]<TextureUnit, void>)
				LoadProc("glActiveTexture");
		glGenTextures = (delegate* unmanaged[Cdecl]<uint, uint*, void>)
				LoadProc("glGenTextures");
		glBindTexture = (delegate* unmanaged[Cdecl]<TextureTarget, uint, void>)
				LoadProc("glBindTexture");
		glTexParameteri = (delegate* unmanaged[Cdecl]<TextureTarget, GLEnum, int, void>)
				LoadProc("glTexParameteri");
		glGenerateMipmap = (delegate* unmanaged[Cdecl]<TextureTarget, void>)
				LoadProc("glGenerateMipmap");
		glTexImage2D =
				(delegate* unmanaged[Cdecl]<TextureTarget, int, InternalFormat, uint, uint, int,
					PixelFormat, PixelType, void*, void>)
				LoadProc("glTexImage2D");
		glTexSubImage2D =
				(delegate* unmanaged[Cdecl]<TextureTarget, int, int, int, uint, uint, PixelFormat,
					PixelType, void*, void>)
				LoadProc("glTexSubImage2D");
		glDeleteTextures = (delegate* unmanaged[Cdecl]<uint, uint*, void>)
				LoadProc("glDeleteTextures");
		// Vertex Arrays
		glGenVertexArrays = (delegate* unmanaged[Cdecl]<uint, uint*, void>)
				LoadProc("glGenVertexArrays");
		glEnableVertexAttribArray = (delegate* unmanaged[Cdecl]<uint, void>)
				LoadProc("glEnableVertexAttribArray");
		glVertexAttribPointer =
				(delegate* unmanaged[Cdecl]<uint, int, VertexAttribPointerType, bool, uint, void*,
					void>)
				LoadProc("glVertexAttribPointer");
		glBindVertexArray = (delegate* unmanaged[Cdecl]<uint, void>)
				LoadProc("glBindVertexArray");
		glDeleteVertexArrays = (delegate* unmanaged[Cdecl]<uint, uint*, void>)
				LoadProc("glDeleteVertexArrays");
		// Framebuffers
		glGenFramebuffers = (delegate* unmanaged[Cdecl]<uint, uint*, void>)
				LoadProc("glGenFramebuffers");
		glBindFramebuffer = (delegate* unmanaged[Cdecl]<FramebufferTarget, uint, void>)
				LoadProc("glBindFramebuffer");
		glFramebufferTexture2D =
				(delegate* unmanaged[Cdecl]<FramebufferTarget, FramebufferAttachment, TextureTarget,
					uint, int, void>)
				LoadProc("glFramebufferTexture2D");
		glCheckFramebufferStatus =
				(delegate* unmanaged[Cdecl]<FramebufferTarget, FramebufferStatus>)
				LoadProc("glCheckFramebufferStatus");
		glDeleteFramebuffers = (delegate* unmanaged[Cdecl]<uint, uint*, void>)
				LoadProc("glDeleteFramebuffers");
		// Renderbuffers
		glGenRenderbuffers = (delegate* unmanaged[Cdecl]<uint, uint*, void>)
				LoadProc("glGenRenderbuffers");
		glBindRenderbuffer = (delegate* unmanaged[Cdecl]<RenderbufferTarget, uint, void>)
				LoadProc("glBindRenderbuffer");
		glRenderbufferStorage =
				(delegate* unmanaged[Cdecl]<RenderbufferTarget, InternalFormat, uint, uint, void>)
				LoadProc("glRenderbufferStorage");
		glFramebufferRenderbuffer =
				(delegate* unmanaged[Cdecl]<FramebufferTarget, FramebufferAttachment,
					RenderbufferTarget, uint, void>)
				LoadProc("glFramebufferRenderbuffer");
		glDeleteRenderbuffers = (delegate* unmanaged[Cdecl]<uint, uint*, void>)
				LoadProc("glDeleteRenderbuffers");
	}
	
	private static void* LoadProc(string name)
	{
		fixed (byte* bName = Encoding.UTF8.GetBytes(name + "\0"))
		{
			var ptr = GlfwNative.glfwGetProcAddress((sbyte*)bName);
			if (ptr == null)
				throw new InvalidOperationException($"OpenGL function not found: {name}");
			
			return ptr;
		}
	}
}