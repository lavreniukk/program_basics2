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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Image logo = new Image();
            BitmapImage bitLogo = new BitmapImage();
            bitLogo.BeginInit();
            bitLogo.UriSource = new Uri("Images/logo.png", UriKind.Relative);
            bitLogo.EndInit();
            logo.Stretch = Stretch.Fill;
            logo.Source = bitLogo;
            logo.Height = 250;
            logo.Width = 250;
            WorkingWindow.Content = logo;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            string TypeOfWindow = ((MenuButton)sender).Text.ToString();
            
            switch (TypeOfWindow)
            {
                case "Переглянути":
                    WorkingWindow.Content = new ChooseShow();
                    break;
                case "Нова заявка":
                    WorkingWindow.Content = new ChooseAppCreation();
                    break;
                case "Новий клієнт":
                    WorkingWindow.Content = new NewClient();
                    break;
                case "Нова продукція":
                    WorkingWindow.Content = new NewProduct();
                    break;
                case "Оплата":
                    WorkingWindow.Content = new Payment();
                    break;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
