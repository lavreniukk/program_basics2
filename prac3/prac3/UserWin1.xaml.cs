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
using System.Data;

namespace prac3
{
    /// <summary>
    /// Логика взаимодействия для UserWin1.xaml
    /// </summary>
    public partial class UserWin1 : Window
    {
        static bool flag = true;
        int error = 0;
        string Query;
        string EnteredLogin;
        string EnteredPassword;
        string connectionString;
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataTable UsersTable;
        public UserWin1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            error = 0;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (flag)
            {
                PasswordText.Visibility = Visibility.Visible;
                PasswordPole.Visibility = Visibility.Hidden;
                PasswordText.Text = PasswordPole.Password;
                flag = false;
            }
            else
            {
                PasswordText.Visibility = Visibility.Hidden;
                PasswordPole.Visibility = Visibility.Visible;
                PasswordPole.Password = PasswordText.Text;
                flag = true;
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Close();
            mw.Show();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegistrUser ru = new RegistrUser();
            Close();
            ru.Show();
        }

        private void LogInAcc_Click(object sender, RoutedEventArgs e)
        {
            EnteredLogin = LoginPole.Text;
            if (flag)
                EnteredPassword = PasswordPole.Password;
            else
                EnteredPassword = PasswordText.Text;

            connection = new SqlConnection(connectionString);
            connection.Open();
            if (connection.State == System.Data.ConnectionState.Open)
            {
                Query = "SELECT * FROM UsersBase WHERE Login = '" + EnteredLogin + "';";
                adapter = new SqlDataAdapter(Query, connection);
                UsersTable = new DataTable();
                adapter.Fill(UsersTable);
                if (UsersTable.Rows.Count == 0)
                    MessageBox.Show("No user with this login found");
                else
                {
                    bool Status = Convert.ToBoolean(UsersTable.Rows[0][4]);
                    if (!Status)
                        MessageBox.Show("You were blocked by Admin");
                    else
                    {
                        if ((UsersTable.Rows[0][2].ToString() == EnteredLogin) &&
                            (UsersTable.Rows[0][3].ToString() == EnteredPassword))
                        {
                            LoggedUser lu = new LoggedUser(LoginPole.Text);
                            Close();
                            lu.Show();
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
            connection.Close();
        }
    }
}
