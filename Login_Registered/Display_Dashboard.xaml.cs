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
    /// Interaction logic for Display_Dashboard.xaml
    /// </summary>
    public partial class Display_Dashboard : Window
    {
        MySqlConnection conn;
        public Display_Dashboard()
        {
            InitializeComponent();
            try
            {
                //Checking for Internet Connection
                if(IsConnectedToInternet())
                {
                    // Do Work 
                    //MessageBox.Show("Great!, You have Internet Connection");
                    conn = new MySqlConnection("datasource=localhost;port=3306;database=wpf;username=root;password=");
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        status.Content = "Connected";
                        status.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));

                        //Display all the members that are registered
                        DisplayMembersRegisteredFillDataGrid();
                        //Display all the 3 products Original price and Customer price
                        DisplayAllProductPrices();

                        //Display all the abandoned checkout
                        DisplayAbandonedFillDataGrid();

                        //Display all the purchased checkout
                        DisplayPurchasedFillDataGrid();

                        //Display total Sales and Abandoned Checkout in 3 boxes
                        DisplayTotalSalesAbandonedCheckout();

                        //Display total Sales Amount in 3 boxes
                        DisplayTotalSalesForAllProducts();

                        //Display if there is any Shipping order or not, Shipped Or Not?
                        IsProductShippedOrNot();
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
                    MessageBox.Show("No Connection, Please check your Connection");
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


        //Display The All Members Registered
        private void DisplayMembersRegisteredFillDataGrid()
        {
            try
            {
                String query = "SELECT * FROM users";
                MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlcmd);

                DataTable dt = new DataTable("Employee");
                adapter.Fill(dt);
                all_members.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors in Displaying Users Table :" + ex);

            }
            finally
            {
                conn.Close();
            }

        }

        //Display all the 3 products Original price and Customer price in their respective labels 
        private void DisplayAllProductPrices()
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                //We have to find the alterate query like JOIN
                //Here, I'm using the simple one
                //BS100
                String query = "SELECT OriginalAmount,PurchasedAmount FROM bs100 ORDER BY bs100_id DESC LIMIT 1";
                MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = sqlcmd.ExecuteReader();
                if (reader.Read())
                {
                    //Show the fetched data in the labels
                    OriginalPriceBS100.Content = "Original Amt : " + reader["OriginalAmount"].ToString();
                    OriginalPriceBS100.Foreground = new SolidColorBrush(Color.FromRgb(128, 0, 128));   // R G B

                    CustomerPriceBS100.Content = "Purchased Amt: " + reader["PurchasedAmount"].ToString();
                    CustomerPriceBS100.Foreground = new SolidColorBrush(Color.FromRgb(128, 0, 128));   // R G B
                    reader.Close();
                }
                else
                {
                    MessageBox.Show("Not Able to Display the Price Details of BS100 ");
                }

                //BS103
                String query1 = "SELECT OriginalAmount,PurchasedAmount FROM bs103 ORDER BY bs103_id DESC LIMIT 1";
                MySqlCommand sqlcmd1 = new MySqlCommand(query1, conn);
                MySqlDataReader reader1 = sqlcmd1.ExecuteReader();
                if (reader1.Read())
                {
                    //Show the fetched data in the labels
                    OriginalPriceBS103.Content = "Original Amt : " + reader1["OriginalAmount"].ToString();
                    OriginalPriceBS103.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));   // R G B

                    CustomerPriceBS103.Content = "Purchased Amt: " + reader1["PurchasedAmount"].ToString();
                    CustomerPriceBS103.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));   // R G B
                    reader1.Close();
                }
                else
                {
                    MessageBox.Show("Not Able to Display the Price Details of BS103 ");
                }

                //BS106
                String query2 = "SELECT OriginalAmount,PurchasedAmount FROM bs106 ORDER BY bs106_id DESC LIMIT 1";
                MySqlCommand sqlcmd2 = new MySqlCommand(query2, conn);
                MySqlDataReader reader2 = sqlcmd2.ExecuteReader();
                if (reader2.Read())
                {
                    //Show the fetched data in the labels
                    OriginalPriceBS106.Content = "Original Amt : " + reader2["OriginalAmount"].ToString();
                    OriginalPriceBS106.Foreground = new SolidColorBrush(Color.FromRgb(222, 184, 135));   // R G B

                    CustomerPriceBS106.Content = "Purchased Amt: " + reader2["PurchasedAmount"].ToString();
                    CustomerPriceBS106.Foreground = new SolidColorBrush(Color.FromRgb(222, 184, 135));   // R G B
                    reader2.Close();
                }
                else
                {
                    MessageBox.Show("Not Able to Display the Price Details of BS106 ");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Caught Displaying the Product Price: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        private void DisplayAbandonedFillDataGrid()
        {

            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                String query = "SELECT * FROM abandoned_checkout";
                MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlcmd);
                DataTable dt = new DataTable("Abandoned Checkout");
                adapter.Fill(dt);
                DisplayAbandoned.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors in Displaying Abandoned Checkout Table:" + ex);

            }
            finally
            {
                conn.Close();
            }

        }


        private void DisplayPurchasedFillDataGrid()
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
        //*************************************************************************************************************************************

        //Display the Order Number related search (Purchased Checkout)
        private void PurchasedFillDataGrid(object sender, RoutedEventArgs e)
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
                PurchasedFillDataGrid(sender, convert);

            }
        }

        //When Any text you Enter
        private void OrderNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            RoutedEventArgs convert = e;
            PurchasedFillDataGrid(sender, convert);
        }


        //Display Total Sales of all the 3 Products
        private string DisplayTotalSalesCheckout(string ProductName)
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            String query = "SELECT COUNT(*) FROM purchased_checkout WHERE PurchasedProductNamer = @ProductName";
            MySqlCommand sqlcmd = new MySqlCommand(query, conn);
            sqlcmd.Parameters.AddWithValue("@ProductName", ProductName);

            Int32 countSales = Convert.ToInt32(sqlcmd.ExecuteScalar());
            if (countSales > 0)
            {
                return Convert.ToString(countSales.ToString());
            }
            else
            {
                return "No Sales At All " + ProductName;
            }
        }

        //Display Total Abandoned Checkout of all the 3 Products
        private string DisplayTotalAbandonedCheckout(string ProductName)
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            String query = "SELECT COUNT(*) FROM abandoned_checkout WHERE ProductLeft = @ProductName";
            MySqlCommand sqlcmd = new MySqlCommand(query, conn);
            sqlcmd.Parameters.AddWithValue("@ProductName", ProductName);

            Int32 countAbandoned = Convert.ToInt32(sqlcmd.ExecuteScalar());
            if (countAbandoned > 0)
            {
                return Convert.ToString(countAbandoned.ToString());
            }
            else
            {
                return "No Abandoned Checkout At All " + ProductName;
            }
        }

        private void DisplayTotalSalesAbandonedCheckout()
        {
            try
            {
                //Total Sales of BS100
                BS100Box.Text = "Total Sales : " + DisplayTotalSalesCheckout("BS 100") + "\n";
                //Total Abandonded Checkout of BS100
                BS100Box.Text = BS100Box.Text + "Total Abandoned Checkout : " + DisplayTotalAbandonedCheckout("BS 100") + "\n";

                //Total Sales of BS103
                BS103Box.Text = "Total Sales : " + DisplayTotalSalesCheckout("BS 103") + "\n";
                //Total Abandonded Checkout of BS103
                BS103Box.Text = BS103Box.Text + "Total Abandoned Checkout : " + DisplayTotalAbandonedCheckout("BS 103") + "\n";

                //Total Sales of BS106
                BS106Box.Text = "Total Sales : " + DisplayTotalSalesCheckout("BS 106") + "\n";
                //Total Abandonded Checkout of BS106
                BS106Box.Text = BS106Box.Text + "Total Abandoned Checkout : " + DisplayTotalAbandonedCheckout("BS 106") + "\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception in Display Total Sales Abandoned Checkout :" + ex);
            }
            finally
            {
                conn.Close();
            }

        }

        //Total Sales Amount of all 3 Products
        private string DisplayTotalSales(string ProductName)
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            String query = "SELECT SUM(PurchasedAmountAfterDiscount) FROM purchased_checkout WHERE PurchasedProductNamer = @ProductName";
            MySqlCommand sqlcmd = new MySqlCommand(query, conn);
            sqlcmd.Parameters.AddWithValue("@ProductName", ProductName);

            Int32 TotalSales = Convert.ToInt32(sqlcmd.ExecuteScalar());
            if (TotalSales > 0)
            {
                return Convert.ToString(TotalSales.ToString());
            }
            else
            {
                return "No Sales for " + ProductName;
            }
        }

        private void DisplayTotalSalesForAllProducts()
        {
            try
            {
                //Total Sales Amount of BS100
                BS100Box.Text = BS100Box.Text + "Sales Amount : $" + DisplayTotalSales("BS 100") + " USD\n";

                //Total Sales Amount of BS103
                BS103Box.Text = BS103Box.Text + "Sales Amount : $" + DisplayTotalSales("BS 103") + " USD\n";

                //Total Sales Amount of BS106
                BS106Box.Text = BS106Box.Text + "Sales Amount : $" + DisplayTotalSales("BS 106") + " USD\n";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception in Display Total Sales Amount :" + ex);
            }
            finally
            {
                conn.Close();
            }

        }

        //Is each purchased product shipped or not
        private void IsProductShippedOrNot()
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                //SELECT OrderNumber,PurchasedProductNamer,TrackingID,ShipmentThrough FROM purchased_checkout WHERE Shipped IS NULL OR Shipped = ' ' AND TrackingID != ' ' AND ShipmentThrough != ' '
                String query = "SELECT OrderNumber,PurchasedProductNamer,TrackingID,ShipmentThrough FROM purchased_checkout WHERE Shipped IS NULL OR Shipped = ' '";
                MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = sqlcmd.ExecuteReader();

                //Data Structure to add list of orders numbers
                // We can Use the Map datastructure for mapping all the items
                List <string> OrderNumberList = new List<string>();
                List<string> PurchasedProductNamerList = new List<string>();
                List <string> TrackingIDList = new List<string>();
                List <string> ShipmentThroughList = new List<string>();

                while (reader.Read())
                {
                    //Show the fetched data in the 3 boxes
                    OrderNumberList.Add(reader["OrderNumber"].ToString());
                    PurchasedProductNamerList.Add(reader["PurchasedProductNamer"].ToString());
                    TrackingIDList.Add(reader["TrackingID"].ToString());
                    ShipmentThroughList.Add(reader["ShipmentThrough"].ToString());
                    
                    //reader.Close();
                }

                /*
                    OrderNumber   Product Name     TrackingID   ShipmentThrough
                    #1001           BS 100          xxxxxxx        DHL
                    #1002           BS 103          xxxxxxx        UPS
                    #1003           BS 106          xxxxxxx        Fedx
                 */
                OnGoingSales.Text = "Order Number" + "\t"+"Product Name"+"\t" +"Tracking ID" + "\t\t" + "Shipment Through\n";
                for(int items =0;items<OrderNumberList.Count();items++)
                {
                    OnGoingSales.Text = OnGoingSales.Text+OrderNumberList[items] + "\t\t"+ PurchasedProductNamerList[items]+"\t\t"+TrackingIDList[items] + "\t\t"+ ShipmentThroughList[items]+"\n";
                }
                /*
                foreach (string items in OrderNumberList)
                {
                    OnGoingSales.Text = items + "\n";
                }
              
                */
            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception Caught in the Displaying Shipped or Not in 3 boxes : "+ex);
            }
            finally
            {
                conn.Close();
            }

        }


        private void BackToEditDashboard_Click(object sender, RoutedEventArgs e)
        {
            Edit_Dashboard window = new Edit_Dashboard();
            window.Show();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            this.Close();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Display_Dashboard window = new Display_Dashboard();
            window.Show();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            this.Close();
        }

        private void BackToPurchasedCheckout_Click(object sender, RoutedEventArgs e)
        {
            Checkout window = new Checkout();
            window.Show();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            this.Close();
        }

        private void BackToAbandonedCheckout_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            window.Show();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            this.Close();
        }


        private void AddMember_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateMember_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteMember_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
