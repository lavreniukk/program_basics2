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
    /// Логика взаимодействия для AdminChange.xaml
    /// </summary>
    public partial class AdminChange : Window
    {
        static int error = 0;
        static bool flag = true;
        string connectionString;
        string Query;
        SqlConnection connection;
        SqlCommand command;
        public AdminChange()
        {
            InitializeComponent();
            error = 0;
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(flag)
            {
                PasswordOld.Visibility = Visibility.Hidden;
                ShowedOld.Visibility = Visibility.Visible;
                PasswordNew.Visibility = Visibility.Hidden;
                ShowedNew.Visibility = Visibility.Visible;
                PasswordCopy.Visibility = Visibility.Hidden;
                ShowedCopy.Visibility = Visibility.Visible;

                ShowedOld.Text = PasswordOld.Password;
                ShowedNew.Text = PasswordNew.Password;
                ShowedCopy.Text = PasswordCopy.Password;
                flag = false;
            }
            else
            {
                PasswordOld.Visibility = Visibility.Visible;
                ShowedOld.Visibility = Visibility.Hidden;
                PasswordNew.Visibility = Visibility.Visible;
                ShowedNew.Visibility = Visibility.Hidden;
                PasswordCopy.Visibility = Visibility.Visible;
                ShowedCopy.Visibility = Visibility.Hidden;

                PasswordOld.Password = ShowedOld.Text;
                PasswordNew.Password = ShowedNew.Text;
                PasswordCopy.Password = ShowedCopy.Text;
                flag = true;
            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            string RealPassword = "SELECT Password From UsersBase Where Login='ADMIN'";
            command = new SqlCommand(RealPassword, connection);
            RealPassword = (string)command.ExecuteScalar();

            string CurrentHidden = PasswordOld.Password; string CurrentVisible = ShowedOld.Text;
            string HiddenPass1 = PasswordNew.Password;   string VisiblePass1 = ShowedNew.Text;
            string HiddenPass2 = PasswordCopy.Password;  string VisiblePass2 = ShowedCopy.Text;

            if (flag)
            {
                if (CurrentHidden == RealPassword && HiddenPass1 != "" && HiddenPass1 == HiddenPass2)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        Query = "UPDATE UsersBase SET Password = '" + HiddenPass1 + "' WHERE Login = 'ADMIN'";
                        command = new SqlCommand(Query, connection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Success!");
                    }
                }
                else if (CurrentHidden != RealPassword)
                {
                    if (error == 1)
                        MessageBox.Show("Wrong password!" + "\n" + $"You have 1 more attempt");
                    else
                        MessageBox.Show("Wrong password!" + "\n" + $"You have {2 - error} attempts");
                    error++;

                    if (error == 3)
                        System.Windows.Application.Current.Shutdown();
                }
                else if (HiddenPass1 != HiddenPass2)
                {
                    MessageBox.Show("Passwords do not match");
                }

                connection.Close();
            }
            else
            {
                if (CurrentVisible == RealPassword && VisiblePass1 != "" && VisiblePass1 == VisiblePass2)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        Query = "UPDATE UsersBase SET Password = '" + VisiblePass1 + "' WHERE Login = 'ADMIN'";
                        command = new SqlCommand(Query, connection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Success!");
                    }
                }
                else if (CurrentVisible != RealPassword)
                {
                    if (error == 1)
                        MessageBox.Show("Wrong password!" + "\n" + $"You have 1 more attempt");
                    else
                        MessageBox.Show("Wrong password!" + "\n" + $"You have {2 - error} attempts");
                    error++;

                    if (error == 3)
                        System.Windows.Application.Current.Shutdown();
                }
                else if (VisiblePass1 != VisiblePass2)
                {
                    MessageBox.Show("Passwords do not match");
                }

                connection.Close();
            }
        }
    }
}
