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
            for (int i = 0; i < MovieRepo.GetMoviesList.Count; i++)
            {

                if (MovieRepo.GetMoviesList[i].Category == searchCategory)
                {
                    catList.Add(MovieRepo.GetMoviesList[i]);
                }
            }
            return catList;
        }
        public Movie GetMovieByTitle(string searchTitle)    //NOTE - method is different, since titles will be unique in this app.
                                                            //I'll make another method that returns a list of movies by the same title,
                                                            //but probably won't use it in the program.  If I used something like a movie ID# as a property,
                                                            //then returning a list of movie titles would make more functional sense.
        {
            Movie searchMovie = new Movie();
            for (int i = 0; i < MovieRepo.GetMoviesList.Count; i++)
            {
                if (MovieRepo.GetMoviesList[i].Title.ToLower().Contains(searchTitle.ToLower().Trim()))
                {
                    searchMovie = MovieRepo.GetMoviesList[i];
                    break;
                }
            }
            if (searchMovie.Title != null)
                return searchMovie;
            else
            {
                Console.WriteLine("No movie found.  Please confirm your spelling and try again.");
                return GetMovieByTitle(GetInput("Movie Title:  ")); //This line loops this menu - different process than the actor search
            }
        }
        public List<Movie> GetMovieListByTitle(string searchTitle)  //still needs unit test
        {
            List<Movie> titleList = new List<Movie>();
            for (int i = 0; i < MovieRepo.GetMoviesList.Count; i++)
            {
                if (MovieRepo.GetMoviesList[i].Title.ToLower().Contains(searchTitle.ToLower().Trim()))
                {
                    titleList.Add(MovieRepo.GetMoviesList[i]);
                }
            }
            return titleList;
        }
        public List<Movie> GetMovieListByActor(string searchActor)    //NOTE - this needs to validate if the list is empty - can be handled in Program if actorList.Count = 0.
        {
            List<Movie> actorList = new List<Movie>();
            for (int i = 0; i < MovieRepo.GetMoviesList.Count; i++)
            {
                if (MovieRepo.GetMoviesList[i].MainActor.ToLower().Contains(searchActor.ToLower().Trim()))
                {
                    actorList.Add(MovieRepo.GetMoviesList[i]);
                }
            }
            return actorList;
        }
        public List<Movie> GetMovieListByDirector(string searchDir)    //NOTE - this needs to validate if the list is empty - can be handled in Program if dirList.Count = 0.
        {
            List<Movie> dirList = new List<Movie>();
            for (int i = 0; i < MovieRepo.GetMoviesList.Count; i++)
            {

                if (MovieRepo.GetMoviesList[i].Director.ToLower().Contains(searchDir.ToLower().Trim()))
                {
                    dirList.Add(MovieRepo.GetMoviesList[i]);
                }
            }
            return dirList;
        }
        public Genre UserSelectedGenre(string userGenreSelection)
        {
            Genre searchGenre = Genre.Action;   //I think it needs an arbitrary default value to make the compiler happy.
                                 //JK, this is breaking your code

            bool validGenre = false;
            while (!validGenre)
            {
                switch (userGenreSelection.Trim())
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
                            userGenreSelection = GetInput("Invalid Selection.  Please select a number of the genre above:  ");
                            continue;
                        }
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
            string mainMenuSelection = GetInput($"\nWhat would you like to do?\n" +
                    $"[1] Display all movies\n" +
                    $"[2] Search by Title\n" +
                    $"[3] Search by Main Actor\n" +
                    $"[4] Filter by Genre\n" +
                    $"[5] Search by Director\n" +
                    $"[6] Admin Menu\n" +
                    $"[7] Exit\n" +
                    "[ ]: ");

            switch (mainMenuSelection)
            {
                case "1":
                    {
                        Console.WriteLine("All movies:");
                        PrintLists(MovieRepo.GetMoviesList); 
                        break;
                    }
                case "2":
                    {
                        Console.WriteLine(GetMovieByTitle(GetInput("Search by movie title: ")));
                        break;
                    }
                case "3":
                    {
                        string actorSearch = GetInput("Search by Main Actor: ");
                        List<Movie> actorList = GetMovieListByActor(actorSearch);
                        if (actorList.Count > 0)
                        {
                            Console.WriteLine($"Movies starring {actorSearch}:\n");
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
                        Genre searchGenre = UserSelectedGenre(GetInput("Please select search genre [number]: "));
                        Console.WriteLine($"\nList of {searchGenre.ToString()} Movies:\n");
                        PrintLists(GetMovieListByGenre(searchGenre));
                        break;
                    }
                case "5":
                    {
                        List <Movie> dirList = (GetMovieListByDirector(GetInput("Search by director:  ")));
                        if (dirList.Count > 0)
                        {
                            PrintLists(dirList);
                        }
                        else
                        {
                            Console.WriteLine("I'm sorry, no movies directed by that person.\n");
                        }
                        break;
                    }
                case "6":
                    {
                        Admin admin = new Admin();
                        string userPass;
                        if (!IsAdmin)
                        {
                            userPass = GetInput("RESTRICTED\n\nPlease input password to continue:  ");
                            if (userPass == Admin.GetAdminPassword)
                            {
                                IsAdmin = true;
                            }
                        }
                        if (IsAdmin)
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
