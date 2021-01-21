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

using MySql.Data.MySqlClient;

namespace Login_Registered
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=wpf;username=root;password=");
        public MainWindow()
        {
            InitializeComponent();
            
            try
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    status.Content = "Connected";
                    status.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                }
                else
                {
                    status.Content = "Something wrong";
                    status.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 255));
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                status.Content = "Not-Connected (Exception)";
                status.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
            
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String query = "SELECT COUNT(1) FROM users WHERE USERNAME=@username AND password=@password";
                MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                sqlcmd.CommandType = System.Data.CommandType.Text;
                sqlcmd.Parameters.AddWithValue("@username", username.Text);
                sqlcmd.Parameters.AddWithValue("@password", password.Password);
                int count = Convert.ToInt32(sqlcmd.ExecuteScalar());

                if(count==1)
                {
                    MessageBox.Show("You are the correct person");
                    Edit_Dashboard dashboard = new Edit_Dashboard();
                    conn.Close();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong XXXXX ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void Registration_link_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            conn.Close();
            this.Close();
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RoutedEventArgs convert = e;
                Login_Click(sender, convert);
                
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ForgetPassword_link_Click(object sender, RoutedEventArgs e)
        {
            Forget_Password mainWindow = new Forget_Password();
            mainWindow.Show();
            this.Close();

        }
    }
}
