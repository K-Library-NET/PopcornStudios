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
// Generated by IDLImporter from file nsICommandManager.idl
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
    /// nsICommandManager is an interface used to executing user-level commands,
    /// and getting the state of available commands.
    ///
    /// Commands are identified by strings, which are documented elsewhere.
    /// In addition, the list of required and optional parameters for
    /// each command, that are passed in via the nsICommandParams, are
    /// also documented elsewhere. (Where? Need a good location for this).
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("080D2001-F91E-11D4-A73C-F9242928207C")]
	public interface nsICommandManager
	{
		
		/// <summary>
        /// Register an observer on the specified command. The observer's Observe
        /// method will get called when the state (enabled/disbaled, or toggled etc)
        /// of the command changes.
        ///
        /// You can register the same observer on multiple commmands by calling this
        /// multiple times.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void AddCommandObserver([MarshalAs(UnmanagedType.Interface)] nsIObserver aCommandObserver, [MarshalAs(UnmanagedType.LPStr)] string aCommandToObserve);
		
		/// <summary>
        /// Stop an observer from observering the specified command. If the observer
        /// was also registered on ther commands, they will continue to be observed.
        ///
        /// Passing an empty string in 'aCommandObserved' will remove the observer
        /// from all commands.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void RemoveCommandObserver([MarshalAs(UnmanagedType.Interface)] nsIObserver aCommandObserver, [MarshalAs(UnmanagedType.LPStr)] string aCommandObserved);
		
		/// <summary>
        /// Ask the command manager if the specified command is supported.
        /// If aTargetWindow is null, the focused window is used.
        ///
        /// </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool IsCommandSupported([MarshalAs(UnmanagedType.LPStr)] string aCommandName, [MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aTargetWindow);
		
		/// <summary>
        /// Ask the command manager if the specified command is currently.
        /// enabled.
        /// If aTargetWindow is null, the focused window is used.
        /// </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool IsCommandEnabled([MarshalAs(UnmanagedType.LPStr)] string aCommandName, [MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aTargetWindow);
		
		/// <summary>
        ///inout </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetCommandState([MarshalAs(UnmanagedType.LPStr)] string aCommandName, [MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aTargetWindow, [MarshalAs(UnmanagedType.Interface)] nsICommandParams aCommandParams);
		
		/// <summary>
        /// Execute the specified command.
        /// The command will be executed in aTargetWindow if it is specified.
        /// If aTargetWindow is null, it will go to the focused window.
        ///
        /// param: aCommandParams, a list of name-value pairs of command parameters,
        /// may be null for parameter-less commands.
        ///
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void DoCommand([MarshalAs(UnmanagedType.LPStr)] string aCommandName, [MarshalAs(UnmanagedType.Interface)] nsICommandParams aCommandParams, [MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aTargetWindow);
	}
}