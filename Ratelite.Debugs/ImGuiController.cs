using System.Runtime.InteropServices;
using Ratelite.Bindings;
using Ratelite.Rendering;

namespace Ratelite.Debugs;

/*
	Récupérer d'un ancien projet "XYEngine", il a été simplement adapté à Ratelite pour fonctionner,
	mais faudrait le revoir entièrement pour le rendre plus efficace.
*/
public class ImGuiController
{
	private readonly Window window;
	
	private GProgram program = null!;
	private GTexture fontTexture = null!;
	
	private bool frameBegun;
	
	private uint vboHandle;
	private uint elementsHandle;
	private uint vertexArrayObject;
	
	public ImGuiController(Window window)
	{
		NativeLibrary.Load("cimgui.dll");
		
		this.window = window;
		ImGui.SetCurrentContext(ImGui.CreateContext());
		ImGui.StyleColorsDark();
		
		CreateDeviceResources();
		SetPerFrameImGuiData(1f / 30f);
		SetDefaultStyle();
		
		ImGui.NewFrame();
		frameBegun = true;
		
		window.keyPressed += OnKeyPressed;
		window.keyReleased += OnKeyReleased;
		window.mouseButtonPressed += OnMouseButtonPressed;
		window.mouseButtonReleased += OnMouseButtonReleased;
		window.cursorMoved += OnCursorMoved;
		window.scrolled += OnScroll;
		window.charTyped += OnCharTyped;
	}
	
	public void Render()
	{
		if (frameBegun)
		{
			frameBegun = false;
			ImGui.Render();
			RenderImDrawData(ImGui.GetDrawData());
		}
	}
	
	public void Update()
	{
		if (frameBegun)
			ImGui.Render();
		
		SetPerFrameImGuiData(Time.delta);
		frameBegun = true;
		ImGui.NewFrame();
	}
	
	private unsafe void RenderImDrawData(ImDrawDataPtr drawDataPtr)
	{
		var framebufferWidth = (int)(drawDataPtr.DisplaySize.x * drawDataPtr.FramebufferScale.x);
		var framebufferHeight = (int)(drawDataPtr.DisplaySize.y * drawDataPtr.FramebufferScale.y);
		if (framebufferWidth <= 0 || framebufferHeight <= 0)
			return;
		
		GL.GetInteger(GetPName.ActiveTexture, out var lastActiveTexture);
		GL.ActiveTexture(TextureUnit.Texture0);
		
		GL.GetInteger(GetPName.CurrentProgram, out var lastProgram);
		GL.GetInteger(GetPName.TextureBinding2D, out var lastTexture);
		
		GL.GetInteger(GetPName.SamplerBinding, out var lastSampler);
		
		GL.GetInteger(GetPName.ArrayBufferBinding, out var lastArrayBuffer);
		GL.GetInteger(GetPName.VertexArrayBinding, out var lastVertexArrayObject);
		
		Span<int> lastPolygonMode = stackalloc int[2];
		GL.GetInteger(GetPName.PolygonMode, lastPolygonMode);
		
		Span<int> lastScissorBox = stackalloc int[4];
		GL.GetInteger(GetPName.ScissorBox, lastScissorBox);
		
		GL.GetInteger(GetPName.BlendSrcRgb, out var lastBlendSrcRgb);
		GL.GetInteger(GetPName.BlendDstRgb, out var lastBlendDstRgb);
		
		GL.GetInteger(GetPName.BlendSrcAlpha, out var lastBlendSrcAlpha);
		GL.GetInteger(GetPName.BlendDstAlpha, out var lastBlendDstAlpha);
		
		GL.GetInteger(GetPName.BlendEquationRgb, out var lastBlendEquationRgb);
		GL.GetInteger(GetPName.BlendEquationAlpha, out var lastBlendEquationAlpha);
		
		var lastEnableBlend = GL.IsEnabled(EnableCap.Blend);
		var lastEnableCullFace = GL.IsEnabled(EnableCap.CullFace);
		var lastEnableDepthTest = GL.IsEnabled(EnableCap.DepthTest);
		var lastEnableStencilTest = GL.IsEnabled(EnableCap.StencilTest);
		var lastEnableScissorTest = GL.IsEnabled(EnableCap.ScissorTest);
		
		SetupRenderState(drawDataPtr);
		
		var clipOff = drawDataPtr.DisplayPos;
		var clipScale = drawDataPtr.FramebufferScale;
		
		for (var n = 0; n < drawDataPtr.CmdListsCount; n++)
		{
			var cmdListPtr = drawDataPtr.CmdLists[n];
			GL.BindBuffer(BufferTargetARB.ArrayBuffer, vboHandle);
			GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, elementsHandle);
			
			GL.BufferData(
				BufferTargetARB.ArrayBuffer,
				(uint)(cmdListPtr.VtxBuffer.Size * sizeof(ImDrawVert)),
				(void*)cmdListPtr.VtxBuffer.Data,
				BufferUsageARB.StreamDraw
			);
			GL.BufferData(
				BufferTargetARB.ElementArrayBuffer,
				(uint)(cmdListPtr.IdxBuffer.Size * sizeof(ushort)),
				(void*)cmdListPtr.IdxBuffer.Data,
				BufferUsageARB.StreamDraw
			);
			
			for (var cmdI = 0; cmdI < cmdListPtr.CmdBuffer.Size; cmdI++)
			{
				var cmdPtr = cmdListPtr.CmdBuffer[cmdI];
				
				if (cmdPtr.UserCallback != IntPtr.Zero)
					throw new NotSupportedException();
				
				var clipRect = new Vector4
				{
					x = (cmdPtr.ClipRect.x - clipOff.x) * clipScale.x,
					y = (cmdPtr.ClipRect.y - clipOff.y) * clipScale.y,
					z = (cmdPtr.ClipRect.z - clipOff.x) * clipScale.x,
					w = (cmdPtr.ClipRect.w - clipOff.y) * clipScale.y
				};
				
				if (clipRect.x < framebufferWidth && clipRect.y < framebufferHeight &&
					clipRect is { z: >= 0.0f, w: >= 0.0f })
				{
					GL.Scissor(
						(int)clipRect.x,
						(int)(framebufferHeight - clipRect.w),
						(uint)(clipRect.z - clipRect.x),
						(uint)(clipRect.w - clipRect.y)
					);
					GL.BindTexture(TextureTarget.Texture2D, (uint)cmdPtr.TextureId);
					GL.DrawElementsBaseVertex(
						PrimitiveType.Triangles,
						cmdPtr.ElemCount,
						DrawElementsType.UnsignedShort,
						(void*)(cmdPtr.IdxOffset * sizeof(ushort)),
						(int)cmdPtr.VtxOffset
					);
				}
			}
		}
		
