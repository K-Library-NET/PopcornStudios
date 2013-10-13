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
// Generated by IDLImporter from file nsIDOMHistory.idl
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
	
	
	/// <summary>nsIDOMHistory </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("d5a3006b-dd6b-4ba3-81be-6559f8889e60")]
	public interface nsIDOMHistory
	{
		
		/// <summary>Member GetLengthAttribute </summary>
		/// <returns>A System.Int32</returns>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		int GetLengthAttribute();
		
		/// <summary>Member GetCurrentAttribute </summary>
		/// <param name='aCurrent'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetCurrentAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAString aCurrent);
		
		/// <summary>Member GetPreviousAttribute </summary>
		/// <param name='aPrevious'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetPreviousAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAString aPrevious);
		
		/// <summary>Member GetNextAttribute </summary>
		/// <param name='aNext'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetNextAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAString aNext);
		
		/// <summary>Member Back </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void Back();
		
		/// <summary>Member Forward </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void Forward();
		
		/// <summary>Member Go </summary>
		/// <param name='aDelta'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void Go(int aDelta);
		
		/// <summary>Member Item </summary>
		/// <param name='index'> </param>
		/// <param name='retval'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void Item(uint index, [MarshalAs(UnmanagedType.LPStruct)] nsAString retval);
		
		/// <summary>Member PushState </summary>
		/// <param name='aData'> </param>
		/// <param name='aTitle'> </param>
		/// <param name='aURL'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void PushState([MarshalAs(UnmanagedType.Interface)] nsIVariant aData, [MarshalAs(UnmanagedType.LPStruct)] nsAString aTitle, [MarshalAs(UnmanagedType.LPStruct)] nsAString aURL);
		
		/// <summary>Member ReplaceState </summary>
		/// <param name='aData'> </param>
		/// <param name='aTitle'> </param>
		/// <param name='aURL'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ReplaceState([MarshalAs(UnmanagedType.Interface)] nsIVariant aData, [MarshalAs(UnmanagedType.LPStruct)] nsAString aTitle, [MarshalAs(UnmanagedType.LPStruct)] nsAString aURL);
		
		/// <summary>Member GetStateAttribute </summary>
		/// <returns>A nsIVariant</returns>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIVariant GetStateAttribute();
	}
}
