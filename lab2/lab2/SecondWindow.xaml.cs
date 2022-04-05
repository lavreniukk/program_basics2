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
    /// Логика взаимодействия для SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        public bool XplayerWin = false;
        public bool OplayerWin = false;
        public bool GameOver;
        public string[,] MarksInBoxes;
        static Button MainButton = new Button();
        static Label Crosses = new Label();
        static Label Circles = new Label();
        static Label Spare = new Label();
        static Button restartBtn = new Button();
        public SecondWindow()
        {
            InitializeComponent();
            if (restartBtn.Parent != null)
            {
                var parent = (Panel)restartBtn.Parent;
                parent.Children.Remove(restartBtn);
                parent.Children.Remove(Crosses);
                parent.Children.Remove(Circles);
                parent.Children.Remove(Spare);
                parent.Children.Remove(MainButton);
            }
            initControls();
            Start();
        }
        public void initControls()
        {
            GridLengthConverter gridLengthConverter = new GridLengthConverter();
            this.Title = " Second Window";
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
            secondGrid.Width = 800 / 3;
            secondGrid.Height = 800 / 3;
            Grid.SetColumn(secondGrid, 1);
            Grid.SetRow(secondGrid, 1);
            firstGrid.Children.Add(secondGrid);

            RowDefinition[] secondRows = new RowDefinition[5];
            ColumnDefinition[] secondCols = new ColumnDefinition[5];
            for (int i = 0; i < 5; i++)
            {
                secondCols[i] = new ColumnDefinition();
                secondRows[i] = new RowDefinition();
                secondGrid.RowDefinitions.Add(secondRows[i]);
                secondGrid.ColumnDefinitions.Add(secondCols[i]);
            }

            //Combo Boxes
            ComboBox[,] poles = new ComboBox[5, 5];
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    poles[i, j] = new ComboBox();
                    poles[i, j].Items.Add("X");
                    poles[i, j].Items.Add("O");
                    poles[i, j].SelectionChanged += PlayersMove;
                    poles[i, j].Width = 50;
                    poles[i, j].Height = 50;
                    Grid.SetRow(poles[i,j], i);
                    Grid.SetColumn(poles[i, j], j);
                    secondGrid.Children.Add(poles[i, j]);
                }

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

            //Restart Button
            Grid.SetRow(restartBtn, 1);
            Grid.SetColumn(restartBtn, 1);
            restartBtn.VerticalAlignment = VerticalAlignment.Bottom;
            restartBtn.HorizontalAlignment = HorizontalAlignment.Right;
            restartBtn.Visibility = Visibility.Hidden;
            restartBtn.Content = "Try Again";
            restartBtn.Height = 32;
            restartBtn.Width = 121;
            restartBtn.FontFamily = new FontFamily("Franklin Gothic Medium");
            restartBtn.FontSize = 14;
            restartBtn.Background = new SolidColorBrush(Color.FromRgb(232, 186, 115));
            restartBtn.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));
            restartBtn.BorderBrush = new SolidColorBrush(Color.FromRgb(166, 130, 73));
            restartBtn.Click += Restart_Click;
            firstGrid.Children.Add(restartBtn);

            //Labels
            Label FirstWinLab = new Label();
            Grid.SetColumn(FirstWinLab, 0);
            Grid.SetRow(FirstWinLab, 0);
            FirstWinLab.VerticalAlignment = VerticalAlignment.Center;
            FirstWinLab.HorizontalAlignment = HorizontalAlignment.Left;
            firstGrid.Children.Add(FirstWinLab);
            FirstWinLab.Content = "   Tic Tac Toe Game";
            FirstWinLab.FontFamily = new FontFamily("Franklin Gothic Medium");
            FirstWinLab.FontSize = 14;
            FirstWinLab.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            FirstWinLab.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));

            Grid.SetColumn(Crosses, 1);
            Grid.SetRow(Crosses, 1);
            Crosses.VerticalAlignment = VerticalAlignment.Bottom;
            Crosses.HorizontalAlignment = HorizontalAlignment.Left;
            Crosses.Visibility = Visibility.Hidden;
            Crosses.Height = 32;
            Crosses.Width = 121;
            Crosses.Content = "Crosses win!";
            Crosses.FontFamily = new FontFamily("Franklin Gothic Medium");
            Crosses.FontSize = 14;
            Crosses.Background = new SolidColorBrush(Color.FromRgb(232, 186, 115));
            Crosses.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));
            Crosses.BorderBrush = new SolidColorBrush(Color.FromRgb(166, 130, 73));
            firstGrid.Children.Add(Crosses);

            Grid.SetColumn(Circles, 1);
            Grid.SetRow(Circles, 1);
            Circles.VerticalAlignment = VerticalAlignment.Bottom;
            Circles.HorizontalAlignment = HorizontalAlignment.Left;
            Circles.Visibility = Visibility.Hidden;
            Circles.Height = 32;
            Circles.Width = 121;
            Circles.Content = "Circles win!";
            Circles.FontFamily = new FontFamily("Franklin Gothic Medium");
            Circles.FontSize = 14;
            Circles.Background = new SolidColorBrush(Color.FromRgb(232, 186, 115));
            Circles.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));
            Circles.BorderBrush = new SolidColorBrush(Color.FromRgb(166, 130, 73));
            firstGrid.Children.Add(Circles);

            Grid.SetColumn(Spare, 1);
            Grid.SetRow(Spare, 1);
            Spare.VerticalAlignment = VerticalAlignment.Bottom;
            Spare.HorizontalAlignment = HorizontalAlignment.Left;
            Spare.Visibility = Visibility.Hidden;
            Spare.Height = 32;
            Spare.Width = 121;
            Spare.Content = "Spare!";
            Spare.FontFamily = new FontFamily("Franklin Gothic Medium");
            Spare.FontSize = 14;
            Spare.Background = new SolidColorBrush(Color.FromRgb(232, 186, 115));
            Spare.Foreground = new SolidColorBrush(Color.FromRgb(79, 77, 73));
            Spare.BorderBrush = new SolidColorBrush(Color.FromRgb(166, 130, 73));
            firstGrid.Children.Add(Spare);

            this.Content = firstGrid;
            this.Show();
        }
        private void Start()
        {
            MarksInBoxes = new string[5, 5];
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    MarksInBoxes[i, j] = "";
            GameOver = false;
        }
        private void PlayersMove(object sender, SelectionChangedEventArgs e)
        {
            ComboBox ChosenBox = (ComboBox)sender;
            if (GameOver)
                ChosenBox.SelectedIndex = -1;
            int row = Grid.GetRow(ChosenBox);
            int col = Grid.GetColumn(ChosenBox);
            MarksInBoxes[row, col] = ChosenBox.SelectedItem.ToString();
            string text = "";
            foreach (string elem in MarksInBoxes)
                text += elem;
            isGameOver();
        }
        private void isGameOver()
        {
            int crossesCount = 0;
            int circlesCount = 0;
            bool IsWinner = false;
            bool NoSpace = true;

            //colls
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (MarksInBoxes[i, j] == "X")
                        crossesCount++;
                    if (MarksInBoxes[i, j] == "O")
                        circlesCount++;
                }
                if (crossesCount == 5 || circlesCount == 5)
                {
                    IsWinner = true;
                    goto Win;
                }
                crossesCount = 0;
                circlesCount = 0;
            }

            //rows
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (MarksInBoxes[i, j] == "X")
                        crossesCount++;
                    if (MarksInBoxes[i, j] == "O")
                        circlesCount++;
                }
                if (crossesCount == 5 || circlesCount == 5)
                {
                    IsWinner = true;
                    goto Win;
                }
                crossesCount = 0;
                circlesCount = 0;
            }

            //diags
            int diagCol = 0;
            for (int i = 0; i < 5; i++)
            {
                if (MarksInBoxes[i, diagCol] == "X")
                    crossesCount++;
                if (MarksInBoxes[i, diagCol] == "O")
                    circlesCount++;
                diagCol++;
            }
            if (crossesCount == 5 || circlesCount == 5)
            {
                IsWinner = true;
                goto Win;
            }
            crossesCount = 0;
            circlesCount = 0;
            diagCol = 0;
            for (int i = 4; i >= 0; i--)
            {
                if (MarksInBoxes[i, diagCol] == "X")
                    crossesCount++;
                if (MarksInBoxes[i, diagCol] == "O")
                    circlesCount++;
                diagCol++;
            }
            if (crossesCount == 5 || circlesCount == 5)
            {
                IsWinner = true;
                goto Win;
            }

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    if (MarksInBoxes[i, j] == "")
                        NoSpace = false;
                }

            Win:
            if (IsWinner)
            {
                MainButton.Visibility = Visibility.Hidden;
                if (crossesCount == 5)
                    XplayerWin = true;
                else
                    OplayerWin = true;
                if (XplayerWin)
                {
                    Crosses.Visibility = Visibility.Visible;
                    restartBtn.Visibility = Visibility.Visible;
                    GameOver = true;
                }
                else if (OplayerWin)
                {
                    Circles.Visibility = Visibility.Visible;
                    restartBtn.Visibility = Visibility.Visible;
                    GameOver = true;
                }
                else
                    GameOver = false;
            }
            else if (NoSpace)
            {
                Spare.Visibility = Visibility.Visible;
                restartBtn.Visibility = Visibility.Visible;
                GameOver = true;
            }
            else return;
        }
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            MainButton.Visibility = Visibility.Visible;
            Hide();
            SecondWindow sw = new SecondWindow();
            sw.Show();
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
