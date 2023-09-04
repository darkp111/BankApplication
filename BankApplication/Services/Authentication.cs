using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Data;

namespace BankApplication.Services
{
    public class Authentication
    {
        private BankAccount bankAccount;

        public Authentication(BankAccount bankAccount)
        {
            this.bankAccount = bankAccount;
        }

        public User? Login(string login, string password)
        {
            return bankAccount.FindUser(user => user.Login == login && user.Password == password);
        }
    }
}
