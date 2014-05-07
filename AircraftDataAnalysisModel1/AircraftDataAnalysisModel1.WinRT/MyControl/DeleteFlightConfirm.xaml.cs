using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AircraftDataAnalysisWinRT.DataModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AircraftDataAnalysisWinRT.Services;
using System.Threading.Tasks;
using AircraftDataAnalysisWinRT;
using System.Collections.ObjectModel;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace AircraftDataAnalysisModel1.WinRT
{
    public sealed partial class DeleteFlightConfirm : AircraftDataAnalysisWinRT.Common.LayoutAwarePage
    {
        public DeleteFlightConfirm()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.SetLoading();
            var flights = ServerHelper.GetAllFlights(AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentAircraftModel);
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                 new Windows.UI.Core.DispatchedHandler(() =>
                 {
                     this.grdFlights.ItemsSource = flights;
                     this.SetCurrentFlight();
                     this.SetUnloading();
                 }));
        }

        private void SetCurrentFlight()
        {
            try
            {
                if (this.grdFlights.ItemsSource != null && this.grdFlights.ItemsSource
                    is IEnumerable<FlightDataEntitiesRT.Flight>)
                {
                    var flights = this.grdFlights.ItemsSource as IEnumerable<FlightDataEntitiesRT.Flight>;

                    if (flights != null && flights.Count() > 0 && ApplicationContext.Instance.CurrentFlight != null)
                    {
                        var f = flights.FirstOrDefault(new Func<FlightDataEntitiesRT.Flight, bool>(
                            delegate(FlightDataEntitiesRT.Flight flight)
                            {
                                if (flight.FlightID == ApplicationContext.Instance.CurrentFlight.FlightID)
                                    return true;
                                return false;
                            }));

                        this.grdFlights.SelectedItem = f;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void btImport_Click(object sender, RoutedEventArgs e)
        {
            SetLoading();
            IEnumerable<FlightDataEntitiesRT.Flight> flights = this.GetSelectedFlights();
            List<Task<string>> tasks = new List<Task<string>>();
            foreach (var flight in flights)
            {
                tasks.Add(DataInputHelper.DeleteExistsDataAsync(flight));
                tasks.Add(DataInputHelper.DeleteFlightAsync(flight));
            }
            Task.WaitAll(tasks.ToArray());
            SetUnloading();
            this.Frame.Navigate(typeof(PStudio.WinApp.Aircraft.FDAPlatform.MainPage));
        }

        private void SetUnloading()
        {
            this.progbar1.IsIndeterminate = false;
            this.btImport.IsEnabled = true;
            this.btCancel.IsEnabled = true;
            this.grdFlights.IsEnabled = true;
        }

        private void SetLoading()
        {
            this.progbar1.IsIndeterminate = true;
            this.btImport.IsEnabled = false;
            this.btCancel.IsEnabled = false;
            this.grdFlights.IsEnabled = false;
        }

        private IEnumerable<FlightDataEntitiesRT.Flight> GetSelectedFlights()
        {
            if (this.grdFlights.SelectedItems != null)
            {
                var items = from one in this.grdFlights.SelectedItems
                            where one != null && one is FlightDataEntitiesRT.Flight
                            select one as FlightDataEntitiesRT.Flight;

                return items;
            }

            return new FlightDataEntitiesRT.Flight[] { };
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }

    internal class DeleteFlightConfirmDataContext : AircraftDataAnalysisWinRT.Common.BindableBase
    {
        public DeleteFlightConfirmDataContext()
        {
        }

        public FlightDataEntitiesRT.Flight[] Flights { get; set; }
    }
}
