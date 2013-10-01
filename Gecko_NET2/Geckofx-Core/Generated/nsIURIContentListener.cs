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
// Generated by IDLImporter from file nsIURIContentListener.idl
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
    /// nsIURIContentListener is an interface used by components which
    /// want to know (and have a chance to handle) a particular content type.
    /// Typical usage scenarios will include running applications which register
    /// a nsIURIContentListener for each of its content windows with the uri
    /// dispatcher service.
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("94928AB3-8B63-11d3-989D-001083010E9B")]
	public interface nsIURIContentListener
	{
		
		/// <summary>
        /// Gives the original content listener first crack at stopping a load before
        /// it happens.
        ///
        /// @param aURI   URI that is being opened.
        ///
        /// @return       <code>false</code> if the load can continue;
        /// <code>true</code> if the open should be aborted.
        /// </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool OnStartURIOpen([MarshalAs(UnmanagedType.Interface)] nsIURI aURI);
		
		/// <summary>
        /// Notifies the content listener to hook up an nsIStreamListener capable of
        /// consuming the data stream.
        ///
        /// @param aContentType         Content type of the data.
        /// @param aIsContentPreferred  Indicates whether the content should be
        /// preferred by this listener.
        /// @param aRequest             Request that is providing the data.
        /// @param aContentHandler      nsIStreamListener that will consume the data.
        /// This should be set to <code>nullptr</code> if
        /// this content listener can't handle the content
        /// type.
        ///
        /// @return                     <code>true</code> if the consumer wants to
        /// handle the load completely by itself.  This
        /// causes the URI Loader do nothing else...
        /// <code>false</code> if the URI Loader should
        /// continue handling the load and call the
        /// returned streamlistener's methods.
        /// </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool DoContent([MarshalAs(UnmanagedType.LPStr)] string aContentType, [MarshalAs(UnmanagedType.U1)] bool aIsContentPreferred, [MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, [MarshalAs(UnmanagedType.Interface)] ref nsIStreamListener aContentHandler);
		
		/// <summary>
        /// When given a uri to dispatch, if the URI is specified as 'preferred
        /// content' then the uri loader tries to find a preferred content handler
        /// for the content type. The thought is that many content listeners may
        /// be able to handle the same content type if they have to. i.e. the mail
        /// content window can handle text/html just like a browser window content
        /// listener. However, if the user clicks on a link with text/html content,
        /// then the browser window should handle that content and not the mail
        /// window where the user may have clicked the link.  This is the difference
        /// between isPreferred and canHandleContent.
        ///
        /// @param aContentType         Content type of the data.
        /// @param aDesiredContentType  Indicates that aContentType must be converted
        /// to aDesiredContentType before processing the
        /// data.  This causes a stream converted to be
        /// inserted into the nsIStreamListener chain.
        /// This argument can be <code>nullptr</code> if
        /// the content should be consumed directly as
        /// aContentType.
        ///
        /// @return                     <code>true</code> if this is a preferred
        /// content handler for aContentType;
        /// <code>false<code> otherwise.
        /// </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool IsPreferred([MarshalAs(UnmanagedType.LPStr)] string aContentType, [MarshalAs(UnmanagedType.LPStr)] ref string aDesiredContentType);
		
		/// <summary>
        /// When given a uri to dispatch, if the URI is not specified as 'preferred
        /// content' then the uri loader calls canHandleContent to see if the content
        /// listener is capable of handling the content.
        ///
        /// @param aContentType         Content type of the data.
        /// @param aIsContentPreferred  Indicates whether the content should be
        /// preferred by this listener.
        /// @param aDesiredContentType  Indicates that aContentType must be converted
        /// to aDesiredContentType before processing the
        /// data.  This causes a stream converted to be
        /// inserted into the nsIStreamListener chain.
        /// This argument can be <code>nullptr</code> if
        /// the content should be consumed directly as
        /// aContentType.
        ///
        /// @return                     <code>true</code> if the data can be consumed.
        /// <code>false</code> otherwise.
        ///
        /// Note: I really envision canHandleContent as a method implemented
        /// by the docshell as the implementation is generic to all doc
        /// shells. The isPreferred decision is a decision made by a top level
        /// application content listener that sits at the top of the docshell
        /// hierarchy.
        /// </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool CanHandleContent([MarshalAs(UnmanagedType.LPStr)] string aContentType, [MarshalAs(UnmanagedType.U1)] bool aIsContentPreferred, [MarshalAs(UnmanagedType.LPStr)] ref string aDesiredContentType);
		
		/// <summary>
        /// The load context associated with a particular content listener.
        /// The URI Loader stores and accesses this value as needed.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsISupports GetLoadCookieAttribute();
		
		/// <summary>
        /// The load context associated with a particular content listener.
        /// The URI Loader stores and accesses this value as needed.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetLoadCookieAttribute([MarshalAs(UnmanagedType.Interface)] nsISupports aLoadCookie);
		
		/// <summary>
        /// The parent content listener if this particular listener is part of a chain
        /// of content listeners (i.e. a docshell!)
        ///
        /// @note If this attribute is set to an object that implements
        /// nsISupportsWeakReference, the implementation should get the
        /// nsIWeakReference and hold that.  Otherwise, the implementation
        /// should not refcount this interface; it should assume that a non
        /// null value is always valid.  In that case, the caller is
        /// responsible for explicitly setting this value back to null if the
        /// parent content listener is destroyed.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIURIContentListener GetParentContentListenerAttribute();
		
		/// <summary>
        /// The parent content listener if this particular listener is part of a chain
        /// of content listeners (i.e. a docshell!)
        ///
        /// @note If this attribute is set to an object that implements
        /// nsISupportsWeakReference, the implementation should get the
        /// nsIWeakReference and hold that.  Otherwise, the implementation
        /// should not refcount this interface; it should assume that a non
        /// null value is always valid.  In that case, the caller is
        /// responsible for explicitly setting this value back to null if the
        /// parent content listener is destroyed.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetParentContentListenerAttribute([MarshalAs(UnmanagedType.Interface)] nsIURIContentListener aParentContentListener);
	}
}
