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
using WorkoutTrackerApp.Classes;

namespace WorkoutTrackerApp
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Page
    {
        WorkoutData db = new WorkoutData(); // Připojení k databázi

        public History()
        {
            InitializeComponent();
            LoadWorkouts();
        }

        private void LoadWorkouts()
        {
            var workouts = db.Workouts
                             .OrderByDescending(w => w.Date) // Seřadíme od nejnovějšího
                             .ToList();

            icWorkouts.ItemsSource = workouts; // Přiřazení dat do XAML
        }
       
        private void DeleteWorkout_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int workoutID = (int)button.Tag;

                var workout = db.Workouts.FirstOrDefault(w => w.WorkoutID == workoutID);
                if (workout != null)
                {
                    // Potvrzení mazání
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this workout?", "Delete", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        db.Workouts.Remove(workout);
                        db.SaveChanges();
                        LoadWorkouts(); // Aktualizace UI
                    }
                }
            }
        }
    }
}
