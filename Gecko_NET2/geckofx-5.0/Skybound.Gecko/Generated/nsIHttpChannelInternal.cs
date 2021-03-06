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
// Generated by IDLImporter from file nsIHttpChannelInternal.idl
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
    /// Dumping ground for http.  This interface will never be frozen.  If you are
    /// using any feature exposed by this interface, be aware that this interface
    /// will change and you will be broken.  You have been warned.
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("12eb906a-71fe-4b79-b33a-6fe9ab57ea38")]
	public interface nsIHttpChannelInternal
	{
		
		/// <summary>
        /// An http channel can own a reference to the document URI
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIURI GetDocumentURIAttribute();
		
		/// <summary>
        /// An http channel can own a reference to the document URI
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetDocumentURIAttribute([MarshalAs(UnmanagedType.Interface)] nsIURI aDocumentURI);
		
		/// <summary>
        /// Get the major/minor version numbers for the request
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetRequestVersion(ref uint major, ref uint minor);
		
		/// <summary>
        /// Get the major/minor version numbers for the response
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetResponseVersion(ref uint major, ref uint minor);
		
		/// <summary>
        /// Helper method to set a cookie with a consumer-provided
        /// cookie header, _but_ using the channel's other information
        /// (URI's, prompters, date headers etc).
        ///
        /// @param aCookieHeader
        /// The cookie header to be parsed.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetCookie([MarshalAs(UnmanagedType.LPStr)] string aCookieHeader);
		
		/// <summary>
        /// Setup this channel as an application cache fallback channel.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetupFallbackChannel([MarshalAs(UnmanagedType.LPStr)] string aFallbackKey);
		
		/// <summary>
        /// Force relevant cookies to be sent with this load even if normally they
        /// wouldn't be.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Bool)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetForceAllowThirdPartyCookieAttribute();
		
		/// <summary>
        /// Force relevant cookies to be sent with this load even if normally they
        /// wouldn't be.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetForceAllowThirdPartyCookieAttribute([MarshalAs(UnmanagedType.Bool)] bool aForceAllowThirdPartyCookie);
		
		/// <summary>
        /// True iff the channel has been canceled.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Bool)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetCanceledAttribute();
		
		/// <summary>
        /// External handlers may set this to true to notify the channel
        /// that it is open on behalf of a download.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Bool)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetChannelIsForDownloadAttribute();
		
		/// <summary>
        /// External handlers may set this to true to notify the channel
        /// that it is open on behalf of a download.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetChannelIsForDownloadAttribute([MarshalAs(UnmanagedType.Bool)] bool aChannelIsForDownload);
		
		/// <summary>
        /// The local IP address to which this channel is bound, in the
        /// format produced by PR_NetAddrToString. May be IPv4 or IPv6.
        /// Note: in the presence of NAT, this may not be the same as the
        /// address that the remote host thinks it's talking to.
        ///
        /// May throw NS_ERROR_NOT_AVAILABLE if accessed when the channel's
        /// endpoints are not yet determined, or in any case when
        /// nsIHttpActivityObserver.isActive is false. See bugs 534698 and 526207.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetLocalAddressAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAUTF8String aLocalAddress);
		
		/// <summary>
        /// The local port number to which this channel is bound.
        ///
        /// May throw NS_ERROR_NOT_AVAILABLE if accessed when the channel's
        /// endpoints are not yet determined, or in any case when
        /// nsIHttpActivityObserver.isActive is false. See bugs 534698 and 526207.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		int GetLocalPortAttribute();
		
		/// <summary>
        /// The IP address of the remote host that this channel is
        /// connected to, in the format produced by PR_NetAddrToString.
        ///
        /// May throw NS_ERROR_NOT_AVAILABLE if accessed when the channel's
        /// endpoints are not yet determined, or in any case when
        /// nsIHttpActivityObserver.isActive is false. See bugs 534698 and 526207.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetRemoteAddressAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAUTF8String aRemoteAddress);
		
		/// <summary>
        /// The remote port number that this channel is connected to.
        ///
        /// May throw NS_ERROR_NOT_AVAILABLE if accessed when the channel's
        /// endpoints are not yet determined, or in any case when
        /// nsIHttpActivityObserver.isActive is false. See bugs 534698 and 526207.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		int GetRemotePortAttribute();
		
		/// <summary>
        /// Transfer chain of redirected cache-keys.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetCacheKeysRedirectChain(System.IntPtr cacheKeys);
	}
}
