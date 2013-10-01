﻿using System;
using System.Collections.Generic;
using BaseTypes = Gecko.BaseTypes;
using System.Runtime.InteropServices;
using System.Text;
using Gecko.Interop;

namespace Gecko.Cache
{
	public static class ImageCache
	{
		private static ComPtr<imgICache> _imgCache;

		static ImageCache()
		{
			_imgCache = Xpcom.GetService2<imgICache>(Contracts.ImageCache);
		}

		public static void ClearCache(bool chrome)
		{
			_imgCache.Instance.ClearCache(chrome);
			
		}

		public static Properties FindEntryProperties(string url)
		{
            var xpComUri = IOService.CreateNsIUri(url);
            var ret = ExtensionMethods.Wrap(_imgCache.Instance.FindEntryProperties(xpComUri), (x) => new Properties(x));
			//var ret= _imgCache.Instance.FindEntryProperties( xpComUri ).Wrap( ( x ) => new Properties( x ) );
			Marshal.ReleaseComObject( xpComUri );
			return ret;
		}

		public static void RemoveEntry(string url)
		{
			var xpComUri = IOService.CreateNsIUri(url);
			_imgCache.Instance.RemoveEntry(xpComUri);
			Marshal.ReleaseComObject(xpComUri);
		}


	}
}
