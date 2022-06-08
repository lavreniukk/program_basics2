using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System;
using System.Windows.Media;
using System.Windows;
using System.IO;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;

namespace AdvertisingAgency
{
    /// <summary>
    /// Логика взаимодействия для Payment.xaml
    /// </summary>
    public partial class Payment : Page
    {
        string Query;
        string connectionString;
        DataTable UsersTable;
        SqlCommand command;
        SqlConnection connection;
        SqlDataAdapter adapter;
        SqlDataReader reader;

        bool IsPaidBool;

        string IDforReplace;
        string FullNameForReplace;
        string PhoneNumForReplace;
        string ProdForReplace;
        string PriceForReplace;
        public Payment()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            LoadCB_ID();
            DisplayData();
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
                AppIDs.Items.Add((row["ID"]).ToString());

            connection.Close();
        }

        public void LoadData()
        {
            connection.Open();

            Query = "SELECT AdvertiserApplication.IDofApplication AS 'ID', CONCAT_WS(' ', Surname, Name), PhoneNumber, Price, " + 
                    "CASE WHEN ApplicationInfo.IsPaid = 'True' THEN N'Сплачено' ELSE N'Не сплачено' END AS 'PAYING' " +
                    "FROM AdvertiserInfo INNER JOIN " +
                    "AdvertiserApplication ON AdvertiserApplication.IDofAdvertiser = AdvertiserInfo.IDofAdvertiser " +
                    "INNER JOIN ApplicationInfo ON AdvertiserApplication.IDofApplication = ApplicationInfo.IDofApplication";
            command = new SqlCommand(Query, connection);
            adapter = new SqlDataAdapter(command);
            UsersTable = new DataTable();
            adapter.Fill(UsersTable);

            connection.Close();
        }

        public void LoadDataForReceipt()
        {
            connection.Open();

            Query = "SELECT AdvertiserApplication.IDofApplication, CONCAT_WS(' ', Surname, Name, SecondName), PhoneNumber, ProdName, ApplicationInfo.Price " +
                "FROM AdvertiserApplication INNER JOIN " +
                "AdvertiserInfo ON AdvertiserApplication.IDofAdvertiser = AdvertiserInfo.IDofAdvertiser INNER JOIN " +
                "ApplicationInfo ON AdvertiserApplication.IDofApplication = ApplicationInfo.IDofApplication INNER JOIN " +
                "AdvertisingProds ON ApplicationInfo.IDofProd = AdvertisingProds.IDofProd " +
                "WHERE ApplicationInfo.IDofApplication = " + AppIDs.SelectedItem.ToString();
            command = new SqlCommand(Query, connection);
            reader = command.ExecuteReader();

            reader.Read();
            IDforReplace = reader.GetValue(0).ToString();
            FullNameForReplace = reader.GetValue(1).ToString();
            PhoneNumForReplace = reader.GetValue(2).ToString();
            ProdForReplace = reader.GetValue(3).ToString();
            PriceForReplace = reader.GetValue(4).ToString();

            connection.Close();
        }

        public void DisplayData()
        {
            LoadData();
            PayBlocks.Children.Clear();

            Label[] colsName = LabelsSets(UsersTable.Columns.Count);
            for (int k = 0; k < colsName.Length; k++)
                colsName[k].Background = new SolidColorBrush(Color.FromRgb(246, 203, 92));

            colsName[0].Width = 30; colsName[0].Content = "ID";
            colsName[1].Width = 150; colsName[1].Content = "Прізвище Ім'я";
            colsName[2].Width = 100; colsName[2].Content = "Телефон";
            colsName[3].Width = 50; colsName[3].Content = "Ціна";
            colsName[4].Width = 70; colsName[4].Content = "Статус";

            StackPanel spForNames = new StackPanel();
            spForNames.Orientation = Orientation.Horizontal;
            spForNames.Width = 400;
            PaySV.Width = 420;
            spForNames.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

            foreach (Label label in colsName)
                spForNames.Children.Add(label);

            PayBlocks.Children.Add(spForNames);

            for (int i = 0; i < UsersTable.Rows.Count; i++)
            {
                Label[] labelArr = LabelsSets(UsersTable.Columns.Count);
                labelArr[0].Width = 30;
                labelArr[1].Width = 150;
                labelArr[2].Width = 100;
                labelArr[3].Width = 50;
                labelArr[4].Width = 70;

                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.Width = 400;
                sp.Background = new SolidColorBrush(Color.FromRgb(237, 210, 140));

                for (int j = 0; j < UsersTable.Columns.Count; j++)
                {
                    labelArr[j].Content = UsersTable.Rows[i][j];
                    sp.Children.Add(labelArr[j]);
                }

                PayBlocks.Children.Add(sp);
            }
        }

