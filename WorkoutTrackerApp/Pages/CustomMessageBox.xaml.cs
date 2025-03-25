using System.Windows;

namespace WorkoutTrackerApp
{
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox(string message)
        {
            InitializeComponent();
            txtMessage.Text = message; // set message text
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // close window
        }

        // show message from anywhere
        public static void Show(string message)
        {
            CustomMessageBox msgBox = new CustomMessageBox(message);
            msgBox.ShowDialog();
        }
    }
}
