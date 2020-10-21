using System;

namespace BankSystem
{
    //Enumeration
    enum MenuOption
    {
        ADDACCOUNT,
        WITHDRAW,
        DEPOSIT,
        PRINT,
        TRANSFER,
        TRANSACTIONHISTORY,
        QUIT
    };
    
    class BankSystem
    {
        //Method to show the menu to the user and accept an option
        public static MenuOption ReadUserOption()
        {
            int option;

            Console.WriteLine("\n\n1. Add new account ");
            Console.WriteLine("2. Withdraw ");
            Console.WriteLine("3. Deposit ");
            Console.WriteLine("4. Print ");
            Console.WriteLine("5. Transfer between two accounts ");
            Console.WriteLine("6. Print numbered transaction history ");
            Console.WriteLine("7. Quit ");

            Console.Write("Please choose an option: ");
            option = Convert.ToInt32(Console.ReadLine());

            //returning the option by casting it in the enum menu option
            return (MenuOption) (option - 1);
        }

        //Method to perform a deposit operation
        public static void DoDeposit(Bank bank)
        {
            try
            {
                Account acc = FindAccount(bank);
                
                //If account is found in the FindAccount method, then do the deposit
                if(acc != null)
                {
                    decimal depositAmount;

                    Console.Write("Please enter the amount to deposit: ");
                    depositAmount = Convert.ToDecimal(Console.ReadLine());

                    DepositTransaction transaction = new DepositTransaction(acc, depositAmount);

                    bank.ExecuteTransaction(transaction);
                    

                }                
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error! : " + e.Message);
            }

        }

        //Method to perform a withdraw operation
        public static void DoWithdraw(Bank bank)
        {
            try
            {
                Account acc = FindAccount(bank);

                //If account is found in the FindAccount method, then do the withdraw
                if (acc != null)
                {
                    decimal withdrawAmount;

                    Console.Write("Please enter the amount to withdraw: ");
                    withdrawAmount = Convert.ToDecimal(Console.ReadLine());

                    WithdrawTransaction transaction = new WithdrawTransaction(acc, withdrawAmount);

                    bank.ExecuteTransaction(transaction);

                }
                                                            
            }
            catch(Exception e)
            {
                Console.WriteLine("Error! : " + e.Message);
            }
        }

        //Method to perform a trasaction operation
        public static void DoTransaction(Bank bank)
        {
            try
            {
                Console.WriteLine("\n.....Debit.....");
                Account acc1 = FindAccount(bank);

                //If account1 is found in the FindAccount method, then ask the amount and account2            
                if (acc1 != null)
                {
                    decimal transactionAmount;
                    Console.Write("Please enter the amount to do transaction: ");
                    transactionAmount = Convert.ToDecimal(Console.ReadLine());

                    Console.WriteLine("\n.....Credit.....");
                    Account acc2 = FindAccount(bank);

                    //If account2 is found in the FindAccount method, then do the transfer transaction
                    if (acc2 != null)
                    {
                        TransferTransaction transaction = new TransferTransaction(acc1, acc2, transactionAmount);

                        bank.ExecuteTransaction(transaction);
                    }
                    
                }
                                  
            }
            catch (Exception e)
            {
                Console.WriteLine("Error! : " + e.Message);
            }

        }

        //Method to print the details of the account
        public static void DoPrint(Bank bank)
        {
            Account acc = FindAccount(bank);

            //If account is found in the FindAccount method, then print the contents of the account
            if (acc != null)
            {
                acc.Print();

            }

        }

        //Method to add a new account into the bank
        public static void DoAddAccount(Bank bank)
        {
            try
            {
                Console.Write("Please enter the account name: ");
                string accountName = Console.ReadLine();

                Console.Write("Please enter the starting balance: ");
                decimal balanceAmount = Convert.ToDecimal(Console.ReadLine());

                Account acc = new Account(accountName, balanceAmount);

                bank.AddAccount(acc);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error! : " + e.Message);
            }

        }

        //Method to rollback a trasaction for the bank
        public static void DoRollback(Bank bank)
        {
            try
            {
                Console.Write("\nDo you wish to rollback a specific transaction? (y/n): ");
                string ans = Console.ReadLine();

                if (ans.ToLower() == "y" || ans.ToLower() == "yes")
                {
                    Console.Write("Please enter the transaction number: ");
                    int position = Convert.ToInt32(Console.ReadLine());

                    //Transaction to rollback
                    Transaction transaction = bank.Transactions[position - 1];

                    bank.RollbackTransaction(transaction);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Error! : " + e.Message);
            }

        }

        //Private method to find the account from the accounts list
        private static Account FindAccount(Bank bank)
        {
            Console.Write("Please enter the account name: ");
            string name = Console.ReadLine();

            Account acc = bank.GetAccount(name);

            if(acc == null)
            {
                Console.WriteLine("Account not found!");
            }

            return acc;

        }
        static void Main(string[] args)
        {
            try
            {
                //Instance of the MenuOption enum
                MenuOption option;

                //Instance of the Bank class
                Bank bank = new Bank();
                
                //Do-While loop to continuously display the menu, until 4 (Quit) is entered
                //and to perform the functionality based on the option entred
                do
                {

                    option = ReadUserOption();

                    Console.WriteLine("\n" + option);

                    switch (option)
                    {
                        case MenuOption.ADDACCOUNT:
                            DoAddAccount(bank);
                            break;

                        case MenuOption.WITHDRAW:
                            DoWithdraw(bank);
                            break;

                        case MenuOption.DEPOSIT:
                            DoDeposit(bank);
                            break;

                        case MenuOption.PRINT:
                            DoPrint(bank);
                            break;

                        case MenuOption.TRANSFER:
                            DoTransaction(bank);
                            break;

                        case MenuOption.TRANSACTIONHISTORY:
                            bank.PrintTransactionHistory();
                            DoRollback(bank);
                            break;

                        case MenuOption.QUIT:
                            Console.WriteLine("Thankyou for using our bank system..");
                            break;

                        default:
                            Console.WriteLine("Invalid Option! \nPlease enter a valid option\n");
                            break;
                    }

                } while (option != MenuOption.QUIT);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error! : " + e.Message);
            }
        }
    }
}