        private void AppIDs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            connection.Open();

            Query = "SELECT IsPaid AS 'IsPaid' " +
                    "FROM ApplicationInfo " +
                    "WHERE (IDofApplication = " + AppIDs.SelectedItem.ToString() + ")";
            command = new SqlCommand(Query, connection);

            IsPaidBox.IsChecked = Convert.ToBoolean(command.ExecuteScalar());

            connection.Close();

            EnableFileBtn();
        }

        public void EnableFileBtn()
        {
            if (IsPaidBox.IsChecked == false)
                CreateWordFile.IsEnabled = true;
            else
                CreateWordFile.IsEnabled = false;
        }

        private void IsPaidBox_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (IsPaidBox.IsChecked == true)
                IsPaidBool = true;
            else
                IsPaidBool = false;

            connection.Open();

            Query = "UPDATE ApplicationInfo " +
                    "SET IsPaid = '" + IsPaidBool +
                    "' WHERE (IDofApplication = " + AppIDs.SelectedItem.ToString() + ")";
            command = new SqlCommand(Query, connection);
            command.ExecuteNonQuery();

            connection.Close();

            DisplayData();
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

        private void FindAndReplaceFunc(Word.Application WordApp, object ToFindText, object ReplaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object matchAllForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            WordApp.Selection.Find.Execute(ref ToFindText,
                ref matchCase, ref matchWholeWord, ref matchWildCards,
                ref matchSoundLike, ref matchAllForms, ref forward,
                ref wrap, ref format, ref ReplaceWithText, ref replace,
                ref matchKashida, ref matchDiactitics, ref matchAlefHamza,
                ref matchControl);
        }

        private void CreateWordDoc(object ExampleFile, object NewFile)
        {
            LoadDataForReceipt();

            Word.Application WordApp = new Word.Application();
            object missing = Missing.Value;
            Word.Document WordDoc = null;

            if (File.Exists((string)ExampleFile))
            {
                object readOnly = false;
                object isVisible = false;
                WordApp.Visible = false;

                WordDoc = WordApp.Documents.Open(ref ExampleFile, ref missing, ref readOnly,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing);
                WordDoc.Activate();

                DateTime dt = DateTime.Now;
                var DateForReplace = dt.Date;
                this.FindAndReplaceFunc(WordApp, "<id>", IDforReplace);
                this.FindAndReplaceFunc(WordApp, "<fullname>", FullNameForReplace);
                this.FindAndReplaceFunc(WordApp, "<phonenum>", PhoneNumForReplace);
                this.FindAndReplaceFunc(WordApp, "<product>", ProdForReplace);
                this.FindAndReplaceFunc(WordApp, "<price>", PriceForReplace);
                this.FindAndReplaceFunc(WordApp, "<date>", DateForReplace);
            }
            else
            {
                MessageWindow mw1 = new MessageWindow();
                mw1.ShowMessage("Файл не знайдено!", "error");
            }

            WordDoc.SaveAs2(ref NewFile, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing);
            WordDoc.Close();
            WordApp.Quit();
            MessageWindow mw = new MessageWindow();
            mw.ShowMessage("Файл створено!", "success");
        }
        
        private void CreateWordFile_Click(object sender, RoutedEventArgs e)
        {
            string newFileSource = @"D:\projects\program_basics2\AdvertisingAgency\AdvertisingAgency\Receipt\" + "Receipt_" + AppIDs.SelectedItem.ToString() + ".docx";
            CreateWordDoc(@"D:\projects\program_basics2\AdvertisingAgency\AdvertisingAgency\wordExample.docx", newFileSource);
        }

        private void IsPaidBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CreateWordFile.IsEnabled = true;
        }
    }
}
