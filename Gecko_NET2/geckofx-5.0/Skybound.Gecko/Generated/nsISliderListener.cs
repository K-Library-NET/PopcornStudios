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
// Generated by IDLImporter from file nsISliderListener.idl
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
    /// Used for <scale> to listen to slider changes to avoid mutation listeners
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("e5b3074e-ee18-4538-83b9-2487d90a2a34")]
	public interface nsISliderListener
	{
		
		/// <summary>
        /// Called when the current, minimum or maximum value has been changed to
        /// newValue. The which parameter will either be 'curpos', 'minpos' or 'maxpos'.
        /// If userChanged is true, then the user changed ths slider, otherwise it
        /// was changed via some other means.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ValueChanged([MarshalAs(UnmanagedType.LPStruct)] nsAString which, int newValue, [MarshalAs(UnmanagedType.Bool)] bool userChanged);
		
		/// <summary>
        /// Called when the user begins or ends dragging the thumb.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void DragStateChanged([MarshalAs(UnmanagedType.Bool)] bool isDragging);
	}
}
