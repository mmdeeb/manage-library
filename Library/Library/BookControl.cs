using Library_Domain;
using LibraryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMain
{
    public class BookControl
    {
        public static void AddBook(List<Book> list)
        {
            using (var context = new LibraryDbContext())
            {
                context.Books.AddRange(list);
                context.SaveChanges();
            }
        }

        public static List<Book> Get()
        {
            using (var context = new LibraryDbContext())
            {
                var books = context.Books.Where(B => B.IsCompleted == true).ToList();
                return books;
            }
        }

        public static Book Get(int Id)
        {
            using (var context = new LibraryDbContext())
            {
                var book = context.Books.Where(b => (b.Id == Id && b.IsCompleted == true)).ToList();
                return book[0];
            }
        }

        public static void Remove()
        {
            var books = Get();
            foreach (var book in books)
            {
                RemoveBook(book.Id);
            }
        }

        public static void RemoveBook(int Id)
        {
            using (var context = new LibraryDbContext())
            {
                var Book = Get(Id);
                Book.IsCompleted = false;
                context.Books.Update(Book);
                context.SaveChanges();
            }
        }
        public static void UpdateBook(int id, string title, double price)
        {
            using (var context = new LibraryDbContext())
            {
                var book = Get(id);
                book.Title = title;
                book.Price = price;
                context.Books.Update(book);
                context.SaveChanges();
            }
        }
    }
}
