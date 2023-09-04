using System.Text.Json;
using Newtonsoft.Json;
using BankApplication.Services;

class Program
{
    static void Main(string[] args)
    {
        BankApp bankApp = new BankApp();
        bankApp.Run();
    }
}