using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmartWeatherApp.Classes;

namespace SmartWeatherApp
{
    public partial class MainWindow : Window
    {
        public Manager manager;
        public MainWindow()
        {
            const string APPID = "2f63cb4d02e27e9b67128d8982fd7d43"; // APPID
            manager = new Manager(APPID); // create new manager instance
            
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ResponseObject weather = manager.GetWeatherByCityName(CityNameInput.Text);
                Temp.Text += Convert.ToString(weather.Temperature) + " C";
                MinTemp.Text += Convert.ToString(weather.TemperatureMin) + " C";
                MaxTemp.Text += Convert.ToString(weather.TemperatureMax) + " C";
                Humidity.Text += Convert.ToString(weather.Humidity) + " %";
                Pressure.Text += Convert.ToString(weather.Pressure) + " hPa";
                WindSpeed.Text += Convert.ToString(weather.WindSpeed) + " m/s";
                CloudPerc.Text += Convert.ToString(weather.CloudPercentage) + " %";
                Lat.Text += Convert.ToString(weather.Lat);
                Lon.Text += Convert.ToString(weather.Lon); 
                CityName.Text += weather.CityName;
                Description.Text += weather.Description;

                manager.GetCities();

                foreach (var city in manager.Cities)
                {
                    CitiesView.Items.Add(new { city.Name, city.Country});
                }
                InfoBox.Text = "Data loaded";
            }
            catch(Exception exc)
            {
                var msg = exc.Message;
                InfoBox.Text += "\n" + msg;

            }
            
        }

        private void CityNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CityNameInput.Text.Equals("Enter city name:"))
            {
                CityNameInput.Clear();
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in CitiesView.Items)
            {
                if ( item.ToString().Contains(Search.Text))
                {
                    SearchResults.Items.Add(item);
                }
            }
        }
    }
}
