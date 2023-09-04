using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data
{
    public class Users
    {
        public List<User> users;

        public Users()
        {
            users = new List<User>();
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public User? FindUser(Func<User, bool> predicate)
        {
            foreach (var user in users)
            {
                if (predicate(user))
                {
                    return user;
                }
            }
            return null;
        }
    }
}
