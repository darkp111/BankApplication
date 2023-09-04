using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data
{
    public class SavingsAccount : BankAccount
    {
        public double InterestRate { get; set; } = 0.05;

        public void AddInterest()
        {
            foreach (var user in Users.users)
            {
                double interest = user.Balance * (InterestRate / 100);
                user.Deposit(interest);
            }
        }

        public void DisplayBalance()
        {
            Console.WriteLine("Savings Account Balance:");
            foreach (var user in Users.users)
            {
                Console.WriteLine($"Account Number: {user.AccountNumber}, Balance: {user.Balance:C}");
            }
        }
    }
}
