// --------------------------------------------------------------------------------------------
// Version: MPL 1.1/GPL 2.0/LGPL 2.1
// 
// The contents of this file are subject to the Mozilla Public License Version
// 1.1 (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at
// http://www.mozilla.org/MPL/
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
// for the specific language governing rights and limitations under the
// License.
// 
// <remarks>
// Generated by IDLImporter from file nsIDOMSVGAnimatedPathData.idl
// 
// You should use these interfaces when you access the COM objects defined in the mentioned
// IDL/IDH file.
// </remarks>
// --------------------------------------------------------------------------------------------
namespace Skybound.Gecko
{
	using System;
	using System.Runtime.InteropServices;
	using System.Runtime.InteropServices.ComTypes;
	using System.Runtime.CompilerServices;
	using System.Windows.Forms;
	
	
	/// <summary>nsIDOMSVGAnimatedPathData </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("6ef2b400-dbf4-4c12-8787-fe15caac5648")]
	public interface nsIDOMSVGAnimatedPathData
	{
		
		/// <summary>Member GetPathSegListAttribute </summary>
		/// <returns>A nsIDOMSVGPathSegList</returns>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMSVGPathSegList GetPathSegListAttribute();
		
		/// <summary>Member GetNormalizedPathSegListAttribute </summary>
		/// <returns>A nsIDOMSVGPathSegList</returns>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMSVGPathSegList GetNormalizedPathSegListAttribute();
		
		/// <summary>Member GetAnimatedPathSegListAttribute </summary>
		/// <returns>A nsIDOMSVGPathSegList</returns>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMSVGPathSegList GetAnimatedPathSegListAttribute();
		
		/// <summary>Member GetAnimatedNormalizedPathSegListAttribute </summary>
		/// <returns>A nsIDOMSVGPathSegList</returns>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMSVGPathSegList GetAnimatedNormalizedPathSegListAttribute();
	}
}
