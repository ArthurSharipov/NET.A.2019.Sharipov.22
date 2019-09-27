namespace BLL.Interface.Entities
{
    public class GoldAccount : Account
    {
        public GoldAccount(string id, string firstName, string lastName, double accountBalance) :
            base(id, firstName, lastName, accountBalance)
        {
            AccountBonus = 2;
        }

        public override string ToString() => "Base Account: " + base.ToString();

        public override int CalculateBonusForDeposit(int accountBonus) => 10 * accountBonus;
    }
}