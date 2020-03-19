using BookManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Data
{
    public class BookRepository
    {
        private List<Book> books;
        public BookRepository()
        {
            if (books == null)
            {
                books = new List<Book>();
                books.Add(new Book() { Id = 1, Author = "Chuck", CheckedOut = false, Title = "Game Design" , ReleaseYear = 2020});
                books.Add(new Book() { Id = 2, Author = "Chuck", CheckedOut = false, Title = "Game Programming", ReleaseYear = 2019 });
            }
        }

        public void Create(List<Book> book)
        {
            books.AddRange(book);
        }

        public List<Book> ReadAll()
        {
            return books;
        }


        public Book ReadId(int id)
        {
            Book selectedBook = new Book();
            foreach (var book in books)
            {
                if (book.Id == id)
                {
                    selectedBook = book;
                    break;
                }
            }
            if (selectedBook.Id == 0)
            {
                selectedBook = null;
            }
            return selectedBook;
        }

        // Search through books to find the book with Id and change values 
        public void Update(int id, Book selectedBook)
        {
            foreach (var book in books)
            {
                if (book.Id == id)
                {
                    book.Id = selectedBook.Id;
                    book.ReleaseYear = selectedBook.ReleaseYear;
                    book.Title = selectedBook.Title;
                    book.CheckedOut = selectedBook.CheckedOut;
                    book.Author = selectedBook.Author;
                    break;
                }
            }
        }

        // Search through books using the Id and remove that book
        public void Delete(int id)
        {
            foreach (var book in books)
            {
                if (book.Id == id)
                {
                    books.Remove(book);
                    break;
                }
            }
        }
        public int rngNum;
        public int GetRandomGeneratedID()
        {
            Random rng = new Random();
            bool keepRunning = true;
            bool isInList;
            List<int> num = new List<int>();

            foreach (var book in books)
            {
                num.Add(book.Id);
            }

            while (keepRunning)
            {
                rngNum = rng.Next(500);
                isInList = num.IndexOf(rngNum) != -1; // find better way to get ID (Linq)

                if (isInList == false)
                {
                    keepRunning = false;
                }
            }
            return rngNum;
        }
    }
}
