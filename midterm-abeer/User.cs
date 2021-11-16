using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace midterm_abeer
{
    public class User
    {
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }

        public User(string UserName)
        {
            this.UserName = UserName;
            this.IsAdmin = false;
        }
        public User()
        { }
        public List<Movie> GetMovieListByGenre(Genre searchCategory)  //NOTE - this needs to validate if the list is empty - can be handled in Program if catList.Count = 0.
        {
            List<Movie> catList = new List<Movie>();
            for (int i = 0; i < MovieRepo.Movies.Count; i++)
            {

                if (MovieRepo.Movies[i].Category == searchCategory)
                {
                    catList.Add(MovieRepo.Movies[i]);
                }
            }
            return catList;
        }
        public Movie GetMovieByTitle(string searchTitle)    //NOTE - method is different, since titles will be unique in this app.

        {
            Movie searchMovie = new Movie();
            for (int i = 0; i < MovieRepo.Movies.Count; i++)
            {
                if (MovieRepo.Movies[i].Title.ToLower().Contains(searchTitle.ToLower().Trim()))
                {
                    searchMovie = MovieRepo.Movies[i];
                    break;
                }
            }
            if (searchMovie.Title != null)
                return searchMovie;
            else
            {
                Console.WriteLine("No movie found.  Please confirm your spelling and try again.");
                return GetMovieByTitle(GetInput("Movie Title:  "));
            }
        }
        public List<Movie> GetMovieListByActor(string searchActor)    //NOTE - this needs to validate if the list is empty - can be handled in Program if actorList.Count = 0.
        {
            List<Movie> actorList = new List<Movie>();
            for (int i = 0; i < MovieRepo.Movies.Count; i++)
            {
                if (MovieRepo.Movies[i].MainActor.ToLower().Contains(searchActor.ToLower().Trim()))
                {
                    actorList.Add(MovieRepo.Movies[i]);
                }
            }
            return actorList;
        }
        public List<Movie> GetMovieListByDirector(string searchDir)    //NOTE - this needs to validate if the list is empty - can be handled in Program if dirList.Count = 0.
        {
            List<Movie> dirList = new List<Movie>();
            for (int i = 0; i < MovieRepo.Movies.Count; i++)
            {

                if (MovieRepo.Movies[i].Director.ToLower().Contains(searchDir.ToLower().Trim()))
                {
                    dirList.Add(MovieRepo.Movies[i]);
                }
            }
            return dirList;
        }
        public Genre UserSelectedGenre(string userGenreSelection)
        {
            Genre searchGenre = Genre.Action;  //I think it needs an arbitrary default value to make the compiler happy.

            switch (userGenreSelection)
            {
                case "1":
                    {
                        searchGenre = Genre.Action;
                        return searchGenre;
                    }
                case "2":
                    {
                        searchGenre = Genre.Animated;
                        return searchGenre;
                    }

                case "3":
                    {
                        searchGenre = Genre.Comedy;
                        return searchGenre;
                    }
                case "4":
                    {
                        searchGenre = Genre.Drama;
                        return searchGenre;
                    }
                case "5":
                    {
                        searchGenre = Genre.Horror;
                        return searchGenre;
                    }
                case "6":
                    {
                        searchGenre = Genre.Romance;
                        return searchGenre;
                    }
                default:
                    {
                        userGenreSelection = GetInput("Invalid Selection.  Please select a number of the genre you wish to search:  ");
                        break;
                    }
            }
            return searchGenre;
        }

        public static bool ContinueLoop(string question)
        {
            string response = GetInput(question);
            if (response.ToLower() == "y")
            {
                return true;
            }
            else if (response.ToLower() == "n")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Invalid input.  Please input \"y\" or \"n\".\n");
                return ContinueLoop(question);
            }
        }
        public static string GetInput(string prompt)
        {
            Console.Write(prompt);
            string output = Console.ReadLine();
            return output;
        }
        public void PrintLists(List<Movie> movies)
        {
            foreach (Movie m in movies.OrderBy(Movie => Movie.Title).ToList())
            {
                Console.WriteLine(m);
            }
        }
        public void UserMenu()
        {
            string mainMenuSelection = GetInput($"What would you like to do?\n" +
                    $"[1] Display all movies\n" +
                    $"[2] Search by Title\n" +
                    $"[3] Search by Main Actor\n" +
                    $"[4] Filter by Genre\n" +
                    $"[5] Search by Director\n" +
                    $"[6] Admin Menu\n" +
                    $"[7] Exit\n");

            switch (mainMenuSelection)
            {
                case "1":
                    {
                        Console.WriteLine("All movies:");
                        PrintLists(MovieRepo.Movies); // I can call the static method in MovieRepo, but it isnt necessary - i can access the list directly
                        break;
                    }
                case "2":
                    {
                        Console.WriteLine(GetMovieByTitle(GetInput("Search by movie title: ")));
                        break;
                    }
                case "3":
                    {
                        List<Movie> actorList = (GetMovieListByActor(GetInput("Search by Main Actor")));
                        if (actorList.Count > 0)
                        {
                            PrintLists(actorList);
                        }
                        else
                        {
                            Console.WriteLine("I'm sorry, no movies contain that actor.\n");
                        }
                        break;
                    }
                case "4":
                    {
                        Console.WriteLine("Genres:\n");
                        int i = 0;
                        foreach (Genre g in Enum.GetValues(typeof(Genre)))
                        {
                            Console.WriteLine($"{i + 1}: " + g);
                            i++;
                        }
                        PrintLists(GetMovieListByGenre(UserSelectedGenre(GetInput("Please select search genre [number]: "))));
                        break;
                    }
                case "5":
                    {
                        PrintLists(GetMovieListByDirector(GetInput("Search by director:  ")));
                        break;
                    }
                case "6":
                    {
                        Admin admin = new Admin();
                        string userPass = User.GetInput("RESTRICTED\n\nPlease input password to continue:  ");
                        if (userPass == admin.Password)
                        {
                            admin.AdminMenu(); 
                        }
                        else
                        {
                            Console.WriteLine("That password does not match - back to main menu.");
                        }
                        break;
                    }
                case "7":
                    {
                        Console.WriteLine("Exiting...");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Selection---\n");
                        UserMenu();
                        break;
                    }
            }
        }
    }
    
}
