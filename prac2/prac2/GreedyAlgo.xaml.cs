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
using System.Windows.Threading;

namespace prac2
{
    /// <summary>
    /// Логика взаимодействия для GreedyAlgo.xaml
    /// </summary>
    public partial class GreedyAlgo : Window
    {
        static Random rnd = new Random();
        static DispatcherTimer dT;
        static int CountOfIterations = 0;
        static double LenghtOfWholePath = 0;
        static int Radius = 30;
        static int NumOfCities = 20;
        static List<Ellipse> EllipseArray = new List<Ellipse>();
        static List<Label> IndexArray = new List<Label>();
        static PointCollection pC = new PointCollection();
        static PointCollection pCofVisited = new PointCollection();
        static int[] IndexOfCities = new int[NumOfCities];
        public GreedyAlgo()
        {
            dT = new DispatcherTimer();
            InitializeComponent();
            InitPoints();
            dT = new DispatcherTimer();
            dT.Tick += new EventHandler(OneStep);
            dT.Interval = new TimeSpan(0, 0, 0, 0, 1000);
        }
        private void PlotWay(Point City1, Point City2)
        {
            Line myLine = new Line();
            myLine.Stroke = new SolidColorBrush(Color.FromRgb(225, 225, 225));
            myLine.StrokeThickness = 2;
            myLine.X1 = City1.X;
            myLine.Y1 = City1.Y;
            myLine.X2 = City2.X;
            myLine.Y2 = City2.Y;

            MyCanvas.Children.Add(myLine);
        }
        private void OneStep(object sender, EventArgs e)
        {
            if (CountOfIterations < NumOfCities)
            {
                int IndexOfCity;
                double LenghtOfWay;

                (IndexOfCity, LenghtOfWay) = FindBestWay(pCofVisited[CountOfIterations]);
                pCofVisited.Add(pC[IndexOfCity]);

                PlotWay(pCofVisited[CountOfIterations], pCofVisited[CountOfIterations + 1]);

                LenghtOfWholePath += LenghtOfWay;
                CountOfIterations++;
                CurrIter.Content = CountOfIterations + " / " + Math.Round(LenghtOfWholePath, 3);
            }
            else 
            {
                PlotWay(pCofVisited[CountOfIterations], pC[0]);
                LenghtOfWholePath += FindPathLenght(pCofVisited[CountOfIterations], pC[0]);
                CurrIter.Content = CountOfIterations + " / " + Math.Round(LenghtOfWholePath, 3);
                dT.Stop();
            }
        }
        private void FirstStep()
        {
            int IndexOfCity;
            double LenghtOfWay;
            
            pCofVisited.Add(pC[0]);
            (IndexOfCity, LenghtOfWay) = FindBestWay(pCofVisited[0]);
            pCofVisited.Add(pC[IndexOfCity]);

            CountOfIterations++;
            
            LenghtOfWholePath += LenghtOfWay;
        }
        public static (int, double) FindBestWay(Point City)
        {
            int IndexOfCity = 0;
            double ShortestPath = 5000;

            for (int i = 0; i < NumOfCities; i++)
            {
                if (FindPathLenght(City, pC[i]) < ShortestPath && !pCofVisited.Contains(pC[i]))
                {
                    ShortestPath = FindPathLenght(City, pC[i]);
                    IndexOfCity = i;
                }

            }

            return (IndexOfCity, ShortestPath);
        }
        private void InitPoints()
        {
            Random rnd = new Random();
            pC.Clear();
            EllipseArray.Clear();
            IndexArray.Clear();

            for (int i = 0; i < NumOfCities; i++)
            {
                Point p = new Point();
                p.X = rnd.Next(Radius, (int)(0.75 * GreedyWin.Width) -
                3 * Radius);

                p.Y = rnd.Next(Radius, (int)(0.90 * GreedyWin.Height -
                3 * Radius));

                pC.Add(p);
            }
            for (int i = 0; i < NumOfCities; i++)
            {
                Ellipse el = new Ellipse();
                Label index = new Label();
                index.Content = " " + i;
                index.FontFamily = new FontFamily("Tahoma");
                index.FontSize = 15;
                index.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                index.Foreground = new SolidColorBrush(Color.FromRgb(31, 31, 31));
                el.StrokeThickness = 2;
                el.Height = el.Width = Radius;
                el.Stroke = new SolidColorBrush(Color.FromRgb(187, 134, 252));
                el.Fill = new SolidColorBrush(Color.FromRgb(187, 134, 252));
                EllipseArray.Add(el);
                IndexArray.Add(index);
            }
        }
        private void PlotPoints()
        {
            for (int i = 0; i < NumOfCities; i++)
            {
                Canvas.SetLeft(EllipseArray[i], pC[i].X - Radius / 2);
                Canvas.SetTop(EllipseArray[i], pC[i].Y - Radius / 2);
                Canvas.SetLeft(IndexArray[i], pC[i].X - Radius / 2);
                Canvas.SetTop(IndexArray[i], pC[i].Y - Radius / 2);
                MyCanvas.Children.Add(EllipseArray[i]);
                MyCanvas.Children.Add(IndexArray[i]);
            }
        }
        public static double FindPathLenght(Point City1, Point City2)
        {
            double x1 = City1.X;
            double y1 = City1.Y;
            double x2 = City2.X;
            double y2 = City2.Y;

            double cityDistance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

            return cityDistance;
        }
        private void StopStart_Click(object sender, RoutedEventArgs e)
        {
            if (dT.IsEnabled)
            {
                dT.Stop();
                PointsCount.IsEnabled = true;
            }
            else
            {
                PointsCount.IsEnabled = false;
                PlotPoints();
                FirstStep();
                PlotWay(pCofVisited[0], pCofVisited[CountOfIterations]);
                CurrIter.Content = CountOfIterations + "  /  " + Math.Round(LenghtOfWholePath, 3);
                dT.Start();
            }
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            for (int i = 0; i < NumOfCities; i++)
            {
                MyCanvas.Children.Remove(EllipseArray[i]);
                MyCanvas.Children.Remove(IndexArray[i]);
            }
            Close();
            mw.Show();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void Changed_PointsCount(object sender, TextChangedEventArgs e)
        {
            try
            {
                NumOfCities = Convert.ToInt32(PointsCount.Text);
                CountOfIterations = 0;
            }
            catch { PointsCount.Text = ""; }
            InitPoints();
            IndexOfCities = new int[NumOfCities];
        }
    }
}
