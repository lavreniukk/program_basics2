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
using System.IO;

namespace lab1
{
    /// <summary>
    /// Логика взаимодействия для FirstWindow.xaml
    /// </summary>
    public partial class FirstWindow : Window
    {
        static List<Student> studentsData = new List<Student>();
        static Random rnd = new Random();

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
                file.WriteLine($"Record Book Number: {ID, 15};   Full Name: {FullName, 50};   Personal Data: {PersonalData, 30}.");
            }
        }

        public FirstWindow()
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

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            string ID = TextBoxID.Text;
            for (int i = 0; i < studentsData.Count; i++)
                if (ID == studentsData[i].getID())
                    studentsData.Remove(studentsData[i]);
            StreamWriter sw = new StreamWriter(@"\studentsData.txt");
            for (int i = 0; i < studentsData.Count; i++)
                studentsData[i].printStudent(sw);
            sw.Close();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string ID = TextBoxID.Text;
            string Name = TextBoxName.Text;
            string Data = TextBoxInfo.Text;
            studentsData.Add(new Student(ID, Name, Data));
            StreamWriter sw = new StreamWriter(@"\studentsData.txt");
            foreach (Student person in studentsData)
                person.printStudent(sw);
            sw.Close();
        }

        private void RndNum_Click(object sender, RoutedEventArgs e)
        {
            string num = rnd.Next(1000, 100000).ToString();
            TextBoxID.Text = num;
        }
    }    
}
