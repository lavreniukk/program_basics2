using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AdvertisingAgency
{
    /// <summary>
    /// Логика взаимодействия для UpdateClient.xaml
    /// </summary>
    public partial class UpdateClient : Page
    {
        string Query;
        string connectionString;
        DataTable UsersTable;
        SqlCommand command;
        SqlConnection connection;
        SqlDataAdapter adapter;

        public UpdateClient()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            LoadCB_ID();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableUpdateButton();
        }

        public void LoadCB_ID()
        {
            IDofClient.Items.Clear();
            connection.Open();

            Query = "SELECT IDofAdvertiser AS 'ID' " +
                    "FROM AdvertiserInfo";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);

            foreach (DataRow row in UsersTable.Rows)
                IDofClient.Items.Add((row["ID"]).ToString());

            connection.Close();
        }

        public void EnableUpdateButton()
        {
            if (string.IsNullOrEmpty(Name.Text) ||
                string.IsNullOrEmpty(Surname.Text) ||
                string.IsNullOrEmpty(SecondName.Text) ||
                string.IsNullOrEmpty(Country.Text) ||
                string.IsNullOrEmpty(City.Text) ||
                string.IsNullOrEmpty(PhoneNum.Text) ||
                string.IsNullOrEmpty(IDofClient.SelectedItem.ToString()))
                UpdateClientBtn.IsEnabled = false;
            else
                UpdateClientBtn.IsEnabled = true;
        }

        public void EnableDelButton()
        {
            try
            {
                if (string.IsNullOrEmpty(IDofClient.SelectedItem.ToString()))
                    DelClientBtn.IsEnabled = false;
                else
                    DelClientBtn.IsEnabled = true;
            }
            catch { DelClientBtn.IsEnabled = false; }
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new ChooseShow();
        }

        private void UpdateClientBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();

                Query = "UPDATE AdvertiserInfo " +
                    "SET Name = N'" + Name.Text + "', " +
                    "Surname = N'" + Surname.Text + "', " +
                    "SecondName = N'" + SecondName.Text + "', " +
                    "Country = N'" + Country.Text + "', " +
                    "City = N'" + City.Text + "', " +
                    "PhoneNumber = '" + PhoneNum.Text + "' " +
                    "WHERE IDofAdvertiser = " + IDofClient.SelectedItem.ToString();
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

        private void DelClientBtn_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();

            Query = "DELETE FROM AdvertiserInfo " +
                    "WHERE IDofAdvertiser = " + IDofClient.SelectedItem.ToString();
            command = new SqlCommand(Query, connection);
            command.ExecuteNonQuery();

            connection.Close();
            LoadCB_ID();
            EnableDelButton();
            Name.Text = ""; Surname.Text = "";
            SecondName.Text = ""; Country.Text = "";
            City.Text = ""; PhoneNum.Text = "";

            MessageWindow mw = new MessageWindow();
            mw.ShowMessage("Успішно!", "success");
        }

        private void IDofClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                connection.Open();

                Query = "SELECT * " +
                        "FROM AdvertiserInfo " +
                        "WHERE IDofAdvertiser = " + IDofClient.SelectedItem.ToString();
                command = new SqlCommand(Query, connection);
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                Name.Text = reader.GetValue(1).ToString();
                Surname.Text = reader.GetValue(2).ToString();
                SecondName.Text = reader.GetValue(3).ToString();
                Country.Text = reader.GetValue(4).ToString();
                City.Text = reader.GetValue(5).ToString();
                PhoneNum.Text = reader.GetValue(6).ToString();

                connection.Close();
                EnableDelButton();
            }
            catch { IDofClient.SelectedItem = null; connection.Close(); }
        }
    }
}
