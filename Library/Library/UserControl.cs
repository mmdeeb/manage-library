using Library_Domain;
using LibraryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMain
{
    public class UserControl
    {
        public static void AddUser(List<User>? defaultUsers)
        {
            using (var context = new LibraryDbContext())
            {
                context.Users.AddRange(defaultUsers);
                context.SaveChanges();
            }
        }
        public static List<User> GetUser()
        {
            using (var context = new LibraryDbContext())
            {
                var Users = context.Users.ToList();
                return Users;
            };
        }
        public static User Get(string username)
        {
            var users = GetUser();
            foreach (var user in users)
            {
                if (user.Username == username)
                {
                    return user;
                }
            }
            return new User();
        }
        public static bool FindUser(string? username, string? password)
        {
            var Users = GetUser();
            foreach (var user in Users)
            {
                if (username == user.Username && password == user.Password)
                {
                    return true;
                }
            }
            return false;

        }
    }
}
