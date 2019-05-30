using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using Newtonsoft.Json;
using EarthQuakeControlWork;
using System.Text.RegularExpressions;
using EarthQuakeModels;

namespace EarthQuakeControlWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            itemsDataGrid.IsReadOnly = true;
        }

        private void TextBoxSearchCountPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        public void ShowInfo(int count)
        {
            using (WebClient web = new WebClient())
            {
                var url = $"https://earthquake.usgs.gov/fdsnws/event/1/query?format=geojson&limit={count}";

                var json = web.DownloadString(url);

                var result = JsonConvert.DeserializeObject<EarthClassJson.GeoData>(json);

                EarthClassJson.GeoData data = result;

                List<EarthClass> earthClasses = new List<EarthClass>();
                for (int i = 0; i < count; i++)
                { 
                earthClasses.Add(new EarthClass { Magnitude = data.features[i].properties.mag, Place = data.features[i].properties.place, Time = data.features[i].properties.time, EpicenterDepth = data.features[i].properties.sig });
                }
                itemsDataGrid.ItemsSource = earthClasses;
            }
        }

        private void ShowButtonClick(object sender, RoutedEventArgs e)
        {
            ShowInfo(int.Parse(countRecordsTextBox.Text));
        }
    }

}
