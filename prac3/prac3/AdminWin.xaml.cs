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
    /// Логика взаимодействия для AdminWin.xaml
    /// </summary>
    public partial class AdminWin : Window
    {
        DataTable UsersTable;
        bool StatData;
        bool ResData;
        int index = 0;
        string Query;
        string connectionString;
        SqlCommand command;
        SqlConnection connection;
        public AdminWin()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            UpdateData();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void ChangePassAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminChange ach = new AdminChange();
            ach.Show();
        }
        private void AddLogin_Click(object sender, RoutedEventArgs e)
        {
            AddNewUser anu = new AddNewUser();
            anu.Owner = this;
            anu.Show();
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Close();
            mw.Show();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (index < UsersTable.Rows.Count - 1)
            {
                index++;
                Name.Text = UsersTable.Rows[index][0].ToString();
                Surname.Text = UsersTable.Rows[index][1].ToString();
                Login.Text = UsersTable.Rows[index][2].ToString();
                Status.Text = UsersTable.Rows[index][3].ToString();
                Restriction.Text = UsersTable.Rows[index][4].ToString();
            }
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (index > 0)
            {
                index--;
                Name.Text = UsersTable.Rows[index][0].ToString();
                Surname.Text = UsersTable.Rows[index][1].ToString();
                Login.Text = UsersTable.Rows[index][2].ToString();
                Status.Text = UsersTable.Rows[index][3].ToString();
                Restriction.Text = UsersTable.Rows[index][4].ToString();
            }
        }

        private void ChangeStatRes_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            StatData = (bool)StatCheck.IsChecked;
            ResData = (bool)ResCheck.IsChecked;
            if (connection.State == System.Data.ConnectionState.Open)
            {
                Query = "UPDATE UsersBase SET Status='" + StatData +
                    "', Restriction = '" + ResData + "' WHERE Login = '" +
                    Login.Text.ToString() + "';";
                command = new SqlCommand(Query, connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
            UpdateData();
        }
        public void UpdateData()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            Query = "SELECT Name, Surname, Login, Status, Restriction FROM UsersBase";
            command = new SqlCommand(Query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);
            DataBase.ItemsSource = UsersTable.DefaultView;
            connection.Close();
            Name.Text = UsersTable.Rows[0][0].ToString();
            Surname.Text = UsersTable.Rows[0][1].ToString();
            Login.Text = UsersTable.Rows[0][2].ToString();
            Status.Text = UsersTable.Rows[0][3].ToString();
            Restriction.Text = UsersTable.Rows[0][4].ToString();
        }
    }
}
