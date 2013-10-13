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
// Generated by IDLImporter from file nsICRLManager.idl
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
	
	
	/// <summary>nsICRLManager </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("486755db-627a-4678-a21b-f6a63bb9c56a")]
	public interface nsICRLManager
	{
		
		/// <summary>
        /// importCrl
        ///
        /// Import a CRL into the certificate database.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ImportCrl(System.IntPtr data, uint length, [MarshalAs(UnmanagedType.Interface)] nsIURI uri, uint type, [MarshalAs(UnmanagedType.Bool)] bool doSilentDownload, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Skybound.Gecko.CustomMarshalers.WStringMarshaler")] string crlKey);
		
		/// <summary>
        /// update crl from url
        /// update an existing crl from the last fetched url. Needed for the update
        /// button in crl manager
        /// </summary>
		[return: MarshalAs(UnmanagedType.Bool)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool UpdateCRLFromURL([MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Skybound.Gecko.CustomMarshalers.WStringMarshaler")] string url, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Skybound.Gecko.CustomMarshalers.WStringMarshaler")] string key);
		
		/// <summary>
        /// getCrls
        ///
        /// Get a list of Crl entries in the DB.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIArray GetCrls();
		
		/// <summary>
        /// deleteCrl
        ///
        /// Delete the crl.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void DeleteCrl(uint crlIndex);
		
		/// <summary>
        ///This would reschedule the autoupdate of crls with auto update enable.
        /// Most likely to be called when update prefs are changed, or when a crl
        /// is deleted, etc. However, this might not be the most relevant place for
        /// this api, but unless we have a separate crl handler object....
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void RescheduleCRLAutoUpdate();
		
		/// <summary>Member ComputeNextAutoUpdateTime </summary>
		/// <param name='info'> </param>
		/// <param name='autoUpdateType'> </param>
		/// <param name='noOfDays'> </param>
		/// <returns>A System.String</returns>
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Skybound.Gecko.CustomMarshalers.WStringMarshaler")]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		string ComputeNextAutoUpdateTime([MarshalAs(UnmanagedType.Interface)] nsICRLInfo info, uint autoUpdateType, double noOfDays);
	}
}
