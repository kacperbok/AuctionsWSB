using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctions
{
    class Auction
    {
        private string _startDate;

        public string StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private string _endDate;

        public string EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        private Array _itemsList;

        public Array ItemsList
        {
            get { return _itemsList; }
            set { _itemsList = value; }
        }

        private Array _customersList;

        public Array CustomersList
        {
            get { return _customersList; }
            set { _customersList = value; }
        }

        private int _itemID;

        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }



        public Auction(int id)
        {
            ItemID = id;
        }


    }
}
