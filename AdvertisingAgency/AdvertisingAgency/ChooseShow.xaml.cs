using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AdvertisingAgency
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class ChooseShow : Page
    {
        public ChooseShow()
        {
            InitializeComponent();
        }

        private void ShowApps_Click(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new ShowApps();
        }

        private void Catalogue_Click(object sender, RoutedEventArgs e)
        {
            string option = "catalogue";
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new ShowCatalOrPubl(option);
        }

        private void Prices_Click(object sender, RoutedEventArgs e)
        {
            string option = "prices";
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new ShowCatalOrPubl(option);
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            string option = "products";
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new ShowCatalOrPubl(option);
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            string option = "clients";
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new ShowCatalOrPubl(option);
        }
    }
}
