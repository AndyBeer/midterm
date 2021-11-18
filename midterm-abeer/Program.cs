using System;
using System.Collections.Generic;

namespace midterm_abeer
{
    class Program
    {
        static void Main(string[] args)
        {   
            Console.WriteLine("Welcome to MockBuster!\n");

            User user = new User();
            user.UserName = User.GetInput("Please enter your User Name below.\n\nUsername: ");
            Console.WriteLine($"\nWelcome {user.UserName}!");

            user.UserMenu();

            Console.WriteLine("\nThanks, goodbye!");
        }
    }
}
