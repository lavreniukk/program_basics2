using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Media;
using System.Windows;

namespace AdvertisingAgency
{
    /// <summary>
    /// Логика взаимодействия для ShowApps.xaml
    /// </summary>
    public partial class ShowApps : Page
    {
        string Query;
        string connectionString;
        DataTable UsersTable;
        SqlCommand command;
        SqlConnection connection;
        SqlDataAdapter adapter;

        public ShowApps()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            LoadComboBoxData();
        }

        public void LoadOnClient()
        {
            connection.Open();

            Query = "SELECT ApplicationInfo.IDofApplication AS ID, AdvertisingProds.ProdName AS PRODUCT, ApplicationDatePlace.ApplicationDate AS DATE, " +
                    "ApplicationInfo.Price AS PRICE " +
                    "FROM ApplicationInfo " +
                    "INNER JOIN ApplicationDatePlace ON ApplicationInfo.IDofApplication = ApplicationDatePlace.IDofApplication " +
                    "INNER JOIN AdvertisingProds ON ApplicationInfo.IDofProd = AdvertisingProds.IDofProd " +
                    "INNER JOIN AdvertiserApplication ON ApplicationInfo.IDofApplication = AdvertiserApplication.IDofApplication " +
                    "INNER JOIN AdvertiserInfo ON AdvertiserApplication.IDofAdvertiser = AdvertiserInfo.IDofAdvertiser " +
                    "WHERE CONCAT_WS(' ', Surname, Name, SecondName) = N'" + Search.SelectedItem.ToString() + "' " +
                    "ORDER BY ID";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);

