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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;

namespace prac3
{
    /// <summary>
    /// Логика взаимодействия для AdminCheck.xaml
    /// </summary>
    public partial class AdminCheck : Window
    {
        static int error = 0;
        string connectionString;
        SqlConnection connection;

        static bool flag = true;
        public AdminCheck()
        {
            InitializeComponent();
            error = 0;
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(flag)
            {
                ShowedPole.Visibility = Visibility.Visible;
                PasswordPole.Visibility = Visibility.Hidden;
                ShowedPole.Text = PasswordPole.Password;
                flag = false;
            }
            else
            {
                ShowedPole.Visibility = Visibility.Hidden;
                PasswordPole.Visibility = Visibility.Visible;
                PasswordPole.Password = ShowedPole.Text;
                flag = true;
            }
        }
        
        private void LoginAdmin(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            string Query = "SELECT Password FROM UsersBase WHERE Login='ADMIN';";
            SqlCommand adminPass = new SqlCommand(Query, connection);
            string realPass = (string)adminPass.ExecuteScalar();

            if (PasswordPole.Password == realPass || ShowedPole.Text == realPass)
            {
                AdminWin aw = new AdminWin();
                connection.Close();
                Close();
                aw.Show();
            }
            else
            {
                if (error == 1)
                    MessageBox.Show("Wrong password!" + "\n" + $"You have 1 more attempt");
                else
                    MessageBox.Show("Wrong password!" + "\n" + $"You have {2 - error} attempts");
                error++;

                if (error == 3)
                    System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
