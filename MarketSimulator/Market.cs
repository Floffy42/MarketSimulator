namespace MarketSimulator
{
    internal class Market
    {
        public List<Product> Products { get; private set; }
        public List<Product> SoldedProducts { get; private set; }
        public Product? LovestPriceProduct { get; private set; }
        public int SalesCount { get; private set; }
        public double Profit { get; private set; }

        public Market()
        {
            Products = new List<Product>();
            SoldedProducts = new List<Product>();

            Products.Clear();
            SoldedProducts.Clear();
            SalesCount = 0;
            Profit = 0.00;

            SetProductList();
        }
        public void SetProductList()
        {
            AddProduct("Хліб", 29.99, 300);
            AddProduct("Молоко", 59.5, 200);
            AddProduct("Сир", 39.7, 150);
            AddProduct("Вода", 15.99, 400);
            AddProduct("Яйце", 5.99, 400);
            AddProduct("Сік", 19.99, 120);
        }
        private void AddProduct(string name, double price, int count)
        {
            Products.Add(new Product(name, price, count));
        }
        public void Buy(Product product, int count)
        {
            if (product == null || count == 0) return;
            product.Purchase(count);
            SalesCount += count;
            Profit += product.Price * count;

            if (product.CheckIsQuanityEmpty())
            {
                Console.WriteLine($"Розпродано товар: {product.Name}");
                Products.Remove(product);
                SoldedProducts.Add(product);
                SetLovestPriceProduct();
            }
        }
        private void SetLovestPriceProduct()
        {
            if (Products.Count <= 0) return;

            LovestPriceProduct = null;
            LovestPriceProduct = Products[0];

            for (int i = 1; i < Products.Count; i++)
            {
                if (Products[i].Price < LovestPriceProduct.Price)
                {
                    LovestPriceProduct = Products[i];
                }
            }
        }
        public double GetLovestPriceProduct()
        {
            if(LovestPriceProduct == null) SetLovestPriceProduct();

            return LovestPriceProduct?.Price ?? 0.0;
        }
    }
}
