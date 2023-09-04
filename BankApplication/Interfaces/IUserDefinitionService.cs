using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Data;

namespace BankApplication.Interfaces
{
    public interface IUserDefinitionService
    {
        void LoadUsersFromJson(BankAccount bankAccount);
        void AddUsersToJson(Users users);
        void SaveUsersToJson(Users users);
    }
}
