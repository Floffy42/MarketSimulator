namespace MarketSimulator
{
    internal class Product
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public int Count { get; private set; }
        public Product(string name, double price, int count)
        {
            Name = name;
            Price = price;
            Count = count;
        }
        private void ReduceCount(int count)
        {
            if (Count < 0) return;
            Count -= count;
        }
        public void Purchase(int count)
        {
            ReduceCount(count);
        }
    }
}
