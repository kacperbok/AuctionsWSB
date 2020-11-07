using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for AuctionsStart.xaml
    /// </summary>
    public partial class AuctionsStart : Window
    {

        private static string _itemID;

        public static string ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        private string _itemName;

        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }

        private string _itemDescription;

        public string ItemDescription
        {
            get { return _itemDescription; }
            set { _itemDescription = value; }
        }

        private int _itemStartPrice;

        public int ItemStartPrice
        {
            get { return _itemStartPrice; }
            set { _itemStartPrice = value; }
        }

        private static int _itemMinimalPrice;

        public static int ItemMinimalPrice
        {
            get { return _itemMinimalPrice; }
            set { _itemMinimalPrice = value; }
        }

        private static int _itemSoldPrice;

        public static int ItemSoldPrice
        {
            get { return _itemSoldPrice; }
            set { _itemSoldPrice = value; }
        }

        private static string _itemSoldDate;

        public static string ItemSoldDate
        {
            get { return _itemSoldDate; }
            set { _itemSoldDate = value; }
        }

        private string _itemOwner;

        public string ItemOwner
        {
            get { return _itemOwner; }
            set { _itemOwner = value; }
        }


        private int _itemCurrentPrice;

        public int ItemCurrentPrice
        {
            get { return _itemCurrentPrice; }
            set { _itemCurrentPrice = value; }
        }

        private int _itemNewBid;

        public int ItemNewBid
        {
            get { return _itemNewBid; }
            set { _itemNewBid = value; }
        }

        private static string _biddingPerson;

        public static string BiddingPerson
        {
            get { return _biddingPerson; }
            set { _biddingPerson = value; }
        }

        private static int _bidCounter = 0;

        public static int BidCounter
        {
            get { return _bidCounter; }
            set { _bidCounter = value; }
        }

        private static bool _winFlag;

        public static bool WinFlag
        {
            get { return _winFlag; }
            set { _winFlag = value; }
        }

        private static int _itemNewOwnerId;

        public static int ItemNewOwnerId
        {
            get { return _itemNewOwnerId; }
            set { _itemNewOwnerId = value; }
        }

        private static string _itemNewOwner;

        public static string ItemNewOwner
        {
            get { return _itemNewOwner; }
            set { _itemNewOwner = value; }
        }



        public AuctionsStart()
        {
            InitializeComponent();
        }

        public AuctionsStart(string id)
        {
            InitializeComponent();
            ItemID = id;
            label_Data.Content = DateTime.Now.ToString();
            ReadItem();
            Connections.FillComboBox(Connections.customersFirstNameColumn, Connections.customersTableName, comboBox_licytujacy); 
        }

        public void ReadItem()
        {
            Connections.OpenSqlConnection();
            try
            {
                String query = "Select ITEM_NAME, ITEM_DESCRIPTION, ITEM_STARTPRICE, ITEM_MINIMALPRICE, ITEM_OWNER from TBL_ITEMS where ITEM_ID = '" + ItemID + "'";
                SqlCommand sqlCmd = new SqlCommand(query, Connections.DataBaseConnection);
                SqlDataReader Reader = sqlCmd.ExecuteReader();
                Reader.Read();

                ItemName = Reader.GetString(0);
                if (!string.IsNullOrEmpty(ItemName))
                {
                   label_Nazwa.Content = Reader.GetString(0);
                }

                ItemDescription = Reader.GetString(1);
                if (!string.IsNullOrEmpty(ItemDescription))
                {
                    label_Opis.Content = Reader.GetString(1);
                }

                ItemStartPrice = Reader.GetInt32(2);
                if (String.IsNullOrEmpty(ItemStartPrice.ToString()))
                {
                    label_CenaWywolawcza.Content = 0;
                }
                else
                {
                    label_CenaWywolawcza.Content = ItemStartPrice;
                    textbox_Oferta.Text = ItemStartPrice.ToString();
                }

                ItemOwner = Reader.GetString(4);
                if (!string.IsNullOrEmpty(ItemOwner))
                {
                    label_Wlasciciel.Content = Reader.GetString(4);
                }

                ItemMinimalPrice = Reader.GetInt32(3);   
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                throw;
            }     
        }

        private void button_AuctionsStartWyjscie(object sender, RoutedEventArgs e)
        {
            AuctionsMain newWindow = new AuctionsMain();
            this.Close();
            newWindow.Show();
        }

        
        private void button_ZlozOferte(object sender, RoutedEventArgs e)
        {
            int.TryParse(textbox_Oferta.Text, out _itemNewBid);

            if (_itemNewBid > ItemStartPrice && _itemNewBid > ItemCurrentPrice)
            {
                if (comboBox_licytujacy.SelectedItem != null)
                {
                    ItemCurrentPrice = _itemNewBid;
                    label_obecnaCena.Content = _itemNewBid.ToString();
                    BiddingPerson = comboBox_licytujacy.Text;
                    if (BiddingPerson == label_najwyzszaOferta.Content)
                    {
                        BidCounter++;
                        SetNewSoldPrice();
                        CheckSetWinner();
                        if (WinFlag == true)
                        {              
                            Thread.Sleep(2000);
                            AuctionsMain newWindow = new AuctionsMain();
                            this.Close();
                            newWindow.Show();
                        }
                    }
                    else
                    {
                        BidCounter = 0;
                    }
                    label_najwyzszaOferta.Content = BiddingPerson;
                }
                else
                {
                    System.Windows.MessageBox.Show("Proszę wybrać licytującego");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Oferta nie może być niższa od ceny początkowej lub od aktualnej oferty");
            }
        }

        public  void SetNewSoldPrice()
        {
            ItemSoldPrice = ItemNewBid;
        }
          private static void CheckSetWinner()
        {
            if (BidCounter == 2)
            {
                if (ItemSoldPrice >= ItemMinimalPrice)
                {
                    System.Windows.MessageBox.Show("Aukcja wygrana przez " + BiddingPerson);
                    ItemNewOwnerId = Connections.GetCustomerID(BiddingPerson);

                    Connections.MarkAsSold(ItemID);
                    Connections.OwnerUpdate(BiddingPerson, ItemID);
                    Connections.ItemOwnerIDUpdate(ItemID, ItemNewOwnerId);
                    ItemSoldDate = DateTime.Now.ToString();
                    Connections.InsertSoldDate(ItemID, ItemSoldDate);
                    Connections.InsertSoldPrice(ItemID, ItemSoldPrice);
                    WinFlag = true;
                    BidCounter = 0;
                }
                else
                {
                    System.Windows.MessageBox.Show("Nie osiągnięto ceny minimalnej.");
                }
            }
        }
    }
}
