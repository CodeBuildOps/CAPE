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
    /// Interaction logic for Checkout.xaml
    /// </summary>
    public partial class Checkout : Window
    {
        public Checkout()
        {
            InitializeComponent();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Checkout Window = new Checkout();
            Window.Show();
            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeVariant_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OriginalAmount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PurchasedAmount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PurchasedAmountAfterDiscount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackToAbandonedCheckout_Click(object sender, RoutedEventArgs e)
        {
            Window1 Window = new Window1();
            Window.Show();
            this.Close();
        }

        private void BackToPurchasedCheckout_Click(object sender, RoutedEventArgs e)
        {
            Checkout Window = new Checkout();
            Window.Show();
            this.Close();
        }

        private void BackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            Display_Dashboard Window = new Display_Dashboard();
            Window.Show();
            this.Close();
        }

        private void EditDashboard_Click(object sender, RoutedEventArgs e)
        {
            Edit_Dashboard Window = new Edit_Dashboard();
            Window.Show();
            this.Close();
        }
    }
}