            connection.Close();
        }

        public void LoadOnDate()
        {
            try
            {
                connection.Open();
                Query = "SELECT ApplicationInfo.IDofApplication AS ID, AdvertisingProds.ProdName AS PRODUCT, " +
                        "ApplicationDatePlace.PublicationName AS PUBLICATION, FORMAT(ApplicationDatePlace.ApplicationDate, 'dd.MM.yyyy') AS DATE, ApplicationInfo.Price AS PRICE " +
                        "FROM ApplicationInfo " +
                        "INNER JOIN ApplicationDatePlace ON ApplicationInfo.IDofApplication = ApplicationDatePlace.IDofApplication " +
                        "INNER JOIN AdvertisingProds ON ApplicationInfo.IDofProd = AdvertisingProds.IDofProd " +
                        "WHERE ApplicationDate = '" + Date.Text + "' " +
                        "ORDER BY ID";
                command = new SqlCommand(Query, connection);
                adapter = new SqlDataAdapter(command);
                UsersTable = new DataTable();
                adapter.Fill(UsersTable);

                connection.Close();
            }
            catch
            {
                MessageBox.Show("Invalid Input");
            }
        }

        public void LoadAllApps()
        {
            connection.Open();

            Query = "SELECT ApplicationInfo.IDofApplication AS 'ID', AdvertisingProds.ProdName AS 'PRODUCT', ApplicationDatePlace.PublicationName AS 'PUBLICATION', " +
                    "ApplicationDatePlace.ApplicationDate AS 'DATE', ApplicationInfo.Price AS 'PRICE'" +
                    "FROM ApplicationInfo INNER JOIN ApplicationDatePlace " + "ON ApplicationInfo.IDofApplication = ApplicationDatePlace.IDofApplication " +
                    "INNER JOIN AdvertisingProds " + "ON ApplicationInfo.IDofProd = AdvertisingProds.IDofProd " +
                    "ORDER BY ID";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);

            connection.Close();
        }

        public void LoadOperative()
        {
            connection.Open();

            Query = "SELECT ApplicationDatePlace.IDofApplication AS ID, ApplicationDatePlace.PublicationName AS PUBLICATION, CONCAT_WS(' ', Surname, Name) AS 'FULL NAME', " +
                    "ApplicationInfo.Price AS PRICE, CASE WHEN ApplicationInfo.IsPaid = 'True' THEN N'Сплачено' ELSE N'Не сплачено' END AS 'PAYING' " +
                    "FROM ApplicationDatePlace INNER JOIN ApplicationInfo " +
                    "ON ApplicationDatePlace.IDofApplication = ApplicationInfo.IDofApplication " +
                    "INNER JOIN AdvertiserApplication ON AdvertiserApplication.IDofApplication = ApplicationInfo.IDofApplication " +
                    "INNER JOIN AdvertiserInfo ON AdvertiserInfo.IDofAdvertiser = AdvertiserApplication.IDofAdvertiser " +
                    "ORDER BY ID";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);

            connection.Close();
        }

        public void LoadComboBoxData()
        {
            connection.Open();

            Query = "SELECT CONCAT_WS(' ', Surname, Name, SecondName) AS 'Full Name' " +
                    "FROM AdvertiserInfo";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);
            foreach (DataRow row in UsersTable.Rows)
                Search.Items.Add((row["Full Name"]).ToString());

            connection.Close();
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

        private void AllApps_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadAllApps();
            AppBlocks.Children.Clear();

            Label[] colsName = LabelsSets(UsersTable.Columns.Count);
            for (int k = 0; k < colsName.Length; k++)
                colsName[k].Background = new SolidColorBrush(Color.FromRgb(246, 203, 92));

            colsName[0].Width = 30; colsName[0].Content = "ID";
            colsName[1].Width = 200; colsName[1].Content = "Продукція";
            colsName[2].Width = 140; colsName[2].Content = "Видання";
            colsName[3].Width = 60; colsName[3].Content = "Дата";
            colsName[4].Width = 50; colsName[4].Content = "Ціна";

            StackPanel spForNames = new StackPanel();
            spForNames.Orientation = Orientation.Horizontal;
            spForNames.Width = 480;
            Apps.Width = 500;
            spForNames.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

            foreach (Label label in colsName)
                spForNames.Children.Add(label);

            AppBlocks.Children.Add(spForNames);

            for (int i = 0; i < UsersTable.Rows.Count; i++)
            {
                Label[] labelArr = LabelsSets(UsersTable.Columns.Count);
                labelArr[0].Width = 30;
                labelArr[1].Width = 200;
                labelArr[2].Width = 140;
                labelArr[3].Width = 60;
                labelArr[4].Width = 50;

                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Width = 480;
                sp.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

                for (int j = 0; j < UsersTable.Columns.Count; j++)
                {
                    labelArr[j].Content = UsersTable.Rows[i][j];
                    sp.Children.Add(labelArr[j]);
                }

                AppBlocks.Children.Add(sp);
            }

        }

        private void OperativeView_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadOperative();
            AppBlocks.Children.Clear();

            Label[] colsName = LabelsSets(UsersTable.Columns.Count);
            for (int k = 0; k < colsName.Length; k++)
                colsName[k].Background = new SolidColorBrush(Color.FromRgb(246, 203, 92));

            colsName[0].Width = 30; colsName[0].Content = "ID";
            colsName[1].Width = 150; colsName[1].Content = "Видання";
            colsName[2].Width = 150; colsName[2].Content = "Прізвище Ім'я";
            colsName[3].Width = 50; colsName[3].Content = "Ціна";
            colsName[4].Width = 70; colsName[4].Content = "Статус";

            StackPanel spForNames = new StackPanel();
            spForNames.Orientation = Orientation.Horizontal;
            spForNames.Width = 450;
            Apps.Width = 470;
            spForNames.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

            foreach (Label label in colsName)
                spForNames.Children.Add(label);

            AppBlocks.Children.Add(spForNames);

            for (int i = 0; i < UsersTable.Rows.Count; i++)
            {
                Label[] labelArr = LabelsSets(UsersTable.Columns.Count);
                labelArr[0].Width = 30;
                labelArr[1].Width = 150;
                labelArr[2].Width = 150;
                labelArr[3].Width = 50;
                labelArr[4].Width = 70;

                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Width = 450;
                sp.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

                for (int j = 0; j < UsersTable.Columns.Count; j++)
                {
                    labelArr[j].Content = UsersTable.Rows[i][j];
                    sp.Children.Add(labelArr[j]);
                }

                AppBlocks.Children.Add(sp);
            }
        }

        private void Search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadOnClient();
            AppBlocks.Children.Clear();

            Label[] colsName = LabelsSets(UsersTable.Columns.Count);
            for (int k = 0; k < colsName.Length; k++)
                colsName[k].Background = new SolidColorBrush(Color.FromRgb(246, 203, 92));

            colsName[0].Width = 30; colsName[0].Content = "ID";
            colsName[1].Width = 200; colsName[1].Content = "Продукція";
            colsName[2].Width = 60; colsName[2].Content = "Дата";
            colsName[3].Width = 50; colsName[3].Content = "Ціна";

            StackPanel spForNames = new StackPanel();
            spForNames.Orientation = Orientation.Horizontal;
            spForNames.Width = 340;
            Apps.Width = 360;
            spForNames.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

            foreach (Label label in colsName)
                spForNames.Children.Add(label);

            AppBlocks.Children.Add(spForNames);

            if (UsersTable.Rows.Count != 0)
            {
                for (int i = 0; i < UsersTable.Rows.Count; i++)
                {
                    Label[] labelArr = LabelsSets(UsersTable.Columns.Count);
                    labelArr[0].Width = 30;
                    labelArr[1].Width = 200;
                    labelArr[2].Width = 60;
                    labelArr[3].Width = 50;

                    StackPanel sp = new StackPanel();
                    sp.Orientation = Orientation.Horizontal;
                    sp.Width = 340;
                    sp.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

                    for (int j = 0; j < UsersTable.Columns.Count; j++)
                    {
                        labelArr[j].Content = UsersTable.Rows[i][j];
                        sp.Children.Add(labelArr[j]);
                    }

                    AppBlocks.Children.Add(sp);
                }
            }
            else
            {
                Label label = new Label();

                label.Foreground = new SolidColorBrush(Color.FromRgb(139, 95, 19));
                label.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));
                label.BorderBrush = new SolidColorBrush(Color.FromRgb(93, 63, 12));
                label.BorderThickness = new Thickness(1, 1, 1, 1);
                label.FontSize = 10;
                label.FontFamily = new FontFamily("SegoeUi Semibold");
                label.Margin = new Thickness(0, 0, 0, -0.2);
                label.Content = "Не знайдено заявок";
                label.Width = 340;
                label.Height = 45;

                AppBlocks.Children.Add(label);
            }

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadOnDate();
           
            AppBlocks.Children.Clear();

            Label[] colsName = LabelsSets(UsersTable.Columns.Count);
            for (int k = 0; k < colsName.Length; k++)
                colsName[k].Background = new SolidColorBrush(Color.FromRgb(246, 203, 92));

            colsName[0].Width = 30; colsName[0].Content = "ID";
            colsName[1].Width = 200; colsName[1].Content = "Продукція";
            colsName[2].Width = 140; colsName[2].Content = "Видання";
            colsName[3].Width = 60; colsName[3].Content = "Дата";
            colsName[4].Width = 50; colsName[4].Content = "Ціна";

            StackPanel spForNames = new StackPanel();
            spForNames.Orientation = Orientation.Horizontal;
            spForNames.Width = 480;
            Apps.Width = 500;
            spForNames.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

            foreach (Label label in colsName)
                spForNames.Children.Add(label);

            AppBlocks.Children.Add(spForNames);

            if (UsersTable.Rows.Count != 0)
            {
                for (int i = 0; i < UsersTable.Rows.Count; i++)
                {
                    Label[] labelArr = LabelsSets(UsersTable.Columns.Count);
                    labelArr[0].Width = 30;
                    labelArr[1].Width = 200;
                    labelArr[2].Width = 140;
                    labelArr[3].Width = 60;
                    labelArr[4].Width = 50;

                    StackPanel sp = new StackPanel();
                    sp.Orientation = Orientation.Horizontal;
                    sp.Width = 480;
                    sp.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

                    for (int j = 0; j < UsersTable.Columns.Count; j++)
                    {
                        labelArr[j].Content = UsersTable.Rows[i][j];
                        sp.Children.Add(labelArr[j]);
                    }

                    AppBlocks.Children.Add(sp);
                }
            }
            else
            {
                Label label = new Label();

                label.Foreground = new SolidColorBrush(Color.FromRgb(139, 95, 19));
                label.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));
                label.BorderBrush = new SolidColorBrush(Color.FromRgb(93, 63, 12));
                label.BorderThickness = new Thickness(1, 1, 1, 1);
                label.FontSize = 10;
                label.FontFamily = new FontFamily("SegoeUi Semibold");
                label.Margin = new Thickness(0, 0, 0, -0.2);
                label.Content = "Не знайдено заявок";
                label.Width = 340;
                label.Height = 45;

                AppBlocks.Children.Add(label);
            }

        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            (window as MainWindow).WorkingWindow.Content = new ChooseShow();
        }
    }
}
