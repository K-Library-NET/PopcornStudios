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
// Generated by IDLImporter from file nsIFeedListener.idl
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
    /// nsIFeedResultListener defines a callback used when feed processing
    /// completes.
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("4d2ebe88-36eb-4e20-bcd1-997b3c1f24ce")]
	public interface nsIFeedResultListener
	{
		
		/// <summary>
        /// Always called, even after an error. There could be new feed-level
        /// data available at this point, if it followed or was interspersed
        /// with the items. Fire-and-Forget implementations only need this.
        ///
        /// @param result
        /// An object implementing nsIFeedResult representing the feed
        /// and its metadata.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void HandleResult([MarshalAs(UnmanagedType.Interface)] nsIFeedResult result);
	}
	
	/// <summary>
    /// nsIFeedProgressListener defines callbacks used during feed
    /// processing.
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("ebfd5de5-713c-40c0-ad7c-f095117fa580")]
	public interface nsIFeedProgressListener : nsIFeedResultListener
	{
		
		/// <summary>
        /// Always called, even after an error. There could be new feed-level
        /// data available at this point, if it followed or was interspersed
        /// with the items. Fire-and-Forget implementations only need this.
        ///
        /// @param result
        /// An object implementing nsIFeedResult representing the feed
        /// and its metadata.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void HandleResult([MarshalAs(UnmanagedType.Interface)] nsIFeedResult result);
		
		/// <summary>
        /// ReportError will be called in the event of fatal
        /// XML errors, or if the document is not a feed. The bozo
        /// bit will be set if the error was due to a fatal error.
        ///
        /// @param errorText
        /// A short description of the error.
        /// @param lineNumber
        /// The line on which the error occurred.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ReportError([MarshalAs(UnmanagedType.LPStruct)] nsAString errorText, int lineNumber, [MarshalAs(UnmanagedType.Bool)] bool bozo);
		
		/// <summary>
        /// StartFeed will be called as soon as a reasonable start to
        /// a feed is detected.
        ///
        /// @param result
        /// An object implementing nsIFeedResult representing the feed
        /// and its metadata. At this point, the result has version
        /// information.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void HandleStartFeed([MarshalAs(UnmanagedType.Interface)] nsIFeedResult result);
		
		/// <summary>
        /// Called when the first entry/item is encountered. In Atom, all
        /// feed data is required to preceed the entries. In RSS, the data
        /// usually does. If the type is one of the entry/item-only types,
        /// this event will not be called.
        ///
        /// @param result
        /// An object implementing nsIFeedResult representing the feed
        /// and its metadata. At this point, the result will likely have
        /// most of its feed-level metadata.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void HandleFeedAtFirstEntry([MarshalAs(UnmanagedType.Interface)] nsIFeedResult result);
		
		/// <summary>
        /// Called after each entry/item. If the document is a standalone
        /// item or entry, this HandleFeedAtFirstEntry will not have been
        /// called. Also, this entry's parent field will be null.
        ///
        /// @param entry
        /// An object implementing nsIFeedEntry that represents the latest
        /// entry encountered.
        /// @param result
        /// An object implementing nsIFeedResult representing the feed
        /// and its metadata.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void HandleEntry([MarshalAs(UnmanagedType.Interface)] nsIFeedEntry entry, [MarshalAs(UnmanagedType.Interface)] nsIFeedResult result);
	}
}
