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
// Generated by IDLImporter from file nsIDOMFileReader.idl
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
	
	
	/// <summary>nsIDOMFileReader </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("f186170f-f07c-4f0b-9e3c-08f7dd496e74")]
	public interface nsIDOMFileReader
	{
		
		/// <summary>Member ReadAsBinaryString </summary>
		/// <param name='filedata'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ReadAsBinaryString([MarshalAs(UnmanagedType.Interface)] nsIDOMBlob filedata);
		
		/// <summary>Member ReadAsText </summary>
		/// <param name='filedata'> </param>
		/// <param name='encoding'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ReadAsText([MarshalAs(UnmanagedType.Interface)] nsIDOMBlob filedata, [MarshalAs(UnmanagedType.LPStruct)] nsAString encoding);
		
		/// <summary>Member ReadAsDataURL </summary>
		/// <param name='file'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ReadAsDataURL([MarshalAs(UnmanagedType.Interface)] nsIDOMBlob file);
		
		/// <summary>Member Abort </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void Abort();
		
		/// <summary>Member GetReadyStateAttribute </summary>
		/// <returns>A System.UInt16</returns>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		ushort GetReadyStateAttribute();
		
		/// <summary>Member GetResultAttribute </summary>
		/// <param name='aResult'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetResultAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAString aResult);
		
		/// <summary>Member GetErrorAttribute </summary>
		/// <returns>A nsIDOMFileError</returns>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIDOMFileError GetErrorAttribute();
	}
}
