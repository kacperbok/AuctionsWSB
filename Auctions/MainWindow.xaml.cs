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

namespace Auctions
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonClick_MainWindowDodajKlienta(object sender, RoutedEventArgs e)
        {
            AddCustomers newWindow = new AddCustomers();
            this.Close();
            newWindow.Show();
        }

        private void buttonClick_MainWindowDodajPrzedmiot(object sender, RoutedEventArgs e)
        {
            AddItem newWindow = new AddItem();
            this.Close();
            newWindow.Show();
        }

        private void buttonClick_MainWindowRozpocznijAukcje(object sender, RoutedEventArgs e)
        {
            AuctionsMain newWindow = new AuctionsMain();
            this.Close();
            newWindow.Show();

        }

        private void buttonClick_MainWindowWyjscie(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
