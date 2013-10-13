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
// Generated by IDLImporter from file nsIURLParser.idl
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
    /// nsIURLParser specifies the interface to an URL parser that attempts to
    /// follow the definitions of RFC 2396.
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("7281076d-cf37-464a-815e-698235802604")]
	public interface nsIURLParser
	{
		
		/// <summary>
        /// ParseSpec breaks the URL string up into its 3 major components: a scheme,
        /// an authority section (hostname, etc.), and a path.
        ///
        /// spec = <scheme>://<authority><path>
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ParseURL([MarshalAs(UnmanagedType.LPStr)] string spec, int specLen, ref uint schemePos, ref int schemeLen, ref uint authorityPos, ref int authorityLen, ref uint pathPos, ref int pathLen);
		
		/// <summary>
        /// ParseAuthority breaks the authority string up into its 4 components:
        /// username, password, hostname, and hostport.
        ///
        /// auth = <username>:<password>@<hostname>:<port>
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ParseAuthority([MarshalAs(UnmanagedType.LPStr)] string authority, int authorityLen, ref uint usernamePos, ref int usernameLen, ref uint passwordPos, ref int passwordLen, ref uint hostnamePos, ref int hostnameLen, ref int port);
		
		/// <summary>
        /// userinfo = <username>:<password>
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ParseUserInfo([MarshalAs(UnmanagedType.LPStr)] string userinfo, int userinfoLen, ref uint usernamePos, ref int usernameLen, ref uint passwordPos, ref int passwordLen);
		
		/// <summary>
        /// serverinfo = <hostname>:<port>
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ParseServerInfo([MarshalAs(UnmanagedType.LPStr)] string serverinfo, int serverinfoLen, ref uint hostnamePos, ref int hostnameLen, ref int port);
		
		/// <summary>
        /// ParsePath breaks the path string up into its 4 major components: a file path,
        /// a param string, a query string, and a reference string.
        ///
        /// path = <filepath>;<param>?<query>#<ref>
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ParsePath([MarshalAs(UnmanagedType.LPStr)] string path, int pathLen, ref uint filepathPos, ref int filepathLen, ref uint paramPos, ref int paramLen, ref uint queryPos, ref int queryLen, ref uint refPos, ref int refLen);
		
		/// <summary>
        /// ParseFilePath breaks the file path string up into: the directory portion,
        /// file base name, and file extension.
        ///
        /// filepath = <directory><basename>.<extension>
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ParseFilePath([MarshalAs(UnmanagedType.LPStr)] string filepath, int filepathLen, ref uint directoryPos, ref int directoryLen, ref uint basenamePos, ref int basenameLen, ref uint extensionPos, ref int extensionLen);
		
		/// <summary>
        /// filename = <basename>.<extension>
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ParseFileName([MarshalAs(UnmanagedType.LPStr)] string filename, int filenameLen, ref uint basenamePos, ref int basenameLen, ref uint extensionPos, ref int extensionLen);
	}
}
