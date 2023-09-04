using BankApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data
{
    public class BankAccount
    {
        public Users Users;
        private UserDefinitionService userDefinitionService;
        public BankAccount()
        {
            Users = new Users();
            userDefinitionService = new UserDefinitionService("users.json");
            userDefinitionService.LoadUsersFromJson(this);
        }

        public void AddUser(User user)
        {
            Users.AddUser(user);
            userDefinitionService.SaveUsersToJson(Users);
        }

        public User? FindUser(Func<User, bool> predicate)
        {
            return Users.FindUser(predicate);
        }
        public Users GetUsers()
        {
            return Users;
        }

        /// <summary>
        /// Function that deposit ammount to user which take as param accountNumber of user nad amount of deposit
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="amount"></param>
        public void Deposit(string accountNumber, double amount)
        {
            User? user = FindUser(u => u.AccountNumber == accountNumber);
            if (user != null)
            {
                user.Deposit(amount);
                userDefinitionService.SaveUsersToJson(Users);
            }
        }

        /// <summary>
        /// Function that withdraw ammount from user bank acc and check, if user doesn't have enough money
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool Withdraw(string accountNumber, double amount)
        {
            User? user = FindUser(u => u.AccountNumber == accountNumber);
            if (user != null)
            {
                bool success = user.Withdraw(amount);
                if (success)
                {
                    userDefinitionService.SaveUsersToJson(Users);
                }
                return success;
            }
            return false;
        }

        public void DisplayBalance(User user)
        {
            if (user != null)
            {
                user.DisplayBalance();
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
    }
}
