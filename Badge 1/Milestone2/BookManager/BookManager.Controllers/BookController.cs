using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Models;
namespace BookManager.Controllers
{
    public class BookController
    {
        // Create a new intance of BookView
        View.BookView view = new View.BookView();
        Data.BookRepository data = new Data.BookRepository();
        public void Run()
        {
            bool keepRunning = true;
            do
            {
                // Call GetMenuChoice from the view, return and store an integer in selection variable
                int selection = view.GetMenuChoice();
                switch (selection)
                {
                    case 1:
                        CreateBook();
                        break;
                    case 2:
                        DisplayBooks();
                        break;
                    case 3:
                        SearchBooks();
                        break;
                    case 4:
                        EditBook();
                        break;
                    case 5:
                        RemoveBook();
                        break;
                    default:
                        keepRunning = false;
                        break;
                }
            } while (keepRunning);
        }

        private void CreateBook()
        {
            // Do only one book at a time
            List<Book> books = new List<Book>();
            int ID = data.GetRandomGeneratedID();
            books.Add(view.GetNewBookInfo(ID));
            data.Create(books);
        }

        private void DisplayBooks()
        {
            var temp = data.ReadAll(); // get books from data repo
            view.DisplayBooks(temp); // send books to view       
        }

        private void SearchBooks()
        {
            int id; 
            Book book;

            do
            {
                id = view.SearchBook();
                book = data.ReadId(id);
                if (book == null)
                {
                    view.ErrorID();
                }
                else
                {
                    view.DisplayBook(book);
                }
            } while (book == null);
        }

        private void EditBook()
        {
            List<Book> temp = data.ReadAll(); // get books from data repo
            int id = 0;
            Book book;

            view.DisplayBooks(temp); // send books to view
            do
            {
                id = view.SearchBook();
                book = data.ReadId(id);
                if (book == null)
                {
                    view.ErrorID();
                }
                else
                {
                    book = view.EditBookInfo(book);
                }
            } while (book == null);
        }

        private void RemoveBook()
        {
            int id;
            Book book;
            bool answer;

            do
            {
                id = view.SearchBook();
                book = data.ReadId(id);
                if (book == null)
                {
                    view.ErrorID();
                }
                else
                {
                    answer = view.ConfirmRemoveBook(book);
                    if (answer)
                    {
                        data.Delete(id);
                    }
                }
            } while (book == null);  
        }
    }
}
