using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            Controllers.BookController bookController = new Controllers.BookController();
            bookController.Run();
            Console.ReadLine();
        }
    }
}