		GL.DeleteVertexArray(vertexArrayObject);
		vertexArrayObject = 0;
		
		GL.UseProgram((uint)lastProgram);
		GL.BindTexture(TextureTarget.Texture2D, (uint)lastTexture);
		
		GL.BindSampler(0, (uint)lastSampler);
		
		GL.ActiveTexture((TextureUnit)lastActiveTexture);
		
		GL.BindVertexArray((uint)lastVertexArrayObject);
		
		GL.BindBuffer(BufferTargetARB.ArrayBuffer, (uint)lastArrayBuffer);
		GL.BlendEquationSeparate(
			(BlendEquationModeEXT)lastBlendEquationRgb,
			(BlendEquationModeEXT)lastBlendEquationAlpha
		);
		GL.BlendFuncSeparate(
			(BlendingFactor)lastBlendSrcRgb,
			(BlendingFactor)lastBlendDstRgb,
			(BlendingFactor)lastBlendSrcAlpha,
			(BlendingFactor)lastBlendDstAlpha
		);
		
		var lastEnablePrimitiveRestart = GL.IsEnabled(EnableCap.PrimitiveRestart);
		
		if (lastEnableBlend)
			GL.Enable(EnableCap.Blend);
		else
			GL.Disable(EnableCap.Blend);
		
		if (lastEnableCullFace)
			GL.Enable(EnableCap.CullFace);
		else
			GL.Disable(EnableCap.CullFace);
		
		if (lastEnableDepthTest)
			GL.Enable(EnableCap.DepthTest);
		else
			GL.Disable(EnableCap.DepthTest);
		
		if (lastEnableStencilTest)
			GL.Enable(EnableCap.StencilTest);
		else
			GL.Disable(EnableCap.StencilTest);
		
		if (lastEnableScissorTest)
			GL.Enable(EnableCap.ScissorTest);
		else
			GL.Disable(EnableCap.ScissorTest);
		
