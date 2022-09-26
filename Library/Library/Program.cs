using Library_Domain;
using LibraryData;
using LibraryLogging;
using LibraryMain;
using System.Text.Json;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Check();
            Console.WriteLine("Welcome!");
            homePage();
        }
        private static void Check()
        {
            using (var context = new LibraryDbContext())
            {
                context.Database.EnsureCreated();
            }
            
            if (BookControl.Get().Count() == 0)
            {
                var books = new List<Book>()
                        {
                            new Book(){Title = "Goodreads", Price = 50.5, NumberAvailable = 30,},
                            new Book(){Title = "Learn C sharp", Price = 20.6, NumberAvailable = 12},
                            new Book(){Title = "Robinson Crusoe", Price = 46.7, NumberAvailable = 4},
                            new Book(){Title = "Tom Jones by", Price = 23.7, NumberAvailable = 46},
                            new Book(){Title = "Emma", Price = 34, NumberAvailable = 1},
                            new Book(){Title = "Nightmare Abbey", Price = 38.6, NumberAvailable = 5},
                            new Book(){Title = "Sybil", Price = 78, NumberAvailable = 22},
                            new Book(){Title = " Jane Eyre", Price = 44, NumberAvailable = 22},
                            new Book(){Title = "Vanity Fair", Price = 75.6, NumberAvailable = 22},
                        };
                BookControl.AddBook(books);
            }
          
            if (UserControl.GetUser().Count() == 0)
            {
                String contents = File.ReadAllText("Users.json");
                var defaultEmployees = JsonSerializer.Deserialize<List<User>>(contents);
                UserControl.AddUser(defaultEmployees);
            }

        }
        private static bool LogIn()
        {
            bool isExist = true;
            string username = "";
            string password = "";
            while (isExist)
            {
                Console.WriteLine("Enter your username: ");
                username = Console.ReadLine();
                Console.WriteLine("Enter your password: ");
                password = Console.ReadLine();
                isExist = !UserControl.FindUser(username, password);
                if (isExist)
                    Console.WriteLine("Error: please try again");
                else
                {
                    ExtendLogger.FullName = UserControl.Get(username).FullName;
                }
            }
            Console.WriteLine($"Welcome {UserControl.Get(username).FullName} ,There are {UserControl.GetUser().Count()} books in the library");
            return !isExist;
        }
        private static void homePage()
        {
            if (LogIn())
            {
                string Exit = "";
                while (Exit != "6")
                {
                    int option = 0;
                    Console.WriteLine("options :");
                    Console.WriteLine("Enter 1 to view brief information about all the books we have available");
                    Console.WriteLine("Enter 2 to view the details of a specific book");
                    Console.WriteLine("Enter 3 to delete a specific book");
                    Console.WriteLine("Enter 4 to update a specific book");
                    Console.WriteLine("Enter 5 to switch account ");
                    Console.WriteLine("Enter 6 When you finish");
                    Console.WriteLine("Enter your option here: ");
                    option = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("--------------------");
                    switch (option)
                    {
                        case 1: briefInformation(); break;
                        case 2: details(); break;
                        case 3: delete(); break;
                        case 4: update(); break;
                        case 5: homePage(); break;
                        case 6: Exit = "6"; break;
                        default: Console.WriteLine("this option is not available"); break;
                    }
                }
            }
        }
        private static void briefInformation()
        {
            var books = BookControl.Get();
            int countOfAllCopies = 0;
            foreach (var book in books)
            {
                var summery = new KeyValuePair<int,string>(book.Id, book.Title);
                Console.WriteLine($"{summery} Number of copies: {book.NumberAvailable}");
                Console.WriteLine("--------------------");
                countOfAllCopies += book.NumberAvailable;
            }
            Console.WriteLine($"Total number of copies of books: {countOfAllCopies} ");
        }
        private static void details()
        {
            Console.WriteLine("Enter the book Id");
            int id = Convert.ToInt32(Console.ReadLine());
            var book =BookControl.Get(id);
            Console.WriteLine("--------------------");
            Console.WriteLine($"Id:{book.Id}, Title: {book.Title}, Price: {book.Price}, Number of copies: {book.NumberAvailable}");
            Console.WriteLine("--------------------");

        }
        private static void delete()
        {
            Console.WriteLine("Enter book ID");
            int id = Convert.ToInt32(Console.ReadLine());
            BookControl.RemoveBook(id);
            Console.WriteLine("Book is deleted");
            Console.WriteLine("--------------------");
        }
        private static void update()
        {
            Console.WriteLine("Enter book ID");
            int id = Convert.ToInt32(Console.ReadLine());
            string? title;
            double price;
            Console.WriteLine("Enter the new title");
            title = Console.ReadLine();
            Console.WriteLine("Enter the new pice");
            price = Convert.ToDouble(Console.ReadLine());
            BookControl.UpdateBook(id, title, price);
            Console.WriteLine("Book is updated");
            Console.WriteLine("--------------------");
        }
    }
}