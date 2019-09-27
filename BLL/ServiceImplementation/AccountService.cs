using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using BLL.Mappers;
using DAL.Interface.Interfaces;
using System;
using System.Collections.Generic;

namespace BLL.ServiceImplementation
{
    public class AccountService : IAccountService
    {
        private readonly IRepository _accountRepository;
        private readonly IAccountGenerateIdNumber _accountGenerateIdNumber;

        public AccountService(IRepository accountRepository, IAccountGenerateIdNumber accountGenerateIdNumber)
        {
            _accountRepository = accountRepository;
            _accountGenerateIdNumber = accountGenerateIdNumber;
        }

        public void AddMoney(Account account, double money)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            account.Deposit(money);
            _accountRepository.UpdateAccount(account.ConvertToDalAccount());
        }

        public void WithdrawMoney(Account account, double money)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            account.Withdraw(money);
            _accountRepository.UpdateAccount(account.ConvertToDalAccount());
        }

        public void RemoveAccount(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            _accountRepository.RemoveAccount(account.ConvertToDalAccount());
        }

        public Account CreateAccount(AccountType accountType, string firstName, string lastName, double accountBalance)
        {
            var account = CreateAccount(TypeOfAccount(accountType), _accountGenerateIdNumber.GenerateId(), firstName,
                lastName, accountBalance);

            _accountRepository.CreateAccount(account.ConvertToDalAccount());
            return account;
        }

        private Account CreateAccount(Type accountType, string id, string firstName, string lastName,
            double accountBalance)
        {
            return (Account)Activator.CreateInstance(accountType, id, firstName, lastName, accountBalance);
        }

        private static Type TypeOfAccount(AccountType accountType)
        {
            if (accountType == AccountType.Base)
                return typeof(BaseAccount);

            else if (accountType == AccountType.Gold)
                return typeof(GoldAccount);

            else if (accountType == AccountType.Platinum)
                return typeof(PlatinumAccount);

            else
                throw new ArgumentException(nameof(accountType));
        }

        public void DisplayBankAccount(IEnumerable<DalAccount> accounts)
        {
            foreach (var item in accounts)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
    }
}