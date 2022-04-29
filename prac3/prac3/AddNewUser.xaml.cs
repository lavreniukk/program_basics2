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
    /// Логика взаимодействия для AddNewUser.xaml
    /// </summary>
    public partial class AddNewUser : Window
    {
        string connectionString;
        string Query;
        SqlConnection connection;
        SqlCommand command;
        public AddNewUser()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ((AdminWin)this.Owner).UpdateData();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (ShowedPole.Text != "")
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string UserLogin = ShowedPole.Text;
                try
                {
                    if(connection.State == System.Data.ConnectionState.Open)
                    {
                        Query = "INSERT INTO UsersBase (Name, Surname, Login, Status, Restriction)" +
                                " values('','','" + UserLogin + "', 1, 0);";
                        command = new SqlCommand(Query, connection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Success!");
                    }
                }
                catch
                {
                    MessageBox.Show("User with the same login already exists");
                }
                connection.Close();
            }
        }
    }
}
