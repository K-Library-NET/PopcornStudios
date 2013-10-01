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
// Generated by IDLImporter from file nsIClipboardHelper.idl
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
    /// helper service for common uses of nsIClipboard.
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("c9d5a750-c3a8-11e1-9b21-0800200c9a66")]
	public interface nsIClipboardHelper
	{
		
		/// <summary>
        /// copy string to given clipboard
        ///
        /// @param aString, the string to copy to the clipboard
        /// @param aDoc, the source document for the string, if available
        /// @param aClipboardID, the ID of the clipboard to copy to
        /// (eg. kSelectionClipboard -- see nsIClipboard.idl)
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void CopyStringToClipboard([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase aString, int aClipboardID, [MarshalAs(UnmanagedType.Interface)] nsIDOMDocument aDoc);
		
		/// <summary>
        /// copy string to (default) clipboard
        ///
        /// @param aString, the string to copy to the clipboard
        /// @param aDoc, the source document for the string, if available
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void CopyString([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase aString, [MarshalAs(UnmanagedType.Interface)] nsIDOMDocument aDoc);
	}
}
