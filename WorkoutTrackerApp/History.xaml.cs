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
    public partial class History : Page
    {
        WorkoutData db = new WorkoutData();

        public History()
        {
            InitializeComponent();
            LoadWorkouts(); 
        }

        //method to load workouts
        public void LoadWorkouts()
        {
            try
            {
                if (db.Workouts == null)
                {
                    CustomMessageBox.Show("Database connection failed.");
                    return;
                }

                var query = from w in db.Workouts
                            orderby w.Date descending
                            select w;

                var workouts = query.ToList();

                icWorkouts.ItemsSource = workouts; 

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading workouts: {ex.Message}");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // check if sender is a button and has integer ID
                if (sender is Button btn && btn.Tag is int workoutId)
                {
                    // find the workout to delete
                    var query = from w in db.Workouts
                                where w.WorkoutID == workoutId
                                select w;

                    var workoutToDelete = query.FirstOrDefault();

                    // if workout is find, delete it from the database
                    if (workoutToDelete != null)
                    {
                        db.Workouts.Remove(workoutToDelete);
                        db.SaveChanges();
                        LoadWorkouts(); // refresh UI
                    }
                }
            }
            catch (Exception ex) 
            {
                CustomMessageBox.Show($"Error deleting workout: {ex.Message}");
            }
        }
    }
}
