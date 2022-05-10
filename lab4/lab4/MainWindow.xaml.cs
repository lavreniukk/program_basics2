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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Query;
        string connectionString;
        DataTable UsersTable;
        SqlCommand command;
        SqlConnection connection;
        SqlDataAdapter adapter;
        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            
            connection.Open();

            Query = "SELECT CONCAT_WS(' ', Surname, Name, SecondName) AS 'Full Name' " +
                    "FROM AdvertiserInfo";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);
            foreach(DataRow row in UsersTable.Rows)
                ShowAppsOfAdvertisers.Items.Add((row["Full Name"]).ToString());

            connection.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);

            connection.Open();

            Query = "SELECT ApplicationInfo.IDofApplication AS 'ID', AdvertisingProds.ProdName AS 'PRODUCT', ApplicationDatePlace.PublicationName AS 'PUBLICATION', " +
                    "ApplicationDatePlace.ApplicationDate AS 'DATE', PriceList.Price AS 'PRICE'" +
                    "FROM ApplicationInfo INNER JOIN ApplicationDatePlace " + "ON ApplicationInfo.IDofApplication = ApplicationDatePlace.IDofApplication " +
                    "INNER JOIN AdvertisingProds " + "ON ApplicationInfo.IDofProd = AdvertisingProds.IDofProd " +
                    "INNER JOIN PriceList " + "ON ApplicationInfo.IDofPrice = PriceList.IDofPrice";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);
            DataBase.ItemsSource = UsersTable.DefaultView;
            try { (DataBase.Columns[3] as DataGridTextColumn).Binding.StringFormat = "yyyy-MM-dd"; }
            catch { }

            connection.Close();
        }

        private void ShowPrices_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);

            connection.Open();

            Query = "SELECT IDofPrice AS 'ID', Price AS 'PRICE', CASE WHEN Color = 'True' THEN N'Кольоровий' ELSE N'Ч/Б' END AS 'COLOR', PublicationType AS 'PUBLICATION' FROM PriceList";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);
            DataBase.ItemsSource = UsersTable.DefaultView;

            connection.Close();
        }

        private void ShowPublications_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);

            connection.Open();

            Query = "SELECT PublicationName AS Name, PublicationType AS Type " +
                    "FROM PublicationsInfo " +
                    "ORDER BY PublicationType";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);
            DataBase.ItemsSource = UsersTable.DefaultView;

            connection.Close();
        }

        private void ShowApplications_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);

            connection.Open();

            Query = "SELECT ApplicationDatePlace.IDofApplication AS ID, ApplicationDatePlace.PublicationName AS PUBLICATION, CONCAT_WS(' ', Surname, Name, SecondName) AS 'FULL NAME', " +
                    "PriceList.Price AS PRICE, CASE WHEN ApplicationInfo.IsPaid = 'True' THEN N'Сплачено' ELSE N'Не сплачено' END AS 'PAYING' " +
                    "FROM ApplicationDatePlace INNER JOIN ApplicationInfo " +
                    "ON ApplicationDatePlace.IDofApplication = ApplicationInfo.IDofApplication " +
                    "INNER JOIN PriceList ON ApplicationInfo.IDofPrice = PriceList.IDofPrice " + 
                    "INNER JOIN AdvertiserApplication ON AdvertiserApplication.IDofApplication = ApplicationInfo.IDofApplication " +
                    "INNER JOIN AdvertiserInfo ON AdvertiserInfo.IDofAdvertiser = AdvertiserApplication.IDofAdvertiser";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);
            DataBase.ItemsSource = UsersTable.DefaultView;

            connection.Close();
        }

        private void ShowAppsOfAdvertisers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            connection = new SqlConnection(connectionString);

            connection.Open();

            Query = "SELECT ApplicationInfo.IDofApplication AS ID, AdvertisingProds.ProdName AS PRODUCT, ApplicationDatePlace.ApplicationDate AS DATE, " +
                    "PriceList.Price AS PRICE, CONCAT_WS(' ', Surname, Name, SecondName) AS 'Full Name' " +
                    "FROM ApplicationInfo " +
                    "INNER JOIN ApplicationDatePlace ON ApplicationInfo.IDofApplication = ApplicationDatePlace.IDofApplication " +
                    "INNER JOIN AdvertisingProds ON ApplicationInfo.IDofProd = AdvertisingProds.IDofProd " +
                    "INNER JOIN PriceList On PriceList.IDofPrice = ApplicationInfo.IDofPrice " +
                    "INNER JOIN AdvertiserApplication ON ApplicationInfo.IDofApplication = AdvertiserApplication.IDofApplication " +
                    "INNER JOIN AdvertiserInfo ON AdvertiserApplication.IDofAdvertiser = AdvertiserInfo.IDofAdvertiser " +
                    "WHERE CONCAT_WS(' ', Surname, Name, SecondName) = N'" + ShowAppsOfAdvertisers.SelectedItem.ToString() + "'";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);
            DataBase.ItemsSource = UsersTable.DefaultView;
            try { (DataBase.Columns[2] as DataGridTextColumn).Binding.StringFormat = "yyyy-MM-dd"; }
            catch { }

            connection.Close();
        }

        private void UpdateDateData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                Query = "SELECT ApplicationInfo.IDofApplication AS ID, AdvertisingProds.ProdName AS PRODUCT, " +
                    "ApplicationDatePlace.PublicationName AS PUBLICATION, FORMAT(ApplicationDatePlace.ApplicationDate, 'dd.MM.yyyy') AS DATE, PriceList.Price AS PRICE " +
                    "FROM ApplicationInfo " +
                    "INNER JOIN ApplicationDatePlace ON ApplicationInfo.IDofApplication = ApplicationDatePlace.IDofApplication " +
                    "INNER JOIN AdvertisingProds ON ApplicationInfo.IDofProd = AdvertisingProds.IDofProd " +
                    "INNER JOIN PriceList ON ApplicationInfo.IDofPrice = PriceList.IDofPrice " +
                    "WHERE ApplicationDate = '" + ShowOnDate.Text + "'";
                command = new SqlCommand(Query, connection);
                adapter = new SqlDataAdapter(command);
                UsersTable = new DataTable();
                adapter.Fill(UsersTable);
                DataBase.ItemsSource = UsersTable.DefaultView;
                try { (DataBase.Columns[3] as DataGridTextColumn).Binding.StringFormat = "yyyy-MM-dd"; }
                catch { }

                connection.Close();
            }
            catch
            {
                MessageBox.Show("No such date found!");
            }

        }
    }
}
