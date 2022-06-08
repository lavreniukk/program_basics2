using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Media;
using System;

namespace AdvertisingAgency
{
    /// <summary>
    /// Логика взаимодействия для ShowCatalOrPubl.xaml
    /// </summary>
    public partial class ShowCatalOrPubl : Page
    {
        string Query;
        string connectionString;
        DataTable UsersTable;
        SqlCommand command;
        SqlConnection connection;
        SqlDataAdapter adapter;
        public ShowCatalOrPubl(string option)
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);

            switch (option)
            {
                case "catalogue":
                    DisplayCatalogue();
                    break;
                case "prices":
                    DisplayPrices();
                    break;
                case "products":
                    DisplayProducts();
                    break;
                case "clients":
                    DisplayClients();
                    break;
            }
        }

        public void DisplayProducts()
        {
            LoadProducts();
            CatalPublBlocks.Children.Clear();

            Label[] colsName = LabelsSets(UsersTable.Columns.Count);
            for (int k = 0; k < colsName.Length; k++)
                colsName[k].Background = new SolidColorBrush(Color.FromRgb(246, 203, 92));

            colsName[0].Width = 30; colsName[0].Content = "ID";
            colsName[1].Width = 200; colsName[1].Content = "Продукція";
            colsName[2].Width = 100; colsName[2].Content = "Ціна";
            colsName[3].Width = 150; colsName[3].Content = "Вид продукції";

            StackPanel spForNames = new StackPanel();
            spForNames.Orientation = Orientation.Horizontal;
            spForNames.Width = 480;
            CatalPubl.Width = 500;
            spForNames.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

            foreach (Label label in colsName)
                spForNames.Children.Add(label);

            CatalPublBlocks.Children.Add(spForNames);

            for (int i = 0; i < UsersTable.Rows.Count; i++)
            {
                Label[] labelArr = LabelsSets(UsersTable.Columns.Count);
                labelArr[0].Width = 30;
                labelArr[1].Width = 200;
                labelArr[2].Width = 100;
                labelArr[3].Width = 150;

                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Width = 480;
                sp.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

                for (int j = 0; j < UsersTable.Columns.Count; j++)
                {
                    labelArr[j].Content = UsersTable.Rows[i][j];
                    sp.Children.Add(labelArr[j]);
                }

                CatalPublBlocks.Children.Add(sp);
            }
        }

        public void DisplayPrices()
        {
            LoadPrices();
            CatalPublBlocks.Children.Clear();

            Label[] colsName = LabelsSets(UsersTable.Columns.Count);
            for (int k = 0; k < colsName.Length; k++)
                colsName[k].Background = new SolidColorBrush(Color.FromRgb(246, 203, 92));

            colsName[0].Width = 50;  colsName[0].Content = "Ціна";
            colsName[1].Width = 100; colsName[1].Content = "Колір";
            colsName[2].Width = 80; colsName[2].Content = "Видання";

            StackPanel spForNames = new StackPanel();
            spForNames.Orientation = Orientation.Horizontal;
            spForNames.Width = 230;
            CatalPubl.Width = 250;
            spForNames.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

            foreach (Label label in colsName)
                spForNames.Children.Add(label);

            CatalPublBlocks.Children.Add(spForNames);

            for (int i = 0; i < UsersTable.Rows.Count; i++)
            {
                Label[] labelArr = LabelsSets(UsersTable.Columns.Count);
                labelArr[0].Width = 50;
                labelArr[1].Width = 100;
                labelArr[2].Width = 80;

                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Width = 230;
                sp.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

                for (int j = 0; j < UsersTable.Columns.Count; j++)
                {
                    labelArr[j].Content = UsersTable.Rows[i][j];
                    sp.Children.Add(labelArr[j]);
                }

                CatalPublBlocks.Children.Add(sp);
            }
        }

        public void DisplayCatalogue()
        {
            LoadCatalogue();
            CatalPublBlocks.Children.Clear();

            Label[] colsName = LabelsSets(UsersTable.Columns.Count);
            for (int k = 0; k < colsName.Length; k++)
            {
                colsName[k].Width = 150;
                colsName[k].Background = new SolidColorBrush(Color.FromRgb(246, 203, 92));
            }

            StackPanel spForNames = new StackPanel();
            spForNames.Orientation = Orientation.Horizontal;
            spForNames.Width = 300;
            CatalPubl.Width = 320;
            spForNames.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

            colsName[0].Content = "Назва"; spForNames.Children.Add(colsName[0]);
            colsName[1].Content = "Тип видання"; spForNames.Children.Add(colsName[1]);
            CatalPublBlocks.Children.Add(spForNames);

            for (int i = 0; i < UsersTable.Rows.Count; i++)
            {
                Label[] labelArr = LabelsSets(UsersTable.Columns.Count);
                for (int k = 0; k < labelArr.Length; k++)
                    labelArr[k].Width = 150;

                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Width = 300;
                sp.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

                for (int j = 0; j < UsersTable.Columns.Count; j++)
                {
                    labelArr[j].Content = UsersTable.Rows[i][j];
                    sp.Children.Add(labelArr[j]);
                }

                CatalPublBlocks.Children.Add(sp);
            }
        }

        public void DisplayClients()
        {
            LoadClients();
            CatalPublBlocks.Children.Clear();

            Label[] colsName = LabelsSets(UsersTable.Columns.Count);
            for (int k = 0; k < colsName.Length; k++)
                colsName[k].Background = new SolidColorBrush(Color.FromRgb(246, 203, 92));

            colsName[0].Width = 30; colsName[0].Content = "ID";
            colsName[1].Width = 75; colsName[1].Content = "Ім'я";
            colsName[2].Width = 100; colsName[2].Content = "Прізвище";
            colsName[3].Width = 70; colsName[3].Content = "Країна";
            colsName[4].Width = 70; colsName[4].Content = "Місто";
            colsName[5].Width = 100; colsName[5].Content = "Телефон";

            StackPanel spForNames = new StackPanel();
            spForNames.Orientation = Orientation.Horizontal;
            spForNames.Width = 445;
            CatalPubl.Width = 465;
            spForNames.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

            foreach (Label label in colsName)
                spForNames.Children.Add(label);

            CatalPublBlocks.Children.Add(spForNames);

            for (int i = 0; i < UsersTable.Rows.Count; i++)
            {
                Label[] labelArr = LabelsSets(UsersTable.Columns.Count);
                labelArr[0].Width = 30;
                labelArr[1].Width = 75;
                labelArr[2].Width = 100;
                labelArr[3].Width = 70;
                labelArr[4].Width = 70;
                labelArr[5].Width = 100;

                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Width = 445;
                sp.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

                for (int j = 0; j < UsersTable.Columns.Count; j++)
                {
                    labelArr[j].Content = UsersTable.Rows[i][j];
                    sp.Children.Add(labelArr[j]);
                }

                CatalPublBlocks.Children.Add(sp);
            }
        }

        public Label[] LabelsSets(int Length)
        {
            Label[] labelArr = new Label[Length];
            for (int k = 0; k < labelArr.Length; k++)
            {
                labelArr[k] = new Label();
                labelArr[k].Foreground = new SolidColorBrush(Color.FromRgb(139, 95, 19));
                labelArr[k].BorderBrush = new SolidColorBrush(Color.FromRgb(93, 63, 12));
                labelArr[k].BorderThickness = new Thickness(1, 1, 1, 1);
                labelArr[k].FontSize = 10;
                labelArr[k].FontFamily = new FontFamily("SegoeUi Semibold");
                labelArr[k].Height = 45;
                labelArr[k].Margin = new Thickness(0, 0, 0, -0.2);
            }

            return labelArr;
        }

        public void LoadCatalogue()
        {
            connection.Open();

            Query = "SELECT PublicationName AS Name, PublicationType AS Type " +
                    "FROM PublicationsInfo " +
                    "ORDER BY PublicationType";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);

            connection.Close();
        }

        public void LoadPrices()
        {
            connection.Open();

            Query = "SELECT Price AS 'PRICE', CASE WHEN Color = 'True' THEN N'Кольоровий' " +
                "ELSE N'Ч/Б' END AS 'COLOR', PublicationType AS 'PUBLICATION' FROM PriceList";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);

            connection.Close();
        }

        public void LoadProducts()
        {
            connection.Open();

            Query = "SELECT IDofProd, ProdName, ProdPrice, ProdType" +
                    " FROM AdvertisingProds";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);

            connection.Close();
        }

        public void LoadClients()
        {
            connection.Open();

            Query = "SELECT IDofAdvertiser, Name, Surname, Country, City, PhoneNumber " +
                    "FROM AdvertiserInfo";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);

            connection.Close();
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new ChooseShow();
        }

        private void PublicationChange_Click(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new UpdatePubl();
        }

        private void PricesChange_Click(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new UpdatePrice();
        }

        private void ProductsChange_Click(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new UpdateProd();
        }

        private void ClientsChange_Click(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new UpdateClient();
        }

        private void AppChange_Click(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new UpdateApp();
        }
    }
}
