using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace AdvertisingAgency
{
    /// <summary>
    /// Логика взаимодействия для UpdateProd.xaml
    /// </summary>
    public partial class UpdateProd : Page
    {
        string Query;
        string connectionString;
        DataTable UsersTable;
        SqlCommand command;
        SqlConnection connection;
        SqlDataAdapter adapter;
        public UpdateProd()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            LoadCB_ID();
        }

        public void LoadCB_ID()
        {
            IDofProd.Items.Clear();
            connection.Open();

            Query = "SELECT IDofProd AS 'ID' " +
                    "FROM AdvertisingProds" +
                    "";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);

            foreach (DataRow row in UsersTable.Rows)
                IDofProd.Items.Add((row["ID"]).ToString());

            connection.Close();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableUpdateButton();
        }

        public void EnableUpdateButton()
        {
            try
            {
                if (string.IsNullOrEmpty(IDofProd.SelectedItem.ToString()) ||
                    string.IsNullOrEmpty(Name.Text) ||
                    string.IsNullOrEmpty(Price.Text) ||
                    string.IsNullOrEmpty(Unit.Text) ||
                    string.IsNullOrEmpty(Type.Text))
                    UpdateProdBtn.IsEnabled = false;
                else
                    UpdateProdBtn.IsEnabled = true;
            }
            catch { UpdateProdBtn.IsEnabled = false; }
        }

        public void EnableDelButton()
        {
            try
            {
                if (string.IsNullOrEmpty(IDofProd.SelectedItem.ToString()))
                    DelProdBtn.IsEnabled = false;
                else
                    DelProdBtn.IsEnabled = true;
            }
            catch { DelProdBtn.IsEnabled = false; }
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new ChooseShow();
        }

        private void UpdateProdBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();

                Query = "UPDATE AdvertisingProds " +
                    "SET ProdName = N'" + Name.Text + "', " +
                    "ProdPrice = " + Price.Text + ", " +
                    "ProdUnit = N'" + Unit.Text + "', " +
                    "ProdType = N'" + Type.Text + "' " +
                    "WHERE IDofProd = " + IDofProd.SelectedItem.ToString();
                command = new SqlCommand(Query, connection);
                command.ExecuteNonQuery();

                connection.Close();
                MessageWindow mw = new MessageWindow();
                mw.ShowMessage("Успішно!", "success");
            }
            catch
            {
                MessageWindow mw = new MessageWindow();
                mw.ShowMessage("Помилка!", "error");
            }
        }

        private void DelProdBtn_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();

            Query = "DELETE FROM AdvertisingProds " +
                    "WHERE IDofProd = " + IDofProd.SelectedItem.ToString();
            command = new SqlCommand(Query, connection);
            command.ExecuteNonQuery();

            connection.Close();
            LoadCB_ID();
            EnableDelButton();

            Name.Text = ""; Price.Text = "";
            Unit.Text = ""; Type.Text = "";

            MessageWindow mw = new MessageWindow();
            mw.ShowMessage("Успішно!", "success");
        }

        private void IDofProd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                connection.Open();

                Query = "SELECT * " +
                        "FROM AdvertisingProds " +
                        "WHERE IDofProd = " + IDofProd.SelectedItem.ToString();
                command = new SqlCommand(Query, connection);
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                Name.Text = reader.GetValue(1).ToString();
                Price.Text = reader.GetValue(2).ToString();
                Unit.Text = reader.GetValue(3).ToString();
                Type.Text = reader.GetValue(4).ToString();

                connection.Close();
                EnableDelButton();
            }
            catch { IDofProd.SelectedItem = null; connection.Close(); }
        }
    }
}
