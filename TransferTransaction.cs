using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    class TransferTransaction : Transaction
    {
        //instance variables
        Account _fromAccount;
        Account _toAccount;
        DepositTransaction _deposit;
        WithdrawTransaction _withdraw;
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
                if (_deposit.Success && _withdraw.Success)
                {
                    return true;
                }

                return false;
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
        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;

            //Class objects for deposit and withdraw transaction
            DepositTransaction deposit = new DepositTransaction(_toAccount, _amount);
            WithdrawTransaction withdraw = new WithdrawTransaction(_fromAccount, _amount);

            _deposit = deposit;
            _withdraw = withdraw;
        }

        //Method to print the transfer transaction's details 
        override public void Print()
        {
            //printing when transfer operation is completed
            if (Success)
            {
                Console.Write("Transferred {0} from {1}'s account to {2}'s account", _amount.ToString("C"), _fromAccount.Name, _toAccount.Name);
            }
            else
            {
                Console.Write("Deposit transaction is not successfull");
            }

        }

        //Method to execute the transfer transaction
        override public void Execute()
        {
            base.Execute();

            //throwing exception if already executed
            if (_executed)
            {
                throw new InvalidOperationException("The transfer transaction has been already attempted");
            }

            _executed = true;

            //withdrawing the amount from _fromAccount first
            _withdraw.Execute();

            //if amount is withdrawn successfully
            if(_withdraw.Success)
            {
                //Depositing the amount to _toAccount 
                _deposit.Execute();

                //if deposit is not succesfull, then rollback the withdraw
                if(! _deposit.Success)
                {
                    _withdraw.Rollback();
                }
            }

            
        }

        //Method to perform reversal of preceding transfer transaction
        override public void Rollback()
        {
            base.Rollback();

            //throwing exception if transaction is not finalized
            if (! Success)
            {
                throw new InvalidOperationException("Transfer transaction has not been completed");
            }

            //throwing exception if already reversed 
            if (_reversed)
            {
                throw new InvalidOperationException("Transfer transaction has been already reversed");
            }
           
            _reversed = true;

            //Depositing the funds back into the _fromAccount
            _withdraw.Rollback();

            //Withdrawing the funds back from the _toAccount
            _deposit.Rollback();

            
            

        }


    }
}
