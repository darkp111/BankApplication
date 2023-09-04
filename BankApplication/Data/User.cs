using BankApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data
{
    public class User : IUser
    {
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public User(string accountNumber, double balance, string login, string password)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Login = login;
            Password = password;
        }

        public void DisplayBalance()
        {
            Console.WriteLine($"Account Number: {AccountNumber}, Balance: {Balance}");
        }

        public void Deposit(double amount)
        {
            Balance += amount;
            Console.WriteLine($"Deposited: {amount}, New Balance: {Balance}");
        }

        public bool Withdraw(double amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrawn: {amount}, New Balance: {Balance}");
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
                return false;
            }
        }
    }
}
