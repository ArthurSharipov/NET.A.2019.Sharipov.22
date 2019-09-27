using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interface.Interfaces
{
    public class DalAccount
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double AccountBalance { get; set; }
        public int AccountBonus { get; set; }
        public string AccountType { get; set; }

        public override string ToString() => $"Id: {Id}, FirstName: {FirstName}, " +
            $"LastName: {LastName}, AccountBalance: {AccountBalance}, AccountBonus: {AccountBonus}, " +
            $"AccountType: {AccountType}";
    }
}