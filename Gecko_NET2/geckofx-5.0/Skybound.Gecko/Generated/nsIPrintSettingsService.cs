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
// Generated by IDLImporter from file nsIPrintSettingsService.idl
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
    ///Interface to the Service for gwetting the Global PrintSettings object
    ///   or a unique PrintSettings object </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("841387C8-72E6-484b-9296-BF6EEA80D58A")]
	public interface nsIPrintSettingsService
	{
		
		/// <summary>
        /// Returns a "global" PrintSettings object
        /// Creates a new the first time, if one doesn't exist.
        ///
        /// Then returns the same object each time after that.
        ///
        /// Initializes the globalPrintSettings from the default printer
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIPrintSettings GetGlobalPrintSettingsAttribute();
		
		/// <summary>
        /// Returns a new, unique PrintSettings object each time.
        ///
        /// For example, if each browser was to have its own unique
        /// PrintSettings, then each browser window would call this to
        /// create its own unique PrintSettings object.
        ///
        /// If each browse window was to use the same PrintSettings object
        /// then it should use "globalPrintSettings"
        ///
        /// Initializes the newPrintSettings from the default printer
        ///
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIPrintSettings GetNewPrintSettingsAttribute();
		
		/// <summary>
        /// The name of the last printer used, or else the system default printer.
        /// </summary>
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Skybound.Gecko.CustomMarshalers.WStringMarshaler")]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		string GetDefaultPrinterNameAttribute();
		
		/// <summary>
        /// Initializes certain settings from the native printer into the PrintSettings
        /// if aPrinterName is null then it uses the default printer name if it can
        /// These settings include, but are not limited to:
        /// Page Orientation
        /// Page Size
        /// Number of Copies
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void InitPrintSettingsFromPrinter([MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Skybound.Gecko.CustomMarshalers.WStringMarshaler")] string aPrinterName, [MarshalAs(UnmanagedType.Interface)] nsIPrintSettings aPrintSettings);
		
		/// <summary>
        /// Reads PrintSettings values from Prefs,
        /// the values to be read are indicated by the "flags" arg.
        ///
        /// aPrintSettings should be initialized with the name of a printer. First
        /// it reads in the PrintSettings from the last print job. Then it uses the
        /// PrinterName in the PrinterSettings to read any settings that were saved
        /// just for that printer.
        ///
        /// aPS - PrintSettings to have its settings read
        /// aUsePrinterNamePrefix - indicates whether to use the printer name as a prefix
        /// aFlags - indicates which prefs to read, see nsIPrintSettings.idl for the
        /// const values.
        ///
        /// Items not read:
        /// startPageRange, endPageRange, scaling, printRange, title
        /// docURL, howToEnableFrameUI, isCancelled, printFrameTypeUsage
        /// printFrameType, printSilent, shrinkToFit, numCopies,
        /// printerName
        ///
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void InitPrintSettingsFromPrefs([MarshalAs(UnmanagedType.Interface)] nsIPrintSettings aPrintSettings, [MarshalAs(UnmanagedType.Bool)] bool aUsePrinterNamePrefix, uint aFlags);
		
		/// <summary>
        /// Writes PrintSettings values to Prefs,
        /// the values to be written are indicated by the "flags" arg.
        ///
        /// If there is no PrinterName in the PrinterSettings
        /// the values are saved as the "generic" values not associated with any printer.
        /// If a PrinterName is there, then it saves the items qualified for that Printer
        ///
        /// aPS - PrintSettings to have its settings saved
        /// aUsePrinterNamePrefix - indicates whether to use the printer name as a prefix
        /// aFlags - indicates which prefs to save, see nsIPrintSettings.idl for the const values.
        ///
        /// Items not written:
        /// startPageRange, endPageRange, scaling, printRange, title
        /// docURL, howToEnableFrameUI, isCancelled, printFrameTypeUsage
        /// printFrameType, printSilent, shrinkToFit, numCopies
        ///
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SavePrintSettingsToPrefs([MarshalAs(UnmanagedType.Interface)] nsIPrintSettings aPrintSettings, [MarshalAs(UnmanagedType.Bool)] bool aUsePrinterNamePrefix, uint aFlags);
	}
}
