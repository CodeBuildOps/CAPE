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

using MySql.Data.MySqlClient;
using System.Data;

namespace Login_Registered
{
    /// <summary>
    /// Interaction logic for Edit_Dashboard.xaml
    /// </summary>
    
    public partial class Edit_Dashboard : Window
    {
        MySqlConnection conn;
        public Edit_Dashboard()
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


        // Here, we have to return the RadioButton object name, i,e whichone, but how we have to find that.
        //One way is to make a variable and store it's content.
        private string SelectedProductName = null;
        private void BSCheck(object sender, RoutedEventArgs e)
        {
            RadioButton whichone = sender as RadioButton;
            SelectedProductName = whichone.Content + "";
            MessageBox.Show("You have selected : "+SelectedProductName);

            
            //After selecting the appropriate radio button, the remaining text fields automatically set a/c to the database previous record
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                //BS100
                if (SelectedProductName == "Blanket Sauna - BS 100, Far Infrared Sauna Blanket")
                {
                    try
                    {
                        //Latest record added --> Last record fetch
                        String query = "SELECT ProductVariant,OriginalAmount,PurchasedAmount FROM bs100 ORDER BY bs100_id DESC LIMIT 1";
                        MySqlCommand sqlcmd = new MySqlCommand(query, conn);

                        MySqlDataReader reader = sqlcmd.ExecuteReader();
                        if (reader.Read())
                        {
                            //Show the fetched data in the textboxes
                            ProductVariant.Text = reader["ProductVariant"].ToString();
                            OriginalAmount.Text = reader["OriginalAmount"].ToString();
                            PurchasedAmount.Text = reader["PurchasedAmount"].ToString();
                            reader.Close();
                        }
                        else
                        {
                            MessageBox.Show("No Records For BS 100: Please enter the details");
                            ClearProductDetailsEntries();
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Exception BS100"+ex);
                    }
                    finally
                    {
                        conn.Close();
                    }
                    
                    
                }
                //BS103
                else if (SelectedProductName == "Blanket Sauna - BS 103, Jade and Tourmaline Stones")
                {
                    try
                    {
                        //Latest record added --> Last record fetch
                        String query = "SELECT ProductVariant,OriginalAmount,PurchasedAmount FROM bs103 ORDER BY bs103_id DESC LIMIT 1";
                        MySqlCommand sqlcmd = new MySqlCommand(query, conn);

                        MySqlDataReader reader = sqlcmd.ExecuteReader();
                        if (reader.Read())
                        {
                            //Show the fetched data in the textboxes
                            ProductVariant.Text = reader["ProductVariant"].ToString();
                            OriginalAmount.Text = reader["OriginalAmount"].ToString();
                            PurchasedAmount.Text = reader["PurchasedAmount"].ToString();
                            reader.Close();
                        }
                        else
                        {
                            MessageBox.Show("No Records For BS 103: Please enter the details");
                            ClearProductDetailsEntries();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception BS103" + ex);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                //BS106
                else if (SelectedProductName == "Blanket Sauna - BS 106, Bain Stones, Negative Ionic Cloth and Photon Lights")
                {
                    try
                    {
                        //Latest record added --> Last record fetch
                        String query = "SELECT ProductVariant,OriginalAmount,PurchasedAmount FROM bs106 ORDER BY bs106_id DESC LIMIT 1";
                        MySqlCommand sqlcmd = new MySqlCommand(query, conn);

                        MySqlDataReader reader = sqlcmd.ExecuteReader();
                        if (reader.Read())
                        {
                            //Show the fetched data in the textboxes
                            ProductVariant.Text = reader["ProductVariant"].ToString();
                            OriginalAmount.Text = reader["OriginalAmount"].ToString();
                            PurchasedAmount.Text = reader["PurchasedAmount"].ToString();
                            reader.Close();
                        }
                        else
                        {
                            MessageBox.Show("No Records For BS 106: Please enter the details");
                            ClearProductDetailsEntries();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception BS106" + ex);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("For accessing all product details, Exception Caught  : " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            
        }
        private void ClearProductDetailsEntries()
        {
            ProductVariant.Text = "";
            OriginalAmount.Text = "";
            PurchasedAmount.Text = "";
        }
  
        //Edit the Blanket Sauna Product Prices
        private void SubmitChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                //BS100
                if (SelectedProductName == "Blanket Sauna - BS 100, Far Infrared Sauna Blanket")
                {
                    if (ProductVariant.Text != "" && OriginalAmount.Text != "" && PurchasedAmount.Text != "")
                    {
                        String query = "INSERT INTO bs100 (ProductVariant,OriginalAmount,PurchasedAmount) VALUES ('" + ProductVariant.Text + "'," + "'" + OriginalAmount.Text + "','" + PurchasedAmount.Text + "')";

                        MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                        if (sqlcmd.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("BS 100 Inserted Successfully \n\nColor: " + ProductVariant.Text + "\nOriginal Amount: " + OriginalAmount.Text + "\nPurchased Amount: " + PurchasedAmount.Text);
                            //Show the new data in the grid
                            //FillDataGrid();
                            //clear all the previous text fields entries
                            ClearProductDetailsEntries();
                        }
                        else
                        {
                            MessageBox.Show("Not Inserted ");
                        }
                    }
                }
                //BS103
                else if (SelectedProductName == "Blanket Sauna - BS 103, Jade and Tourmaline Stones")
                {
                    if (ProductVariant.Text != "" && OriginalAmount.Text != "" && PurchasedAmount.Text != "")
                    {
                        String query = "INSERT INTO bs103 (ProductVariant,OriginalAmount,PurchasedAmount) VALUES ('" + ProductVariant.Text + "'," + "'" + OriginalAmount.Text + "','" + PurchasedAmount.Text + "')";

                        MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                        if (sqlcmd.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("BS 103 Inserted Successfully \n\nColor: " + ProductVariant.Text + "\nOriginal Amount: " + OriginalAmount.Text + "\nPurchased Amount: " + PurchasedAmount.Text);
                            //Show the new data in the grid
                            //FillDataGrid();
                            //clear all the previous text fields entries
                            ClearProductDetailsEntries();
                        }
                        else
                        {
                            MessageBox.Show("Not Inserted ");
                        }
                    }
                }
                //BS106
                else if (SelectedProductName == "Blanket Sauna - BS 106, Bain Stones, Negative Ionic Cloth and Photon Lights")
                {
                    if (ProductVariant.Text != "" && OriginalAmount.Text != "" && PurchasedAmount.Text != "")
                    {
                        String query = "INSERT INTO bs106 (ProductVariant,OriginalAmount,PurchasedAmount) VALUES ('" + ProductVariant.Text + "'," + "'" + OriginalAmount.Text + "','" + PurchasedAmount.Text + "')";

                        MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                        if (sqlcmd.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("BS 106 Inserted Successfully \n\nColor: " + ProductVariant.Text + "\nOriginal Amount: " + OriginalAmount.Text + "\nPurchased Amount: " + PurchasedAmount.Text);
                            //Show the new data in the grid
                            //FillDataGrid();
                            //clear all the previous text fields entries
                            ClearProductDetailsEntries();
                        }
                        else
                        {
                            MessageBox.Show("Not Inserted ");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please! Fill all the fields");
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


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Tracking ID Details

        private void ClearTrackingDetailsEntries()
        {
            //OrderNumber.Text = "";
            TrackingServiceName.Text = "";
            TrackingID.Text = "";

        }
        private void FillDataGrid(object sender, RoutedEventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                String query = "SELECT FullName,Email,Address,City,Country,Province,ZipCode,SpecialNote," +
                    "OrderNumber,DateTime,PurchasedProductNamer,ProductVariant,QuantityPurchased,OriginalAmount,PurchasedAmount" +
                    ",DiscountGiven,PurchasedAmountAfterDiscount,TrackingID,ShipmentThrough,Shipped FROM purchased_checkout where OrderNumber = @OrderNumber";
                MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                sqlcmd.Parameters.AddWithValue("@OrderNumber", OrderNumber.Text);

                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlcmd);
                DataTable dt = new DataTable("Purchased Checkout");
                adapter.Fill(dt);
                DisplaySearch.ItemsSource = dt.DefaultView;

                //display the tracking id and Shipment through
                MySqlDataReader read =sqlcmd.ExecuteReader();
                while(read.Read())
                {
                    TrackingID.Text =read.GetValue(17).ToString();
                    TrackingServiceName.Text = read.GetValue(18).ToString();
                }
                read.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors in Displaying Searched Table:" + ex);
            }
            finally
            {
                conn.Close();
                
            }

        }

        //When Enter Key is pressed
        private void OrderNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RoutedEventArgs convert = e;
                FillDataGrid(sender, convert);

            }
        }
        //When any text changes
        private void OrderNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            RoutedEventArgs convert = e;
            FillDataGrid(sender, convert);
        }

        //Submit TrackingID Status
        private void SubmitTracking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                //Adding the Tracking id and Shippment through like DHL,UPS,Fedx, etc
                if (TrackingServiceName.Text != "" && TrackingID.Text != "" && OrderNumber.Text != "")
                {
                    String query = "UPDATE purchased_checkout SET TrackingID = @TrackingID,ShipmentThrough = @TrackingServiceName WHERE OrderNumber = @OrderNumber";
                    MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                    sqlcmd.Parameters.AddWithValue("@TrackingID", TrackingID.Text);
                    sqlcmd.Parameters.AddWithValue("@TrackingServiceName", TrackingServiceName.Text);
                    sqlcmd.Parameters.AddWithValue("@OrderNumber", OrderNumber.Text);

                    if (sqlcmd.ExecuteNonQuery() == 1)
                    {
                       MessageBox.Show("Tracking Details Added for Order Number : " + OrderNumber.Text);
                       //Show the new data in the grid
                       FillDataGrid(sender,e);
                       //clear all the previous text fields entries
                       ClearTrackingDetailsEntries();
                    }
                    else
                    {
                       MessageBox.Show("Tracking Details Not Inserted, Please Check the Order Number ");
                    }
                    
                }
                
                else
                {
                    MessageBox.Show("Please! Fill all the Tracking fields");
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


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

        string ShippedStatus = null;
        private void ShippedCheck(object sender, RoutedEventArgs e)
        {
            RadioButton whichone = sender as RadioButton;
            ShippedStatus = whichone.Content.ToString();
        }

        //Submit Shipped Status 
        private void SubmitShippedStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                
                //Adding the Tracking id and Shippment through like DHL,UPS,Fedx, etc
                if (TrackingServiceName.Text != "" && TrackingID.Text != "" && OrderNumber.Text != "" && ShippedStatus != null)
                {
                    String query = "UPDATE purchased_checkout SET Shipped = @ShippedStatus WHERE OrderNumber = @OrderNumber";
                    MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                    sqlcmd.Parameters.AddWithValue("@ShippedStatus", ShippedStatus);
                    sqlcmd.Parameters.AddWithValue("@OrderNumber", OrderNumber.Text);

                    if (sqlcmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Shipped Status Changed : " + ShippedStatus +
                                        "\nOrder Number : " + OrderNumber.Text +
                                        "\nTracking ID : " + TrackingID.Text +
                                        "\nShipment Through : " + TrackingServiceName.Text);
                        //Show the new data after shipped status in the grid
                        FillDataGrid(sender, e);
                        //clear all the previous text fields entries
                        ClearTrackingDetailsEntries();
                    }
                    else
                    {
                        MessageBox.Show("Shippment Status Not Updated, Please Check the Order Number,Tracking ID and Service Name ");
                    }
                }

                else
                {
                    MessageBox.Show("Please check Order Number, Tracking Id, Service Name or Shipped Status");
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
            MainWindow Window = new MainWindow();
            Window.Show();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            this.Close();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Edit_Dashboard mainWindow = new Edit_Dashboard();
            mainWindow.Show();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            this.Close();
        }
        private void BackToPurchasedCheckout_Click(object sender, RoutedEventArgs e)
        {
   
            Checkout mainWindow = new Checkout();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            mainWindow.Show();
            this.Close();
        }

        private void BackToAbandonedCheckout_Click(object sender, RoutedEventArgs e)
        {
            Window1 mainWindow = new Window1();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            mainWindow.Show();
            this.Close();
        }

        private void BackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            Display_Dashboard mainWindow = new Display_Dashboard();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            mainWindow.Show();
            this.Close();
        }

       
    }
}
