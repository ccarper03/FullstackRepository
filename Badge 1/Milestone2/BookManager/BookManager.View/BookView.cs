using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager;
using BookManager.Models;

namespace BookManager.View
{
    public class BookView
    {
        public int GetMenuChoice()
        {
            int num;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Enter a number between 1 - 5:");
            Console.WriteLine("[1] - Create Book");
            Console.WriteLine("[2] - Display Books");
            Console.WriteLine("[3] - Search for book by ID");
            Console.WriteLine("[4] - Edit Book");
            Console.WriteLine("[5] - Remove Book");
            Console.WriteLine("Any other number to quit.");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Your selection: ");
            num = GetIntegerInput();
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

            return num;
        }

        public Book GetNewBookInfo(int RngGenID)
        {

            int id = RngGenID;
            Console.Write("Release Year: \t");
            int releaseYear = GetIntegerInput();
            Console.Write("Title: \t\t");
            var title = GetStringInput();
            bool ischeckedOut = false;
            Console.Write("Author: \t");
            var author = GetStringInput();
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

            return new Book() { Id = id, Title = title , Author = author, ReleaseYear = releaseYear , CheckedOut = ischeckedOut };
        }

        public void DisplayBook(Book book) // display book to user
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Book ID\t\t:" + book.Id);
            Console.WriteLine("Book Title\t:"+book.Title);
            Console.WriteLine("Author\t\t:" + book.Author);
            Console.WriteLine("Release Year\t:" + book.ReleaseYear);
            Console.WriteLine("Book Available\t:" + book.CheckedOut + "\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DisplayBooks(List<Book> books)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (Book book in books) 
            {
                DisplayBook(book);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public Book EditBookInfo(Book book)
        {
            Console.WriteLine("Title");
            book.Title = GetStringInput();
            Console.WriteLine("Author");
            book.Author = GetStringInput();
            Console.WriteLine("Release Year");
            book.ReleaseYear = GetIntegerInput();
            Console.WriteLine("Is book checked out?");
            book.CheckedOut = GetStringtoBoolInput();

            return book;
        }

        public int SearchBook()
        {
            int num;

            Console.Write("Enter the Id of a book: ");
            num = GetIntegerInput();

            return num;
        }

        public bool ConfirmRemoveBook(Book book)
        {
            bool answer = false;
            Console.WriteLine("Remove dis book bro? [Y/N]");
            DisplayBook(book);
            string temp = GetStringInput();
            if (temp.Contains("yes") || temp.Contains("Yes") || temp.Contains("Y") || temp.Contains("y"))
            {
                answer = true;
            }
            return answer;
        }

        private int GetIntegerInput() // GetIntegerInput(int min, int max)  get a number in Range 
        {
            int number;
            while (true)
            {
                string input = Console.ReadLine() + "\n";
                if (int.TryParse(input, out number))
                {
                    if (number < 1)
                    {
                        continue;
                    }
                    else
                    {
                        break; // output has value and not lower than 1 or higher than 3
                    }
                }
                else
                {
                    Console.WriteLine("Try again, select a whole number.");
                }
            }
            return number;
        }
        private string GetStringInput()   // GetStringInput(string prompt)
        {
            string stringAnswer;
            while (true)
            {
                stringAnswer = Console.ReadLine();
                if (string.IsNullOrEmpty(stringAnswer))
                {
                    Console.WriteLine("Nothing was entered try again.\n");
                }
                else
                {
                    break;
                }
            }
            return stringAnswer;
        }
        private bool GetStringtoBoolInput()
        {
            string stringAnswer;
            while (true)
            {
                stringAnswer = Console.ReadLine().ToLower();
                if (string.IsNullOrEmpty(stringAnswer))
                {
                    Console.WriteLine("Nothing was entered try again.\n");
                }
                else if(stringAnswer.Contains("y") || stringAnswer.Contains("yes") ||  stringAnswer.Contains("true"))
                {
                    return true;
                }
                else
                {
                    break;
                }
            }
            return false;
        }

        public void ErrorID()
        {
            Console.WriteLine("ID is not avaiable.\n");
        }
    }
}
