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
    /// Логика взаимодействия для LoggedUser.xaml
    /// </summary>
    public partial class LoggedUser : Window
    {
        string Query;
        string connectionString;
        SqlConnection connection;
        SqlDataAdapter adapter;
        SqlCommand command;
        public LoggedUser(string Login)
        {
            InitializeComponent();
            UsersLogin.Content = Login;
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            UserWin1 uw = new UserWin1();
            Close();
            uw.Show();
        }
        private void ChangeDataUser(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            Query = "SELECT Restriction FROM UsersBase WHERE Login='" + UsersLogin.Content + "';";
            command = new SqlCommand(Query, connection);
            bool RestrOfUser = (bool)command.ExecuteScalar();

            string NewName = NamePole.Text;
            string NewSur = SurnamePole.Text;
            string NewPassword1 = PasswordPole.Text;
            string NewPassword2 = RepeatPole.Text;

            if (connection.State == System.Data.ConnectionState.Open)
            {
                if ((NewPassword1 == NewPassword2) && (NewPassword1 != ""))
                {
                    bool flag = RestrictionCheck(NewPassword1);
                    if (RestrOfUser)
                    {
                        if (flag)
                        {
                            Query = "UPDATE UsersBase SET Name = '" + NewName + "', " +
                                "Surname = '" + NewSur + "', " + "Password = '" + NewPassword1 + "' " +
                                "WHERE Login = '" + UsersLogin.Content + "';";
                            command = new SqlCommand(Query, connection);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Success!");
                        }
                        else
                        {
                            MessageBox.Show("Password doesn't have letter of upper and lower register, and maths operators! Choose another one");
                        }
                    }
                    else
                    {
                        Query = "UPDATE UsersBase SET Name = '" + NewName + "', " +
                                "Surname = '" + NewSur + "', " + "Password = '" + NewPassword1 + "' " +
                                "WHERE Login = '" + UsersLogin.Content + "';";
                        command = new SqlCommand(Query, connection);
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("Passwords do not match! Or password is empty");
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
