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
// Generated by IDLImporter from file nsIDOMSettingsManager.idl
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
	[Guid("ef95ddd0-6308-11e1-b86c-0800200c9a66")]
	public interface nsIDOMSettingsLock
	{
		
		/// <summary>
        /// Contains a JSON object with name/value pairs to be set.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMDOMRequest Set([MarshalAs(UnmanagedType.Interface)] nsIVariant settings);
		
		/// <summary>
        /// result contains the value of the setting.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMDOMRequest Get(Gecko.JsVal name);
		
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMDOMRequest Clear();
	}
	
	/// <summary>nsIDOMSettingsManager </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("c40b1c70-00fb-11e2-a21f-0800200c9a66")]
	public interface nsIDOMSettingsManager
	{
		
		/// <summary>Member CreateLock </summary>
		/// <returns>A nsIDOMSettingsLock</returns>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMSettingsLock CreateLock();
		
		/// <summary>Member AddObserver </summary>
		/// <param name='name'> </param>
		/// <param name='callback'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void AddObserver([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase name, Gecko.JsVal callback);
		
		/// <summary>Member RemoveObserver </summary>
		/// <param name='name'> </param>
		/// <param name='callback'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void RemoveObserver([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase name, Gecko.JsVal callback);
		
		/// <summary>Member GetOnsettingchangeAttribute </summary>
		/// <returns>A nsIDOMEventListener</returns>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMEventListener GetOnsettingchangeAttribute();
		
		/// <summary>Member SetOnsettingchangeAttribute </summary>
		/// <param name='aOnsettingchange'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetOnsettingchangeAttribute([MarshalAs(UnmanagedType.Interface)] nsIDOMEventListener aOnsettingchange);
	}
}
