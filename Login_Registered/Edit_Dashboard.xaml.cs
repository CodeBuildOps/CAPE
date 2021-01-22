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
    /// Interaction logic for Edit_Dashboard.xaml
    /// </summary>
    public partial class Edit_Dashboard : Window
    {
        MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;database=wpf;username=root;password=");
        public Edit_Dashboard()
        {
            InitializeComponent();
            try
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    status.Content = "Connected";
                    status.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    //Calling the function to display the grid
                    //FillDataGrid();
                }
                else
                {
                    status.Content = "Something wrong, Refresh";
                    status.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 255));
                }

            }
            catch (Exception ex)
            {
                status.Content = "Not-Connected, Refresh";
                status.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
        }

        // Here, we have to return the RadioButton object name, i,e whichone, but how we have to find that.
        //One way is to make a variable and store it's content.
        private string SelectedProductName = null;
        private void BSCheck(object sender, RoutedEventArgs e)
        {
            RadioButton whichone = sender as RadioButton;
            SelectedProductName = whichone.Content + "";
            MessageBox.Show(SelectedProductName);

            
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
            
        }
        private void ClearProductDetailsEntries()
        {
            ProductVariant.Text = "";
            OriginalAmount.Text = "";
            PurchasedAmount.Text = "";
        }
        private void ClearTrackingDetailsEntries()
        {
            //For tacking ID
            OrderNumber.Text = "";
            SearchedText.Text = "";
            TrackingServiceName.Text = "";
            TrackingID.Text = "";

        }

        private void SubmitChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }


                if (ProductVariant.Text != "" && OriginalAmount.Text != "" && PurchasedAmount.Text != "")
                {
                    String query = "INSERT INTO bs100 (ProductVariant,OriginalAmount,PurchasedAmount) VALUES ('" + ProductVariant.Text + "'," + "'" + OriginalAmount.Text + "','" + PurchasedAmount.Text + "')";

                    MySqlCommand sqlcmd = new MySqlCommand(query, conn);
                    if (sqlcmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("BS 100 Inserted Successfully \nColor: " + ProductVariant.Text + "\nOriginal Amount: " + OriginalAmount.Text + "\nPurchased Amount: " + PurchasedAmount.Text);
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

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Edit_Dashboard mainWindow = new Edit_Dashboard();
            mainWindow.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Edit_Dashboard Window = new Edit_Dashboard();
            Window.Show();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            this.Close();
        }

        

        private void SubmitTracking_Click(object sender, RoutedEventArgs e)
        {

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
