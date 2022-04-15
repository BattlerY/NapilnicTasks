using System;

namespace PaymentSystem
{
    class PaymentSystem
    {
        static void Main(string[] args)
        {
            var order = new Order(122542, 5000);

            //Честно сказать, не понял как это задание относится к наследованию и полиморфизму
            //И где тут с ними надо разгуляться

            var PaymentSystem1 = new PaymentSystem1();
            Console.WriteLine(PaymentSystem1.GetPayingLink(order));
            var PaymentSystem2 = new PaymentSystem2();
            Console.WriteLine(PaymentSystem2.GetPayingLink(order));
            var PaymentSystem3 = new PaymentSystem3(254);
            Console.WriteLine(PaymentSystem3.GetPayingLink(order));
        }
    }

    public class Order
    {
        public readonly int Id;
        public readonly int Amount;

        public Order(int id, int amount) => (Id, Amount) = (id, amount);
    }

    public class Hasher
    {
        public static string GetMd5Text(int number)
        {
            return (number ^ 1).ToString() + "MD";
        }

        public static string GetSHA1Text(int number)
        {
            return (number ^ 2).ToString() + "SHA";
        }
    }

    public interface IPaymentSystem
    {
        public string GetPayingLink(Order order);
    }

    public class PaymentSystem1 : IPaymentSystem
    {
        public string GetPayingLink(Order order)
        {
            return $"pay.system1.ru/order?amount={order.Amount}RUB&hash={Hasher.GetMd5Text(order.Id)}";
        }
    }

    public class PaymentSystem2 : IPaymentSystem
    {
        public string GetPayingLink(Order order)
        {
            return $"order.system2.ru/pay?hash={Hasher.GetMd5Text(order.Id+order.Amount)}";
        }
    }

    public class PaymentSystem3 : IPaymentSystem
    {
        private int _secretCode;

        public PaymentSystem3(int secretCode)
        {
            _secretCode = secretCode;
        }

        public string GetPayingLink(Order order)
        {
            return $"system3.com/pay?amount={order.Amount}&curency=RUB&hash={Hasher.GetSHA1Text(order.Id + order.Amount+_secretCode)}";
        }
    }
}
