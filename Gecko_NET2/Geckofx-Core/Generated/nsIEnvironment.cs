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
// Generated by IDLImporter from file nsIEnvironment.idl
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
    /// Scriptable access to the current process environment.
    ///
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("101d5941-d820-4e85-a266-9a3469940807")]
	public interface nsIEnvironment
	{
		
		/// <summary>
        /// Set the value of an environment variable.
        ///
        /// @param aName   the variable name to set.
        /// @param aValue  the value to set.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void Set([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase aName, [MarshalAs(UnmanagedType.LPStruct)] nsAStringBase aValue);
		
		/// <summary>
        /// Get the value of an environment variable.
        ///
        /// @param aName   the variable name to retrieve.
        /// @return        returns the value of the env variable. An empty string
        /// will be returned when the env variable does not exist or
        /// when the value itself is an empty string - please use
        /// |exists()| to probe whether the env variable exists
        /// or not.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void Get([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase aName, [MarshalAs(UnmanagedType.LPStruct)] nsAStringBase retval);
		
		/// <summary>
        /// Check the existence of an environment variable.
        /// This method checks whether an environment variable is present in
        /// the environment or not.
        ///
        /// - For Unix/Linux platforms we follow the Unix definition:
        /// An environment variable exists when |getenv()| returns a non-NULL value.
        /// An environment variable does not exist when |getenv()| returns NULL.
        /// - For non-Unix/Linux platforms we have to fall back to a
        /// "portable" definition (which is incorrect for Unix/Linux!!!!)
        /// which simply checks whether the string returned by |Get()| is empty
        /// or not.
        ///
        /// @param aName   the variable name to probe.
        /// @return        if the variable has been set, the value returned is
        /// PR_TRUE. If the variable was not defined in the
        /// environment PR_FALSE will be returned.
        /// </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool Exists([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase aName);
	}
}