		if (lastEnablePrimitiveRestart)
			GL.Enable(EnableCap.PrimitiveRestart);
		else
			GL.Disable(EnableCap.PrimitiveRestart);
		
		GL.PolygonMode(TriangleFace.FrontAndBack, (PolygonMode)lastPolygonMode[0]);
		GL.Scissor(
			lastScissorBox[0],
			lastScissorBox[1],
			(uint)lastScissorBox[2],
			(uint)lastScissorBox[3]
		);
	}
	
	private unsafe void SetupRenderState(ImDrawDataPtr drawDataPtr)
	{
		GL.Enable(EnableCap.Blend);
		GL.BlendEquation(BlendEquationModeEXT.FuncAdd);
		GL.BlendFuncSeparate(
			BlendingFactor.SrcAlpha,
			BlendingFactor.OneMinusSrcAlpha,
			BlendingFactor.One,
			BlendingFactor.OneMinusSrcAlpha
		);
		GL.Disable(EnableCap.CullFace);
		GL.Disable(EnableCap.DepthTest);
		GL.Disable(EnableCap.StencilTest);
		GL.Enable(EnableCap.ScissorTest);
		GL.Disable(EnableCap.PrimitiveRestart);
		GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Fill);
		
		GL.Viewport(0, 0, (uint)window.frameBufferSize.x, (uint)window.frameBufferSize.y);
		GL.UseProgram(program.handle);
		
		var left = drawDataPtr.DisplayPos.x;
		var right = drawDataPtr.DisplayPos.x + drawDataPtr.DisplaySize.x;
		var top = drawDataPtr.DisplayPos.y;
		var bottom = drawDataPtr.DisplayPos.y + drawDataPtr.DisplaySize.y;
		
		Span<float> orthoProjection =
		[
			2.0f / (right - left), 0.0f, 0.0f, 0.0f,
			0.0f, 2.0f / (top - bottom), 0.0f, 0.0f,
			0.0f, 0.0f, -1.0f, 0.0f,
			(right + left) / (left - right), (top + bottom) / (bottom - top), 0.0f, 1.0f
		];
		
		program.SetUniform("Texture", 0);
		program.GetUniformLocation("ProjMtx", out var locationProjMtx);
		fixed (float* p = orthoProjection)
		{
			GL.UniformMatrix4(locationProjMtx, 1, false, p);
		}
		
		GL.BindSampler(0, 0);
		
		vertexArrayObject = GL.GenVertexArray();
		GL.BindVertexArray(vertexArrayObject);
		GL.BindBuffer(BufferTargetARB.ArrayBuffer, vboHandle);
		GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, elementsHandle);
		
		var attribLocationVtxPos = GL.GetAttribLocation(program.handle, "Position");
		var attribLocationVtxUV = GL.GetAttribLocation(program.handle, "UV");
		var attribLocationVtxColor = GL.GetAttribLocation(program.handle, "Color");
		
		GL.EnableVertexAttribArray((uint)attribLocationVtxPos);
		GL.EnableVertexAttribArray((uint)attribLocationVtxUV);
		GL.EnableVertexAttribArray((uint)attribLocationVtxColor);
		GL.VertexAttribPointer(
			(uint)attribLocationVtxPos,
			2,
			VertexAttribPointerType.Float,
			false,
			(uint)sizeof
					(ImDrawVert),
			(void*)0
		);
		GL.VertexAttribPointer(
			(uint)attribLocationVtxUV,
			2,
			VertexAttribPointerType.Float,
			false,
			(uint)sizeof(ImDrawVert),
			(void*)8
		);
		GL.VertexAttribPointer(
			(uint)attribLocationVtxColor,
			4,
			VertexAttribPointerType.UnsignedByte,
			true,
			(uint)
			sizeof(ImDrawVert),
			(void*)16
		);
	}
	
	private static void OnKeyPressed(Key keycode, int scancode)
		=> OnKeyEvent(keycode, scancode, true);
	
	private static void OnKeyReleased(Key keycode, int scancode)
		=> OnKeyEvent(keycode, scancode, false);
	
	private static void OnCharTyped(char c)
		=> ImGui.GetIO().AddInputCharacter(c);
	
	private static void OnScroll(Vector2Int delta)
		=> ImGui.GetIO().MouseWheel += delta.y;
	
	private static void OnCursorMoved(Vector2 delta)
		=> ImGui.GetIO().MousePos = new Vector2(
			Window.current.cursorPosition.x,
			Window.current.size.y - Window.current.cursorPosition.y
		);
	
	private static void OnMouseButtonPressed(MouseButton button)
		=> OnMouseButtonEvent(button, true);
	
	private static void OnMouseButtonReleased(MouseButton button)
		=> OnMouseButtonEvent(button, false);
	
	private static void OnMouseButtonEvent(MouseButton button, bool pressed)
	{
		var io = ImGui.GetIO();
		switch (button)
		{
			case MouseButton.Left:
				io.MouseDown[0] = pressed;
				break;
			case MouseButton.Right:
				io.MouseDown[1] = pressed;
				break;
			case MouseButton.Middle:
				io.MouseDown[2] = pressed;
				break;
		}
	}
	
	private static void OnKeyEvent(Key key, int scancode, bool pressed)
	{
		var io = ImGui.GetIO();
		var imGuiKey = TranslateInputKeyToImGuiKey(key);
		io.AddKeyEvent(imGuiKey, pressed);
		io.SetKeyEventNativeData(imGuiKey, (int)key, scancode);
		
		switch (key)
		{
			case Key.LeftCtrl or Key.RightCtrl:
				io.KeyCtrl = pressed;
				break;
			case Key.LeftAlt or Key.RightAlt:
				io.KeyAlt = pressed;
				break;
			case Key.LeftShift or Key.RightShift:
				io.KeyShift = pressed;
				break;
			case Key.LeftSuper or Key.RightSuper:
				io.KeySuper = pressed;
				break;
		}
	}
	
	private void CreateDeviceResources()
	{
		const string vertexSource =
				"""
				#version 330 core
				
				layout (location = 0) in vec2 Position;
				layout (location = 1) in vec2 UV;
				layout (location = 2) in vec4 Color;
				uniform mat4 ProjMtx;
				out vec2 Frag_UV;
				out vec4 Frag_Color;
				void main()
				{
				    Frag_UV = UV;
				    Frag_Color = Color;
				    gl_Position = ProjMtx * vec4(Position.xy,0,1);
				}
				""";
		
		const string fragmentSource =
				"""
				#version 330 core
				
				in vec2 Frag_UV;
				in vec4 Frag_Color;
				uniform sampler2D Texture;
				layout (location = 0) out vec4 Out_Color;
				void main()
				{
				    Out_Color = Frag_Color * texture(Texture, Frag_UV.st);
				}
				""";
		
		program = new GProgram();
		program.Compile(vertexSource, fragmentSource);
		
		vboHandle = GL.GenBuffer();
		elementsHandle = GL.GenBuffer();
		
		var io = ImGui.GetIO();
		io.Fonts.Clear();
		using var memoryStream = new MemoryStream();
		GetType().Assembly
				 .GetManifestResourceStream("Ratelite.Debugs.assets.fonts.compliance-sans.ttf")
				 .CopyTo(memoryStream);
		var fontBytes = memoryStream.ToArray();
		
		unsafe
		{
			fixed (byte* pFont = fontBytes)
			{
				io.Fonts.AddFontFromMemoryTTF((IntPtr)pFont, fontBytes.Length, 14f);
			}
		}
		
		io.Fonts.GetTexDataAsRGBA32(
			out IntPtr pixels,
			out var width,
			out var height,
			out var bytesPerPixel
		);
		
		var data = new byte[width * height * bytesPerPixel];
		Marshal.Copy(pixels, data, 0, width * height * bytesPerPixel);
		
		var colors = new Color[width * height];
		for (var i = 0; i < width * height; i++)
		{
			var baseIndex = i * bytesPerPixel;
			colors[i] = new Color(
				data[baseIndex],
				data[baseIndex + 1],
				data[baseIndex + 2],
				data[baseIndex + 3]
			);
		}
		
		fontTexture = new GTexture();
		fontTexture.SetImage2D((uint)width, (uint)height, colors);
		fontTexture.SetMinFilter(TextureMin.Linear);
		fontTexture.SetMagFilter(TextureMag.Linear);
		io.Fonts.SetTexID((IntPtr)fontTexture.handle);
	}
	
	private void SetPerFrameImGuiData(float deltaSeconds)
	{
		var io = ImGui.GetIO();
		var resolution = window.size;
		
		io.DisplaySize = resolution;
		if (resolution is { x: > 0, y: > 0 })
			io.DisplayFramebufferScale = new Vector2(
				(float)window.frameBufferSize.x / resolution.x,
				(float)window.frameBufferSize.y / resolution.y
			);
		
		io.DeltaTime = deltaSeconds;
	}
	
	private static void SetDefaultStyle()
	{
		var style = ImGui.GetStyle();
		style.WindowPadding = new Vector2(8);
		style.FramePadding = new Vector2(4, 3);
		style.ItemSpacing = new Vector2(8, 4);
		style.ItemInnerSpacing = new Vector2(4);
		style.TouchExtraPadding = new Vector2();
		style.IndentSpacing = 21;
		style.ScrollbarSize = 14;
		style.GrabMinSize = 12;
		
		style.WindowBorderSize = 1;
		style.ChildBorderSize = 0;
		style.PopupBorderSize = 0;
		style.FrameBorderSize = 0;
		style.TabBorderSize = 0;
		style.TabBarBorderSize = 1;
		style.TabBarOverlineSize = 2;
		
		style.WindowRounding = 8F;
		style.ChildRounding = 0;
		style.FrameRounding = 4F;
		style.PopupRounding = 0;
		style.ScrollbarRounding = 9F;
		style.GrabRounding = 0;
		style.TabRounding = 2;
		
		style.WindowMenuButtonPosition = ImGuiDir.None;
		style.WindowTitleAlign = new Vector2(1, 0.5F);
		
		var colors = style.Colors;
		colors[(int)ImGuiCol.Text] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
		colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.50f, 0.50f, 0.50f, 1.00f);
		colors[(int)ImGuiCol.WindowBg] = new Vector4(0.06f, 0.07f, 0.08f, 1.00f);
		colors[(int)ImGuiCol.ChildBg] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
		colors[(int)ImGuiCol.PopupBg] = new Vector4(0.08f, 0.08f, 0.08f, 0.94f);
		colors[(int)ImGuiCol.Border] = new Vector4(0.43f, 0.43f, 0.50f, 0.50f);
		colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
		colors[(int)ImGuiCol.FrameBg] = new Vector4(0.25f, 0.20f, 0.31f, 0.54f);
		colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.41f, 0.29f, 0.55f, 0.40f);
		colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.37f, 0.28f, 0.53f, 0.63f);
		colors[(int)ImGuiCol.TitleBg] = new Vector4(0.04f, 0.04f, 0.04f, 1.00f);
		colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.12f, 0.18f, 0.28f, 1.00f);
		colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.00f, 0.00f, 0.00f, 0.51f);
		colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f);
		colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.02f, 0.02f, 0.02f, 0.53f);
		colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.26f, 0.59f, 0.98f, 0.55f);
		colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.75f);
		colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.26f, 0.59f, 0.98f, 0.95f);
		colors[(int)ImGuiCol.CheckMark] = new Vector4(0.26f, 0.59f, 0.98f, 1.00f);
		colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.26f, 0.59f, 0.98f, 0.70f);
		colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.26f, 0.59f, 0.98f, 1.00f);
		colors[(int)ImGuiCol.Button] = new Vector4(0.26f, 0.59f, 0.98f, 0.25f);
		colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.45f);
		colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.26f, 0.59f, 0.98f, 0.65f);
		colors[(int)ImGuiCol.Header] = new Vector4(0.26f, 0.59f, 0.98f, 0.22f);
		colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.40f);
		colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.26f, 0.59f, 0.98f, 0.55f);
		colors[(int)ImGuiCol.Separator] = new Vector4(0.43f, 0.43f, 0.50f, 0.50f);
		colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.60f);
		colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.26f, 0.59f, 0.98f, 0.85f);
		colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.26f, 0.59f, 0.98f, 0.18f);
		colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.45f);
		colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.26f, 0.59f, 0.98f, 0.75f);
		colors[(int)ImGuiCol.TabHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.55f);
		colors[(int)ImGuiCol.Tab] = new Vector4(0.12f, 0.14f, 0.18f, 0.86f);
		colors[(int)ImGuiCol.TabSelected] = new Vector4(0.18f, 0.22f, 0.30f, 1.00f);
		colors[(int)ImGuiCol.TabSelectedOverline] = new Vector4(0.26f, 0.59f, 0.98f, 1.00f);
		colors[(int)ImGuiCol.TabDimmed] = new Vector4(0.07f, 0.10f, 0.15f, 0.97f);
		colors[(int)ImGuiCol.TabDimmedSelected] = new Vector4(0.14f, 0.18f, 0.25f, 1.00f);
		colors[(int)ImGuiCol.TabDimmedSelectedOverline] = new Vector4(0.50f, 0.50f, 0.50f, 0.00f);
		colors[(int)ImGuiCol.DockingPreview] = new Vector4(0.26f, 0.59f, 0.98f, 0.45f);
		colors[(int)ImGuiCol.DockingEmptyBg] = new Vector4(0.20f, 0.20f, 0.20f, 1.00f);
		colors[(int)ImGuiCol.PlotLines] = new Vector4(0.61f, 0.61f, 0.61f, 1.00f);
		colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.00f, 0.43f, 0.35f, 1.00f);
		colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.90f, 0.70f, 0.00f, 1.00f);
		colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.00f, 0.60f, 0.00f, 1.00f);
		colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(0.19f, 0.19f, 0.20f, 1.00f);
		colors[(int)ImGuiCol.TableBorderStrong] = new Vector4(0.31f, 0.31f, 0.35f, 1.00f);
		colors[(int)ImGuiCol.TableBorderLight] = new Vector4(0.23f, 0.23f, 0.25f, 1.00f);
		colors[(int)ImGuiCol.TableRowBg] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
		colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1.00f, 1.00f, 1.00f, 0.06f);
		colors[(int)ImGuiCol.TextLink] = new Vector4(0.57f, 0.26f, 0.98f, 1.00f);
		colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.26f, 0.59f, 0.98f, 0.35f);
		colors[(int)ImGuiCol.DragDropTarget] = new Vector4(1.00f, 1.00f, 0.00f, 0.90f);
		colors[(int)ImGuiCol.NavCursor] = new Vector4(0.26f, 0.59f, 0.98f, 1.00f);
		colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.00f, 1.00f, 1.00f, 0.70f);
		colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.80f, 0.80f, 0.80f, 0.20f);
		colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.80f, 0.80f, 0.80f, 0.35f);
	}
	
	private static ImGuiKey TranslateInputKeyToImGuiKey(Key key) => key switch
	{
		Key.Tab            => ImGuiKey.Tab,
		Key.Left           => ImGuiKey.LeftArrow,
		Key.Right          => ImGuiKey.RightArrow,
		Key.Up             => ImGuiKey.UpArrow,
		Key.Down           => ImGuiKey.DownArrow,
		Key.PageUp         => ImGuiKey.PageUp,
		Key.PageDown       => ImGuiKey.PageDown,
		Key.Home           => ImGuiKey.Home,
		Key.End            => ImGuiKey.End,
		Key.Insert         => ImGuiKey.Insert,
		Key.Delete         => ImGuiKey.Delete,
		Key.Backspace      => ImGuiKey.Backspace,
		Key.Space          => ImGuiKey.Space,
		Key.Enter          => ImGuiKey.Enter,
		Key.Escape         => ImGuiKey.Escape,
		Key.Apostrophe     => ImGuiKey.Apostrophe,
		Key.Comma          => ImGuiKey.Comma,
		Key.Minus          => ImGuiKey.Minus,
		Key.Period         => ImGuiKey.Period,
		Key.Slash          => ImGuiKey.Slash,
		Key.Semicolon      => ImGuiKey.Semicolon,
		Key.Equal          => ImGuiKey.Equal,
		Key.LeftBracket    => ImGuiKey.LeftBracket,
		Key.BackSlash      => ImGuiKey.Backslash,
		Key.RightBracket   => ImGuiKey.RightBracket,
		Key.GraveAccent    => ImGuiKey.GraveAccent,
		Key.CapsLock       => ImGuiKey.CapsLock,
		Key.ScrollLock     => ImGuiKey.ScrollLock,
		Key.NumLock        => ImGuiKey.NumLock,
		Key.PrintScreen    => ImGuiKey.PrintScreen,
		Key.Pause          => ImGuiKey.Pause,
		Key.Keypad0        => ImGuiKey.Keypad0,
		Key.Keypad1        => ImGuiKey.Keypad1,
		Key.Keypad2        => ImGuiKey.Keypad2,
		Key.Keypad3        => ImGuiKey.Keypad3,
		Key.Keypad4        => ImGuiKey.Keypad4,
		Key.Keypad5        => ImGuiKey.Keypad5,
		Key.Keypad6        => ImGuiKey.Keypad6,
		Key.Keypad7        => ImGuiKey.Keypad7,
		Key.Keypad8        => ImGuiKey.Keypad8,
		Key.Keypad9        => ImGuiKey.Keypad9,
		Key.KeypadDecimal  => ImGuiKey.KeypadDecimal,
		Key.KeypadDivide   => ImGuiKey.KeypadDivide,
		Key.KeypadMultiply => ImGuiKey.KeypadMultiply,
		Key.KeypadSubtract => ImGuiKey.KeypadSubtract,
		Key.KeypadAdd      => ImGuiKey.KeypadAdd,
		Key.KeypadEnter    => ImGuiKey.KeypadEnter,
		Key.KeypadEqual    => ImGuiKey.KeypadEqual,
		Key.LeftShift      => ImGuiKey.LeftShift,
		Key.LeftCtrl       => ImGuiKey.LeftCtrl,
		Key.LeftAlt        => ImGuiKey.LeftAlt,
		Key.LeftSuper      => ImGuiKey.LeftSuper,
		Key.RightShift     => ImGuiKey.RightShift,
		Key.RightCtrl      => ImGuiKey.RightCtrl,
		Key.RightAlt       => ImGuiKey.RightAlt,
		Key.RightSuper     => ImGuiKey.RightSuper,
		Key.Menu           => ImGuiKey.Menu,
		Key.Num0           => ImGuiKey._0,
		Key.Num1           => ImGuiKey._1,
		Key.Num2           => ImGuiKey._2,
		Key.Num3           => ImGuiKey._3,
		Key.Num4           => ImGuiKey._4,
		Key.Num5           => ImGuiKey._5,
		Key.Num6           => ImGuiKey._6,
		Key.Num7           => ImGuiKey._7,
		Key.Num8           => ImGuiKey._8,
		Key.Num9           => ImGuiKey._9,
		Key.A              => ImGuiKey.A,
		Key.B              => ImGuiKey.B,
		Key.C              => ImGuiKey.C,
		Key.D              => ImGuiKey.D,
		Key.E              => ImGuiKey.E,
		Key.F              => ImGuiKey.F,
		Key.G              => ImGuiKey.G,
		Key.H              => ImGuiKey.H,
		Key.I              => ImGuiKey.I,
		Key.J              => ImGuiKey.J,
		Key.K              => ImGuiKey.K,
		Key.L              => ImGuiKey.L,
		Key.M              => ImGuiKey.M,
		Key.N              => ImGuiKey.N,
		Key.O              => ImGuiKey.O,
		Key.P              => ImGuiKey.P,
		Key.Q              => ImGuiKey.Q,
		Key.R              => ImGuiKey.R,
		Key.S              => ImGuiKey.S,
		Key.T              => ImGuiKey.T,
		Key.U              => ImGuiKey.U,
		Key.V              => ImGuiKey.V,
		Key.W              => ImGuiKey.W,
		Key.X              => ImGuiKey.X,
		Key.Y              => ImGuiKey.Y,
		Key.Z              => ImGuiKey.Z,
		Key.F1             => ImGuiKey.F1,
		Key.F2             => ImGuiKey.F2,
		Key.F3             => ImGuiKey.F3,
		Key.F4             => ImGuiKey.F4,
		Key.F5             => ImGuiKey.F5,
		Key.F6             => ImGuiKey.F6,
		Key.F7             => ImGuiKey.F7,
		Key.F8             => ImGuiKey.F8,
		Key.F9             => ImGuiKey.F9,
		Key.F10            => ImGuiKey.F10,
		Key.F11            => ImGuiKey.F11,
		Key.F12            => ImGuiKey.F12,
		Key.F13            => ImGuiKey.F13,
		Key.F14            => ImGuiKey.F14,
		Key.F15            => ImGuiKey.F15,
		Key.F16            => ImGuiKey.F16,
		Key.F17            => ImGuiKey.F17,
		Key.F18            => ImGuiKey.F18,
		Key.F19            => ImGuiKey.F19,
		Key.F20            => ImGuiKey.F20,
		Key.F21            => ImGuiKey.F21,
		Key.F22            => ImGuiKey.F22,
		Key.F23            => ImGuiKey.F23,
		Key.F24            => ImGuiKey.F24,
		Key.World1         => ImGuiKey.None,
		Key.World2         => ImGuiKey.None,
		Key.F25            => ImGuiKey.None,
		_                  => throw new NotSupportedException()
	};
}