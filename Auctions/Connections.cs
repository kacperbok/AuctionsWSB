using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Auctions
{
    class Connections
    {
        public static readonly string loadCustomersTableQuery = "Select * from TBL_CUSTOMERS";
        public static readonly string loadItemsTableQuery = "Select * from TBL_ITEMS";
        public static readonly string connectionString = "MultipleActiveResultSets=True;" + Properties.Settings.Default.AuctionsDataBaseConnectionString;
        public static readonly string customersTableName = "TBL_CUSTOMERS";
        public static readonly string itemsTableName = "TBL_ITEMS";
        public static readonly string customersFirstNameColumn = "CUSTOMER_FIRSTNAME";
        public static SqlConnection DataBaseConnection = new SqlConnection(connectionString);



        public static void OpenSqlConnection()
        {
            if (DataBaseConnection.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    DataBaseConnection.Open();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex + "");
                    throw ex;
                }
            }
        }

        public static void LoadTableToDataGrid(string query, System.Windows.Controls.DataGrid gridName, string tableName)
        {
            OpenSqlConnection();

            SqlCommand SqlCmd = new SqlCommand(query, DataBaseConnection);
            SqlDataAdapter Adapter = new SqlDataAdapter(SqlCmd);
            DataTable Table = new DataTable(tableName);
            try
            {
                Adapter.Update(Table);
                Adapter.Fill(Table);
                gridName.ItemsSource = Table.DefaultView;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex + "");
                throw ex;
            }
            finally
            {
                DataBaseConnection.Close();
            }
        }

        public static void LoadTableToDataGrid(string query, System.Windows.Controls.DataGrid gridName, string tableName, string exception)
        {
            OpenSqlConnection();

            string newQuery = query + " " + exception;
            SqlCommand SqlCmd = new SqlCommand(newQuery, DataBaseConnection);
            SqlDataAdapter Adapter = new SqlDataAdapter(SqlCmd);
            DataTable Table = new DataTable(tableName);
            try
            {
                Adapter.Update(Table);
                Adapter.Fill(Table);
                gridName.ItemsSource = Table.DefaultView;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex + "");
                throw ex;
            }
            finally
            {
                DataBaseConnection.Close();
            }
        }
        public static void InsertToCustomersTable(Customer customer)
        {
            OpenSqlConnection();

            string query = "INSERT INTO TBL_CUSTOMERS (CUSTOMER_FIRSTNAME,CUSTOMER_LASTNAME,CUSTOMER_ADDRESS,CUSTOMER_BIRTHDAYDATE) VALUES (@firstname, @lastname, @address, @date)";
            SqlCommand SqlCmd = new SqlCommand(query, DataBaseConnection);


            if (string.IsNullOrEmpty(customer.FirstName))
            {
                System.Windows.MessageBox.Show("Wprowadź imię");
            }
            else
            {
                SqlCmd.Parameters.AddWithValue("@firstname", customer.FirstName);
            }

            if (string.IsNullOrEmpty(customer.Lastname))
            {
                System.Windows.MessageBox.Show("Wprowadź nazwisko");
            }
            else
            {
                SqlCmd.Parameters.AddWithValue("@lastname", customer.Lastname);
            }

            if (string.IsNullOrEmpty(customer.Address))
            {
                System.Windows.MessageBox.Show("Wprowadź adres");
            }
            else
            {
                SqlCmd.Parameters.AddWithValue("@address", customer.Address);
            }

            if (string.IsNullOrEmpty(customer.DateOfBirth))
            {
                System.Windows.MessageBox.Show("Wprowadź date urodzenia");
            }
            else
            {
                SqlCmd.Parameters.AddWithValue("@date", customer.DateOfBirth);
            }

            if (!string.IsNullOrEmpty(customer.FirstName) && !string.IsNullOrEmpty(customer.Lastname) && !string.IsNullOrEmpty(customer.Address) && !string.IsNullOrEmpty(customer.DateOfBirth))
            {
                try
                {
                    SqlCmd.ExecuteNonQuery();
                    System.Windows.MessageBox.Show("Pomyślnie dodano");

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex + "");
                    throw;
                }
                finally
                {
                    DataBaseConnection.Close();
                }         
            }
        }

        public static void InsertToItemsTable(Item item)
        {
            OpenSqlConnection();

            string query = "INSERT INTO TBL_ITEMS (ITEM_NAME, ITEM_DESCRIPTION,ITEM_STARTPRICE,ITEM_MINIMALPRICE,ITEM_OWNER, ITEM_CUSTOMER_ID, ITEM_ISSOLD) VALUES (@itemname, @description, @startprice, @minimalprice, @owner, @ownerid, 0)";
            SqlCommand SqlCmd = new SqlCommand(query, DataBaseConnection);


            if (string.IsNullOrEmpty(item.Name))
            {
                System.Windows.MessageBox.Show("Wprowadź nazwę przedmiotu");
            }
            else
            {
                SqlCmd.Parameters.AddWithValue("@itemname", item.Name);
            }

            if (string.IsNullOrEmpty(item.Description))
            {
                System.Windows.MessageBox.Show("Wprowadź opis przedmiotu");
            }
            else
            {
                SqlCmd.Parameters.AddWithValue("@description", item.Description);
            }

            if (string.IsNullOrEmpty(item.Owner))
            {
                System.Windows.MessageBox.Show("Wprowadź dane właściciela");
            }
            else
            {
                SqlCmd.Parameters.AddWithValue("@owner", item.Owner);
                SqlCmd.Parameters.AddWithValue("@ownerid", Connections.GetCustomerID(item));
            }

            SqlCmd.Parameters.AddWithValue("@minimalprice", item.MinimalPrice);
            SqlCmd.Parameters.AddWithValue("@startprice", item.StartPrice);

            if (!string.IsNullOrEmpty(item.Description) && !string.IsNullOrEmpty(item.Owner) && !string.IsNullOrEmpty(item.Name))
            {
                try
                {
                    SqlCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("" + ex);
                    throw;
                }
                finally
                {
                    DataBaseConnection.Close();
                }
            }                      
    }
            
   
        public static void FillComboBox(string columnName, string tableName, System.Windows.Controls.ComboBox comboBoxName)
        {
            OpenSqlConnection();

            try
            {
                comboBoxName.Items.Clear();
                string query = "SELECT " + columnName + " FROM " + tableName;
                SqlCommand SqlCmd = new SqlCommand(query, DataBaseConnection);
                SqlDataAdapter Adapter = new SqlDataAdapter(SqlCmd);
                DataTable Table = new DataTable(tableName);
                Adapter.Fill(Table);

                foreach (DataRow dataRow in Table.Rows)
                {
                    comboBoxName.Items.Add(dataRow[columnName].ToString());
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                DataBaseConnection.Close();
            }
        }
        
        public static int GetCustomerID(Item item)
        {
            OpenSqlConnection();

            try
            {
                string query = "SELECT CUSTOMER_ID FROM TBL_CUSTOMERS WHERE CUSTOMER_FIRSTNAME = '" + item.Owner + "'";
                SqlCommand SqlCmd = new SqlCommand(query, DataBaseConnection);
                SqlDataReader Reader = SqlCmd.ExecuteReader();
                Reader.Read();
                int itemCustomerID = Reader.GetInt32(0);
                return itemCustomerID;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                DataBaseConnection.Close();
            }    
        }

        public static int GetCustomerID(Customer customer)
        {
            OpenSqlConnection();

            try
            {
                string query = "SELECT CUSTOMER_ID FROM TBL_CUSTOMERS WHERE CUSTOMER_FIRSTNAME = '" + customer.FirstName + "'";
                SqlCommand SqlCmd = new SqlCommand(query, DataBaseConnection);
                SqlDataReader Reader = SqlCmd.ExecuteReader();
                Reader.Read();
                int customerID = Reader.GetInt32(0);
                return customerID;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                DataBaseConnection.Close();
            }          
        }
        public static int GetCustomerID(string customer)
        {
            OpenSqlConnection();

            try
            {
                string query = "SELECT CUSTOMER_ID FROM TBL_CUSTOMERS WHERE CUSTOMER_FIRSTNAME = '" + customer + "'";
                SqlCommand SqlCmd = new SqlCommand(query, DataBaseConnection);
                SqlDataReader Reader = SqlCmd.ExecuteReader();
                Reader.Read();
                int customerID = Reader.GetInt32(0);
                return customerID;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                DataBaseConnection.Close();
            }       
        }
        public static void OwnerUpdate(string owner, string itemID)
        {
            OpenSqlConnection();

            try
            {
                string query = "UPDATE TBL_ITEMS SET ITEM_OWNER = '" + owner + "' WHERE ITEM_ID = '" + itemID + "'";
                SqlCommand SqlCmd = new SqlCommand(query, DataBaseConnection);
                SqlDataAdapter Adapter = new SqlDataAdapter(SqlCmd);
                DataTable Table = new DataTable(itemsTableName);
                SqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                DataBaseConnection.Close();
            }     
        }
        public static void MarkAsSold(string itemID)
        {
            OpenSqlConnection();

            try
            {
                string query = "UPDATE TBL_ITEMS SET ITEM_ISSOLD = 1 WHERE ITEM_ID = '" + itemID + "'";
                SqlCommand SqlCmd = new SqlCommand(query, DataBaseConnection);
                SqlDataAdapter Adapter = new SqlDataAdapter(SqlCmd);
                DataTable Table = new DataTable(itemsTableName);
                SqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                DataBaseConnection.Close();
            }   
        }
        public static void ItemOwnerIDUpdate(string itemID, int newownerID)
        {
            OpenSqlConnection();

            try
            {
                string query = "UPDATE TBL_ITEMS SET ITEM_CUSTOMER_ID = '" + newownerID + "' WHERE ITEM_ID = '" + itemID + "'";
                SqlCommand SqlCmd = new SqlCommand(query, DataBaseConnection);
                SqlDataAdapter Adapter = new SqlDataAdapter(SqlCmd);
                DataTable Table = new DataTable(itemsTableName);
                SqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                DataBaseConnection.Close();
            }
        }

        public static void InsertSoldDate(string itemID, string date)
        {
            OpenSqlConnection();

            try
            {
                string query = "UPDATE TBL_ITEMS SET ITEM_SOLDDATE = '" + date + "' WHERE ITEM_ID = '" + itemID + "'";
                SqlCommand SqlCmd = new SqlCommand(query, DataBaseConnection);
                SqlDataAdapter Adapter = new SqlDataAdapter(SqlCmd);
                DataTable Table = new DataTable(itemsTableName);
                SqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                DataBaseConnection.Close();
            }
        }

        public static void InsertSoldPrice(string itemID, int price)
        {
            OpenSqlConnection();

            try
            {
                string query = "UPDATE TBL_ITEMS SET ITEM_SOLDPRICE = '" + price + "' WHERE ITEM_ID = '" + itemID + "'";
                SqlCommand SqlCmd = new SqlCommand(query, DataBaseConnection);
                SqlDataAdapter Adapter = new SqlDataAdapter(SqlCmd);
                DataTable Table = new DataTable(itemsTableName);
                SqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                DataBaseConnection.Close();
            }
        }


    }
}
