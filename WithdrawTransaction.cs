using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    class WithdrawTransaction : Transaction
    {
        //Instance variables
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
        public WithdrawTransaction (Account account, decimal amount) : base(amount)
        {
            _account = account;          
        }

        

        //Method to print the withdraw transaction's details 
         override public void Print()
        {
            //printing when withdraw operation is completed
            if (_success) 
            {
                Console.Write(_amount.ToString("C") + " withdrawn successfuly" );
            }
            else 
            {
                Console.Write("Withdraw transaction is not successfull");
            }
                
        }

        //Method to execute the withdraw transaction
        override public void Execute()
        {
            base.Execute();

            //throwing exception if already executed
            if(_executed)
            {
                throw new InvalidOperationException("The withdraw transaction has been already attempted");
            }

            //throwing exeption if insufficient funds
            if(_amount > _account.Balance)
            {
                throw new InvalidOperationException("Insufficient funds");
            }

            _executed = true;

            //Deducting the _amount from _account
            _success = _account.Withdraw(_amount);
            
        }

        //Method to perform reversal of preceding withdraw transaction
        override public void Rollback()
        {
            base.Rollback();

            //throwing exception if transaction is not finalized
            if(! _success)
            {
                throw new InvalidOperationException("Withdraw transaction has not been finalized");
            }

            //throwing exception if already reversed 
            if(_reversed)
            {
                throw new InvalidOperationException("Withdraw transaction has been already reversed");
            }

            //Depositing the funds back into the _account
            _reversed = _account.Deposit(_amount);

        }

    }
}
