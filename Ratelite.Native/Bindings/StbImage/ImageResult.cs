using System.Runtime.InteropServices;

namespace Ratelite.Bindings;

public class ImageResult
{
	public int width { get; set; }
	public int height { get; set; }
	public ColorComponents sourceComp { get; set; }
	public ColorComponents comp { get; set; }
	public byte[] data { get; set; } = null!;
	
	public static unsafe ImageResult FromStream(
		Stream stream,
		ColorComponents requiredComponents = ColorComponents.Default
	)
	{
		byte* result = null;
		
		try
		{
			int x, y, comp;
			
			var context = new StbImage.StbiContext(stream);
			
			result = StbImage.stbi__load_and_postprocess_8bit(
				context,
				&x,
				&y,
				&comp,
				(int)requiredComponents
			);
			
			return FromResult(result, x, y, (ColorComponents)comp, requiredComponents);
		}
		finally
		{
			if (result != null)
				CRuntime.free(result);
		}
	}
	
	private static unsafe ImageResult FromResult(
		byte* result,
		int width,
		int height,
		ColorComponents comp,
		ColorComponents reqComp
	)
	{
		if (result == null)
			throw new InvalidOperationException(StbImage.gFailurReason);
		
		var image = new ImageResult
		{
			width = width,
			height = height,
			sourceComp = comp,
			comp = reqComp == ColorComponents.Default ? comp : reqComp
		};
		
		// Convert to array
		image.data = new byte[width * height * (int)image.comp];
		Marshal.Copy(new IntPtr(result), image.data, 0, image.data.Length);
		
		return image;
	}
	
	public static ImageResult FromMemory(
		byte[] data,
		ColorComponents requiredComponents = ColorComponents.Default
	)
	{
		using var stream = new MemoryStream(data);
		return FromStream(stream, requiredComponents);
	}
	
	public static IEnumerable<AnimatedFrameResult> AnimatedGifFramesFromStream(
		Stream stream,
		ColorComponents requiredComponents = ColorComponents.Default
	)
		=> new AnimatedGifEnumerable(stream, requiredComponents);
}