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
// Generated by IDLImporter from file nsIX509CertDB.idl
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
    /// This represents a service to access and manipulate
    /// X.509 certificates stored in a database.
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("da48b3c0-1284-11d5-ac67-000064657374")]
	public interface nsIX509CertDB
	{
		
		/// <summary>
        /// Given a nickname and optionally a token,
        /// locate the matching certificate.
        ///
        /// @param aToken Optionally limits the scope of
        /// this function to a token device.
        /// Can be null to mean any token.
        /// @param aNickname The nickname to be used as the key
        /// to find a certificate.
        ///
        /// @return The matching certificate if found.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIX509Cert FindCertByNickname([MarshalAs(UnmanagedType.Interface)] nsISupports aToken, [MarshalAs(UnmanagedType.LPStruct)] nsAString aNickname);
		
		/// <summary>
        /// Will find a certificate based on its dbkey
        /// retrieved by getting the dbKey attribute of
        /// the certificate.
        ///
        /// @param aDBkey Database internal key, as obtained using
        /// attribute dbkey in nsIX509Cert.
        /// @param aToken Optionally limits the scope of
        /// this function to a token device.
        /// Can be null to mean any token.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIX509Cert FindCertByDBKey([MarshalAs(UnmanagedType.LPStr)] string aDBkey, [MarshalAs(UnmanagedType.Interface)] nsISupports aToken);
		
		/// <summary>
        /// Obtain a list of certificate nicknames from the database.
        /// What the name is depends on type:
        /// user, ca, or server cert - the nickname
        /// email cert - the email address
        ///
        /// @param aToken Optionally limits the scope of
        /// this function to a token device.
        /// Can be null to mean any token.
        /// @param aType Type of certificate to obtain
        /// See certificate type constants in nsIX509Cert.
        /// @param count The number of nicknames in the returned array
        /// @param certNameList The returned array of certificate nicknames.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void FindCertNicknames([MarshalAs(UnmanagedType.Interface)] nsISupports aToken, uint aType, ref uint count, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Skybound.Gecko.CustomMarshalers.WStringMarshaler", SizeParamIndex=2)] ref string certNameList);
		
		/// <summary>
        /// Find user's own email encryption certificate by nickname.
        ///
        /// @param aNickname The nickname to be used as the key
        /// to find the certificate.
        ///
        /// @return The matching certificate if found.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIX509Cert FindEmailEncryptionCert([MarshalAs(UnmanagedType.LPStruct)] nsAString aNickname);
		
		/// <summary>
        /// Find user's own email signing certificate by nickname.
        ///
        /// @param aNickname The nickname to be used as the key
        /// to find the certificate.
        ///
        /// @return The matching certificate if found.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIX509Cert FindEmailSigningCert([MarshalAs(UnmanagedType.LPStruct)] nsAString aNickname);
		
		/// <summary>
        /// Find a certificate by email address.
        ///
        /// @param aToken Optionally limits the scope of
        /// this function to a token device.
        /// Can be null to mean any token.
        /// @param aEmailAddress The email address to be used as the key
        /// to find the certificate.
        ///
        /// @return The matching certificate if found.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIX509Cert FindCertByEmailAddress([MarshalAs(UnmanagedType.Interface)] nsISupports aToken, [MarshalAs(UnmanagedType.LPStr)] string aEmailAddress);
		
		/// <summary>
        /// Use this to import a stream sent down as a mime type into
        /// the certificate database on the default token.
        /// The stream may consist of one or more certificates.
        ///
        /// @param data The raw data to be imported
        /// @param length The length of the data to be imported
        /// @param type The type of the certificate, see constants in nsIX509Cert
        /// @param ctx A UI context.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ImportCertificates(System.IntPtr data, uint length, uint type, [MarshalAs(UnmanagedType.Interface)] nsIInterfaceRequestor ctx);
		
		/// <summary>
        /// Import another person's email certificate into the database.
        ///
        /// @param data The raw data to be imported
        /// @param length The length of the data to be imported
        /// @param ctx A UI context.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ImportEmailCertificate(System.IntPtr data, uint length, [MarshalAs(UnmanagedType.Interface)] nsIInterfaceRequestor ctx);
		
		/// <summary>
        /// Import a server machine's certificate into the database.
        ///
        /// @param data The raw data to be imported
        /// @param length The length of the data to be imported
        /// @param ctx A UI context.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ImportServerCertificate(System.IntPtr data, uint length, [MarshalAs(UnmanagedType.Interface)] nsIInterfaceRequestor ctx);
		
		/// <summary>
        /// Import a personal certificate into the database, assuming
        /// the database already contains the private key for this certificate.
        ///
        /// @param data The raw data to be imported
        /// @param length The length of the data to be imported
        /// @param ctx A UI context.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ImportUserCertificate(System.IntPtr data, uint length, [MarshalAs(UnmanagedType.Interface)] nsIInterfaceRequestor ctx);
		
		/// <summary>
        /// Delete a certificate stored in the database.
        ///
        /// @param aCert Delete this certificate.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void DeleteCertificate([MarshalAs(UnmanagedType.Interface)] nsIX509Cert aCert);
		
		/// <summary>
        /// Modify the trust that is stored and associated to a certificate within
        /// a database. Separate trust is stored for
        /// One call manipulates the trust for one trust type only.
        /// See the trust type constants defined within this interface.
        ///
        /// @param cert Change the stored trust of this certificate.
        /// @param type The type of the certificate. See nsIX509Cert.
        /// @param trust A bitmask. The new trust for the possible usages.
        /// See the trust constants defined within this interface.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetCertTrust([MarshalAs(UnmanagedType.Interface)] nsIX509Cert cert, uint type, uint trust);
		
		/// <summary>
        /// Query whether a certificate is trusted for a particular use.
        ///
        /// @param cert Obtain the stored trust of this certificate.
        /// @param certType The type of the certificate. See nsIX509Cert.
        /// @param trustType A single bit from the usages constants defined
        /// within this interface.
        ///
        /// @return Returns true if the certificate is trusted for the given use.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Bool)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool IsCertTrusted([MarshalAs(UnmanagedType.Interface)] nsIX509Cert cert, uint certType, uint trustType);
		
		/// <summary>
        /// Import certificate(s) from file
        ///
        /// @param aToken Optionally limits the scope of
        /// this function to a token device.
        /// Can be null to mean any token.
        /// @param aFile Identifies a file that contains the certificate
        /// to be imported.
        /// @param aType Describes the type of certificate that is going to
        /// be imported. See type constants in nsIX509Cert.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ImportCertsFromFile([MarshalAs(UnmanagedType.Interface)] nsISupports aToken, [MarshalAs(UnmanagedType.Interface)] nsILocalFile aFile, uint aType);
		
		/// <summary>
        /// Import a PKCS#12 file containing cert(s) and key(s) into the database.
        ///
        /// @param aToken Optionally limits the scope of
        /// this function to a token device.
        /// Can be null to mean any token.
        /// @param aFile Identifies a file that contains the data
        /// to be imported.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ImportPKCS12File([MarshalAs(UnmanagedType.Interface)] nsISupports aToken, [MarshalAs(UnmanagedType.Interface)] nsILocalFile aFile);
		
		/// <summary>
        /// Export a set of certs and keys from the database to a PKCS#12 file.
        ///
        /// @param aToken Optionally limits the scope of
        /// this function to a token device.
        /// Can be null to mean any token.
        /// @param aFile Identifies a file that will be filled with the data
        /// to be exported.
        /// @param count The number of certificates to be exported.
        /// @param aCerts The array of all certificates to be exported.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ExportPKCS12File([MarshalAs(UnmanagedType.Interface)] nsISupports aToken, [MarshalAs(UnmanagedType.Interface)] nsILocalFile aFile, uint count, nsIX509Cert aCerts);
		
		/// <summary>
        /// An array of all known OCSP responders within the scope of the
        /// certificate database.
        ///
        /// @return Array of OCSP responders, entries are QIable to nsIOCSPResponder.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIArray GetOCSPResponders();
		
		/// <summary>
        /// Whether OCSP is enabled in preferences.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Bool)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetIsOcspOnAttribute();
		
		/// <summary>
        /// Decode a raw data presentation and instantiate an object in memory.
        ///
        /// @param base64 The raw representation of a certificate,
        /// encoded as Base 64.
        /// @return The new certificate object.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIX509Cert ConstructX509FromBase64([MarshalAs(UnmanagedType.LPStr)] string base64);
	}
}
