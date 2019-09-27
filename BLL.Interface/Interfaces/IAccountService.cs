using BLL.Interface.Entities;
using DAL.Interface.Interfaces;
using System.Collections.Generic;

namespace BLL.Interface.Interfaces
{
    public interface IAccountService
    {
        Account CreateAccount(AccountType accountType, string firstName, string lastName, double accountBalance);
        void AddMoney(Account account, double money);
        void WithdrawMoney(Account account, double money);
        void RemoveAccount(Account account);
        void DisplayBankAccount(IEnumerable<DalAccount> accounts);
    }
}
