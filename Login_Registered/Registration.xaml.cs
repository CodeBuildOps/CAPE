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

using MySql.Data.MySqlClient;

namespace Login_Registered
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=wpf;username=root;password=");
        public Registration()
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

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String query = "INSERT INTO users (username,password,firstname,lastname) VALUES ('"+username.Text+"'," +
                    "'" + password.Password + "','" + firstname.Text + "','" + lastname.Text + "')";

                MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                if (sqlcmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Registered Successfully");
                    MainWindow login = new MainWindow();
                    login.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Not Registered ");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Registration mainWindow = new Registration();
            mainWindow.Show();
            this.Close();
        }
    }
}
