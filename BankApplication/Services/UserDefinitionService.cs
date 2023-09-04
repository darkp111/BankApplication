using BankApplication.Data;
using BankApplication.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankApplication.Services
{
    public class UserDefinitionService : IUserDefinitionService
    {
        private string filePath;

        public UserDefinitionService(string filePath)
        {
            this.filePath = filePath;
        }
        public void LoadUsersFromJson(BankAccount bankAccount)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                var users = JsonConvert.DeserializeObject<Users>(json);
                if (users != null)
                {
                    foreach (var user in users.users)
                    {
                        bankAccount.AddUser(user);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading users from JSON file: {ex.Message}");
            }
        }

        public void AddUsersToJson(Users users)
        {
            try
            {
                string json = JsonConvert.SerializeObject(users);
                File.WriteAllText(filePath, json);
                Console.WriteLine("Users added and saved to users.json successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding users to JSON file: {ex.Message}");
            }
        }

        public void SaveUsersToJson(Users users)
        {
            try
            {
                string json = JsonConvert.SerializeObject(users);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding users to JSON file: {ex.Message}");
            }
        }
    }
}
