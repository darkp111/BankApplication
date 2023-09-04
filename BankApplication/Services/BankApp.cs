using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Data;
using BankApplication.Enums;

namespace BankApplication.Services
{
    public class BankApp
    {
        private BankAccount bankAccount;
        private SavingsAccount savingsAccount;
        private Authentication authentication;
        private UserDefinitionService userDefinitionService;
        public BankApp()
        {
            bankAccount = new BankAccount();
            savingsAccount = new SavingsAccount();
            authentication = new Authentication(bankAccount);
            userDefinitionService = new UserDefinitionService("users.json");
        }

        public void LoginningUser(BankAccount account)
        {
            if (account is SavingsAccount)
                savingsAccount.AddInterest();


            Console.Write("Enter login: ");
            string? login = Console.ReadLine();
            Console.Write("Enter password: ");
            string? password = Console.ReadLine();

            if (login != null && password != null)
            {
                User? currentUser = authentication.Login(login, password);
                if (currentUser != null)
                {
                    Console.WriteLine("Login successful!");
                    currentUser.DisplayBalance();

                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Choose an option:");
                        Console.WriteLine("0. Exit");
                        Console.WriteLine("1. Deposit");
                        Console.WriteLine("2. Withdraw");
                        Console.WriteLine("3. Display Balance");
                        Console.Write("Enter your choice: ");
                        string? choiceAsString = Console.ReadLine();
                        if (choiceAsString is null)
                        {
                            throw new ArgumentNullException(nameof(choiceAsString));
                        }
                        LoginedUserChoices choice = (LoginedUserChoices)int.Parse(choiceAsString);
                        try
                        {

                            switch (choice)
                            {
                                case LoginedUserChoices.Deposit:
                                    Console.Write("Enter the amount to deposit: ");
                                    double depositAmount = Convert.ToDouble(Console.ReadLine());
                                    bankAccount.Deposit(currentUser.AccountNumber, depositAmount);
                                    Console.WriteLine("Press any button to continue");
                                    Console.ReadKey();
                                    break;

                                case LoginedUserChoices.Withdraw:
                                    Console.Write("Enter the amount to withdraw: ");
                                    double withdrawAmount = Convert.ToDouble(Console.ReadLine());
                                    bankAccount.Withdraw(currentUser.AccountNumber, withdrawAmount);
                                    Console.WriteLine("Press any button to continue");
                                    Console.ReadKey();
                                    break;

                                case LoginedUserChoices.DisplayBalance:
                                    currentUser.DisplayBalance();
                                    Console.WriteLine("Press any button to continue");
                                    Console.ReadKey();
                                    break;

                                case LoginedUserChoices.Exit:
                                    return;

                                default:
                                    Console.WriteLine("Invalid choice. Please select a valid option.");
                                    break;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Invalid choise, try again :(");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid login or password.");
                }
            }
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the BankApp!");
            Console.WriteLine("Press any button to start");
            Console.ReadKey();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Login with existing account");
                Console.WriteLine("2. Login with savings account");
                Console.WriteLine("3. Register a new account");
                string? choiceAsString = Console.ReadLine();
                try
                {
                    if (choiceAsString is null)
                    {
                        throw new ArgumentNullException(nameof(choiceAsString));
                    }
                    MainMenuChoices choice = (MainMenuChoices)int.Parse(choiceAsString);
                    switch (choice)
                    {
                        case MainMenuChoices.ExistingAccount:
                            LoginningUser(bankAccount);
                            Console.WriteLine("Press any button to continue");
                            Console.ReadKey();
                            break;
                        case MainMenuChoices.Registration:
                            CreateNewUser();
                            Console.WriteLine("Press any button to continue");
                            Console.ReadKey();
                            break;
                        case MainMenuChoices.SavingsAccount:
                            LoginningUser(savingsAccount);
                            Console.WriteLine("Press any button to continue");
                            Console.ReadKey();
                            break;
                        case MainMenuChoices.Exit:
                            Console.WriteLine("Thank you for choosing our bank, goodbye :)");
                            return;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid choice, try again :(");
                }
            }
        }

        public void CreateNewUser()
        {
            Console.Write("Enter your login: ");
            string? login = Console.ReadLine();

            // Check if the login already exists
            if (bankAccount.FindUser(u => u.Login == login) != null)
            {
                Console.WriteLine("This login is already taken. Please choose a different one.");
                return;
            }

            Console.Write("Enter your password: ");
            string? password = Console.ReadLine();

            // Generate a random account number and check for uniqueness
            string accountNumber = GenerateRandomAccountNumber();

            // Generate a new user
            if (login != null && password != null)
            {
                User newUser = new User(accountNumber, 0.0, login, password);

                // Add the new user to the bank account
                bankAccount.AddUser(newUser);

                // Save the updated users list to the JSON file
                userDefinitionService.SaveUsersToJson(bankAccount.GetUsers());

                Console.WriteLine("Registration successful!");
            }
        }

        private string GenerateRandomAccountNumber()
        {
            Random random = new Random();
            const string chars = "0123456789";
            string newAccountNumber;

            // Generate a new account number and check if it's unique
            do
            {
                newAccountNumber = new string(Enumerable.Repeat(chars, 10)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            while (bankAccount.FindUser(u => u.AccountNumber == newAccountNumber) != null);

            return newAccountNumber;
        }
    }
}
