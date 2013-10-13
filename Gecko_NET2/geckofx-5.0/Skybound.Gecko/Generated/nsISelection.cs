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
// Generated by IDLImporter from file nsISelection.idl
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
    /// Interface for manipulating and querying the current selected range
    /// of nodes within the document.
    ///
    /// @version 1.0
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("B2C7ED59-8634-4352-9E37-5484C8B6E4E1")]
	public interface nsISelection
	{
		
		/// <summary>
        /// Returns the node in which the selection begins.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMNode GetAnchorNodeAttribute();
		
		/// <summary>
        /// The offset within the (text) node where the selection begins.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		int GetAnchorOffsetAttribute();
		
		/// <summary>
        /// Returns the node in which the selection ends.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMNode GetFocusNodeAttribute();
		
		/// <summary>
        /// The offset within the (text) node where the selection ends.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		int GetFocusOffsetAttribute();
		
		/// <summary>
        /// Indicates if the selection is collapsed or not.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Bool)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetIsCollapsedAttribute();
		
		/// <summary>
        /// Returns the number of ranges in the selection.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		int GetRangeCountAttribute();
		
		/// <summary>
        /// Returns the range at the specified index.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMRange GetRangeAt(int index);
		
		/// <summary>
        /// Collapses the selection to a single point, at the specified offset
        /// in the given DOM node. When the selection is collapsed, and the content
        /// is focused and editable, the caret will blink there.
        /// @param parentNode      The given dom node where the selection will be set
        /// @param offset          Where in given dom node to place the selection (the offset into the given node)
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void Collapse([MarshalAs(UnmanagedType.Interface)] nsIDOMNode parentNode, int offset);
		
		/// <summary>
        /// Extends the selection by moving the selection end to the specified node and offset,
        /// preserving the selection begin position. The new selection end result will always
        /// be from the anchorNode to the new focusNode, regardless of direction.
        /// @param parentNode      The node where the selection will be extended to
        /// @param offset          Where in node to place the offset in the new selection end
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void Extend([MarshalAs(UnmanagedType.Interface)] nsIDOMNode parentNode, int offset);
		
		/// <summary>
        /// Collapses the whole selection to a single point at the start
        /// of the current selection (irrespective of direction).  If content
        /// is focused and editable, the caret will blink there.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void CollapseToStart();
		
		/// <summary>
        /// Collapses the whole selection to a single point at the end
        /// of the current selection (irrespective of direction).  If content
        /// is focused and editable, the caret will blink there.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void CollapseToEnd();
		
		/// <summary>
        /// Indicates whether the node is part of the selection. If partlyContained
        /// is set to PR_TRUE, the function returns true when some part of the node
        /// is part of the selection. If partlyContained is set to PR_FALSE, the
        /// function only returns true when the entire node is part of the selection.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Bool)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool ContainsNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode node, [MarshalAs(UnmanagedType.Bool)] bool partlyContained);
		
		/// <summary>
        /// Adds all children of the specified node to the selection.
        /// @param parentNode  the parent of the children to be added to the selection.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SelectAllChildren([MarshalAs(UnmanagedType.Interface)] nsIDOMNode parentNode);
		
		/// <summary>
        /// Adds a range to the current selection.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void AddRange([MarshalAs(UnmanagedType.Interface)] nsIDOMRange range);
		
		/// <summary>
        /// Removes a range from the current selection.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void RemoveRange([MarshalAs(UnmanagedType.Interface)] nsIDOMRange range);
		
		/// <summary>
        /// Removes all ranges from the current selection.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void RemoveAllRanges();
		
		/// <summary>
        /// Deletes this selection from document the nodes belong to.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void DeleteFromDocument();
		
		/// <summary>
        /// Modifies the cursor Bidi level after a change in keyboard direction
        /// @param langRTL is PR_TRUE if the new language is right-to-left or
        /// PR_FALSE if the new language is left-to-right.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SelectionLanguageChange([MarshalAs(UnmanagedType.Bool)] bool langRTL);
		
		/// <summary>
        /// Returns the whole selection into a plain text string.
        /// </summary>
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Skybound.Gecko.CustomMarshalers.WStringMarshaler")]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		string ToString();
	}
}
