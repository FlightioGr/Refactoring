
using System;

namespace LegacyApp.Consumer
{
    /*
    *  DOT NOT CHANGE THIS FILE AT ALL
    */
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ProveAddUser(args);
        }

        public static void ProveAddUser(string[] args)
        {
            var userService = new UserService();
            var addResult = userService.AddUser("Mohsen", "Taei", "mohsen.taei@gmail.com", new DateTime(1985, 9, 11), 3);
            Console.WriteLine("Adding Mohsen Taei was " + (addResult ? "successful" : "unsuccessful"));
        }
    }
}
