using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System;
using System.Globalization;

namespace AdvertisingAgency
{
    /// <summary>
    /// Логика взаимодействия для NewApplication.xaml
    /// </summary>
    public partial class NewApplication : Page
    {
        CultureInfo provider = CultureInfo.InvariantCulture;

        string Query;
        string connectionString;
        DataTable UsersTable;
        SqlCommand command;
        SqlConnection connection;
        SqlDataAdapter adapter;

        int ClientID;
        int ProdID;
        int ApplicationID;
        double Price;
        string PublicationType;
        bool IsColored;
        public NewApplication(string option)
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);

            if (option == "new")
            {
                LoadCB_Client();
                LoadCB_Product();
                LoadCB_Publication(PublicationCB);
                New.Visibility = Visibility.Visible;
            }
            else
            {
                LoadCB_ID();
                LoadCB_Publication(OldAppPublicationsCB);
                Update.Visibility = Visibility.Visible;
            }
        }

        private void AddApplication_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] SelectedProd = ProductsCB.SelectedItem.ToString().Split();
                ProdID = Convert.ToInt32(SelectedProd[0]);
                GetClientID();
                DateTime dt = DateTime.ParseExact(DateOfApp.Text, "yyyy-MM-dd", provider);

                connection.Open();

                //вставка інформації про заявку
                Query = "INSERT INTO ApplicationInfo(ApplicationText, IDofProd, Price, IsPaid, Color) " +
                        "VALUES(N'" + TextOfApp.Text + "', " + ProdID + ", " + Price + ", 'False', '" + IsColored + "')";
                command = new SqlCommand(Query, connection);
                command.ExecuteNonQuery();

                //отримання ID нової заявки
                Query = "SELECT TOP 1 IDofApplication FROM ApplicationInfo ORDER BY IDofApplication DESC";
                command = new SqlCommand(Query, connection);
                ApplicationID = Convert.ToInt32(command.ExecuteScalar());

                //вставка розміщення і дати заявки
                Query = "INSERT INTO ApplicationDatePlace(IDofApplication, PublicationName, ApplicationDate) " +
                        "VALUES(" + ApplicationID + ", N'" + PublicationCB.SelectedItem.ToString() +
                        "', '" + dt.ToString("yyyy-MM-dd") + "')";
                command = new SqlCommand(Query, connection);
                command.ExecuteNonQuery();

                //пов'язання заявки з рекламодавцем
                Query = "INSERT INTO AdvertiserApplication(IDofAdvertiser, IDofApplication)" +
                      "VALUES(" + ClientID + ", " + ApplicationID + ")";
                command = new SqlCommand(Query, connection);
                command.ExecuteNonQuery();

                connection.Close();
                //зробити кастомний месседж збоку екрану
                MessageWindow mw = new MessageWindow();
                mw.ShowMessage("Успішно!", "success");
            }
            catch
            {
                MessageWindow mw = new MessageWindow();
                mw.ShowMessage("Помилка!", "error");
            }
        }

        public void GetClientID()
        {
            connection.Open();

            Query = "SELECT IDofAdvertiser " +
                    "FROM AdvertiserInfo " +
                    "WHERE (CONCAT_WS(' ', Surname, Name, SecondName) = N'" + ClientCB.SelectedItem + "')";
            command = new SqlCommand(Query, connection);

            ClientID = Convert.ToInt32(command.ExecuteScalar());

            connection.Close();
        }

        public void GetColorOfApp()
        {
            try
            {
                connection.Open();

                Query = "SELECT Color " +
                        "FROM ApplicationInfo " +
                        "WHERE (ApplicationInfo.IDofApplication = " + IDcb.SelectedItem.ToString() + ")";
                command = new SqlCommand(Query, connection);

                IsColored = Convert.ToBoolean(command.ExecuteScalar());

                connection.Close();
            }
            catch { connection.Close(); }
        }

        public void AppPriceNew()
        {
            if ((Color_BW.IsChecked == true || Color.IsChecked == true) &&
                !string.IsNullOrEmpty(PublicationCB.SelectedItem.ToString()))
            {
                if (Color.IsChecked == true)
                    IsColored = true;
                else
                    IsColored = false;

                connection.Open();

                Query = "SELECT Price " +
                        "FROM PriceList " +
                        "WHERE (Color = '" + IsColored + "') AND " +
                        "(PublicationType = N'" + PublicationType + "') ";
                command = new SqlCommand(Query, connection);
                Price = Convert.ToDouble(command.ExecuteScalar());

                PriceOfApp.Text = Price.ToString();

                connection.Close();
            }
        }

        public void OldAppPrice()
        {
            try
            {
                GetColorOfApp();
                connection.Open();

                Query = "SELECT Price " +
                        "FROM PriceList " +
                        "WHERE (Color = '" + IsColored + "') AND " +
                        "(PublicationType = N'" + PublicationType + "') ";
                command = new SqlCommand(Query, connection);

                Price = Convert.ToDouble(command.ExecuteScalar());
                PriceOfOldApp.Text = Price.ToString();

                connection.Close();
            }
            catch { connection.Close(); }
        }

        public void LoadCB_ID()
        {
            connection.Open();

            Query = "SELECT IDofApplication AS 'ID' " +
                    "FROM ApplicationInfo";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);
            foreach (DataRow row in UsersTable.Rows)
                IDcb.Items.Add((row["ID"]).ToString());

            connection.Close();
        }

        public void LoadCB_Client()
        {
            connection.Open();

            Query = "SELECT CONCAT_WS(' ', Surname, Name, SecondName) AS 'Full Name' " +
                    "FROM AdvertiserInfo";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);
            foreach (DataRow row in UsersTable.Rows)
                ClientCB.Items.Add((row["Full Name"]).ToString());

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

        public void LoadCB_Publication(ComboBox cb)
        {
            connection.Open();

            Query = "SELECT PublicationName AS 'PublName' " +
                    "FROM PublicationsInfo";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);
            foreach (DataRow row in UsersTable.Rows)
                cb.Items.Add((row["PublName"]).ToString());

            connection.Close();
        }

        private void PublicationCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            connection.Open();

            Query = "SELECT PublicationType AS 'PublType' " +
                    "FROM PublicationsInfo " +
                    "WHERE PublicationName = N'" + PublicationCB.SelectedItem.ToString() + "'";
            command = new SqlCommand(Query, connection);
            PublicationType = command.ExecuteScalar().ToString();

            connection.Close();

            AppPriceNew();
            EnableButton();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableButton();
        }

        private void Color_Checked(object sender, RoutedEventArgs e)
        {
            AppPriceNew();
            EnableButton();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButton();
        }

        public void EnableButton()
        {
            if (New.Visibility == Visibility.Visible)
            {
                try
                {
                    if (string.IsNullOrEmpty(ClientCB.SelectedItem.ToString()) ||
                        string.IsNullOrEmpty(ProductsCB.SelectedItem.ToString()) ||
                        string.IsNullOrEmpty(PublicationCB.SelectedItem.ToString()) ||
                        string.IsNullOrEmpty(TextOfApp.Text) ||
                        string.IsNullOrEmpty(DateOfApp.Text) ||
                        (Color.IsChecked == false &&
                        Color_BW.IsChecked == false))
                        AddApplication.IsEnabled = false;
                    else
                        AddApplication.IsEnabled = true;
                }
                catch { }
            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(IDcb.SelectedItem.ToString()) ||
                        string.IsNullOrEmpty(OldAppPublicationsCB.SelectedItem.ToString()) ||
                        string.IsNullOrEmpty(DateOfOldApp.Text))
                        UpdateOldApp.IsEnabled = false;
                    else
                        UpdateOldApp.IsEnabled = true;
                }
                catch { }
            }
        }

        private void UpdateOldApp_Click(object sender, RoutedEventArgs e)
        {
            double price;
            DateTime dt = DateTime.ParseExact(DateOfOldApp.Text, "yyyy-MM-dd", provider);

            connection.Open();
            Query = "SELECT Price " +
                    "FROM ApplicationInfo " +
                    "WHERE (IDofApplication = " + IDcb.SelectedItem.ToString() + ")";
            command = new SqlCommand(Query, connection);

            price = Convert.ToDouble(command.ExecuteScalar());
            price += Convert.ToDouble(PriceOfOldApp.Text);

            Query = "INSERT INTO ApplicationDatePlace(IDofApplication, PublicationName, ApplicationDate) " +
                    "VALUES (" + IDcb.SelectedItem.ToString() + ", N'" + OldAppPublicationsCB.SelectedItem.ToString() +   
                    "', '" + dt.ToString("yyyy-MM-dd") + "')";
            command = new SqlCommand(Query, connection);
            command.ExecuteNonQuery();

            Query = "UPDATE ApplicationInfo " +
                    "SET Price = " + price +
                    " WHERE (IDofApplication = " + IDcb.SelectedItem.ToString() + ")";
            command = new SqlCommand(Query, connection);
            command.ExecuteNonQuery();

            connection.Close();

            MessageWindow mw = new MessageWindow();
            mw.ShowMessage("Успіх", "success");
        }

        private void OldAppPublicationsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            connection.Open();

            Query = "SELECT PublicationType AS 'PublType' " +
                    "FROM PublicationsInfo " +
                    "WHERE PublicationName = N'" + OldAppPublicationsCB.SelectedItem.ToString() + "'";
            command = new SqlCommand(Query, connection);
            PublicationType = command.ExecuteScalar().ToString();

            connection.Close();

            OldAppPrice();
            EnableButton();
        }

        private void IDcb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OldAppPrice();
            EnableButton();
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new ChooseAppCreation();
        }
    }
}
