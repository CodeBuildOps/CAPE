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
using System.Data;

namespace Login_Registered
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=wpf;username=root;password=");
        public Dashboard()
        {
            InitializeComponent();
            try
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    status.Content = "Connected";
                    status.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    FillDataGrid();
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

        private void Logout_Click(object sender, RoutedEventArgs e)
        { 

            MainWindow login = new MainWindow();
            login.Show();
            conn.Close();
            this.Close();

        }  
      
        private void FillDataGrid()
        {
            String query = "SELECT * FROM users";
            MySqlCommand sqlcmd = new MySqlCommand(query, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlcmd);

            DataTable dt = new DataTable("Employee");
            adapter.Fill(dt);
            all_members.ItemsSource = dt.DefaultView;

          
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Dashboard mainWindow = new Dashboard();
            mainWindow.Show();
            this.Close();
        }
    }
}
