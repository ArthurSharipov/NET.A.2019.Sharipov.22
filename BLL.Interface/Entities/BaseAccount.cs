namespace BLL.Interface.Entities
{
    public class BaseAccount : Account
    {
        public BaseAccount(string id, string firstName, string lastName, double accountBalance) :
            base(id, firstName, lastName, accountBalance)
        {
            AccountBonus = 1;
        }

        public override string ToString() => "Base Account: " + base.ToString();

        public override int CalculateBonusForDeposit(int accountBonus) => 10 * accountBonus;
    }
}
