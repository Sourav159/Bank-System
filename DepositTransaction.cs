using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    class DepositTransaction : Transaction
    {
        //instance variables
        Account _account;
        bool _executed;
        bool _reversed;

        //Properties
        new public bool Executed
        {
            get
            {
                return _executed;
            }

        }

        override public bool Success
        {
            get
            {
                return _success;
            }

        }

        new public bool Reversed
        {
            get
            {
                return _reversed;
            }
        }

        //Methods
        //Constructor
        public DepositTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
        }

        //Method to print the deposit transaction's details 
        override public void Print()
        {
            //printing when deposit operation is completed
            if (_success)
            {
                Console.Write(_amount.ToString("C") + " deposited successfuly");
            }
            else
            {
                Console.Write("Deposit transaction is not successfull");
            }

        }

        //Method to execute the deposit transaction
        override public void Execute()
        {
            base.Execute();

            //throwing exception if already executed
            if (_executed)
            {
                throw new InvalidOperationException("The deposit transaction has been already attempted");
            }

            //throwing exeption if insufficient funds
            if (_amount < 0)
            {
                throw new InvalidOperationException("Invalid deposit amount!");
            }

            _executed = true;

            //Depositing the _amount into _account
            _success = _account.Deposit(_amount);
            
            
        }

        //Method to perform reversal of preceding withdraw transaction
        override public void Rollback()
        {
            base.Rollback();

            //throwing exception if transaction is not finalized
            if (!_success)
            {
                throw new InvalidOperationException("Deposit transaction has not been finalized");
            }

            //throwing exception if already reversed 
            if (_reversed)
            {
                throw new InvalidOperationException("Deposit transaction has been already reversed");
            }

            //Withdrawing the funds back from the _account
            _reversed = _account.Withdraw(_amount);

        }
    }
}
