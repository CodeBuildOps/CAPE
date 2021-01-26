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

/*
 Database
 -----------------------
 CREATE TABLE purchased_checkout (
    pid int NOT NULL AUTO_INCREMENT,
    FullName varchar(255) NOT NULL,
    Email varchar(255) NOT NULL,
    PhoneNumber varchar(255) NOT NULL,
    Address varchar(255) NOT NULL,
    City varchar(255) NOT NULL,
    Country varchar(255) NOT NULL,
    Province varchar(255) NOT NULL,
    ZipCode varchar(255) NOT NULL,
    SpecialNote varchar(255) NOT NULL,
    OrderNumber varchar(255) NOT NULL,
    DateTime varchar(255) NOT NULL,
    PurchasedProductNamer varchar(255) NOT NULL,
    ProductVariant varchar(255) NOT NULL,
    QuantityPurchased varchar(255) NOT NULL,
    OriginalAmount varchar(255) NOT NULL,
    PurchasedAmount varchar(255) NOT NULL,
    DiscountGiven varchar(255) NOT NULL,
    PurchasedAmountAfterDiscount varchar(255) NOT NULL,
    PRIMARY KEY (pid)
);
 */

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
    /// Interaction logic for Checkout.xaml
    /// </summary>
    public partial class Checkout : Window
    {
        MySqlConnection conn;
        public Checkout()
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
                        //Calling the function to display the grid
                        FillDataGrid();

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
  
        // Here, we have to return the RadioButton object name, i,e whichone, but how we have to find that.
        //One way is to make a variable and store it's content.
        private string PurchasedProductName = null;
        private void BSCheck(object sender, RoutedEventArgs e)
        {
            RadioButton whichone = sender as RadioButton;
            PurchasedProductName = whichone.Content + "";
            //MessageBox.Show(ProductName);
        }
        private void ClearEntries()
        {
            FullName.Text = "";
            Email.Text = "";
            PhoneNumber.Text = "";
            Address.Text = "";
            City.Text = "";
            Country.Text = "";
            Province.Text = "";
            ZipCode.Text = "";
            SpecialNote.Text = "";
            OrderNumber.Text = "";
            DateTime.Text = "";
            //PurchasedProductName = null;
            ProductVariant.Text = "";
            QuantityPurchased.Text = "";
            OriginalAmount.Text = "";
            PurchasedAmount.Text = "";
            DiscountGiven.Text = "";
            PurchasedAmountAfterDiscount.Text = "";
        }

        private void FillDataGrid()
        {

            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                String query = "SELECT * FROM purchased_checkout";
                MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlcmd);
                DataTable dt = new DataTable("Purchased Checkout");
                adapter.Fill(dt);
                DisplayPurchased.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors in Displaying Purchased Checkout Table:" + ex);

            }
            finally
            {
                conn.Close();
            }

        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                //All the text fields entries are mandatory except the SpecialNote
                if (FullName.Text != "" && Email.Text != "" && PhoneNumber.Text != "" && Address.Text != "" && City.Text != "" && Country.Text != ""
                    && Province.Text != "" && ZipCode.Text != "" && OrderNumber.Text != "" && DateTime.Text != ""
                    && PurchasedProductName != null && ProductVariant.Text != "" && QuantityPurchased.Text != "" && OriginalAmount.Text != ""
                    && PurchasedAmount.Text != "" && DiscountGiven.Text != "" && PurchasedAmountAfterDiscount.Text != "")
                {
                    String query = "INSERT INTO purchased_checkout (FullName, Email, PhoneNumber, Address, City, Country, Province, ZipCode, " +
                        "SpecialNote, OrderNumber, DateTime, PurchasedProductNamer, ProductVariant, QuantityPurchased,OriginalAmount, PurchasedAmount," +
                        " DiscountGiven, PurchasedAmountAfterDiscount) VALUES ('" + FullName.Text + "'," + "'" + Email.Text + "','" + PhoneNumber.Text + "','" 
                        + Address.Text + "','" + City.Text + "','" + Country.Text + "','" + Province.Text + "','" + ZipCode.Text + "','" + SpecialNote.Text + "','" + OrderNumber.Text
                        + "','" + DateTime.Text + "','" + PurchasedProductName + "','" + ProductVariant.Text + "','" + QuantityPurchased.Text + "','" + OriginalAmount.Text + "','" + PurchasedAmount.Text+ "','" +
                        DiscountGiven.Text + "','" + PurchasedAmountAfterDiscount.Text + "')";

                    MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                    if (sqlcmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Inserted Successfully");
                        //Show the new data in the grid
                        FillDataGrid();
                        //clear all the previous text fields entries
                        ClearEntries();
                    }
                    else
                    {
                        MessageBox.Show("Not Inserted ");
                    }
                }
                else
                {
                    MessageBox.Show("Please! Fill all the fields (Special Note is Optional)");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Caught : " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            //Here Window1 --> Abandoned_Checkout
            MainWindow Window = new MainWindow();
            Window.Show();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            this.Close();
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

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {

            Checkout Window = new Checkout();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            Window.Show();
            this.Close();
        }

        private void BackToAbandonedCheckout_Click(object sender, RoutedEventArgs e)
        {
            Window1 Window = new Window1();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            Window.Show();
            this.Close();
        }

        private void BackToPurchasedCheckout_Click(object sender, RoutedEventArgs e)
        {
            Checkout Window = new Checkout();
            Window.Show();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            this.Close();
        }

        private void BackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            Display_Dashboard Window = new Display_Dashboard();
            Window.Show();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            this.Close();
        }

        private void EditDashboard_Click(object sender, RoutedEventArgs e)
        {
            Edit_Dashboard Window = new Edit_Dashboard();
            Window.Show();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            this.Close();
        }
    }
}
