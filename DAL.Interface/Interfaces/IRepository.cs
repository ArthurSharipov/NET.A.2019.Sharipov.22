using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interface.Interfaces
{
    public interface IRepository
    {
        void CreateAccount(DalAccount account);
        void RemoveAccount(DalAccount account);
        IEnumerable<DalAccount> GetAccounts();
        void UpdateAccount(DalAccount account);
    }
}