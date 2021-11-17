using System;
using System.Collections.Generic;

namespace midterm_abeer
{
    class Program
    {
        static void Main(string[] args)
        {
            //will need a new instance of movie repo to access the list of movies - couldnt figure out the arrangement to allow for class.list
            //I guess the static method GetMoviesList() basically does the same thing
            //MovieRepo movieDB = new MovieRepo();

            Console.WriteLine("Welcome to MockBuster!\n");

            User user = new User();
            user.UserName = User.GetInput("Please enter your User Name below.\n\nUsername: ");
            Console.WriteLine($"Welcome {user.UserName}!\n");
            bool keepGoing = true;
            while (keepGoing)
            {
                user.UserMenu();
                keepGoing = User.ContinueLoop("Back to Menu? [y] or [n]:  ");
            }
            Console.WriteLine("Thanks, goodbye!");
        }

    }
}
