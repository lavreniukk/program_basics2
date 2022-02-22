using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static System.Math;

namespace prac1
{
    /// <summary>
    /// Логика взаимодействия для ProtectionModeWindow.xaml
    /// </summary>
    public partial class ProtectionModeWindow : Window
    {
        public double[,] intervals;
        public int NumberOfBreaks = 0;
        public int NumberOfTries = 0;
        public Stopwatch timer = new Stopwatch();
        public double[] StudTable = { 12.706, 4.3027, 3.1825, 2.7764,
                                      2.5706, 2.4469, 2.3646, 2.3060,
                                      2.2622, 2.2281, 2.2010, 2.1788,
                                      2.1604, 2.1448, 2.1315, 2.1199, 2.1098 };
        public double[,] FisherTable = { { 161.45, 199.50, 215.71, 224.58, 230.16, 233.99, 236.77, 238.88, 240.54, 241.88 },
                                         { 18.51, 19, 19.16, 19.25, 19.30, 19.33, 19.35, 19.37, 19.38, 19.40 },
                                         { 10.13, 9.55, 9.28, 9.12, 9.01, 8.94, 8.89,  8.85, 8.81, 8.79},
                                         { 7.71, 6.94, 6.59, 6.39, 6.26, 6.16, 6.09, 6.04, 6, 5.96},
                                         { 6.61, 5.79, 5.41, 5.19, 5.05, 4.95, 4.88, 4.82, 4.77, 4.74 },
                                         { 5.99, 5.14, 4.76, 4.53, 4.39, 4.28, 4.21, 4.15, 4.10, 4.06 },
                                         { 5.59, 4.74, 4.35, 4.12, 3.97, 3.87, 3.79, 3.73, 3.68, 3.64},
                                         { 5.32, 4.46, 4.07, 3.84, 3.69, 3.58, 3.50, 3.44, 3.39, 3.35},
                                         { 5.12, 4.26, 3.86, 3.63, 3.48, 3.37, 3.29, 3.23, 3.18, 3.14 },
                                         { 4.96, 4.10, 3.71, 3.48, 3.33, 3.22, 3.14, 3.07, 3.02, 2.98 } };
        public List<double> MiLearningList = new List<double>();
        public List<double> SiLearningList = new List<double>();
        public List<double> MiCheckList = new List<double>();
        public List<double> SiCheckList = new List<double>();

        public ProtectionModeWindow()
        {
            InitializeComponent();
            ComboBoxItem Item = (ComboBoxItem)CountProtection.SelectedItem;
            intervals = new double[Convert.ToInt32(Item.Content.ToString()), VerifyField.Text.Length - 1];
            StreamReader sr = new StreamReader(@"D://statistics.txt");
            string[] rows = sr.ReadToEnd().Split('\n');
            int count = Convert.ToInt32(rows[0]);
            for (int i = 1; i < count + 1; i++)
                MiLearningList.Add(Convert.ToDouble(rows[i]));
            for (int i = count + 1; i < rows.Length - 1; i++)
                SiLearningList.Add(Convert.ToDouble(rows[i]));
            sr.Close();
            timer.Start();
        }

        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                CheckAutentificcation();
                return;
            }

            if (!CheckString(InputField.Text, VerifyField.Text))
            {
                try { InputField.Text = InputField.Text.Remove(InputField.Text.Length - 1); }
                catch { InputField.Clear(); }
                return;
            }
            timer.Stop();

