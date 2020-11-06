using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctions
{
   public class Item
    {

        public Item(string name, string description, string startPrice, string minimalPrice, string owner)
        {
            Name = name;
            Description = description;
            int.TryParse(startPrice, out _startPrice);
            StartPrice = _startPrice;
            int.TryParse(minimalPrice, out _minimalPrice);
            MinimalPrice = _minimalPrice;
            Owner = owner;      
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private int _startPrice;

        public int StartPrice
        {
            get { return _startPrice; }
            set { _startPrice = value; }
        }

        private int _minimalPrice;

        public int MinimalPrice
        {
            get { return _minimalPrice; }
            set { _minimalPrice = value; }
        }

        private int _soldPrice;

        public int SoldPrice
        {
            get { return _soldPrice; }
            set { _soldPrice = value; }
        }

        private string _soldDate;

        public string SoldDate
        {
            get { return _soldDate; }
            set { _soldDate = value; }
        }

        private string _owner;

        public string Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }




    }
}
