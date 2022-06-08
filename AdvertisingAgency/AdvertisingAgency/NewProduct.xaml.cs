using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AdvertisingAgency
{
    /// <summary>
    /// Логика взаимодействия для NewProduct.xaml
    /// </summary>
    public partial class NewProduct : Page
    {
        string Query;
        string connectionString;
        DataTable UsersTable;
        SqlCommand command;
        SqlConnection connection;
        SqlDataAdapter adapter;
        public NewProduct()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            LoadComboBoxData();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();

                if (NewR.IsChecked == true)
                    Query = "INSERT INTO AdvertisingProds(ProdName, ProdPrice, ProdUnit, ProdType)" +
                    "VALUES( N'" + Name.Text + "', " + Price.Text + ", N'" + Unit.Text + "', N'" + Type.Text + "')";
                else
                    Query = "INSERT INTO AdvertisingProds(ProdName, ProdPrice, ProdUnit, ProdType)" +
                    "VALUES( N'" + Name.Text + "', " + Price.Text + ", N'" + Unit.Text + "', N'" + TypeExisted.Text + "')";

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

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButton();
        }

        public void LoadComboBoxData()
        {
            connection.Open();

            Query = "SELECT DISTINCT ProdType AS 'Type' " +
                    "FROM AdvertisingProds";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);
            foreach (DataRow row in UsersTable.Rows)
                TypeExisted.Items.Add((row["Type"]).ToString());

            connection.Close();
        }

        public void EnableButton()
        {
            if (NewR.IsChecked == true)
            {
                if (string.IsNullOrEmpty(Name.Text) ||
                    string.IsNullOrEmpty(Price.Text) ||
                    string.IsNullOrEmpty(Unit.Text) ||
                    string.IsNullOrEmpty(Type.Text))
                    AddProduct.IsEnabled = false;
                else
                    AddProduct.IsEnabled = true;
            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(Name.Text) ||
                        string.IsNullOrEmpty(Price.Text) ||
                        string.IsNullOrEmpty(Unit.Text) ||
                        string.IsNullOrEmpty(TypeExisted.SelectedItem.ToString()))
                        AddProduct.IsEnabled = false;
                    else
                        AddProduct.IsEnabled = true;
                }
                catch { }
            }
        }

        private void TypeExisted_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableButton();
        }

        private void New_Checked(object sender, RoutedEventArgs e)
        {
            Type.Visibility = Visibility.Visible;
            TypeExisted.Visibility = Visibility.Hidden;
            TypeExisted.SelectedItem = null;
            AddProduct.IsEnabled = false;

            TypeChoiceAnimation(Type, TypeExisted);
        }

        private void Exist_Checked(object sender, RoutedEventArgs e)
        {
            Type.Visibility = Visibility.Hidden;
            TypeExisted.Visibility = Visibility.Visible;
            Type.Text = "";
            AddProduct.IsEnabled = false;

            TypeChoiceAnimation(TypeExisted, Type);
        }

        public void TypeChoiceAnimation(Control element1, Control element2)
        {
            Storyboard storyboard = new Storyboard();
            TimeSpan time = new TimeSpan(0, 0, 0, 0, 155);

            DoubleAnimation fadeIn = new DoubleAnimation()
            { From = 0.0, To = 1.0, Duration = new Duration(time) };
            DoubleAnimation fadeOut = new DoubleAnimation()
            { From = 1.0, To = 0.0, Duration = new Duration(time) };

            Storyboard.SetTargetName(fadeIn, element1.Name);
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath("Opacity", 1));
            storyboard.Children.Add(fadeIn);
            storyboard.Begin(element1);

            Storyboard.SetTargetName(fadeOut, element2.Name);
            Storyboard.SetTargetProperty(fadeOut, new PropertyPath("Opacity", 0));
            storyboard.Children.Add(fadeOut);
            storyboard.Begin(element2);
        }
    }
}
