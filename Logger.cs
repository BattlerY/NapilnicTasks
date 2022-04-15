using System;
using System.IO;

namespace Logger
{
    class Logger
    {
        static void Main(string[] args)
        {
            var a1 = new Pathfinder(new FileLogWritter(new Loger()));
            var a2 = new Pathfinder(new ConsoleLogWritter(new Loger()));
            var a3 = new Pathfinder(new FridayLogWritter(new FileLogWritter(new Loger())));
            var a4 = new Pathfinder(new FridayLogWritter(new ConsoleLogWritter(new Loger())));
            var a5 = new Pathfinder(new FridayLogWritter(new ConsoleLogWritter(new FileLogWritter(new Loger()))));
            a1.Find("1exm");
            a2.Find("2exm");
            a3.Find("3exm");
            a4.Find("4exm");
            a5.Find("5exm");
        }
    }

    interface ILogger
    {
        void WriteError(string message);
    }

    //Не уверен насчет данного логера пустышки
    //Возможно его стоит убрать и везде просто добавить пустые конструкты с проверкой на нулл в методе,
    //Кроме FridayLogWritter, так как там не имеет смысла не иметь последующего звенья в цепочке

    class Loger : ILogger
    {
        public void WriteError(string message)
        {
  
        }
    }

    class FileLogWritter : ILogger
    {
        private ILogger _logger;

        public FileLogWritter(ILogger logger)
        {
            _logger = logger;
        }

        public  void WriteError(string message)
        {
            //File.WriteAllText("log.txt", message);
            Console.WriteLine(message + "File");
            _logger.WriteError(message);
        }
    }

    class ConsoleLogWritter: ILogger
    {
        private ILogger _logger;

        public ConsoleLogWritter(ILogger logger)
        {
            _logger = logger;
        }

        public  void WriteError(string message)
        {
            Console.WriteLine(message + "Console");
            _logger.WriteError(message);
        }
    }

    class FridayLogWritter : ILogger
    {
        private ILogger _logger;

        public FridayLogWritter(ILogger logger)
        {
            _logger = logger;
        }

        public  void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                _logger.WriteError(message);
            }
        }
    }

    class Pathfinder
    {
        private ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            _logger = logger;
        }

        public void Find(string message)
        {
            _logger.WriteError(message);
        }
    }

}
