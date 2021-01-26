/*---------------------------------------------------------------------+\
|                                                                       |
|   Copyright 2020-2021 BLANKET SAUNA and/or its subsidiaries and       |
|   affiliates.                                                         |
|   All Rights Reserved                                                 |
|                                                                       |
|   Including software, file formats, and audio-visual displays;        |
|   may only be used pursuant to applicable software license            |
|   agreement; contains confidential and proprietary information of     |
|   BLANKET SAUNA and/or third parties which is protected by copyright  |
|   and trade secret law and may not be provided or otherwise made      |
|   available without proper authorization.                             |
|                                                                       |
|   Unpublished -- rights reserved under the Copyright Laws of the      |
|   INDIA.                                                              |
|                                                                       |
|   BLANKET SAUNA                                                       |
|   INDIA                                                               |
|   Co-Founder :Abhishek Kumar Singh                                    |
|   Founder    :Ajit Kumar Singh                                        |
|   Website    :https://blanketsauna.com                                |
\+---------------------------------------------------------------------*/

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
        MySqlConnection conn;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                //Checking for Internet Connection
                if (IsConnectedToInternet())
                {
                    // Do Work 
                    //MessageBox.Show("Great!, You have Internet Connection");
                    conn = new MySqlConnection("datasource=localhost;port=3306;database=wpf;username=root;password=");
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        status.Content = "Connected";
                        status.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    }
                    else
                    {
                        status.Content = "Not-Connected With Database";
                        status.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 255));
                    }
                }
                else
                {
                    // Show Error MeassgeBox 
                    MessageBox.Show("No Connection, Please check your Internet Connection");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Either No Internet Connection or Not Connected with Database");
            }
        }

        //Checking the internet connection
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        //Creating a function that uses the API function...
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
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
                    Display_Dashboard dashboard = new Display_Dashboard();
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
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
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
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            this.Close();
        }

        private void ForgetPassword_link_Click(object sender, RoutedEventArgs e)
        {
            Forget_Password mainWindow = new Forget_Password();
            mainWindow.Show();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            this.Close();

        }
    }
}
