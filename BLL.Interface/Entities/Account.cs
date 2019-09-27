using System;

namespace BLL.Interface.Entities
{
    public abstract class Account
    {
        public Account(string id, string firstName, string lastName, double accountBalance)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            AccountBalance = accountBalance;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double AccountBalance { get; set; }
        public int AccountBonus { get; set; }

        public abstract int CalculateBonusForDeposit(int accountBonus);

        public void Deposit(double money)
        {
            if (money <= 0)
            {
                throw new ArgumentException(nameof(money));
            }

            AccountBalance += money;
            AccountBonus += CalculateBonusForDeposit(AccountBonus);
        }

        public void Withdraw(double money)
        {
            if (money <= 0)
            {
                throw new ArgumentException(nameof(money));
            }

            if (AccountBalance <= money)
            {
                throw new ArgumentException(nameof(money));
            }

            AccountBalance -= money;
        }

        public override string ToString() => $"Id: {Id}, FirstName: {FirstName}, " +
            $"LastName: {LastName}, AccountBalance: {AccountBalance}, AccountBonus: {AccountBonus}";
    }
}