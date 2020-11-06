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

namespace Auctions
{
    /// <summary>
    /// Interaction logic for AddCustomers.xaml
    /// </summary>
    public partial class AddCustomers : Window
    {
        public AddCustomers()
        {
            InitializeComponent();
            // Auto DataGridUpdate
            Connections.LoadTableToDataGrid(Connections.loadCustomersTableQuery, dataGridAddCustomer, Connections.customersTableName);
        }

       
        private void buttonClick_AddCustomersWyjscie(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();    
            this.Close();
            newWindow.Show();
        }


        private void buttonClick_AddCustomersUpdate(object sender, RoutedEventArgs e)
        {
            Connections.LoadTableToDataGrid(Connections.loadCustomersTableQuery, dataGridAddCustomer, Connections.customersTableName);
        }

        private void buttonClick_AddCustomersDodaj(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer(textBox_Imie.Text, textBox_Nazwisko.Text, textBox_Adres.Text, dataPicker_DataUrodzenia.Text);
            Connections.InsertToCustomersTable(customer);
            Connections.LoadTableToDataGrid(Connections.loadCustomersTableQuery, dataGridAddCustomer, Connections.customersTableName);
        }


    }
}
