namespace MarketSimulator
{
    internal class Customer
    {
        public int CardNumber { get; private set; }
        public int Money { get; private set; }

        public Customer(int cardNumber, int money)
        {
            CardNumber = cardNumber;
            Money = money;
        }
    }
}
