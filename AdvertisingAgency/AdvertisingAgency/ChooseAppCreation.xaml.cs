using System;
using System.Collections.Generic;
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

namespace AdvertisingAgency
{
    /// <summary>
    /// Логика взаимодействия для ChooseAppCreation.xaml
    /// </summary>
    public partial class ChooseAppCreation : Page
    {
        public ChooseAppCreation()
        {
            InitializeComponent();
        }

        private void CreateNew_Click(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new NewApplication("new");
        }

        private void UpdateOld_Click(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new NewApplication("update");
        }
    }
}
