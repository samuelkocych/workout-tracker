using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
