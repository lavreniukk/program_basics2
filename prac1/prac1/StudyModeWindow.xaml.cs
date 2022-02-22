using System;
using static System.Math;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.IO;

namespace prac1
{
    /// <summary>
    /// Логика взаимодействия для StudyModeWindow.xaml
    /// </summary>
    public partial class StudyModeWindow : Window
    {
        public double[,] intervals;
        public int NumberOfBreaks = 0;
        public int NumberOfTries = 0;
        public Stopwatch timer = new Stopwatch();
        public double[] StudTable = { 12.706, 4.3027, 3.1825, 2.7764,
                                      2.5706, 2.4469, 2.3646, 2.3060,
                                      2.2622, 2.2281, 2.2010, 2.1788,
                                      2.1604, 2.1448, 2.1315, 2.1199, 2.1098 };
        public List<double> MiLearningList = new List<double>();
        public List<double> SiLearningList = new List<double>();

        public StudyModeWindow()
        {
            InitializeComponent();
            ComboBoxItem Item = (ComboBoxItem)CountProtection.SelectedItem;
            intervals = new double[Convert.ToInt32(Item.Content.ToString()), VerifyField.Text.Length - 1];
            download.IsEnabled = false;
            timer.Start();
        }

        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void InputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountProtection.IsEnabled = false;
            ComboBoxItem Item = (ComboBoxItem)CountProtection.SelectedItem;
            if (NumberOfTries >= Convert.ToInt32(Item.Content))
            {
                InputField.Clear();
                CalculateStats(intervals);
                InputField.IsEnabled = false;
                download.IsEnabled = true;
                return;
            }

            if (!CheckString(InputField.Text, VerifyField.Text))
            {
                try{ InputField.Text = InputField.Text.Remove(InputField.Text.Length - 1); }
                catch{ InputField.Clear(); }
                return;
            }
            timer.Stop();

            SymbolCount.Content = InputField.Text.Length;
            if (InputField.Text.Length == 1)
            {
                timer.Reset();
                timer.Start();
                return;
            }
            else if (NumberOfTries < Convert.ToInt32(Item.Content) && NumberOfBreaks < (VerifyField.Text.Length - 1) * Convert.ToInt32(Item.Content.ToString()))
            {
                double sec = timer.Elapsed.Seconds + timer.Elapsed.Milliseconds/1000.0;
                intervals[NumberOfTries, NumberOfBreaks] = sec;
                NumberOfBreaks++;
            }
            if (InputField.Text.Length == VerifyField.Text.Length)
            {
                NumberOfTries++;
                NumberOfBreaks = 0;
                InputField.Clear();
            }
            timer.Reset();
            timer.Start();
            return;
        }

        private bool CheckString(string UsersText, string codeWord)
        {
            bool IsSameChar;
            try {
                if (UsersText[UsersText.Length - 1] == codeWord[UsersText.Length - 1])
                    IsSameChar = true;
                else
                    IsSameChar = false; }
            catch { IsSameChar = false; }
            return IsSameChar;
        }

        private void CountProtection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem Item = (ComboBoxItem)CountProtection.SelectedItem;
            intervals = new double[Convert.ToInt32(Item.Content.ToString()), VerifyField.Text.Length - 1];
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter(@"D://statistics.txt");
            ComboBoxItem Item = (ComboBoxItem)CountProtection.SelectedItem;
            sw.WriteLine(Item.Content);
            foreach (double elem in MiLearningList)
                sw.WriteLine(elem);
            foreach (double elem in SiLearningList)
                sw.WriteLine(elem);
            sw.Close();
        }

        public void CalculateStats(double[,] intervals)
        {
            ComboBoxItem Item = (ComboBoxItem)CountProtection.SelectedItem;
            for (int i = 0; i < Convert.ToInt32(Item.Content.ToString()); i++)
                for (int j = 0; j < VerifyField.Text.Length - 1; j++)
                {
                    int n = intervals.GetLength(1);
                    double summ = 0;
                    double Mi;

                    List<double> intervalsForCalc = new List<double>();
                    for (int k = 0; k < intervals.GetLength(1); k++)
                        intervalsForCalc.Add(intervals[i, k]);
                    intervalsForCalc.Remove(j);

                    foreach (double elem in intervalsForCalc)
                        summ += elem;
                    Mi = summ / (n - 1);
                    summ = 0;

                    double Si;
                    foreach (double elem in intervalsForCalc)
                        summ += Pow(elem - Mi, 2);
                    Si = Sqrt(summ / n - 2);

                    double tp = Abs((intervals[i, j] - Mi) / (Si / Sqrt(n - 1)));
                    if (tp > StudTable[n - 3])
                    {
                        MessageBox.Show(" Значення критерію Стьюдента незадовільне ");
                        Close();
                        StudyModeWindow studyModeWindow = new StudyModeWindow();
                        studyModeWindow.Show();
                        return;
                    }
                }

            for (int i = 0; i < Convert.ToInt32(Item.Content.ToString()); i++)
            {
                int n = intervals.GetLength(1);
                double summ = 0;

                List<double> intervalsForCalc = new List<double>();
                for (int k = 0; k < intervals.GetLength(1); k++)
                    intervalsForCalc.Add(intervals[i, k]);

                foreach (double elem in intervalsForCalc)
                    summ += elem;
                MiLearningList.Add(summ / (n - 1));
                summ = 0;

                foreach (double elem in intervalsForCalc)
                    summ += Pow(elem - MiLearningList[i], 2);
                SiLearningList.Add(summ / (n - 2));
            }
        }
    }
}
