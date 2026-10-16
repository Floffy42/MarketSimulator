namespace MarketSimulator
{
    internal class Market
    {
        public List<Product> Products { get; private set; }
        public int SalesCount { get; private set; }
        public double Profit { get; private set; }

        public Market()
        {
            Products = new List<Product>();
            Products.Clear();
            SalesCount = 0;
            Profit = 0.00;

            SetProductList();
        }
        public void SetProductList()
        {
            AddProduct("Хліб", 29.99, 30);
            AddProduct("Молоко", 59.5, 20);
            AddProduct("Сир", 39.7, 15);
            AddProduct("Вода", 15.99, 40);
            AddProduct("Яйце", 5.99, 40);
            AddProduct("Сік", 19.99, 12);
        }
        private void AddProduct(string name, double price, int count)
        {
            Products.Add(new Product(name, price, count));
        }
        public void Buy(Product product, int count)
        {
            product.Purchase(count);
            SalesCount += count;
            Profit += product.Price;
        }
    }
}
