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
// Generated by IDLImporter from file nsIFeedEntry.idl
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
    /// An nsIFeedEntry represents an Atom or RSS entry/item. Summary
    /// and/or full-text content may be available, but callers will have to
    /// check both.
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("31bfd5b4-8ff5-4bfd-a8cb-b3dfbd4f0a5b")]
	public interface nsIFeedEntry : nsIFeedContainer
	{
		
		/// <summary>
        /// The attributes found on the element. Most interfaces provide convenience
        /// accessors for their standard fields, so this useful only when looking for
        /// an extension.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new nsISAXAttributes GetAttributesAttribute();
		
		/// <summary>
        /// The attributes found on the element. Most interfaces provide convenience
        /// accessors for their standard fields, so this useful only when looking for
        /// an extension.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void SetAttributesAttribute([MarshalAs(UnmanagedType.Interface)] nsISAXAttributes aAttributes);
		
		/// <summary>
        /// The baseURI for the Entry or Feed.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new nsIURI GetBaseURIAttribute();
		
		/// <summary>
        /// The baseURI for the Entry or Feed.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void SetBaseURIAttribute([MarshalAs(UnmanagedType.Interface)] nsIURI aBaseURI);
		
		/// <summary>
        /// Many feeds contain an ID distinct from their URI, and
        /// entries have standard fields for this in all major formats.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void GetIdAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase aId);
		
		/// <summary>
        /// Many feeds contain an ID distinct from their URI, and
        /// entries have standard fields for this in all major formats.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void SetIdAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase aId);
		
		/// <summary>
        /// The fields found in the document. Common Atom
        /// and RSS fields are normalized. This includes some namespaced
        /// extensions such as dc:subject and content:encoded.
        /// Consumers can avoid normalization by checking the feed type
        /// and accessing specific fields.
        ///
        /// Common namespaces are accessed using prefixes, like get("dc:subject");.
        /// See nsIFeedResult::registerExtensionPrefix.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new nsIWritablePropertyBag2 GetFieldsAttribute();
		
		/// <summary>
        /// The fields found in the document. Common Atom
        /// and RSS fields are normalized. This includes some namespaced
        /// extensions such as dc:subject and content:encoded.
        /// Consumers can avoid normalization by checking the feed type
        /// and accessing specific fields.
        ///
        /// Common namespaces are accessed using prefixes, like get("dc:subject");.
        /// See nsIFeedResult::registerExtensionPrefix.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void SetFieldsAttribute([MarshalAs(UnmanagedType.Interface)] nsIWritablePropertyBag2 aFields);
		
		/// <summary>
        /// Sometimes there's no title, or the title contains markup, so take
        /// care in decoding the attribute.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new nsIFeedTextConstruct GetTitleAttribute();
		
		/// <summary>
        /// Sometimes there's no title, or the title contains markup, so take
        /// care in decoding the attribute.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void SetTitleAttribute([MarshalAs(UnmanagedType.Interface)] nsIFeedTextConstruct aTitle);
		
		/// <summary>
        /// Returns the primary link for the feed or entry.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new nsIURI GetLinkAttribute();
		
		/// <summary>
        /// Returns the primary link for the feed or entry.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void SetLinkAttribute([MarshalAs(UnmanagedType.Interface)] nsIURI aLink);
		
		/// <summary>
        /// Returns all links for a feed or entry.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new nsIArray GetLinksAttribute();
		
		/// <summary>
        /// Returns all links for a feed or entry.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void SetLinksAttribute([MarshalAs(UnmanagedType.Interface)] nsIArray aLinks);
		
		/// <summary>
        /// Returns the categories found in a feed or entry.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new nsIArray GetCategoriesAttribute();
		
		/// <summary>
        /// Returns the categories found in a feed or entry.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void SetCategoriesAttribute([MarshalAs(UnmanagedType.Interface)] nsIArray aCategories);
		
		/// <summary>
        /// The rights or license associated with a feed or entry.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new nsIFeedTextConstruct GetRightsAttribute();
		
		/// <summary>
        /// The rights or license associated with a feed or entry.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void SetRightsAttribute([MarshalAs(UnmanagedType.Interface)] nsIFeedTextConstruct aRights);
		
		/// <summary>
        /// A list of nsIFeedPersons that authored the feed.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new nsIArray GetAuthorsAttribute();
		
		/// <summary>
        /// A list of nsIFeedPersons that authored the feed.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void SetAuthorsAttribute([MarshalAs(UnmanagedType.Interface)] nsIArray aAuthors);
		
		/// <summary>
        /// A list of nsIFeedPersons that contributed to the feed.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new nsIArray GetContributorsAttribute();
		
		/// <summary>
        /// A list of nsIFeedPersons that contributed to the feed.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void SetContributorsAttribute([MarshalAs(UnmanagedType.Interface)] nsIArray aContributors);
		
		/// <summary>
        /// The date the feed was updated, in RFC822 form. Parsable by JS
        /// and mail code.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void GetUpdatedAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase aUpdated);
		
		/// <summary>
        /// The date the feed was updated, in RFC822 form. Parsable by JS
        /// and mail code.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void SetUpdatedAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase aUpdated);
		
		/// <summary>
        /// Syncs a container's fields with its convenience attributes.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void Normalize();
		
		/// <summary>
        /// Uses description, subtitle, summary, content and extensions
        /// to generate a summary.
        ///
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIFeedTextConstruct GetSummaryAttribute();
		
		/// <summary>
        /// Uses description, subtitle, summary, content and extensions
        /// to generate a summary.
        ///
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetSummaryAttribute([MarshalAs(UnmanagedType.Interface)] nsIFeedTextConstruct aSummary);
		
		/// <summary>
        /// The date the entry was published, in RFC822 form. Parsable by JS
        /// and mail code.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetPublishedAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase aPublished);
		
		/// <summary>
        /// The date the entry was published, in RFC822 form. Parsable by JS
        /// and mail code.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetPublishedAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase aPublished);
		
		/// <summary>
        /// Uses atom:content and content:encoded to provide
        /// a 'full text' view of an entry.
        ///
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIFeedTextConstruct GetContentAttribute();
		
		/// <summary>
        /// Uses atom:content and content:encoded to provide
        /// a 'full text' view of an entry.
        ///
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetContentAttribute([MarshalAs(UnmanagedType.Interface)] nsIFeedTextConstruct aContent);
		
		/// <summary>
        /// Enclosures are podcasts, photocasts, etc.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIArray GetEnclosuresAttribute();
		
		/// <summary>
        /// Enclosures are podcasts, photocasts, etc.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetEnclosuresAttribute([MarshalAs(UnmanagedType.Interface)] nsIArray aEnclosures);
		
		/// <summary>
        /// Enclosures, etc. that might be displayed inline.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIArray GetMediaContentAttribute();
		
		/// <summary>
        /// Enclosures, etc. that might be displayed inline.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetMediaContentAttribute([MarshalAs(UnmanagedType.Interface)] nsIArray aMediaContent);
	}
}
