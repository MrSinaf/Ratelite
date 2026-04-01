using System.Collections;
using System.Runtime.InteropServices;

namespace Ratelite.Bindings;

internal sealed class AnimatedGifEnumerator : IEnumerator<AnimatedFrameResult>
{
	private readonly StbImage.StbiContext context;
	private StbImage.stbi__gif? gif;
	private readonly ColorComponents colorComponents;
	
	public AnimatedFrameResult Current { get; private set; } = null!;
	object IEnumerator.Current => Current;
	
	public AnimatedGifEnumerator(Stream input, ColorComponents colorComponents)
	{
        ArgumentNullException.ThrowIfNull(input);

        context = new StbImage.StbiContext(input);
		
		if (StbImage.stbi__gif_test(context) == 0)
			throw new Exception("Input stream is not GIF file.");
		
		gif = new StbImage.stbi__gif();
		this.colorComponents = colorComponents;
	}
	
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
	
	public unsafe bool MoveNext()
	{
		if (gif == null) return false;
		
		// Read next frame
		int ccomp;
		byte two_back;
		var result = StbImage.stbi__gif_load_next(
			context,
			gif,
			&ccomp,
			(int)colorComponents,
			&two_back
		);
		if (result == null) return false;
		
		if (Current == null)
		{
			Current = new AnimatedFrameResult
			{
				width = gif.w,
				height = gif.h,
				sourceComp = (ColorComponents)ccomp,
				comp = colorComponents == ColorComponents.Default
						? (ColorComponents)ccomp
						: colorComponents
			};
			
			Current.data = new byte[Current.width * Current.height * (int)Current.comp];
		}
		
		Current.delayInMs = gif.delay;
		
		Marshal.Copy(new IntPtr(result), Current.data, 0, Current.data.Length);
		
		return true;
	}
	
	public void Reset()
	{
		// throw new NotImplementedException();
	}
	
	~AnimatedGifEnumerator()
	{
		Dispose(false);
	}
	
	private unsafe void Dispose(bool disposing)
	{
		if (gif != null)
		{
			if (gif._out_ != null)
			{
				CRuntime.free(gif._out_);
				gif._out_ = null;
			}
			
			if (gif.history != null)
			{
				CRuntime.free(gif.history);
				gif.history = null;
			}
			
			if (gif.background != null)
			{
				CRuntime.free(gif.background);
				gif.background = null;
			}
			
			gif = null;
		}
	}
}

internal class AnimatedGifEnumerable : IEnumerable<AnimatedFrameResult>
{
	private readonly Stream _input;
	private readonly ColorComponents _colorComponents;
	
	public AnimatedGifEnumerable(Stream input, ColorComponents colorComponents)
	{
		_input = input;
		_colorComponents = colorComponents;
	}
	
	public ColorComponents ColorComponents => _colorComponents;
	
	public IEnumerator<AnimatedFrameResult> GetEnumerator()
		=> new AnimatedGifEnumerator(_input, ColorComponents);
	
	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();
}