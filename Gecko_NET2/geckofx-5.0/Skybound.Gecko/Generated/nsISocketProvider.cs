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
// Generated by IDLImporter from file nsISocketProvider.idl
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
    /// nsISocketProvider
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00b3df92-e830-11d8-d48e-0004e22243f8")]
	public interface nsISocketProvider
	{
		
		/// <summary>
        /// newSocket
        ///
        /// @param aFamily
        /// The address family for this socket (PR_AF_INET or PR_AF_INET6).
        /// @param aHost
        /// The hostname for this connection.
        /// @param aPort
        /// The port for this connection.
        /// @param aProxyHost
        /// If non-null, the proxy hostname for this connection.
        /// @param aProxyPort
        /// The proxy port for this connection.
        /// @param aFlags
        /// Control flags that govern this connection (see below.)
        /// @param aFileDesc
        /// The resulting PRFileDesc.
        /// @param aSecurityInfo
        /// Any security info that should be associated with aFileDesc.  This
        /// object typically implements nsITransportSecurityInfo.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void NewSocket(int aFamily, [MarshalAs(UnmanagedType.LPStr)] string aHost, int aPort, [MarshalAs(UnmanagedType.LPStr)] string aProxyHost, int aProxyPort, uint aFlags, ref System.IntPtr aFileDesc, [MarshalAs(UnmanagedType.Interface)] ref nsISupports aSecurityInfo);
		
		/// <summary>
        /// addToSocket
        ///
        /// This function is called to allow the socket provider to layer a
        /// PRFileDesc on top of another PRFileDesc.  For example, SSL via a SOCKS
        /// proxy.
        ///
        /// Parameters are the same as newSocket with the exception of aFileDesc,
        /// which is an in-param instead.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void AddToSocket(int aFamily, [MarshalAs(UnmanagedType.LPStr)] string aHost, int aPort, [MarshalAs(UnmanagedType.LPStr)] string aProxyHost, int aProxyPort, uint aFlags, System.IntPtr aFileDesc, [MarshalAs(UnmanagedType.Interface)] ref nsISupports aSecurityInfo);
	}
}
