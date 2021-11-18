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
            return searchMovie;
        }
        public List<Movie> GetMovieListByTitle(string searchTitle) 
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
            Genre genre = Genre.Action; //default value so the compiler passes - must be a better way to do this.
            bool validGenre = false;
            while (!validGenre)
            {
                switch (userGenreSelection.Trim())
                {
                    case "1":
                        {
                            genre = (Genre)1;
                            validGenre = true;
                            break;
                        }
                    case "2":
                        {
                            genre = (Genre)2;
                            validGenre = true;
                            break;
                        }

                    case "3":
                        {
                            genre = (Genre)3;
                            validGenre = true;
                            break;
                        }
                    case "4":
                        {
                            genre = (Genre)4;
                            validGenre = true;
                            break;
                        }
                    case "5":
                        {
                            genre = (Genre)5;
                            validGenre = true;
                            break;
                        }
                    case "6":
                        {
                            genre = (Genre)6;
                            validGenre = true;
                            break;
                        }
                    default:
                        {
                            userGenreSelection = GetInput("\nInvalid Selection.  Please select a number of the genre above:  ");
                            continue;
                        }
                }
            }
            return genre;
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
                Console.WriteLine("\nInvalid input.  Please input \"y\" or \"n\".");
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
            Console.WriteLine("\n|---------------------------------------------------------------------------------------------------------------|");
            Console.WriteLine("|------------Title------------|----------Main Actor----------|--------Genre--------|----------Director----------|");
            Console.WriteLine("|---------------------------------------------------------------------------------------------------------------|");
            int i = 1;
            foreach (Movie m in movies.OrderBy(Movie => Movie.Title).ToList())
            {
                Console.WriteLine(string.Format("|{0, -28} | {1, -28} | {2,-19} | {3,-27}|", ($"[{i}] "+ m.Title), m.MainActor, m.Category, m.Director));
                i++;
            }
            Console.WriteLine("|---------------------------------------------------------------------------------------------------------------|\n");
        }
        public void UserMenu()
        {
            bool continueUserMenu = true;

            while (continueUserMenu)
            {
                string mainMenuSelection = GetInput($"\n        MAIN MENU\n    =================\n" +
                        $"[1] Display All Movies\n" +
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
                            Console.WriteLine("\nAll movies:");
                            PrintLists(MovieRepo.GetMoviesList);
                            continueUserMenu = ContinueLoop("Back to main menu?  [y] or [n]:  ");
                            break;
                        }
                    case "2":
                        {
                            Movie printMovie = GetMovieByTitle(GetInput("\nSearch by movie title: "));
                            if (printMovie.Title != null)
                            {
                                Console.WriteLine("\n" + printMovie);
                                continueUserMenu = ContinueLoop("Back to main menu?  [y] or [n]:  ");
                            }
                            else
                            {
                                Console.WriteLine("\nThat title was not found.  Please try again.");
                            }
                            break;
                        }
                    case "3":
                        {
                            string actorSearch = GetInput("\nSearch by Main Actor: ");
                            List<Movie> actorList = GetMovieListByActor(actorSearch);
                            if (actorList.Count > 0)
                            {
                                Console.WriteLine("\nActor Search Results:");
                                PrintLists(actorList);
                                continueUserMenu = ContinueLoop("Back to main menu?  [y] or [n]:  ");
                            }
                            else
                            {
                                Console.WriteLine("\nI'm sorry, no movies contain that actor.");
                            }
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("\nGenres:\n");
                            int i = 0;
                            foreach (Genre g in Enum.GetValues(typeof(Genre)))
                            {
                                Console.WriteLine($"{i + 1}: " + g);
                                i++;
                            }
                            Genre searchGenre = UserSelectedGenre(GetInput("\nPlease select search genre [number]: "));
                            Console.WriteLine($"\nList of {searchGenre} Movies:");
                            PrintLists(GetMovieListByGenre(searchGenre));
                            continueUserMenu = ContinueLoop("Back to main menu?  [y] or [n]:  ");
                            break;
                        }
                    case "5":
                        {
                            List<Movie> dirList = (GetMovieListByDirector(GetInput("\nSearch by director:  ")));
                            if (dirList.Count > 0)
                            {
                                PrintLists(dirList);
                                continueUserMenu = ContinueLoop("Back to main menu?  [y] or [n]:  ");
                            }
                            else
                            {
                                Console.WriteLine("\nI'm sorry, no movies were found with that director.  Please try again.");
                            }
                            break;
                        }
                    case "6":
                        {
                            string userPass;
                            if (!IsAdmin)
                            {
                                userPass = GetInput("\nRESTRICTED\n\nPlease input password to continue:  ");
                                if (userPass == Admin.GetAdminPassword)
                                {
                                    IsAdmin = true;
                                }
                            }
                            if (IsAdmin)
                            {

                                Admin admin = new Admin();
                                admin.AdminMenu();
                            }
                            else
                            {
                                Console.WriteLine("\nThat password does not match - back to main menu.");
                            }
                            break;
                        }
                    case "7":
                        {
                            Console.WriteLine("\nExiting...");
                            continueUserMenu = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\nInvalid Selection---");
                            UserMenu();
                            break;
                        }
                }
            }
        }
    }
    
}
