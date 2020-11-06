using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctions
{
    public class Customer
    {

		public Customer(string firstName, string lastName, string address, string dateOfBirth)
		{
			FirstName = firstName;
			Lastname = lastName;
			Address = address;
			DateOfBirth = dateOfBirth;
		}

		private int _id;

		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private string _firstName;

		public string FirstName
		{
			get { return _firstName; }
			set { _firstName = value; }
		}

		private string _lastname;

		public string Lastname
		{
			get { return _lastname; }
			set { _lastname = value; }
		}

		private string _address;

		public string Address
		{
			get { return _address; }
			set { _address = value; }
		}

		private string _dateOfBirth;

		public string DateOfBirth
		{
			get { return _dateOfBirth; }
			set { _dateOfBirth = value; }
		}


	}
}
