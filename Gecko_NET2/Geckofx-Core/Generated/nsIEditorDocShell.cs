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
// Generated by IDLImporter from file nsIEditorDocShell.idl
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
    /// nsIEditorDocShell provides a way to get an editor from
    /// a specific frame in a docShell hierarchy. It is intended
    /// to be only used internally. Use nsIEditingShell.getEditorForFrame
    /// from out side.
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("3BDB8F01-F141-11D4-A73C-FBA4ABA8A3FC")]
	public interface nsIEditorDocShell
	{
		
		/// <summary>
        /// nsIEditorDocShell provides a way to get an editor from
        /// a specific frame in a docShell hierarchy. It is intended
        /// to be only used internally. Use nsIEditingShell.getEditorForFrame
        /// from out side.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIEditor GetEditorAttribute();
		
		/// <summary>
        /// nsIEditorDocShell provides a way to get an editor from
        /// a specific frame in a docShell hierarchy. It is intended
        /// to be only used internally. Use nsIEditingShell.getEditorForFrame
        /// from out side.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetEditorAttribute([MarshalAs(UnmanagedType.Interface)] nsIEditor aEditor);
		
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetEditableAttribute();
		
		/// <summary>
        ///this docShell is editable </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetHasEditingSessionAttribute();
		
		/// <summary>
        /// Make this docShell editable, setting a flag that causes
        /// an editor to get created, either immediately, or after
        /// a url has been loaded.
        /// @param  inWaitForUriLoad    true to wait for a URI before
        /// creating the editor.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void MakeEditable([MarshalAs(UnmanagedType.U1)] bool inWaitForUriLoad);
	}
}
