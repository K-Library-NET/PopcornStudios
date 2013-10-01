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
// Generated by IDLImporter from file nsISystemMessagesInternal.idl
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
    /// Implemented by the contract id @mozilla.org/system-message-internal;1
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("d8de761a-94fe-44d5-80eb-3c8bd8cd7d0b")]
	public interface nsISystemMessagesInternal
	{
		
		/// <summary>
        /// Allow any internal user to broadcast a message of a given type.
        /// @param type        The type of the message to be sent.
        /// @param message     The message payload.
        /// @param pageURI     The URI of the page that will be opened.
        /// @param manifestURI The webapp's manifest URI.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SendMessage([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase type, Gecko.JsVal message, [MarshalAs(UnmanagedType.Interface)] nsIURI pageURI, [MarshalAs(UnmanagedType.Interface)] nsIURI manifestURI);
		
		/// <summary>
        /// Allow any internal user to broadcast a message of a given type.
        /// The application that registers the message will be launched.
        /// @param type        The type of the message to be sent.
        /// @param message     The message payload.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void BroadcastMessage([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase type, Gecko.JsVal message);
		
		/// <summary>
        /// Registration of a page that wants to be notified of a message type.
        /// @param type          The message type.
        /// @param pageURI       The URI of the page that will be opened.
        /// @param manifestURI   The webapp's manifest URI.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void RegisterPage([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase type, [MarshalAs(UnmanagedType.Interface)] nsIURI pageURI, [MarshalAs(UnmanagedType.Interface)] nsIURI manifestURI);
	}
	
	/// <summary>nsISystemMessagesWrapper </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("002f0e82-91f0-41de-ad43-569a2b9d12df")]
	public interface nsISystemMessagesWrapper
	{
		
		/// <summary>
        /// Wrap a message and gives back any kind of object.
        /// @param message  The json blob to wrap.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		Gecko.JsVal WrapMessage(Gecko.JsVal message, [MarshalAs(UnmanagedType.Interface)] nsIDOMWindow window);
	}
}
