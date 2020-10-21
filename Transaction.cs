using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    abstract class Transaction
    {
        //Instance variables
        protected decimal _amount;
        protected bool _success;
        private bool _executed, _reversed;
        private DateTime _dateStamp;

        //Properties
        abstract public bool Success
        {
            get;
        }

        public bool Executed
        {
            get
            {
                return _executed;
            }
        }

        public bool Reversed
        {
            get
            {
                return _reversed;
            }
        }

        public DateTime DateStamp
        {
            get
            {
                return _dateStamp;
            }
        }

        //Methods
        //Constructor
        public Transaction(decimal amount)
        {
            _amount = amount;
        }

        abstract public void Print();

        virtual public void Execute()
        {
            _dateStamp = DateTime.Now;
        }

        virtual public void Rollback()
        {
            _dateStamp = DateTime.Now;
        }










    }
}
