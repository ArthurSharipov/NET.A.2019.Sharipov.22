namespace BLL.Interface.Entities
{
    public class PlatinumAccount : Account
    {
        public PlatinumAccount(string id, string firstName, string lastName, double accountBalance) :
            base(id, firstName, lastName, accountBalance)
        {
            AccountBonus = 3;
        }

        public override string ToString() => "Base Account: " + base.ToString();

        public override int CalculateBonusForDeposit(int accountBonus) => 10 * accountBonus;
    }
}
