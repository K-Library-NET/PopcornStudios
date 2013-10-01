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
// Generated by IDLImporter from file nsIMemoryReporter.idl
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
    /// An nsIMemoryReporter reports a single memory measurement as an object.
    /// Use this when it makes sense to gather this measurement without gathering
    /// related measurements at the same time.
    ///
    /// Note that the |amount| field may be implemented as a function, and so
    /// accessing it can trigger significant computation;  the other fields can
    /// be accessed without triggering this computation.  (Compare and contrast
    /// this with nsIMemoryMultiReporter.)
    ///
    /// aboutMemory.js is the most important consumer of memory reports.  It
    /// places the following constraints on reports.
    ///
    /// - There must be an "explicit" tree.  It represents non-overlapping
    /// regions of memory that have been explicitly allocated with an
    /// OS-level allocation (e.g. mmap/VirtualAlloc/vm_allocate) or a
    /// heap-level allocation (e.g. malloc/calloc/operator new).  Reporters
    /// in this tree must have kind HEAP or NONHEAP, units BYTES, and a
    /// description that is a sentence (i.e. starts with a capital letter and
    /// ends with a period, or similar).
    ///
    /// - The "size", "rss", "pss" and "swap" trees are optional.  They
    /// represent regions of virtual memory that the process has mapped.
    /// Reporters in this category must have kind NONHEAP, units BYTES, and a
    /// non-empty description.
    ///
    /// - The "compartments" and "ghost-windows" trees are optional.  They are
    /// used by about:compartments.  Reporters in these trees must have kind
    /// OTHER, units COUNT, an amount of 1, and a description that's an empty
    /// string.
    ///
    /// - All other reports are unconstrained except that they must have a
    /// description that is a sentence.
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("b2c39f65-1799-4b92-a806-ab3cf6af3cfa")]
	public interface nsIMemoryReporter
	{
		
		/// <summary>
        /// The name of the process containing this reporter.  Each reporter initially
        /// has "" in this field, indicating that it applies to the current process.
        /// (This is true even for reporters in a child process.)  When a reporter
        /// from a child process is copied into the main process, the copy has its
        /// 'process' field set appropriately.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetProcessAttribute([MarshalAs(UnmanagedType.LPStruct)] nsACStringBase aProcess);
		
		/// <summary>
        /// The path that this memory usage should be reported under.  Paths are
        /// '/'-delimited, eg. "a/b/c".
        ///
        /// Each reporter can be viewed as representing a leaf node in a tree.
        /// Internal nodes of the tree don't have reporters.  So, for example, the
        /// reporters "explicit/a/b", "explicit/a/c", "explicit/d/e", and
        /// "explicit/d/f" define this tree:
        ///
        /// explicit
        /// |--a
        /// |  |--b [*]
        /// |  \--c [*]
        /// \--d
        /// |--e [*]
        /// \--f [*]
        ///
        /// Nodes marked with a [*] have a reporter.  Notice that the internal
        /// nodes are implicitly defined by the paths.
        ///
        /// Nodes within a tree should not overlap measurements, otherwise the
        /// parent node measurements will be double-counted.  So in the example
        /// above, |b| should not count any allocations counted by |c|, and vice
        /// versa.
        ///
        /// All nodes within each tree must have the same units.
        ///
        /// If you want to include a '/' not as a path separator, e.g. because the
        /// path contains a URL, you need to convert each '/' in the URL to a '\'.
        /// Consumers of the path will undo this change.  Any other '\' character
        /// in a path will also be changed.  This is clumsy but hasn't caused any
        /// problems so far.
        ///
        /// The paths of all reporters form a set of trees.  Trees can be
        /// "degenerate", i.e. contain a single entry with no '/'.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetPathAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAUTF8StringBase aPath);
		
		/// <summary>
        /// The reporter kind.  See KIND_* above.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		int GetKindAttribute();
		
		/// <summary>
        /// The units on the reporter's amount.  See UNITS_* above.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		int GetUnitsAttribute();
		
		/// <summary>
        /// The numeric value reported by this memory reporter.  Accesses can fail if
        /// something goes wrong when getting the amount.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		long GetAmountAttribute();
		
		/// <summary>
        /// A human-readable description of this memory usage report.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetDescriptionAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAUTF8StringBase aDescription);
	}
	
	/// <summary>nsIMemoryReporterConsts </summary>
	public class nsIMemoryReporterConsts
	{
		
		// <summary>
        // There are three kinds of memory reporters.
        //
        // - HEAP: reporters measuring memory allocated by the heap allocator,
        // e.g. by calling malloc, calloc, realloc, memalign, operator new, or
        // operator new[].  Reporters in this category must have units
        // UNITS_BYTES.
        //
        // - NONHEAP: reporters measuring memory which the program explicitly
        // allocated, but does not live on the heap.  Such memory is commonly
        // allocated by calling one of the OS's memory-mapping functions (e.g.
        // mmap, VirtualAlloc, or vm_allocate).  Reporters in this category
        // must have units UNITS_BYTES.
        //
        // - OTHER: reporters which don't fit into either of these categories.
        // They can have any units.
        //
        // The kind only matters for reporters in the "explicit" tree;
        // aboutMemory.js uses it to calculate "heap-unclassified".
        // </summary>
		public const long KIND_NONHEAP = 0;
		
		// 
		public const long KIND_HEAP = 1;
		
		// 
		public const long KIND_OTHER = 2;
		
		// <summary>
        // The amount reported by a memory reporter must have one of the following
        // units, but you may of course add new units as necessary:
        //
        // - BYTES: The amount contains a number of bytes.
        //
        // - COUNT: The amount is an instantaneous count of things currently in
        // existence.  For instance, the number of tabs currently open would have
        // units COUNT.
        //
        // - COUNT_CUMULATIVE: The amount contains the number of times some event
        // has occurred since the application started up.  For instance, the
        // number of times the user has opened a new tab would have units
        // COUNT_CUMULATIVE.
        //
        // The amount returned by a reporter with units COUNT_CUMULATIVE must
        // never decrease over the lifetime of the application.
        //
        // - PERCENTAGE: The amount contains a fraction that should be expressed as
        // a percentage.  NOTE!  The |amount| field should be given a value 100x
        // the actual percentage;  this number will be divided by 100 when shown.
        // This allows a fractional percentage to be shown even though |amount| is
        // an integer.  E.g. if the actual percentage is 12.34%, |amount| should
        // be 1234.
        //
        // Values greater than 100% are allowed.
        // </summary>
		public const long UNITS_BYTES = 0;
		
		// 
		public const long UNITS_COUNT = 1;
		
		// 
		public const long UNITS_COUNT_CUMULATIVE = 2;
		
		// 
		public const long UNITS_PERCENTAGE = 3;
	}
	
	/// <summary>nsIMemoryMultiReporterCallback </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("5b15f3fa-ba15-443c-8337-7770f5f0ce5d")]
	public interface nsIMemoryMultiReporterCallback
	{
		
		/// <summary>Member Callback </summary>
		/// <param name='process'> </param>
		/// <param name='path'> </param>
		/// <param name='kind'> </param>
		/// <param name='units'> </param>
		/// <param name='amount'> </param>
		/// <param name='description'> </param>
		/// <param name='closure'> </param>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void Callback([MarshalAs(UnmanagedType.LPStruct)] nsACStringBase process, [MarshalAs(UnmanagedType.LPStruct)] nsAUTF8StringBase path, int kind, int units, long amount, [MarshalAs(UnmanagedType.LPStruct)] nsAUTF8StringBase description, [MarshalAs(UnmanagedType.Interface)] nsISupports closure);
	}
	
	/// <summary>
    /// An nsIMemoryMultiReporter reports multiple memory measurements via a
    /// callback function which is called once for each measurement.  Use this
    /// when you want to gather multiple measurements in a single operation (eg.
    /// a single traversal of a large data structure).
    ///
    /// The arguments to the callback deliberately match the fields in
    /// nsIMemoryReporter, but note that seeing any of these arguments requires
    /// calling collectReports which will trigger all relevant computation.
    /// (Compare and contrast this with nsIMemoryReporter, which allows all
    /// fields except |amount| to be accessed without triggering computation.)
    /// </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("61d498d5-b460-4398-a8ea-7f75208534b4")]
	public interface nsIMemoryMultiReporter
	{
		
		/// <summary>
        /// The name of the multi-reporter.  Useful when only one multi-reporter
        /// needs to be run.  Must be unique;  if multi-reporters share names it's
        /// likely the wrong one will be called in certain circumstances.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetNameAttribute([MarshalAs(UnmanagedType.LPStruct)] nsACStringBase aName);
		
		/// <summary>
        /// Run the multi-reporter.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void CollectReports([MarshalAs(UnmanagedType.Interface)] nsIMemoryMultiReporterCallback callback, [MarshalAs(UnmanagedType.Interface)] nsISupports closure);
		
		/// <summary>
        /// Return the sum of all this multi-reporter's measurements that have a
        /// path that starts with "explicit" and are KIND_NONHEAP.
        ///
        /// This is a hack that's required to implement
        /// nsIMemoryReporterManager::explicit efficiently, which is important --
        /// multi-reporters can special-case this operation so it's much faster
        /// than getting all the reports, filtering out the unneeded ones, and
        /// summing the remainder.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		long GetExplicitNonHeapAttribute();
	}
	
	/// <summary>nsIMemoryReporterManager </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("8b670411-ea2a-44c2-a36b-529db0670821")]
	public interface nsIMemoryReporterManager
	{
		
		/// <summary>
        /// Return an enumerator of nsIMemoryReporters that are currently registered.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsISimpleEnumerator EnumerateReporters();
		
		/// <summary>
        /// Return an enumerator of nsIMemoryMultiReporters that are currently
        /// registered.
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsISimpleEnumerator EnumerateMultiReporters();
		
		/// <summary>
        /// Register the given nsIMemoryReporter.  After a reporter is registered,
        /// it will be available via enumerateReporters().  The Manager service
        /// will hold a strong reference to the given reporter.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void RegisterReporter([MarshalAs(UnmanagedType.Interface)] nsIMemoryReporter reporter);
		
		/// <summary>
        /// Register the given nsIMemoryMultiReporter.  After a multi-reporter is
        /// registered, it will be available via enumerateMultiReporters().  The
        /// Manager service will hold a strong reference to the given
        /// multi-reporter.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void RegisterMultiReporter([MarshalAs(UnmanagedType.Interface)] nsIMemoryMultiReporter reporter);
		
		/// <summary>
        /// Unregister the given memory reporter.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void UnregisterReporter([MarshalAs(UnmanagedType.Interface)] nsIMemoryReporter reporter);
		
		/// <summary>
        /// Unregister the given memory multi-reporter.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void UnregisterMultiReporter([MarshalAs(UnmanagedType.Interface)] nsIMemoryMultiReporter reporter);
		
		/// <summary>
        /// Initialize.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void Init();
		
		/// <summary>
        /// Get the resident size (aka. RSS, physical memory used).  This reporter
        /// is special-cased because it's interesting, is available on all
        /// platforms, and returns a meaningful result on all common platforms.
        /// Accesses can fail.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		long GetResidentAttribute();
		
		/// <summary>
        /// Get the total size of explicit memory allocations, both at the OS-level
        /// (eg. via mmap, VirtualAlloc) and at the heap level (eg. via malloc,
        /// calloc, operator new).  (Nb: it covers all heap allocations, but will
        /// miss any OS-level ones not covered by memory reporters.)  This reporter
        /// is special-cased because it's interesting, and is moderately difficult
        /// to compute in JS.  Accesses can fail.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		long GetExplicitAttribute();
		
		/// <summary>
        /// This attribute indicates if moz_malloc_usable_size() works.
        /// </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetHasMozMallocUsableSizeAttribute();
		
		/// <summary>
        /// Run a series of GC/CC's in an attempt to minimize the application's memory
        /// usage.  When we're finished, we invoke the given runnable if it's not
        /// null.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void MinimizeMemoryUsage([MarshalAs(UnmanagedType.Interface)] nsIRunnable callback);
	}
}
