using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab2
{
    /// <summary>
    /// Логика взаимодействия для FirstWindow.xaml
    /// </summary>
    public partial class FirstWindow : Window
    {
        static List<Student> studentsData = new List<Student>();
        static Random rnd = new Random();
        static TextBox TextBoxID = new TextBox();
        static TextBox TextBoxName = new TextBox();
        static TextBox TextBoxInfo = new TextBox();

        struct Student
        {
            private string ID;
            private string FullName;
            private string PersonalData;
            public Student(string ID, string FullName, string PersonalData)
            {
                this.ID = ID;
                this.FullName = FullName;
                this.PersonalData = PersonalData;
            }
            public string getID() => ID;
            public void printStudent(StreamWriter file)
            {
                file.WriteLine($"Record Book Number: {ID,15};   Full Name: {FullName,50};   Personal Data: {PersonalData,30}.");
            }
        }
        public FirstWindow()
        {
            InitializeComponent();
            if (TextBoxID.Parent != null)
            {
                var parent = (Panel)TextBoxID.Parent;
                parent.Children.Remove(TextBoxID);
                parent.Children.Remove(TextBoxName);
                parent.Children.Remove(TextBoxInfo);
            }
            initControls();
        }
        public void initControls()
        {
            GridLengthConverter gridLengthConverter = new GridLengthConverter();
            this.Title = " First Window";
            this.ResizeMode = ResizeMode.NoResize;
            this.Background = new SolidColorBrush(Color.FromRgb(255, 234, 202));

            //first grid
            Grid firstGrid = new Grid();
            firstGrid.Width = 800;
            firstGrid.Height = 450;
            firstGrid.HorizontalAlignment = HorizontalAlignment.Center;
            firstGrid.VerticalAlignment = VerticalAlignment.Center;

            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            ColumnDefinition colDef3 = new ColumnDefinition();
            firstGrid.ColumnDefinitions.Add(colDef1);
            firstGrid.ColumnDefinitions.Add(colDef2);
            firstGrid.ColumnDefinitions.Add(colDef3);

            RowDefinition rowDef1 = new RowDefinition();
            rowDef1.Height = (GridLength)gridLengthConverter.ConvertFrom("30*");
            RowDefinition rowDef2 = new RowDefinition();
            rowDef2.Height = (GridLength)gridLengthConverter.ConvertFrom("420*");
            firstGrid.RowDefinitions.Add(rowDef1);
            firstGrid.RowDefinitions.Add(rowDef2);

            //second grid
            Grid secondGrid = new Grid();
            Grid.SetRow(secondGrid, 1);
            Grid.SetColumn(secondGrid, 0);

            RowDefinition SecondRowDef1 = new RowDefinition();
            SecondRowDef1.Height = (GridLength)gridLengthConverter.ConvertFrom("1*");
            RowDefinition SecondRowDef2 = new RowDefinition();
            SecondRowDef2.Height = (GridLength)gridLengthConverter.ConvertFrom("1,5*");
            RowDefinition SecondRowDef3 = new RowDefinition();
            SecondRowDef3.Height = (GridLength)gridLengthConverter.ConvertFrom("1*");

            secondGrid.RowDefinitions.Add(SecondRowDef1);
            secondGrid.RowDefinitions.Add(SecondRowDef2);
            secondGrid.RowDefinitions.Add(SecondRowDef3);
            firstGrid.Children.Add(secondGrid);

            //third grid
            Grid thirdGrid = new Grid();
            Grid.SetRow(thirdGrid, 1);
            Grid.SetColumn(thirdGrid, 1);

            RowDefinition ThirdRowDef1 = new RowDefinition();
            ThirdRowDef1.Height = (GridLength)gridLengthConverter.ConvertFrom("1*");
            RowDefinition ThirdRowDef2 = new RowDefinition();
            ThirdRowDef2.Height = (GridLength)gridLengthConverter.ConvertFrom("1,5*");
            RowDefinition ThirdRowDef3 = new RowDefinition();
            ThirdRowDef3.Height = (GridLength)gridLengthConverter.ConvertFrom("1*");

            thirdGrid.RowDefinitions.Add(ThirdRowDef1);
            thirdGrid.RowDefinitions.Add(ThirdRowDef2);
            thirdGrid.RowDefinitions.Add(ThirdRowDef3);
            firstGrid.Children.Add(thirdGrid);

            //Rectangle
            Rectangle bar = new Rectangle();
            Grid.SetColumnSpan(bar, 3);
            Grid.SetRow(bar, 0);
            firstGrid.Children.Add(bar);
            bar.Fill = new SolidColorBrush(Color.FromRgb(212, 189, 117));

            //Add To Data button
            Button AddToData = new Button();
            AddToData.Height = 80;
            AddToData.Width = 215;
            AddToData.FontFamily = new FontFamily ("Franklin Gothic Medium");
            AddToData.FontSize = 20;
            AddToData.Background = new SolidColorBrush(Color.FromRgb(232, 186, 115));
            AddToData.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));
            AddToData.BorderBrush = new SolidColorBrush(Color.FromRgb(166, 130, 73));
            AddToData.Content = "Add To Data";
            AddToData.Click += AddBtn_Click;
            Grid.SetRow(AddToData, 1);
            Grid.SetColumn(AddToData, 1);
            AddToData.VerticalAlignment = VerticalAlignment.Top;
            AddToData.HorizontalAlignment = HorizontalAlignment.Center;
            thirdGrid.Children.Add(AddToData);

            //Delete button
            Button DeleteButton = new Button();
            DeleteButton.Height = 80;
            DeleteButton.Width = 215;
            DeleteButton.FontFamily = new FontFamily("Franklin Gothic Medium");
            DeleteButton.FontSize = 20;
            DeleteButton.Background = new SolidColorBrush(Color.FromRgb(232, 186, 115));
            DeleteButton.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));
            DeleteButton.BorderBrush = new SolidColorBrush(Color.FromRgb(166, 130, 73));
            DeleteButton.Content = "Delete From Data" + "\n" + " by Book Number";
            DeleteButton.Click += DelBtn_Click;
            Grid.SetRow(DeleteButton, 1);
            Grid.SetColumn(DeleteButton, 1);
            DeleteButton.VerticalAlignment = VerticalAlignment.Bottom;
            DeleteButton.HorizontalAlignment = HorizontalAlignment.Center;
            thirdGrid.Children.Add(DeleteButton);

            //Main Button
            Button MainButton = new Button();
            MainButton.Content = " Main Window";
            MainButton.Height = 40;
            MainButton.Width = 155;
            Grid.SetRow(MainButton, 2);
            Grid.SetColumn(MainButton, 2);
            MainButton.VerticalAlignment = VerticalAlignment.Bottom;
            MainButton.HorizontalAlignment = HorizontalAlignment.Right;
            firstGrid.Children.Add(MainButton);
            MainButton.FontFamily = new FontFamily("Franklin Gothic Medium");
            MainButton.FontSize = 14;
            MainButton.Background = new SolidColorBrush(Color.FromRgb(232, 186, 115));
            MainButton.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));
            MainButton.BorderBrush = new SolidColorBrush(Color.FromRgb(166, 130, 73));
            MainButton.Click += Button_Click;

            //Exit Button
            Button Exit = new Button();
            Exit.Content = "X";
            Exit.Height = 30;
            Exit.Width = 30;
            Grid.SetRow(Exit, 0);
            Grid.SetColumn(Exit, 2);
            Exit.HorizontalAlignment = HorizontalAlignment.Right;
            firstGrid.Children.Add(Exit);
            Exit.Click += ExitBtn_Click;
            Exit.FontSize = 20;
            Exit.FontFamily = new FontFamily("Tahoma");
            Exit.Background = new SolidColorBrush(Color.FromRgb(212, 117, 117));
            Exit.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            Exit.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));


            //Text Boxes
            Grid.SetRow(TextBoxID, 1);
            Grid.SetColumn(TextBoxID, 0);
            Grid.SetRow(TextBoxName, 1);
            Grid.SetColumn(TextBoxName, 0);
            Grid.SetRow(TextBoxInfo, 1);
            Grid.SetColumn(TextBoxInfo, 0);

            TextBoxID.Height = 40;
            TextBoxID.Width = 209;
            TextBoxName.Height = 40;
            TextBoxName.Width = 209;
            TextBoxInfo.Height = 40;
            TextBoxInfo.Width = 209;

            TextBoxID.VerticalAlignment = VerticalAlignment.Top;
            TextBoxID.HorizontalAlignment = HorizontalAlignment.Right;
            TextBoxName.VerticalAlignment = VerticalAlignment.Center;
            TextBoxName.HorizontalAlignment = HorizontalAlignment.Right;
            TextBoxInfo.VerticalAlignment = VerticalAlignment.Bottom;
            TextBoxInfo.HorizontalAlignment = HorizontalAlignment.Right;

            TextBoxID.Background = new SolidColorBrush(Color.FromRgb(253, 246, 231));
            TextBoxID.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));
            TextBoxID.BorderBrush = new SolidColorBrush(Color.FromRgb(191, 178, 156));
            TextBoxName.Background = new SolidColorBrush(Color.FromRgb(253, 246, 231));
            TextBoxName.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));
            TextBoxName.BorderBrush = new SolidColorBrush(Color.FromRgb(191, 178, 156));
            TextBoxInfo.Background = new SolidColorBrush(Color.FromRgb(253, 246, 231));
            TextBoxInfo.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));
            TextBoxInfo.BorderBrush = new SolidColorBrush(Color.FromRgb(191, 178, 156));

            TextBoxID.Text = " Record Book Number";
            TextBoxName.Text = " Full Name";
            TextBoxInfo.Text = " Personal Data";

            secondGrid.Children.Add(TextBoxID);
            secondGrid.Children.Add(TextBoxName);
            secondGrid.Children.Add(TextBoxInfo);

            //Labels
            Label FirstWinLab = new Label();
            Grid.SetColumn(FirstWinLab, 0);
            Grid.SetRow(FirstWinLab, 0);
            FirstWinLab.VerticalAlignment = VerticalAlignment.Center;
            FirstWinLab.HorizontalAlignment = HorizontalAlignment.Left;
            firstGrid.Children.Add(FirstWinLab);
            FirstWinLab.Content = "   First Window";
            FirstWinLab.FontFamily = new FontFamily("Franklin Gothic Medium");
            FirstWinLab.FontSize = 14;
            FirstWinLab.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            FirstWinLab.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));

            this.Content = firstGrid;
            this.Show();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string ID = TextBoxID.Text;
            string Name = TextBoxName.Text;
            string Data = TextBoxInfo.Text;
            studentsData.Add(new Student(ID, Name, Data));
            StreamWriter sw = new StreamWriter(@"D://projects//studentsData.txt");
            foreach (Student person in studentsData)
                person.printStudent(sw);
            sw.Close();
        }
        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            string ID = TextBoxID.Text;
            for (int i = 0; i < studentsData.Count; i++)
                if (ID == studentsData[i].getID())
                    studentsData.Remove(studentsData[i]);
            StreamWriter sw = new StreamWriter(@"D://projects//studentsData.txt");
            foreach (Student person in studentsData)
                person.printStudent(sw);
            sw.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
