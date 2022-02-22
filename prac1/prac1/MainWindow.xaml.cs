using System.Windows;
namespace prac1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void StudyModeBtn_Click(object sender, RoutedEventArgs e)
        {
            StudyModeWindow studyModeWindow = new StudyModeWindow();
            studyModeWindow.Show();
        }

        private void ProtectionModeBtn_Click(object sender, RoutedEventArgs e)
        {
            ProtectionModeWindow protectionModeWindow = new ProtectionModeWindow();
            protectionModeWindow.Show();
        }
    }
}