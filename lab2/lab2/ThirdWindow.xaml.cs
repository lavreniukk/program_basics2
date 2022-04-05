using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для ThirdWindow.xaml
    /// </summary>
    public partial class ThirdWindow : Window
    {
        public int OperationCount = 0;
        public int ComasCount = 0;
        static TextBlock Equation = new TextBlock();
        public ThirdWindow()
        {
            InitializeComponent();
            if (Equation.Parent != null)
            {
                var parent = (Panel)Equation.Parent;
                parent.Children.Remove(Equation);
            }
            initControls();
        }
        public void initControls()
        {
            GridLengthConverter gridLengthConverter = new GridLengthConverter();
            this.Title = " Third Window";
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

            //Rectangle
            Rectangle bar = new Rectangle();
            Grid.SetColumnSpan(bar, 3);
            Grid.SetRow(bar, 0);
            firstGrid.Children.Add(bar);
            bar.Fill = new SolidColorBrush(Color.FromRgb(212, 189, 117));

            
            Rectangle body = new Rectangle();
            Grid.SetRow(body, 1);
            Grid.SetColumnSpan(body, 3);
            firstGrid.Children.Add(body);
            body.Fill = new SolidColorBrush(Color.FromRgb(132, 133, 133));
            body.Height = 340;
            body.Width = 255;

            //second grid
            Grid secondGrid = new Grid();
            Grid.SetColumn(secondGrid, 1);
            Grid.SetRow(secondGrid, 1);
            secondGrid.Width = 214;
            secondGrid.Height = 281;

            ColumnDefinition secondCol1 = new ColumnDefinition();
            ColumnDefinition secondCol2 = new ColumnDefinition();
            ColumnDefinition secondCol3 = new ColumnDefinition();
            ColumnDefinition secondCol4 = new ColumnDefinition();
            secondGrid.ColumnDefinitions.Add(secondCol1);
            secondGrid.ColumnDefinitions.Add(secondCol2);
            secondGrid.ColumnDefinitions.Add(secondCol3);
            secondGrid.ColumnDefinitions.Add(secondCol4);

            RowDefinition secondRow1 = new RowDefinition();
            RowDefinition secondRow2 = new RowDefinition();
            RowDefinition secondRow3 = new RowDefinition();
            RowDefinition secondRow4 = new RowDefinition();
            RowDefinition secondRow5 = new RowDefinition();
            RowDefinition secondRow6 = new RowDefinition();
            secondRow1.Height = (GridLength)gridLengthConverter.ConvertFrom("1,8*");
            secondGrid.RowDefinitions.Add(secondRow1);
            secondGrid.RowDefinitions.Add(secondRow2);
            secondGrid.RowDefinitions.Add(secondRow3);
            secondGrid.RowDefinitions.Add(secondRow4);
            secondGrid.RowDefinitions.Add(secondRow5);
            secondGrid.RowDefinitions.Add(secondRow6);
            firstGrid.Children.Add(secondGrid);

            //Calc Buttons
            Button[,] CalcButtons = new Button[5, 4];
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 4; j++)
                {
                    CalcButtons[i, j] = new Button();
                    CalcButtons[i, j].Click += Calc_Button_Click;
                    CalcButtons[i, j].FontFamily = new FontFamily("Microsoft Sans Serif");
                    CalcButtons[i, j].FontSize = 20;
                    CalcButtons[i, j].Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
                    Grid.SetRow(CalcButtons[i, j], i+1);
                    Grid.SetColumn(CalcButtons[i, j], j);
                    secondGrid.Children.Add(CalcButtons[i, j]);
                }
            CalcButtons[0, 0].IsEnabled = false;
            CalcButtons[0, 1].Content = "=";
            CalcButtons[0, 2].Content = "C";
            CalcButtons[0, 3].Content = "⌫";
            int num = 9;
            for(int i = 1; i < 4; i++)
                for(int j = 2; j >= 0; j--)
                {
                    CalcButtons[i, j].Content = num;
                    num--;
                }
            CalcButtons[4, 0].Content = "±";
            CalcButtons[4, 1].Content = "0";
            CalcButtons[4, 2].Content = ",";
            CalcButtons[1, 3].Content = "÷";
            CalcButtons[2, 3].Content = "x";
            CalcButtons[3, 3].Content = "-";
            CalcButtons[4, 3].Content = "+";

            //Text Box
            Equation.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            Equation.Foreground = new SolidColorBrush(Color.FromRgb(32, 29, 20));
            Equation.TextAlignment = TextAlignment.Right;
            Equation.TextWrapping = TextWrapping.Wrap;
            Equation.FontFamily = new FontFamily("Microsoft Sans Serif");
            Equation.FontSize = 20;
            Grid.SetRow(Equation, 0);
            Grid.SetColumnSpan(Equation, 4);
            secondGrid.Children.Add(Equation);


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
            FirstWinLab.Content = "   Calculator";
            FirstWinLab.FontFamily = new FontFamily("Franklin Gothic Medium");
            FirstWinLab.FontSize = 14;
            FirstWinLab.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            FirstWinLab.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));

            this.Content = firstGrid;
            this.Show();
        }
        private void Calc_Button_Click(object sender, RoutedEventArgs e)
        {

            Button ChosenButton = (Button)sender;
            string Content = ChosenButton.Content.ToString();
            if (Content == "x" || Content == "÷" || Content == "+" || Content == "-")
                OperationCount++;
            switch (Content)
            {
                case "С":
                    Equation.Text = "";
                    OperationCount = 0;
                    ComasCount = 0;
                    break;
                case ",":
                    if (ComasCount <= OperationCount)
                    {
                        ComasCount++;
                        Equation.Text += Content;
                    }
                    else
                        return;
                    break;
                case "=":
                    string res = Equation.Text;
                    if (res.Contains('x'))
                        res = res.Replace('x', '*');
                    if (res.Contains('÷'))
                        res = res.Replace('÷', '/');
                    if (res.Contains(','))
                        res = res.Replace(',', '.');
                    Compute:
                    try
                    {
                        res = new DataTable().Compute(res, null).ToString();
                        Equation.Text = res;
                        OperationCount = 0;
                        ComasCount = 0;
                        foreach (char elem in res)
                            if (elem == ',')
                                ComasCount++;
                    }
                    catch { res = res.Remove(res.Length - 1); goto Compute; }
                    break;
                case "⌫":
                    try { Equation.Text = Equation.Text.Remove(Equation.Text.Length - 1); }
                    catch { return; }
                    break;
                case "±":
                    try
                    {
                        if (OperationCount == 0 && Equation.Text[0] != '-')
                        {
                            Equation.Text = "-" + Equation.Text;
                            return;
                        }
                    }
                    catch { return; }
                    for (int i = Equation.Text.Length - 1; i > 0; i--)
                    {
                        if (Equation.Text[i] == 'x' || Equation.Text[i] == '÷')
                        {
                            Equation.Text = Equation.Text.Insert(i + 1, "-");
                            return;
                        }
                        else if (Equation.Text[i] == '+')
                        {
                            Equation.Text = Equation.Text.Remove(i, 1);
                            Equation.Text = Equation.Text.Insert(i, "-");
                            return;
                        }
                        else if (Equation.Text[i] == '-')
                        {
                            Equation.Text = Equation.Text.Remove(i, 1);
                            Equation.Text = Equation.Text.Insert(i, "+");
                            return;
                        }
                    }
                    if (Equation.Text[0] == '-')
                        Equation.Text = Equation.Text.Remove(0, 1);
                    break;
                default:
                    Equation.Text += Content;
                    break;
            }
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
