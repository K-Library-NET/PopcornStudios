using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AircraftDataAnalysisWinRT.MyControl
{
    public interface IConfirmDialog<T>
    {
        void Show(Grid parent, T dataContext);

        void Close();
    }
}
