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
// Generated by IDLImporter from file nsIDOMSVGAnimatedAngle.idl
// 
// You should use these interfaces when you access the COM objects defined in the mentioned
// IDL/IDH file.
// </remarks>
// --------------------------------------------------------------------------------------------
namespace Gecko
{
	using System;
	using System.Runtime.InteropServices;
	using System.Runtime.InteropServices.ComTypes;
	using System.Runtime.CompilerServices;
	
	
	/// <summary>
    /// The nsIDOMSVGAnimatedAngle interface is the interface to an SVG
    /// animated angle.
    ///
    /// For more information on this interface please see
    /// http://www.w3.org/TR/SVG11/types.html#InterfaceSVGAnimatedAngle
    ///
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("c6ab8b9e-32db-464a-ae33-8691d44bc60a")]
	public interface nsIDOMSVGAnimatedAngle
	{
		
		/// <summary>
        /// The nsIDOMSVGAnimatedAngle interface is the interface to an SVG
        /// animated angle.
        ///
        /// For more information on this interface please see
        /// http://www.w3.org/TR/SVG11/types.html#InterfaceSVGAnimatedAngle
        ///
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMSVGAngle GetBaseValAttribute();
		
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMSVGAngle GetAnimValAttribute();
	}
}
