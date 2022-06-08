using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Media.Animation;
using System;

namespace AdvertisingAgency
{
    /// <summary>
    /// Логика взаимодействия для UpdatePubl.xaml
    /// </summary>
    public partial class UpdatePubl : Page
    {
        string Query;
        string Query1;
        string Query2;
        string connectionString;
        DataTable UsersTable;
        SqlConnection connection;
        public UpdatePubl()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        public void EnableButton()
        {
            if (string.IsNullOrEmpty(Name.Text) ||
                string.IsNullOrEmpty(Type.Text) ||
                string.IsNullOrEmpty(Price.Text))
                AddPubl.IsEnabled = false;
            else
                AddPubl.IsEnabled = true;
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new ChooseShow();
        }

        private void AddPubl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //75 відсотків від ціни кольорової публікації
                double bwPrice = Convert.ToDouble(Price.Text) * 75.0 / 100;
                connection.Open();


                using (var cmd = new SqlCommand(@"INSERT INTO PriceList(Price, Color, PublicationType) VALUES(@p, @c, @t)", connection))
                {
                    cmd.Parameters.AddWithValue("@p", Price.Text);
                    cmd.Parameters.AddWithValue("@c", true);
                    cmd.Parameters.AddWithValue("@t", Type.Text);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand(@"INSERT INTO PriceList(Price, Color, PublicationType) VALUES(@p, @c, @t)", connection))
                {
                    cmd.Parameters.AddWithValue("@p", bwPrice);
                    cmd.Parameters.AddWithValue("@c", false);
                    cmd.Parameters.AddWithValue("@t", Type.Text);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand(@"INSERT INTO PublicationsInfo(PublicationName, PublicationType) VALUES(@n, @t)", connection))
                {
                    cmd.Parameters.AddWithValue("@n", Name.Text);
                    cmd.Parameters.AddWithValue("@t", Type.Text);
                    cmd.ExecuteNonQuery();
                }

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

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButton();
        }
    }
}
