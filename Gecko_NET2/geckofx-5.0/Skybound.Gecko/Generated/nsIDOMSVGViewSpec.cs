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
// Generated by IDLImporter from file nsIDOMSVGViewSpec.idl
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
	
	
	/// <summary>
    ///The SVG DOM makes use of multiple interface inheritance.
    ///        Since XPCOM only supports single interface inheritance,
    ///        the best thing that we can do is to promise that whenever
    ///        an object implements _this_ interface it will also
    ///        implement the following interfaces. (We then have to QI to
    ///        hop between them.)
    ///
    ///    nsIDOMSVGFitToViewBox </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("ede34b03-57b6-45bf-a259-3550b5697286")]
	public interface nsIDOMSVGViewSpec : nsIDOMSVGZoomAndPan
	{
		
		/// <summary>
        /// Zoom and Pan Types
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new ushort GetZoomAndPanAttribute();
		
		/// <summary>
        /// Zoom and Pan Types
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void SetZoomAndPanAttribute(ushort aZoomAndPan);
		
		/// <summary>
        ///The SVG DOM makes use of multiple interface inheritance.
        ///        Since XPCOM only supports single interface inheritance,
        ///        the best thing that we can do is to promise that whenever
        ///        an object implements _this_ interface it will also
        ///        implement the following interfaces. (We then have to QI to
        ///        hop between them.)
        ///
        ///    nsIDOMSVGFitToViewBox </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMSVGTransformList GetTransformAttribute();
		
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMSVGElement GetViewTargetAttribute();
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetViewBoxStringAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAString aViewBoxString);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetPreserveAspectRatioStringAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAString aPreserveAspectRatioString);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetTransformStringAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAString aTransformString);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetViewTargetStringAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAString aViewTargetString);
	}
}
