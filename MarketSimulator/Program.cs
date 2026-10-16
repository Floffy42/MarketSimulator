namespace MarketSimulator
{
    internal class Program
    {
        static Random rnd = new Random();
        static Market market = new Market();
        //Розробити програму, яка б симулювала покупців у магазині з наступними вимогами:

        //[+] В системі буде декілька покупців.Кількість покупців вводить користувач з консолі.
        //[+] Кожен покупець має картку з його номером(випадкове число від 1 до 1000, для цього можете використовувати  Random rnd = new Random(); rnd.next(1000)
        //[+] У кожного покупця є стартова кількість грошей(також випадкове число від 1000 до 10_000)

        //[+] В магазині є багато різних видів товарів(описані в коді) та є кількість прибутку(спочатку дорівнює 0 )
        //[+] Кожен товар має назву, ціну, кількість у магазині. (усі кількості - описані в коді чи можете використати генератор випадкових чисел)
        //[] коли покупець покупає товар -кількість товару зменшується на одиницю, гроші покупця зменщуются на ціну товару, прибуток магазину зростає на ціну товара.
        //[] Зробіть так щоб покупці покупали товари до тих пір поки в них є гроші чи не закінчилися товари
        //[] вкінкці програми вивесту прибуток магазину та номер картки покупця що купив максимальну кількість товарів та картку покупця що витратив найбільшу кількість грошей
        //[] Для виконання роботи використовуйте класи. структури та функції для організації коду. 
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int numberOfCustomers = AskNumOfCustomers();

            List<Customer> customers = new List<Customer>();
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
