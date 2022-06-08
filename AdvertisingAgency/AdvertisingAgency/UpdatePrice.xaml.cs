using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace AdvertisingAgency
{
    /// <summary>
    /// Логика взаимодействия для UpdatePrice.xaml
    /// </summary>
    public partial class UpdatePrice : Page
    {
        string Query;
        string connectionString;
        DataTable UsersTable;
        SqlCommand command;
        SqlConnection connection;
        SqlDataAdapter adapter;

        public UpdatePrice()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            LoadCB_ID();
        }

        public void LoadCB_ID()
        {
            IDofPrice.Items.Clear();
            connection.Open();

            Query = "SELECT IDofPrice AS 'ID' " +
                    "FROM PriceList" +
                    "";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);

            foreach (DataRow row in UsersTable.Rows)
                IDofPrice.Items.Add((row["ID"]).ToString());

            connection.Close();
        }

        public void EnableButton()
        {
            if (string.IsNullOrEmpty(PriceTB.Text))
                UpdatePriceBtn.IsEnabled = false;
            else
                UpdatePriceBtn.IsEnabled = true;
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new ChooseShow();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButton();
        }

        private void IDofPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                connection.Open();

                Query = "SELECT Price, CASE WHEN Color = 'True' THEN N'Кольоровий' ELSE N'Ч/Б' END, PublicationType " +
                        "FROM PriceList " +
                        "WHERE IDofPrice = " + IDofPrice.SelectedItem.ToString();
                command = new SqlCommand(Query, connection);
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                PriceTB.Text = reader.GetValue(0).ToString();
                Color.Text = reader.GetValue(1).ToString();
                Type.Text = reader.GetValue(2).ToString();

                connection.Close();
            }
            catch { IDofPrice.SelectedItem = null; connection.Close(); }
        }

        private void UpdatePriceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();

                Query = "UPDATE PriceList " +
                        "SET Price = '" + PriceTB.Text + "' " +
                        "WHERE IDofPrice = " + IDofPrice.SelectedItem.ToString();
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
    }
}
