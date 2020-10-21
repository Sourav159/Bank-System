using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    //Bank class
    class Bank
    {
        //Instance variable
        List<Transaction> _transactions;
        List<Account> _accounts;

        //Read-only property for _transactions
        public List<Transaction> Transactions
        {
            get
            {
                return _transactions;
            }
        }

        //Constructor
        
        public Bank()
        {
            _transactions = new List<Transaction>();
            _accounts = new List<Account>();
        }

        //Methods     
        //Method to add an account into the _accounts list
        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        //Method to return the account for the given name
        public Account GetAccount(String name)
        {
            for (int i = 0; i < _accounts.Count; i++)
            {
                if (_accounts[i].Name == name)
                    return _accounts[i];
            }

            return null;
        }

        //Method to execute the transaction and print the result
        public void ExecuteTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);

            transaction.Execute();

            transaction.Print();

        }

        //Method to rollback the transaction
        public void RollbackTransaction(Transaction transaction)
        {

            transaction.Rollback();

        }

        //Method to print the transaction history
        public void PrintTransactionHistory()
        {
            for(int i = 0; i < _transactions.Count; i++)
            {
                Console.Write(i+1 + ": ");

                _transactions[i].Print();

                Console.WriteLine(" - " + _transactions[i].DateStamp);
            }

            
        }
       
    }
}
