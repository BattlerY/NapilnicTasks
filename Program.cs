using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            //Вывод всех товаров на складе с их остатком
            warehouse.Revise();

            Cart cart = shop.CreateCart();
            cart.Add(iPhone12, 4);
            cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

            //Вывод всех товаров в корзине
            cart.ShowContent();

            Console.WriteLine($"\n{cart.Order().Paylink}");

            warehouse.Revise();
            cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары
        }
    }

    class Good
    {
        public string Name { get; private set; }

        public Good(string name) => 
            Name = name;
    }

    class Warehouse
    {
        private List<Block> _blocks;

        public Warehouse() =>
            _blocks = new List<Block>();

        public void Delive(Good good, int count)
        {
            if (TryFindBlock(out Block block, good)==false)
                _blocks.Add(new Block(good, count));
            else
                block.Add(count);
        }

        public void Ship(Block block)
        {
            Block curentBlock = FindBlock(block);
            curentBlock.Delete(block.Count);
            if (curentBlock.Avaiable == false)
                _blocks.Remove(curentBlock);
        }

        public bool CheckWarehouse(Good good, int count)
        {
            return (TryFindBlock(out Block block, good)) && block.Count>=count;
        }

        public void Revise()
        {
            Console.WriteLine("\nВсе товары на складе");
            _blocks.ForEach(block => block.ShowInfo());
        }

        private bool TryFindBlock(out Block block, Good good)
        {
            block = FindBlock(good);
            return block != null;
        }

        private Block FindBlock(Good good)
        {
            return _blocks.FirstOrDefault(block => block.Good.Name == good.Name);
        }

        private Block FindBlock(Block block)
        {
            return FindBlock(block.Good);
        }
    }

    class Block
    {
        public Good Good { get; private set; }
        public int Count { get; private set; }

        public bool Avaiable => Count > 0;

        public Block(Good good, int count)
        {
            Good = good;
            Count = count > 0 ? count : 0;
        }

        public void Add(int count)
        {
            Count += count;
        }

        public void Delete(int count)
        {
            Count -= count <= Count ? count : Count;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"\n{Good.Name} = {Count} штук");
        }
    }

    class Shop
    {
        public Warehouse Warehouse { get; private set; }

        public Shop(Warehouse warehouse) =>
            Warehouse = warehouse;

        public Cart CreateCart()
        {
            return new Cart(this);
        }

    }

    class Cart
    {
        private List<Block> _blocks;
        private Shop _shop;

        public Cart(Shop shop)
        {
            _blocks = new List<Block>();
            _shop = shop;
        }

        public void Add(Good good, int count)
        {   
            if(_shop.Warehouse.CheckWarehouse(good, count))
            {
                var block = _blocks.FirstOrDefault(block => block.Good.Name == good.Name);

                if (block != null)
                    _blocks.Remove(block);

                _blocks.Add(new Block(good, count));
            }
            else
            {
                Console.WriteLine("Ошибка. Не хватает товара на складе");
            }
        }

        public Bill Order()
        {
            _blocks.ForEach(block => _shop.Warehouse.Ship(block));

            return new Bill();
        }

        public void ShowContent()
        {
            Console.WriteLine("\nВсе товары в корзине");
            _blocks.ForEach(block => block.ShowInfo());
        }
    }

    class Bill
    {
        public string Paylink { get; private set;}

        public Bill()
        {
            Paylink = "Get bill";
        }
    }
}


