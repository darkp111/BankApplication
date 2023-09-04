using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Interfaces
{
    public interface IUser
    {
        string AccountNumber { get; set; }
        double Balance { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        void DisplayBalance();
    }
}
