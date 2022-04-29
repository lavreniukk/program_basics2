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
    /// Логика взаимодействия для RegistrUser.xaml
    /// </summary>
    public partial class RegistrUser : Window
    {
        string Query;
        string connectionString;
        SqlConnection connection;
        SqlDataAdapter adapter;
        SqlCommand command;
        public RegistrUser()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            UserWin1 uw = new UserWin1();
            Close();
            uw.Show();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void RegisterNewUser(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            string RegName = NamePole.Text;
            string RegSur = SurnamePole.Text;
            string RegLog = LoginPole.Text;
            string RegPassword1 = PasswordPole.Text;
            string RegPassword2 = RepeatPole.Text;

            if (connection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    if ((RegPassword1 == RegPassword2) && (RegPassword1 != "") && RestrictionCheck(RegPassword1))
                    {
                        Query = "INSERT INTO UsersBase VALUES ('" + RegName + "', '" + 
                            RegSur + "', '" + RegLog + "', '" + RegPassword1 + "', 'True', 'True');";
                        command = new SqlCommand(Query, connection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Success!");
                    }
                    else
                    {
                        MessageBox.Show("Account wasn't created! Try again!");
                    }
                }
                catch
                {
                    MessageBox.Show("Account with the same login already exists");
                }
            }
            connection.Close();
        }
        bool RestrictionCheck(string Password)
        {
            byte Count1, Count2, Count3;
            byte PasswordLength = (byte)Password.Length;

            Count1 = Count2 = Count3 = 0;
            for (byte i = 0; i < PasswordLength; i++)
            {
                if ((Convert.ToInt32(Password[i]) >= 65) &&
                    (Convert.ToInt32(Password[i]) <= 65 + 25))
                    Count1++;
                if ((Convert.ToInt32(Password[i]) >= 97) &&
                    (Convert.ToInt32(Password[i]) <= 97 + 25))
                    Count2++;
                if ((Password[i] == '+') || (Password[i] == '-') ||
                    (Password[i] == '*') || (Password[i] == '/'))
                    Count3++;
            }

            return (Count1 * Count2 * Count3 != 0);
        }
    }
}
