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
    /// Interaction logic for Forget_Password.xaml
    /// </summary>
    public partial class Forget_Password : Window
    {
        public Forget_Password()
        {
            InitializeComponent();
        }

        private void Back_To_Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Forget_Password mainWindow = new Forget_Password();
            mainWindow.Show();
            this.Close();
        }

        private void OTP_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
