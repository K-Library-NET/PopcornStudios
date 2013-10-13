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
// Generated by IDLImporter from file nsIWifiAccessPoint.idl
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
	
	
	/// <summary>nsIWifiAccessPoint </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("E28E614F-8F86-44FF-BCF5-5F18225834A0")]
	public interface nsIWifiAccessPoint
	{
		
		/// <summary>
        /// The mac address of the WiFi node.  The format of this string is:
        /// XX-XX-XX-XX-XX-XX
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetMacAttribute([MarshalAs(UnmanagedType.LPStruct)] nsACString aMac);
		
		/// <summary>
        /// Public name of a wireless network.  The charset of this string is ASCII.
        /// This string will be null if not available.
        ///
        /// Note that this is a conversion of the SSID which makes it "displayable".
        /// for any comparisons, you want to use the Raw SSID.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetSsidAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAString aSsid);
		
		/// <summary>
        /// Public name of a wireless network.  These are the bytes that are read off
        /// of the network, may contain nulls, and generally shouldn't be displayed to
        /// the user.
        ///
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetRawSSIDAttribute([MarshalAs(UnmanagedType.LPStruct)] nsACString aRawSSID);
		
		/// <summary>
        /// Current signal strength measured in dBm.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		int GetSignalAttribute();
	}
}
