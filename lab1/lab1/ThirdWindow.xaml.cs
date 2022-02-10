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

namespace lab1
{
    /// <summary>
    /// Логика взаимодействия для ThirdWindow.xaml
    /// </summary>
    public partial class ThirdWindow : Window
    {
        public int OperationCount = 0;
        public int ComasCount = 0;
        public ThirdWindow()
        {
            InitializeComponent();
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
    }
}
