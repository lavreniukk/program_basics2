using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;


namespace AdvertisingAgency
{
    /// <summary>
    /// Логика взаимодействия для NewClient.xaml
    /// </summary>
    public partial class NewClient : Page
    {
        string Query;
        string connectionString;
        SqlCommand command;
        SqlConnection connection;
        public NewClient()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButton();
        }

        public void EnableButton()
        {
            if (string.IsNullOrEmpty(Name.Text) ||
                string.IsNullOrEmpty(Surname.Text) ||
                string.IsNullOrEmpty(SecondName.Text) ||
                string.IsNullOrEmpty(Country.Text) ||
                string.IsNullOrEmpty(City.Text) ||
                string.IsNullOrEmpty(PhoneNum.Text))
                AddClient.IsEnabled = false;
            else
                AddClient.IsEnabled = true;
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();

                Query = "INSERT INTO AdvertiserInfo(Name, Surname, SecondName, Country, City, PhoneNumber)" +
                    "VALUES( N'" + Name.Text + "', N'" + Surname.Text + "', N'" + SecondName.Text + "', N'" + Country.Text +
                    "', N'" + City.Text + "', N'" + PhoneNum.Text + "')";
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
    }
}
