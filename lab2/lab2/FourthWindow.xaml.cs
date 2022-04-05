using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для FourthWindow.xaml
    /// </summary>
    public partial class FourthWindow : Window
    {
        public FourthWindow()
        {
            InitializeComponent();
            initControls();
        }
        public void initControls()
        {
            GridLengthConverter gridLengthConverter = new GridLengthConverter();
            this.Title = " Fourth Window";
            this.ResizeMode = ResizeMode.NoResize;
            this.Background = new SolidColorBrush(Color.FromRgb(255, 234, 202));

            //first grid
            Grid firstGrid = new Grid();
            firstGrid.Width = 800;
            firstGrid.Height = 450;
            firstGrid.HorizontalAlignment = HorizontalAlignment.Center;
            firstGrid.VerticalAlignment = VerticalAlignment.Center;

            ColumnDefinition colDef1 = new ColumnDefinition();
            colDef1.Width = (GridLength)gridLengthConverter.ConvertFrom("1,5*");
            ColumnDefinition colDef2 = new ColumnDefinition();
            colDef2.Width = (GridLength)gridLengthConverter.ConvertFrom("1,1*");
            ColumnDefinition colDef3 = new ColumnDefinition();
            colDef3.Width = (GridLength)gridLengthConverter.ConvertFrom("1,5*");
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
            Grid.SetColumn(secondGrid, 1);
            Grid.SetRow(secondGrid, 1);

            RowDefinition secondRow1 = new RowDefinition();
            RowDefinition secondRow2 = new RowDefinition();
            RowDefinition secondRow3 = new RowDefinition();
            secondGrid.RowDefinitions.Add(secondRow1);
            secondGrid.RowDefinitions.Add(secondRow2);
            secondGrid.RowDefinitions.Add(secondRow3);

            firstGrid.Children.Add(secondGrid);

            //Rectangle
            Rectangle bar = new Rectangle();
            Grid.SetColumnSpan(bar, 3);
            Grid.SetRow(bar, 0);
            firstGrid.Children.Add(bar);
            bar.Fill = new SolidColorBrush(Color.FromRgb(212, 189, 117));

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

            //Labels
            Label FirstWinLab = new Label();
            Grid.SetColumn(FirstWinLab, 0);
            Grid.SetRow(FirstWinLab, 0);
            FirstWinLab.VerticalAlignment = VerticalAlignment.Center;
            FirstWinLab.HorizontalAlignment = HorizontalAlignment.Left;
            firstGrid.Children.Add(FirstWinLab);
            FirstWinLab.Content = "   Personal Data";
            FirstWinLab.FontFamily = new FontFamily("Franklin Gothic Medium");
            FirstWinLab.FontSize = 14;
            FirstWinLab.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            FirstWinLab.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));

            Label Name = new Label();
            Grid.SetRow(Name, 1);
            Name.VerticalAlignment = VerticalAlignment.Top;
            Name.HorizontalAlignment = HorizontalAlignment.Center;
            Name.FontFamily = new FontFamily("Segoe UI");
            Name.FontSize = 14;
            Name.Content = "Лавренюк Андрій Віталійович";
            Name.Width = 209;
            Name.Height = 40;
            Name.Background = new SolidColorBrush(Color.FromRgb(253, 246, 231));
            Name.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));
            secondGrid.Children.Add(Name);

            Label Group = new Label();
            Grid.SetRow(Group, 1);
            Group.VerticalAlignment = VerticalAlignment.Center;
            Group.HorizontalAlignment = HorizontalAlignment.Center;
            Group.FontFamily = new FontFamily("Segoe UI");
            Group.FontSize = 18;
            Group.Content = "               КП-12";
            Group.Width = 209;
            Group.Height = 40;
            Group.Background = new SolidColorBrush(Color.FromRgb(253, 246, 231));
            Group.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));
            secondGrid.Children.Add(Group);

            Label Data = new Label();
            Grid.SetRow(Data, 1);
            Data.VerticalAlignment = VerticalAlignment.Bottom;
            Data.HorizontalAlignment = HorizontalAlignment.Center;
            Data.FontFamily = new FontFamily("Segoe UI");
            Data.FontSize = 18;
            Data.Content = "                 2022";
            Data.Width = 209;
            Data.Height = 40;
            Data.Background = new SolidColorBrush(Color.FromRgb(253, 246, 231));
            Data.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));
            secondGrid.Children.Add(Data);

            this.Content = firstGrid;
            this.Show();
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
