using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for AuctionsMain.xaml
    /// </summary>
    public partial class AuctionsMain : Window
    {
        private string _itemID;

        public string ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        public AuctionsMain()
        {
            InitializeComponent();
            Connections.LoadTableToDataGrid(Connections.loadItemsTableQuery, AuctionsMain_dataGrid, Connections.itemsTableName, "WHERE ITEM_ISSOLD = '0' OR ITEM_ISSOLD is null");
        }

        private void AuctionsMain_dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView dataRow = (DataRowView)AuctionsMain_dataGrid.SelectedItem;
            if (System.Windows.MessageBox.Show("Czy chcesz rozpocząć aukcje?", "Question", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Warning) == System.Windows.MessageBoxResult.Yes)
            {
                try
                {
                    int index = AuctionsMain_dataGrid.CurrentCell.Column.DisplayIndex;
                    ItemID = dataRow.Row.ItemArray[0].ToString();
                    AuctionsStart newWindow = new AuctionsStart(ItemID);
                    this.Close();
                    newWindow.Show();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
            else { }           
        }

        private void buttonClick_AuctionsMainWyjscie(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            this.Close();
            newWindow.Show();
        }
    }
}
