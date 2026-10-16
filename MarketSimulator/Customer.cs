namespace MarketSimulator
{
    internal class Customer
    {
        public int CardNumber { get; private set; }
        public double Money { get; private set; }
        public double SpendedMoney { get; private set; }
        public int PurchasedProducts { get; private set; }
        public Customer(int cardNumber, double money)
        {
            CardNumber = cardNumber;
            Money = money;
            PurchasedProducts = 0;
        }

        public void PurchasedProduct(double price, int count)
        {
            Money -= price * count;
            SpendedMoney += price * count;
            PurchasedProducts += count;
        }
    }
}
