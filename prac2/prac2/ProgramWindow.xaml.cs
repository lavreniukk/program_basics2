using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace prac2
{
    public partial class ProgramWindow : Window
    {
        static Random rnd = new Random();
        static DispatcherTimer dT;
        static int CountOfIterations = 0;
        static int Radius = 30;
        static int NumOfCities = 5;
        static int PopSize = 10;
        static double MutProb = 50;
        static int MaxIter = 700;
        static Polygon myPolygon = new Polygon();
        static List<Ellipse> EllipseArray = new List<Ellipse>();
        static List<Label> IndexArray = new List<Label>();
        static PointCollection pC = new PointCollection();
        static List<PointCollection> RoadsBetweenCities;
        public ProgramWindow()
        {
            dT = new DispatcherTimer();
            InitializeComponent();
            InitPoints();
            InitPolygon();
            RoadsBetweenCities = Generate_StartPopulation(PopSize);
            dT = new DispatcherTimer();
            dT.Tick += new EventHandler(OneStep);
            dT.Interval = new TimeSpan(0, 0, 0, 0, 1000);
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
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
                p.X = rnd.Next(Radius, (int)(0.75 * ProgWin.Width) -
                3 * Radius);

                p.Y = rnd.Next(Radius, (int)(0.90 * ProgWin.Height -
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
        private void InitPolygon()
        {
            myPolygon.Stroke = new SolidColorBrush(Color.FromRgb(225, 225, 225));
            myPolygon.StrokeThickness = 2;
        }
        private void PlotPoints()
        {
            for (int i = 0; i < NumOfCities; i++)
            {
                Canvas.SetLeft(EllipseArray[i], pC[i].X - Radius / 2);
                Canvas.SetTop(EllipseArray[i], pC[i].Y - Radius / 2);
                MyCanvas.Children.Add(EllipseArray[i]);
            }
        }
        private void PlotWay(PointCollection Road)
        {
            myPolygon.Points = Road;
            MyCanvas.Children.Add(myPolygon);
            for (int i = 0; i < NumOfCities; i++)
            {
                Canvas.SetLeft(IndexArray[i], pC[i].X - Radius / 2);
                Canvas.SetTop(IndexArray[i], pC[i].Y - Radius / 2);
                MyCanvas.Children.Add(IndexArray[i]);
            }
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
                PopulationSize.IsEnabled = false;
                MutProbability.IsEnabled = false;
                IterCount.IsEnabled = false;
                dT.Start();
            }
        }
        private void OneStep(object sender, EventArgs e)
        {
            if (CountOfIterations < MaxIter)
            {
                MyCanvas.Children.Clear();
                PlotPoints();
                PointCollection shortestPath = FindShortest(RoadsBetweenCities);
                PlotWay(shortestPath);
                RoadsBetweenCities = MakeNextPopulation(RoadsBetweenCities);
                CountOfIterations++;
                CurrIter.Content = CountOfIterations.ToString() + "  /  " + Math.Round(FindPath(shortestPath),3);
            }
            else
            {
                MyCanvas.Children.Clear();
                PlotPoints();
                PlotWay(FindShortest(RoadsBetweenCities));
                dT.Stop();
            }
        }
        //functions for genetic algorithm
        private List<PointCollection> Generate_StartPopulation(int PopSize)
        {
            RoadsBetweenCities = new List<PointCollection>();
            RoadsBetweenCities.Add(new PointCollection());

            //generate first chromosome
            for (int i = 0; i < NumOfCities; i++)
                RoadsBetweenCities[0].Add(pC[i]);

            for (int i = 1; i < PopSize; i++)
            {
                PointCollection NewRoad = Shuffle_PointColl();

                while (RoadsBetweenCities.Contains(NewRoad))
                      NewRoad = Shuffle_PointColl();

                RoadsBetweenCities.Add(NewRoad);
            }
            return RoadsBetweenCities;
        }
        public static PointCollection Shuffle_PointColl()
        {
            PointCollection Road = new PointCollection();
            Road.Add(pC[0]);
            for (int i = 1; i < NumOfCities; i++)
            {
                int index = rnd.Next(1, NumOfCities);
                while (Road.Contains(pC[index]))
                    index = rnd.Next(1, NumOfCities);
                Road.Add(pC[index]);
            }
            return Road;
        }
        public static double FindPath(PointCollection Road)
        {
            double Path = 0;
            for (int i = 0; i < NumOfCities - 1; i++)
            {
                double x1 = Road[i].X;
                double y1 = Road[i].Y;
                double x2 = Road[i + 1].X;
                double y2 = Road[i + 1].Y;

                double cityDistance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                Path += cityDistance;
            }
            return Path;

        }
        public static List<PointCollection> MakeNextPopulation(List<PointCollection> RoadsBetweenCities)
        {
            int FirstIndex = rnd.Next(PopSize);
            int SecIndex = rnd.Next(PopSize);
            int dotOfGap = rnd.Next(1, NumOfCities - 1);
            while (FirstIndex == SecIndex)
                SecIndex = rnd.Next(PopSize);

            PointCollection FirstParent = RoadsBetweenCities[FirstIndex];
            PointCollection SecondParent = RoadsBetweenCities[SecIndex];

            PointCollection FirstChild = new PointCollection();
            PointCollection SecondChild = new PointCollection();

            //making and mutating childs
            FirstChild = CrossingOfPopulation(FirstParent, SecondParent, dotOfGap);
            SecondChild = CrossingOfPopulation(SecondParent, FirstParent, dotOfGap);
            FirstChild = Mutate(FirstChild);
            SecondChild = Mutate(SecondChild);

            RoadsBetweenCities.Add(FirstChild);
            RoadsBetweenCities.Add(SecondChild);

            List<PointCollection> NextPopulation = PreparePopulation(RoadsBetweenCities);
            return RoadsBetweenCities;
        }
        public static PointCollection CrossingOfPopulation(PointCollection FirstParent, PointCollection SecondParent, int dotOfGap)
        {
            PointCollection Child = new PointCollection();
            PointCollection Inheritted = new PointCollection();

            for (int i = 0; i < dotOfGap; i++)
            {
                Child.Add(FirstParent[i]);
                Inheritted.Add(FirstParent[i]);
            }

            for (int i = dotOfGap; i < NumOfCities; i++)
            {
                if (!Inheritted.Contains(SecondParent[i]))
                {
                    Child.Add(SecondParent[i]);
                    Inheritted.Add(SecondParent[i]);
                }
            }

            while (Child.Count != NumOfCities)
            {
                for (int i = dotOfGap; i < NumOfCities; i++)
                  if (!Inheritted.Contains(FirstParent[i]))
                        Child.Add(FirstParent[i]);
            }

            return Child; 
        }
        public static PointCollection Mutate(PointCollection Child)
        {
            int percent = rnd.Next(100);
            if (percent < MutProb)
            {
                int gen1 = rnd.Next(NumOfCities);
                int gen2 = rnd.Next(NumOfCities);
                Point temp = Child[gen1];
                Child[gen1] = Child[gen2];
                Child[gen2] = temp;
            }
            return Child;
        }
        public static List<PointCollection>  PreparePopulation(List<PointCollection> RoadsBetweenCities)
        {
            int NumOfRoads = RoadsBetweenCities.Count;
            PointCollection buff;

            for (int i = 0; i < NumOfRoads; i++)
            {
                for (int j = 0; j < NumOfRoads - 1; j++)
                {
                    if (FindPath(RoadsBetweenCities[j]) > FindPath(RoadsBetweenCities[j + 1]))
                    {
                        buff = RoadsBetweenCities[j + 1];
                        RoadsBetweenCities[j + 1] = RoadsBetweenCities[j];
                        RoadsBetweenCities[j] = buff;
                    }
                }
            }
            RoadsBetweenCities.RemoveRange(NumOfRoads - 2, 2);
            
            return RoadsBetweenCities;
        }
        public static PointCollection FindShortest(List<PointCollection> RoadsBetweenCities)
        {
            double ShortestPath = Double.MaxValue;
            int IndexOfShortestPath = 0;
            for (int i = 0; i < RoadsBetweenCities.Count; i++)
                if (ShortestPath > FindPath(RoadsBetweenCities[i]))
                {
                    ShortestPath = FindPath(RoadsBetweenCities[i]);
                    IndexOfShortestPath = i;
                }
            
            return RoadsBetweenCities[IndexOfShortestPath];
        }
        private void Changed_PointsCount(object sender, TextChangedEventArgs e)
        {
            try { NumOfCities = Convert.ToInt32(PointsCount.Text); }
            catch { PointsCount.Text = ""; }
            InitPoints();
            InitPolygon();
            RoadsBetweenCities = Generate_StartPopulation(PopSize);
        }
        private void Changed_PopulationSize(object sender, TextChangedEventArgs e)
        {
            try { PopSize = Convert.ToInt32(PopulationSize.Text); }
            catch { PopulationSize.Text = ""; }
            InitPoints(); 
            InitPolygon();
            RoadsBetweenCities = Generate_StartPopulation(PopSize);
        }
        private void Changed_MutProbability(object sender, TextChangedEventArgs e)
        {
            try { MutProb = Convert.ToInt32(MutProbability.Text); }
            catch { MutProbability.Text = ""; }
            InitPoints();
            InitPolygon();
            RoadsBetweenCities = Generate_StartPopulation(PopSize);
        }
        private void Changed_IterCount(object sender, TextChangedEventArgs e)
        {
            try { MaxIter = Convert.ToInt32(IterCount.Text); }
            catch { IterCount.Text = ""; }
            InitPoints();
            InitPolygon();
            RoadsBetweenCities = Generate_StartPopulation(PopSize);
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(); 
            Close();
            MyCanvas.Children.Clear();
            mw.Show();
        }
    }
}
