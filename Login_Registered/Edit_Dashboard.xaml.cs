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

namespace Login_Registered
{
    /// <summary>
    /// Interaction logic for Edit_Dashboard.xaml
    /// </summary>
    public partial class Edit_Dashboard : Window
    {
        public Edit_Dashboard()
        {
            InitializeComponent();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Edit_Dashboard mainWindow = new Edit_Dashboard();
            mainWindow.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubmitChanges_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubmitTracking_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackToPurchasedCheckout_Click(object sender, RoutedEventArgs e)
        {
            Checkout mainWindow = new Checkout();
            mainWindow.Show();
            this.Close();
        }

        private void BackToAbandonedCheckout_Click(object sender, RoutedEventArgs e)
        {
            Window1 mainWindow = new Window1();
            mainWindow.Show();
            this.Close();
        }

        private void BackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            Display_Dashboard mainWindow = new Display_Dashboard();
            mainWindow.Show();
            this.Close();
        }
    }
}
