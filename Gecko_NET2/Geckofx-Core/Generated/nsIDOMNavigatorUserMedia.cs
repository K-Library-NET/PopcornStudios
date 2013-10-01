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
// Generated by IDLImporter from file nsIDOMNavigatorUserMedia.idl
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
    ///This Source Code Form is subject to the terms of the Mozilla Public
    /// License, v. 2.0. If a copy of the MPL was not distributed with this file,
    /// You can obtain one at http://mozilla.org/MPL/2.0/. </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("6de854f9-acf8-4383-b464-4803631ef309")]
	public interface nsIMediaDevice
	{
		
		/// <summary>
        ///This Source Code Form is subject to the terms of the Mozilla Public
        /// License, v. 2.0. If a copy of the MPL was not distributed with this file,
        /// You can obtain one at http://mozilla.org/MPL/2.0/. </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetTypeAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase aType);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetNameAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase aName);
	}
	
	/// <summary>nsIGetUserMediaDevicesSuccessCallback </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("24544878-d35e-4962-8c5f-fb84e97bdfee")]
	public interface nsIGetUserMediaDevicesSuccessCallback
	{
		
		/// <summary>Member OnSuccess </summary>
		/// <param name='devices'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void OnSuccess([MarshalAs(UnmanagedType.Interface)] nsIVariant devices);
	}
	
	/// <summary>nsIDOMGetUserMediaSuccessCallback </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("f2a144fc-3534-4761-8c5d-989ae720f89a")]
	public interface nsIDOMGetUserMediaSuccessCallback
	{
		
		/// <summary>
        /// value must be a nsIDOMBlob if picture is true and a
        /// nsIDOMMediaStream if either audio or video are true.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void OnSuccess([MarshalAs(UnmanagedType.Interface)] nsISupports value);
	}
	
	/// <summary>nsIDOMGetUserMediaErrorCallback </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("2614bbcf-85cc-43e5-8740-964f52bdc7ca")]
	public interface nsIDOMGetUserMediaErrorCallback
	{
		
		/// <summary>Member OnError </summary>
		/// <param name='error'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void OnError([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase error);
	}
	
	/// <summary>nsIMediaStreamOptions </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("36d9c3b7-7594-4035-8a7e-92c2cecdb2c5")]
	public interface nsIMediaStreamOptions
	{
		
		/// <summary>Member GetFakeAttribute </summary>
		/// <returns>A System.Boolean</returns>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetFakeAttribute();
		
		/// <summary>Member GetAudioAttribute </summary>
		/// <returns>A System.Boolean</returns>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetAudioAttribute();
		
		/// <summary>Member GetVideoAttribute </summary>
		/// <returns>A System.Boolean</returns>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetVideoAttribute();
		
		/// <summary>Member GetPictureAttribute </summary>
		/// <returns>A System.Boolean</returns>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetPictureAttribute();
		
		/// <summary>Member GetCameraAttribute </summary>
		/// <param name='aCamera'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetCameraAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase aCamera);
		
		/// <summary>Member GetDeviceAttribute </summary>
		/// <returns>A nsIMediaDevice</returns>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIMediaDevice GetDeviceAttribute();
	}
	
	/// <summary>nsIDOMNavigatorUserMedia </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("381e0071-0be5-4f6b-ae21-8e3407a37faa")]
	public interface nsIDOMNavigatorUserMedia
	{
		
		/// <summary>Member MozGetUserMedia </summary>
		/// <param name='params'> </param>
		/// <param name='onsuccess'> </param>
		/// <param name='onerror'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void MozGetUserMedia([MarshalAs(UnmanagedType.Interface)] nsIMediaStreamOptions @params, [MarshalAs(UnmanagedType.Interface)] nsIDOMGetUserMediaSuccessCallback onsuccess, [MarshalAs(UnmanagedType.Interface)] nsIDOMGetUserMediaErrorCallback onerror);
	}
	
	/// <summary>nsINavigatorUserMedia </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("20e9c794-fdfe-43f4-a81b-ebd9069e0af1")]
	public interface nsINavigatorUserMedia
	{
		
		/// <summary>Member MozGetUserMediaDevices </summary>
		/// <param name='onsuccess'> </param>
		/// <param name='onerror'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void MozGetUserMediaDevices([MarshalAs(UnmanagedType.Interface)] nsIGetUserMediaDevicesSuccessCallback onsuccess, [MarshalAs(UnmanagedType.Interface)] nsIDOMGetUserMediaErrorCallback onerror);
	}
}
