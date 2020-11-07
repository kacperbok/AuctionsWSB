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
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        public AddItem()
        {
            InitializeComponent();
            Connections.LoadTableToDataGrid(Connections.loadItemsTableQuery, dataGridAddItem, Connections.itemsTableName);
            Connections.FillComboBox(Connections.customersFirstNameColumn, Connections.customersTableName, comboBox_Wlasciciel);
        }
        private void button_AddItemWyjscie(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void buttonClick_AddItemDodaj(object sender, RoutedEventArgs e)
        {
            Item item = new Item(textBox_Nazwa.Text, textBox_Opis.Text, textBox_CenaWywolawcza.Text, textBox_CenaMinimalna.Text, comboBox_Wlasciciel.Text);
            Connections.InsertToItemsTable(item);
            Connections.LoadTableToDataGrid(Connections.loadItemsTableQuery, dataGridAddItem, Connections.itemsTableName);
        }
    }
}
