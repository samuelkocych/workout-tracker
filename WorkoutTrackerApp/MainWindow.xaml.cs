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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkoutTrackerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Uri("Home.xaml", UriKind.Relative));
        }

        private void btnNewTraining_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Uri("NewTraining.xaml", UriKind.Relative));
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Uri("History.xaml", UriKind.Relative));
        }

        private void btnExercises_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Uri("Exercises.xaml", UriKind.Relative));
        }
    }
}
