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

namespace lab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            FirstWindow fw = new FirstWindow();
            Hide();
            fw.Show();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            SecondWindow sw = new SecondWindow();
            Hide();
            sw.Show();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            ThirdWindow tw = new ThirdWindow();
            Hide();
            tw.Show();
        }

        private void Button4_Click_1(object sender, RoutedEventArgs e)
        {
            FourthWindow fw = new FourthWindow();
            Hide();
            fw.Show();
        }
    }
}
