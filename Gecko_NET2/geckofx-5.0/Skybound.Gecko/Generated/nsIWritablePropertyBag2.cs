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
// Generated by IDLImporter from file nsIWritablePropertyBag2.idl
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
    ///nsIVariant based Property Bag support. </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("9cfd1587-360e-4957-a58f-4c2b1c5e7ed9")]
	public interface nsIWritablePropertyBag2 : nsIPropertyBag2
	{
		
		/// <summary>
        /// Get a nsISimpleEnumerator whose elements are nsIProperty objects.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new nsISimpleEnumerator GetEnumeratorAttribute();
		
		/// <summary>
        /// Get a property value for the given name.
        /// @throws NS_ERROR_FAILURE if a property with that name doesn't
        /// exist.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new nsIVariant GetProperty([MarshalAs(UnmanagedType.LPStruct)] nsAString name);
		
		/// <summary>
        /// requested value
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new int GetPropertyAsInt32([MarshalAs(UnmanagedType.LPStruct)] nsAString prop);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new uint GetPropertyAsUint32([MarshalAs(UnmanagedType.LPStruct)] nsAString prop);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new long GetPropertyAsInt64([MarshalAs(UnmanagedType.LPStruct)] nsAString prop);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new ulong GetPropertyAsUint64([MarshalAs(UnmanagedType.LPStruct)] nsAString prop);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new double GetPropertyAsDouble([MarshalAs(UnmanagedType.LPStruct)] nsAString prop);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void GetPropertyAsAString([MarshalAs(UnmanagedType.LPStruct)] nsAString prop, [MarshalAs(UnmanagedType.LPStruct)] nsAString retval);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void GetPropertyAsACString([MarshalAs(UnmanagedType.LPStruct)] nsAString prop, [MarshalAs(UnmanagedType.LPStruct)] nsACString retval);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void GetPropertyAsAUTF8String([MarshalAs(UnmanagedType.LPStruct)] nsAString prop, [MarshalAs(UnmanagedType.LPStruct)] nsAUTF8String retval);
		
		[return: MarshalAs(UnmanagedType.Bool)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new bool GetPropertyAsBool([MarshalAs(UnmanagedType.LPStruct)] nsAString prop);
		
		/// <summary>
        /// This method returns null if the value exists, but is null.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new System.IntPtr GetPropertyAsInterface([MarshalAs(UnmanagedType.LPStruct)] nsAString prop, ref System.Guid iid);
		
		/// <summary>
        /// This method returns null if the value does not exist,
        /// or exists but is null.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new nsIVariant Get([MarshalAs(UnmanagedType.LPStruct)] nsAString prop);
		
		/// <summary>
        /// Check for the existence of a key.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Bool)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new bool HasKey([MarshalAs(UnmanagedType.LPStruct)] nsAString prop);
		
		/// <summary>
        ///nsIVariant based Property Bag support. </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetPropertyAsInt32([MarshalAs(UnmanagedType.LPStruct)] nsAString prop, int value);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetPropertyAsUint32([MarshalAs(UnmanagedType.LPStruct)] nsAString prop, uint value);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetPropertyAsInt64([MarshalAs(UnmanagedType.LPStruct)] nsAString prop, long value);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetPropertyAsUint64([MarshalAs(UnmanagedType.LPStruct)] nsAString prop, ulong value);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetPropertyAsDouble([MarshalAs(UnmanagedType.LPStruct)] nsAString prop, double value);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetPropertyAsAString([MarshalAs(UnmanagedType.LPStruct)] nsAString prop, [MarshalAs(UnmanagedType.LPStruct)] nsAString value);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetPropertyAsACString([MarshalAs(UnmanagedType.LPStruct)] nsAString prop, [MarshalAs(UnmanagedType.LPStruct)] nsACString value);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetPropertyAsAUTF8String([MarshalAs(UnmanagedType.LPStruct)] nsAString prop, [MarshalAs(UnmanagedType.LPStruct)] nsAUTF8String value);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetPropertyAsBool([MarshalAs(UnmanagedType.LPStruct)] nsAString prop, [MarshalAs(UnmanagedType.Bool)] bool value);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetPropertyAsInterface([MarshalAs(UnmanagedType.LPStruct)] nsAString prop, [MarshalAs(UnmanagedType.Interface)] nsISupports value);
	}
}
