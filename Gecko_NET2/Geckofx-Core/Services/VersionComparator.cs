﻿using System;
using System.Collections.Generic;
using BaseTypes = Gecko.BaseTypes;
using System.Text;
using Gecko.Interop;

namespace Gecko.Services
{
	public static class VersionComparator
	{
		private static ComPtr<nsIVersionComparator> _versionComparator;

		static VersionComparator()
		{
			_versionComparator = Xpcom.GetService2<nsIVersionComparator>(Contracts.VersionComparator);
		}


		public static int Compare(string x, string y)
		{
			return nsString.Pass<int>(_versionComparator.Instance.Compare, x, y);
		}
	}
}
