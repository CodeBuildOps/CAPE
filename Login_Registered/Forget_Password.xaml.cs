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
using System.Windows.Shapes;

using System.Net.Mail;
using MySql.Data.MySqlClient;
namespace Login_Registered
{
    /// <summary>
    /// Interaction logic for Forget_Password.xaml
    /// </summary>
    public partial class Forget_Password : Window
    {
        MySqlConnection conn;
        public Forget_Password()
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

        //When Enter key is pressed at Username/Email textbox
        private void UsernameEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RoutedEventArgs convert = e;
                OTP_Click(sender, convert);
            }
        }
       
        string Your_OTP = null;

        private void OTP_Click(object sender, RoutedEventArgs e)
        {
            //Accessing the receiver email from database

            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                String query = "SELECT email FROM users WHERE username = @username or email = @username";
                MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                sqlcmd.Parameters.AddWithValue("@username", username.Text);

                MySqlDataReader read = sqlcmd.ExecuteReader();
                if (read.Read())
                {
                    string receiverEmail = read["email"].ToString();
                    read.Close();

                    //Email Send
                    try
                    {
                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                        client.EnableSsl = true;
                        client.Timeout = 10000;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential("abhiksingh1999@gmail.com", "abhishek1999");

                        //OTP Generator
                        Your_OTP = OTP_Generator();

                        MailMessage message = new MailMessage();
                        message.To.Add(receiverEmail);
                        message.From = new MailAddress("abhiksingh1999@gmail.com");
                        message.Subject = "CAPE OTP";
                        message.Body = "Hello CAPE users.\n" + "You can take your password from here :)\n" + "This is your OTP :" + Your_OTP;

                        client.Send(message);
                        MessageBox.Show("OTP is Send Successfully to your registered email. Kindly Check!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception Caught is sending OTP registered email :" + ex);
                    }

                }
                else
                {
                    MessageBox.Show("Wrong Username/Email, check again");
                }
            }
            catch (Exception ex)
            {
                //Less secure app access --> Please Enable for sending the email
                MessageBox.Show("Exception caught in accessing the receiver email/Sending the OTP : " + ex);
            }
            finally
            {
                conn.Close();
            }
        }

        //When Enter key is pressed at OTPBox textbox
        private void OTPBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
        
                    if (OTPBox.Text == Your_OTP)
                    {
                        if (conn.State == System.Data.ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        String query = "SELECT password FROM users WHERE username = @username or email = @username";
                        MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                        sqlcmd.Parameters.AddWithValue("@username", username.Text);

                        MySqlDataReader read = sqlcmd.ExecuteReader();
                        if (read.Read())
                        {
                            MessageBox.Show("Correct. Please see your Password Below");

                            YourPassword.Content = "Password is : " + read["password"].ToString();
                            YourPassword.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                            read.Close();

                            //Clear the Your_OTP variable and OTPText box
                            Your_OTP = null;
                            OTPBox.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Wrong OTP Either Check your Username/Email or OTP");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wrong OTP Either Check your Username/Email or OTP");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception raised in accessing the password" + ex);
            }
            finally
            {
                conn.Close();
            }

        }



        private string OTP_Generator()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            var Your_OTP = new char[6];
            var random = new Random();

            for (int i = 0; i < Your_OTP.Length; i++)
            {
                Your_OTP[i] = chars[random.Next(chars.Length)];
            }

            return new string(Your_OTP);
        }
        
    private void Back_To_Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            this.Close();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
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
