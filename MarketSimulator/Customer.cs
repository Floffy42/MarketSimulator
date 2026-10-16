namespace MarketSimulator
{
    internal class Customer
    {
        public int CardNumber { get; private set; }
        public double Money { get; private set; }

        public Customer(int cardNumber, double money)
        {
            CardNumber = cardNumber;
            Money = money;
        }
    }
}
