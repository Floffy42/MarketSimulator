namespace MarketSimulator
{
    internal class Program
    {
        static Random rnd = new Random();
        static Market market = new Market();
        //Розробити програму, яка б симулювала покупців у магазині з наступними вимогами:

        //[+] В системі буде декілька покупців.Кількість покупців вводить користувач з консолі.
        //[+] Кожен покупець має картку з його номером(випадкове число від 1 до 1000,
        //      для цього можете використовувати  Random rnd = new Random(); rnd.next(1000)

        //[+] У кожного покупця є стартова кількість грошей(також випадкове число від 1000 до 10_000)

        //[+] В магазині є багато різних видів товарів(описані в коді) та є кількість прибутку(спочатку дорівнює 0 )
        //[+] Кожен товар має назву, ціну, кількість у магазині.
        //      (усі кількості - описані в коді чи можете використати генератор випадкових чисел)

        //[+] коли покупець покупає товар -кількість товару зменшується на одиницю,
        //      гроші покупця зменщуются на ціну товару, прибуток магазину зростає на ціну товара.

        //[+] Зробіть так щоб покупці покупали товари до тих пір поки в них є гроші чи не закінчилися товари
        //[+] вкінкці програми вивесту прибуток магазину та номер картки покупця
        //      що купив максимальну кількість товарів та картку покупця що витратив найбільшу кількість грошей

        //[+] Для виконання роботи використовуйте класи. структури та функції для організації коду. 
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int numberOfCustomers = AskNumOfCustomers();

            List<Customer> customers = new List<Customer>();
            List<Customer> customersWithoutMoney = new List<Customer>();
            CreateCustomers(numberOfCustomers, customers);

            P("Покупці створені:", 2);
            foreach (Customer customer in customers)
            {
                P($"Картка: {customer.CardNumber}, Гроші: {customer.Money}");
            }

            P("Список товарів: ", 2);
            foreach (Product product in market.Products)
            {
                P($"Назва: {product.Name}, Ціна: {product.Price}, Кількість: {product.Count}");
            }

            P("Покупці роблять покупки", 2);
            while(market.Products.Count > 0 && customers.Count > 0)
            {
                int randomCustomer = rnd.Next(0, customers.Count);
                Customer selectedCustomer = customers[randomCustomer];

                bool isPurchased = false;
                while(isPurchased == false && market.Products.Count > 0 && customers.Count > 0)
                {
                    int randomProduct = rnd.Next(0, market.Products.Count);
                    Product selectedProduct = market.Products[randomProduct];
                    int randomCount = rnd.Next(1, selectedProduct.Count);

                    isPurchased = ProcessPurchase(selectedProduct, selectedCustomer, randomCount, false);

                    if (selectedCustomer.Money < market.GetLovestPriceProduct())
                    {
                        customers.Remove(selectedCustomer);
                        customersWithoutMoney.Add(selectedCustomer);
                        break;
                    }
                }
            }

            P($"Залишок товарів в магазині: {market.Products.Count}/{market.Products.Count + market.SoldedProducts.Count}:", 2);
            foreach (Product product in market.Products)
            {
                P($"Назва: {product.Name}, Кількість: {product.Count}");
            }
            P($"Залишок активних покупців: {customers.Count}", 2);
            foreach (Customer customer in customers)
            {
                P($"Картка покупця: {customer.CardNumber}, Гроші: {customer.Money:F2} / {customer.Money+customer.SpendedMoney:F2}, Куплено продуктів: {customer.PurchasedProducts}");
            }
            P($"Неактивні покупці: {customersWithoutMoney.Count}", 2);
            foreach (Customer customer in customersWithoutMoney)
            {
                P($"Картка покупця: {customer.CardNumber}, Гроші: {customer.Money:F2} / {customer.Money+customer.SpendedMoney:F2}, Куплено продуктів: {customer.PurchasedProducts}");
            }

            P($"Прибуток магазину: {market.Profit:F2}", 2);

            Customer? bestPurchaseCountCustomer = null;
            if (customers.Count > 0)
            {
                if(bestPurchaseCountCustomer == null) bestPurchaseCountCustomer = customers[0];
                for (int i = 0; i < customers.Count; i++)
                {
                    if (customers[i].PurchasedProducts > bestPurchaseCountCustomer.PurchasedProducts) bestPurchaseCountCustomer = customers[i];
                }
            }
            if (customersWithoutMoney.Count > 0)
            {
                if (bestPurchaseCountCustomer == null) bestPurchaseCountCustomer = customersWithoutMoney[0];
                for (int i = 0; i < customersWithoutMoney.Count; i++)
                {
                    if (customersWithoutMoney[i].PurchasedProducts > bestPurchaseCountCustomer.PurchasedProducts) bestPurchaseCountCustomer = customersWithoutMoney[i];
                }
            }
            if(bestPurchaseCountCustomer != null) P($"Максимальну кількість товару купив: {bestPurchaseCountCustomer.CardNumber} - {bestPurchaseCountCustomer.PurchasedProducts}", 1);

            Customer? bestSpendedMoneyCustomer = null;
            if (customers.Count > 0)
            {
                if(bestSpendedMoneyCustomer == null) bestSpendedMoneyCustomer = customers[0];
                for (int i = 0; i < customers.Count; i++)
                {
                    if (customers[i].SpendedMoney > bestSpendedMoneyCustomer.SpendedMoney) bestSpendedMoneyCustomer = customers[i];
                }
            }
            if (customersWithoutMoney.Count > 0)
            {
                if (bestSpendedMoneyCustomer == null) bestSpendedMoneyCustomer = customersWithoutMoney[0];
                for (int i = 0; i < customersWithoutMoney.Count; i++)
                {
                    if (customersWithoutMoney[i].SpendedMoney > bestSpendedMoneyCustomer.SpendedMoney) bestSpendedMoneyCustomer = customersWithoutMoney[i];
                }
            }
            if(bestSpendedMoneyCustomer != null) P($"Більше всього витратив грошей: {bestSpendedMoneyCustomer.CardNumber} - {bestSpendedMoneyCustomer.SpendedMoney:F2}", 1);

            P("", 1);
            P("Натисніть будь яку клавішу щоб завершити.", 0);
            Console.ReadKey();
        }

        private static bool ProcessPurchase(Product product, Customer customer, int count, bool showPurchases)
        {
            if (product == null || count == 0) return false;

            if (customer.Money >= product.Price * count)
            {
                market.Buy(product, count);
                customer.PurchasedProduct(product.Price, count);
                if(showPurchases) P($"Покупець з карткою {customer.CardNumber} купив {count} '{product.Name}', його залишок - {customer.Money:F2}, " +
                    $"залишок продукту - {product.Count}");
                return true;
            }
            else if (customer.Money >= product.Price)
            {
                market.Buy(product, 1);
                customer.PurchasedProduct(product.Price, 1);
                if (showPurchases) P($"Покупець з карткою {customer.CardNumber} купив 1 '{product.Name}', його залишок - {customer.Money:F2}, залишок продукту - {product.Count}");
                return true;
            }
            return false;
        }



        private static int AskNumOfCustomers()
        {
            while(true)
            {
                P("Введіть кількість покупців: ", 0);
                string? input = Console.ReadLine();

                if (input == null) return 0;

                if (int.TryParse(input, out int result)) return int.Parse(input);
            }

        }
        private static void CreateCustomers(int numberOfCustomers, List<Customer> customers)
        {
            for (int i = 0; i < numberOfCustomers; i++)
            {
                int cardNumber = rnd.Next(1, 1001);
                double money = rnd.Next(1000, 10001);
                Customer customer = new Customer(cardNumber, money);
                customers.Add(customer);
            }
        }






        //Console.WriteLine
        private static void P(string text, int extraSpaces = 1) //P - Print
        {
            if (extraSpaces >= 2) Console.WriteLine("");
            Console.Write(text);
            if (extraSpaces >= 1) Console.WriteLine("");
            if (extraSpaces >= 3) Console.WriteLine("");
        }
    }
}