            if (InputField.Text.Length == 1)
            {
                timer.Reset();
                timer.Start();
                return;
            }
            else if (NumberOfTries < Convert.ToInt32(Item.Content) && NumberOfBreaks < (VerifyField.Text.Length - 1) * Convert.ToInt32(Item.Content.ToString()))
            {
                double sec = timer.Elapsed.Seconds + timer.Elapsed.Milliseconds / 1000.0;
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

        public void CheckAutentificcation()
        {
            List<double> SiMax = new List<double>();
            List<double> SiMin = new List<double>();

            foreach (double SiLearn in SiLearningList)
                foreach (double SiCheck in SiCheckList)
                {
                    SiMax.Add(Max(SiLearn, SiCheck));
                    SiMin.Add(Min(SiLearn, SiCheck));
                }

            List<double> Fisher = new List<double>();
            for (int i = 0; i < SiMin.Count; i++)
                Fisher.Add(SiMax[i] / SiMin[i]);

            int FisherTrue = 0;
            int FisherFalse = 0;
            foreach (double elem in Fisher)
            {
                if (elem > FisherTable[SiLearningList.Count - 1, SiCheckList.Count - 1])
                    FisherFalse++;
                else
                    FisherTrue++;
            }

            bool DispersionBool = FisherTrue > FisherFalse ? true : false;
            if (DispersionBool)
                DispField.Content = "Однорідні";
            else
                DispField.Content = "Неоднорідні";

            int Ke = MiLearningList.Count;
            int n = VerifyField.Text.Length;
            int r = 0;
            List<double> SiFinalCheck = new List<double>();
            List<double> tpFinal = new List<double>();

            for (int i = 0; i < SiLearningList.Count; i++)
                for (int j = 0; j < SiCheckList.Count; j++)
                    SiFinalCheck.Add(Sqrt((Pow(SiLearningList[i], 2) + Pow(SiCheckList[j], 2)) * (n - 1) / (2.0 * n - 1)));

            if (MiLearningList.Count > MiCheckList.Count)
                for (int i = 0; i < MiLearningList.Count; i++)
                {
                    double tpSumm = 0;
                    for (int j = 0; j < MiCheckList.Count; j++)
                    {
                        tpFinal.Add(Abs(MiLearningList[i] - MiCheckList[j]) / SiFinalCheck[j + (i * MiCheckList.Count)] * Sqrt(2.0 / n));
                        tpSumm += tpFinal[j];
                    }
                    tpSumm /= MiCheckList.Count;
                    if (tpSumm <= StudTable[VerifyField.Text.Length - 2])
                        r++;
                }
            else
                for (int i = 0; i < MiCheckList.Count; i++)
                {
                    double tpSumm = 0;
                    for (int j = 0; j < MiLearningList.Count; j++)
                    {
                        tpFinal.Add(Abs(MiLearningList[j] - MiCheckList[i]) / SiFinalCheck[i + (j * MiCheckList.Count)] * Sqrt(2.0 / n));
                        tpSumm += tpFinal[j];
                    }
                    tpSumm /= MiCheckList.Count;
                    if (tpSumm <= StudTable[VerifyField.Text.Length - 2])
                        r++;
                }
            double P = r / Ke * 1.0;
            ComboBoxItem Item = (ComboBoxItem)CountProtection.SelectedItem;
            int N0 = Convert.ToInt32(Item.Content);
            double P1 = (N0 - r) / N0 * 1.0;
            double P2 = r / N0 * 1.0;
            StatisticsBlock.Content = P;
            P1Field.Content = P1;
            P2Field.Content = P2;


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
                MiCheckList.Add(summ / (n - 1));
                summ = 0;

                foreach (double elem in intervalsForCalc)
                    summ += Pow(elem - MiCheckList[i], 2);
                SiCheckList.Add(summ / (n - 1));
            }
        }

        private bool CheckString(string UsersText, string codeWord)
        {
            bool IsSameChar;
            try
            {
                if (UsersText[UsersText.Length - 1] == codeWord[UsersText.Length - 1])
                    IsSameChar = true;
                else
                    IsSameChar = false;
            }
            catch { IsSameChar = false; }
            return IsSameChar;
        }

        private void CountProtection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem Item = (ComboBoxItem)CountProtection.SelectedItem;
            intervals = new double[Convert.ToInt32(Item.Content.ToString()), VerifyField.Text.Length - 1];
        }
    }
}
