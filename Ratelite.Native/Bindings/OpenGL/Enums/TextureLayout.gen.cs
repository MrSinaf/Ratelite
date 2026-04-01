#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum TextureLayout : int
{
	[Obsolete("Deprecated in favour of \"DepthReadOnlyStencilAttachmentExt\"")]
	LayoutDepthReadOnlyStencilAttachmentExt = 0x9530,
	[Obsolete("Deprecated in favour of \"DepthAttachmentStencilReadOnlyExt\"")]
	LayoutDepthAttachmentStencilReadOnlyExt = 0x9531,
	[Obsolete("Deprecated in favour of \"GeneralExt\"")]
	LayoutGeneralExt = 0x958D,
	[Obsolete("Deprecated in favour of \"ColorAttachmentExt\"")]
	LayoutColorAttachmentExt = 0x958E,
	[Obsolete("Deprecated in favour of \"DepthStencilAttachmentExt\"")]
	LayoutDepthStencilAttachmentExt = 0x958F,
	[Obsolete("Deprecated in favour of \"DepthStencilReadOnlyExt\"")]
	LayoutDepthStencilReadOnlyExt = 0x9590,
	[Obsolete("Deprecated in favour of \"ShaderReadOnlyExt\"")]
	LayoutShaderReadOnlyExt = 0x9591,
	[Obsolete("Deprecated in favour of \"TransferSrcExt\"")]
	LayoutTransferSrcExt = 0x9592,
	[Obsolete("Deprecated in favour of \"TransferDstExt\"")]
	LayoutTransferDstExt = 0x9593,
	
	DepthReadOnlyStencilAttachmentExt = 0x9530,
	
	DepthAttachmentStencilReadOnlyExt = 0x9531,
	
	GeneralExt = 0x958D,
	
	ColorAttachmentExt = 0x958E,
	
	DepthStencilAttachmentExt = 0x958F,
	
	DepthStencilReadOnlyExt = 0x9590,
	
	ShaderReadOnlyExt = 0x9591,
	
	TransferSrcExt = 0x9592,
	
	TransferDstExt = 0x9593
}