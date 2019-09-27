using BLL.Interface.Entities;
using DAL.Interface.Interfaces;
using System;

namespace BLL.Mappers
{
    public static class Mapper
    {
        public static Account ConvertToAccount(this DalAccount dalAccount)
        {
            return (Account)Activator.CreateInstance(
                GetAccountType(dalAccount.AccountType),
                dalAccount.Id,
                dalAccount.FirstName,
                dalAccount.LastName,
                dalAccount.AccountBalance,
                dalAccount.AccountBonus);
        }

        public static DalAccount ConvertToDalAccount(this Account account)
        {
            return new DalAccount
            {
                Id = account.Id,
                FirstName = account.FirstName,
                LastName = account.LastName,
                AccountBalance = account.AccountBalance,
                AccountBonus = account.AccountBonus,
                AccountType = account.GetType().Name
            };
        }

        private static Type GetAccountType(string type)
        {
            if (type.Contains("Gold"))
            {
                return typeof(GoldAccount);
            }

            if (type.Contains("Platinum"))
            {
                return typeof(PlatinumAccount);
            }

            return typeof(BaseAccount);
        }
    }
}