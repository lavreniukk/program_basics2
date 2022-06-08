using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AdvertisingAgency
{
    /// <summary>
    /// Логика взаимодействия для UpdateApp.xaml
    /// </summary>
    public partial class UpdateApp : Page
    {
        string Query;
        string connectionString;
        DataTable UsersTable;
        SqlCommand command;
        SqlConnection connection;
        SqlDataAdapter adapter;

        string ChosenProd;
        string ChosenText;
        public UpdateApp()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            LoadCB_ID();
            LoadCB_Product();
        }

        public void LoadCB_ID()
        {
            IDofApp.Items.Clear();
            connection.Open();

            Query = "SELECT IDofApplication AS 'ID' " +
                    "FROM ApplicationInfo";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);

            foreach (DataRow row in UsersTable.Rows)
                IDofApp.Items.Add((row["ID"]).ToString());

            connection.Close();
        }

        public void LoadCB_Product()
        {
            connection.Open();

            Query = "SELECT CONCAT_WS(' ', IDofProd, ProdName) AS 'Product' " +
                    "FROM AdvertisingProds";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);
            foreach (DataRow row in UsersTable.Rows)
                ProductsCB.Items.Add((row["Product"]).ToString());

            connection.Close();
        }

        public void EnableUpdateButton()
        {
            try
            {
                if (string.IsNullOrEmpty(TextOfApp.Text) ||
                    string.IsNullOrEmpty(ProductsCB.SelectedItem.ToString()))
                    UpdateAppBtn.IsEnabled = false;
                else
                    UpdateAppBtn.IsEnabled = true;
            }
            catch { UpdateAppBtn.IsEnabled = false; }
        }

        public void EnableDelButton()
        {
            try
            {
                if (string.IsNullOrEmpty(IDofApp.SelectedItem.ToString()))
                    DelAppBtn.IsEnabled = false;
                else
                    DelAppBtn.IsEnabled = true;
            }
            catch { DelAppBtn.IsEnabled = false; }
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new ChooseShow();
        }

        private void UpdateAppBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] selectedItem = ProductsCB.SelectedItem.ToString().Split();
            string ProdID = selectedItem[0];

            connection.Open();

            Query = "UPDATE ApplicationInfo " +
                    "SET ApplicationText = N'" + TextOfApp.Text + "', " +
                    "IDofProd = " + ProdID +
                    "WHERE IDofApplication = " + IDofApp.SelectedItem.ToString();
            command = new SqlCommand(Query, connection);
            command.ExecuteNonQuery();

            connection.Close();
            MessageWindow mw = new MessageWindow();
            mw.ShowMessage("Успішно!", "success");
        }

        private void DelAppBtn_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();

            Query = "DELETE FROM ApplicationInfo " +
                    "WHERE IDofApplication = " + IDofApp.SelectedItem.ToString();
            command = new SqlCommand(Query, connection);
            command.ExecuteNonQuery();

            connection.Close();

            ProductsCB.SelectedItem = null;
            TextOfApp.Text = "";

            LoadCB_ID();
            EnableDelButton();

            MessageWindow mw = new MessageWindow();
            mw.ShowMessage("Успішно!", "success");
        }

        private void IDofApp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                connection.Open();

                Query = "SELECT ApplicationText, CONCAT_WS(' ', AdvertisingProds.IDofProd, AdvertisingProds.ProdName) " +
                        "FROM ApplicationInfo INNER JOIN " +
                        "AdvertisingProds ON ApplicationInfo.IDofProd = AdvertisingProds.IDofProd " +
                        "WHERE ApplicationInfo.IDofApplication = " + IDofApp.SelectedItem.ToString();
                command = new SqlCommand(Query, connection);
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                ChosenText = reader.GetValue(0).ToString();
                ChosenProd = reader.GetValue(1).ToString();

                connection.Close();

                TextOfApp.Text = ChosenText;
                ProductsCB.SelectedIndex = ProductsCB.Items.IndexOf(ChosenProd);
                EnableDelButton();
            }
            catch { IDofApp.SelectedItem = null; connection.Close(); }
        }

        private void ProductsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableUpdateButton();
        }

        private void TextOfApp_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableUpdateButton();
        }
    }
}
