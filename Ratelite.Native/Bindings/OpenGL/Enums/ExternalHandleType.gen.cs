#pragma warning disable 1591

namespace Ratelite.Bindings;

public enum ExternalHandleType : int
{
	[Obsolete("Deprecated in favour of \"OpaqueFDExt\"")]
	HandleTypeOpaqueFDExt = 0x9586,
	[Obsolete("Deprecated in favour of \"OpaqueWin32Ext\"")]
	HandleTypeOpaqueWin32Ext = 0x9587,
	[Obsolete("Deprecated in favour of \"OpaqueWin32KmtExt\"")]
	HandleTypeOpaqueWin32KmtExt = 0x9588,
	[Obsolete("Deprecated in favour of \"D3D12TilepoolExt\"")]
	HandleTypeD3D12TilepoolExt = 0x9589,
	[Obsolete("Deprecated in favour of \"D3D12ResourceExt\"")]
	HandleTypeD3D12ResourceExt = 0x958A,
	[Obsolete("Deprecated in favour of \"D3D11ImageExt\"")]
	HandleTypeD3D11ImageExt = 0x958B,
	[Obsolete("Deprecated in favour of \"D3D11ImageKmtExt\"")]
	HandleTypeD3D11ImageKmtExt = 0x958C,
	[Obsolete("Deprecated in favour of \"D3D12FenceExt\"")]
	HandleTypeD3D12FenceExt = 0x9594,
	
	OpaqueFDExt = 0x9586,
	
	OpaqueWin32Ext = 0x9587,
	
	OpaqueWin32KmtExt = 0x9588,
	
	D3D12TilepoolExt = 0x9589,
	
	D3D12ResourceExt = 0x958A,
	
	D3D11ImageExt = 0x958B,
	
	D3D11ImageKmtExt = 0x958C,
	
	D3D12FenceExt = 0x9594
}