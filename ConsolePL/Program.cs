using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using DAL.Interface.Interfaces;
using DependencyResolver;
using Ninject;
using System;

namespace ConsolePL
{
    class Program
    {
        private static readonly IKernel resolver;

        static Program()
        {
            resolver = new StandardKernel();
            resolver.ConfigurateResolver();
        }

        static void Main(string[] args)
        {
            IAccountService service = resolver.Get<IAccountService>();
            IRepository repository = resolver.Get<IRepository>();

            var accountBase = service.CreateAccount(AccountType.Base, "Base", "Base", 1337);
            var accountGold = service.CreateAccount(AccountType.Gold, "Gold", "Gold", 1608);
            var accountPlatinum = service.CreateAccount(AccountType.Platinum, "Platinum", "Platinum", 1312);

            service.AddMoney(accountBase, 55.5);
            service.AddMoney(accountGold, 13.37);
            service.AddMoney(accountPlatinum, 16.08);

            service.DisplayBankAccount(repository.GetAccounts());
            Console.WriteLine();

            service.WithdrawMoney(accountBase, 55.5);
            service.WithdrawMoney(accountGold, 13.37);
            service.WithdrawMoney(accountPlatinum, 16.08);

            service.DisplayBankAccount(repository.GetAccounts());
            Console.WriteLine();

            service.RemoveAccount(accountBase);

            service.DisplayBankAccount(repository.GetAccounts());
        }
    }
}