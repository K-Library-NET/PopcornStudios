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
// Generated by IDLImporter from file inISearchProcess.idl
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
	
	
	/// <summary>inISearchProcess </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("D5FA765B-2448-4686-B7C1-5FF13ACB0FC9")]
	public interface inISearchProcess
	{
		
		/// <summary>
        /// indicates if an asynchronous search is in progress
        /// </summary>
		[return: MarshalAs(UnmanagedType.Bool)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetIsActiveAttribute();
		
		/// <summary>
        /// the number of results returned
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		int GetResultCountAttribute();
		
		/// <summary>
        /// other than the most recent one, and getResults will return null always.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Bool)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetHoldResultsAttribute();
		
		/// <summary>
        /// other than the most recent one, and getResults will return null always.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetHoldResultsAttribute([MarshalAs(UnmanagedType.Bool)] bool aHoldResults);
		
		/// <summary>
        /// start a synchronous search
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SearchSync();
		
		/// <summary>
        /// start an asynchronous search
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SearchAsync(inISearchObserver aObserver);
		
		/// <summary>
        /// command an async process to stop immediately
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SearchStop();
		
		/// <summary>
        /// and is not for use by those who just wish to call searchAsync
        /// </summary>
		[return: MarshalAs(UnmanagedType.Bool)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool SearchStep();
		
		/// <summary>
        /// methods for getting results of specific types
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetStringResultAt(int aIndex, [MarshalAs(UnmanagedType.LPStruct)] nsAString retval);
		
		/// <summary>Member GetIntResultAt </summary>
		/// <param name='aIndex'> </param>
		/// <returns>A System.Int32</returns>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		int GetIntResultAt(int aIndex);
		
		/// <summary>Member GetUIntResultAt </summary>
		/// <param name='aIndex'> </param>
		/// <returns>A System.UInt32</returns>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		uint GetUIntResultAt(int aIndex);
	}
}
