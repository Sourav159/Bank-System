using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    class Account
    {
        //Instance variables
        private decimal _balance;
        private string _name;

        //Read Only Property for _name
        public string Name
        {
            get
            { 
                return _name;
            }

        }

        public decimal Balance
        {
            get
            {
                return _balance;
            }
        }

        //Constructor
         public Account(string name, decimal balance)
        {
            _name = name;
            _balance = balance;
        }

        //Methods
        //Method to add funds to the account
        public bool Deposit (decimal amount)
        {
            if(amount > 0)
            {
                _balance += amount;
                return true;
            }

            else
            {
                return false;
            }
            
        }

        //Method to withdraw funds from the account
        public bool Withdraw(decimal amount)
        {
            if(amount <= _balance && amount > 0)
            {
                _balance -= amount;
                return true;
            }

            else
            {
                return false;
            }
            
        }

        //Method to print the contents to terminal
        public void Print()
        {
            Console.WriteLine("Name: " + _name + "\nBalance: " + _balance.ToString("C"));
        }








    }
}
