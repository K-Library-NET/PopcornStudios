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
// Generated by IDLImporter from file nsIScriptError.idl
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
    /// nsIConsoleMessage subclass for representing JavaScript errors and warnings.
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("b0196fc7-1913-441a-882a-453c0d8b89b8")]
	public interface nsIScriptError : nsIConsoleMessage
	{
		
		/// <summary>
        /// This is intended as a base interface; implementations may want to
        /// provide an object that can be qi'ed to provide more specific
        /// message information.
        /// </summary>
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Skybound.Gecko.CustomMarshalers.WStringMarshaler")]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new string GetMessageAttribute();
		
		/// <summary>
        /// The error message without any context/line number information.
        ///
        /// @note nsIConsoleMessage.message will return the error formatted
        /// with file/line information.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetErrorMessageAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAString aErrorMessage);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetSourceNameAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAString aSourceName);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetSourceLineAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAString aSourceLine);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		uint GetLineNumberAttribute();
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		uint GetColumnNumberAttribute();
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		uint GetFlagsAttribute();
		
		/// <summary>
        /// Categories I know about -
        /// XUL javascript
        /// content javascript (both of these from nsDocShell, currently)
        /// component javascript (errors in JS components)
        /// </summary>
		[return: MarshalAs(UnmanagedType.LPStr)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		string GetCategoryAttribute();
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void Init([MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Skybound.Gecko.CustomMarshalers.WStringMarshaler")] string message, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Skybound.Gecko.CustomMarshalers.WStringMarshaler")] string sourceName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Skybound.Gecko.CustomMarshalers.WStringMarshaler")] string sourceLine, uint lineNumber, uint columnNumber, uint flags, [MarshalAs(UnmanagedType.LPStr)] string category);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ToString([MarshalAs(UnmanagedType.LPStruct)] nsAUTF8String retval);
	}
	
	/// <summary>
    /// An interface that nsIScriptError objects can implement to allow
    /// them to be initialized with a window id.
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("35cd0f6a-f5bb-497a-ba83-9c8d089b52cd")]
	public interface nsIScriptError2
	{
		
		/// <summary>
        ///Get the window id this was initialized with.  Zero will be
        ///       returned if init() was used instead of initWithWindowId(). </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		uint GetOuterWindowIDAttribute();
		
		/// <summary>
        ///This should be called instead of nsIScriptError.init to
        ///       initialize with a window id.  The window id should be for the
        ///       outer window associated with this error. </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void InitWithWindowID([MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Skybound.Gecko.CustomMarshalers.WStringMarshaler")] string message, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Skybound.Gecko.CustomMarshalers.WStringMarshaler")] string sourceName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Skybound.Gecko.CustomMarshalers.WStringMarshaler")] string sourceLine, uint lineNumber, uint columnNumber, uint flags, [MarshalAs(UnmanagedType.LPStr)] string category, uint windowID);
	}
}
