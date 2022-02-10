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

namespace lab1
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
        public SecondWindow()
        {
            InitializeComponent();
            Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Start()
        {
            MarksInBoxes = new string[5,5];
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    MarksInBoxes[i,j] = "";
            GameOver = false;
        }
        private void PlayersMove(object sender, SelectionChangedEventArgs e)
        {
            ComboBox ChosenBox = (ComboBox)sender;
            if (GameOver)
                ChosenBox.SelectedIndex = -1;
            int row = Grid.GetRow(ChosenBox);
            int col = Grid.GetColumn(ChosenBox);
            ListBoxItem typeItem = (ListBoxItem)ChosenBox.SelectedItem;
            MarksInBoxes[row, col] = typeItem.Content.ToString();
            string text = "";
            foreach (string elem in MarksInBoxes)
               text  += elem;
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
                MainBtn.Visibility = Visibility.Hidden;
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
            Hide();
            SecondWindow sw = new SecondWindow();
            sw.Show();
        }
    }
}
