using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace midterm_abeer
{
    public class Admin : User
    {
        public Admin(string UserName) : base(UserName)
        {
            this.IsAdmin = true;
        }
        public Admin() : base()
        {
            this.IsAdmin = true;
        }
        private static string Password = "Password123";
        public static string GetAdminPassword
        {
            get { return Password; }
            set { Password = value; }
        }

        public void AddMovieToList(Movie m) 
        {
            bool newMovie = true;
            for (int i = 0; i < MovieRepo.GetMoviesList.Count; i++)
            {
                if (MovieRepo.GetMoviesList[i].Title.ToLower() == m.Title.ToLower())
                {
                    newMovie = false;
                    break;
                }
                else
                {
                    continue;
                }
            }
            if (newMovie && m.Title.Length > 3)
            {
                Console.WriteLine($"\n{m.Title} does not currently exist - let's add it!");
                m.MainActor = " "; //If I didnt do this, I would get null exceptions
                m.Director = " ";  //If I didnt do this, I would get null exceptions
                MovieRepo.GetMoviesList.Add(m);
            }
            else if (!newMovie)
            {
                Console.WriteLine("\nThat movie already exists - here are the current movies available:");
                PrintLists(MovieRepo.GetMoviesList);
                //AdminMenu();
            }
            else
            {
                Console.WriteLine("\nThat title is invalid.  Movie not saved.");
                //AdminMenu();
            }
        }
        public void UpdateExistingMovie(Movie m)  
        {
            string response = GetInput($"\n{m}\nWhat would you like to update?\n[1] Title\n[2] Main Actor\n[3] Genre\n[4] Director\n[5] Cancel\n");
            switch (response)
            {
                case "1":
                    {
                        UpdateMovieTitle(m);
                        break;
                    }
                case "2":
                    {
                        UpdateMovieActor(m);
                        break;
                    }
                case "3":
                    {
                        UpdateMovieGenre(m);
                        break;
                    }
                case "4":
                    {
                        UpdateMovieDir(m);
                        break;
                    }
                case "5":
                    {
                        Console.WriteLine("\nExiting update menu.");
                        break;
                    }
                default:
                    {
                        response = GetInput($"Invalid response.  \nWhat would you like to update?\n[1] Title\n[2] Main Actor\n[3] Genre\n[4] Director\n[5] Cancel\n");
                        break;
                    }
            }
        }
        public void UpdateExistingMovie(Movie m, string response, string newString) //override created for unit testing
        {
            switch (response)
            {
                case "1":
                    {
                        if (newString.ToLower().Trim() != m.Title.ToLower())
                        {
                            m.Title = newString;
                            Console.WriteLine($"Title updated: {m.Title}\n");
                        }
                        else
                        {
                            Console.WriteLine("This matches the existing title - No changes have been made.");
                        }
                        break;
                    }
                case "2":
                    {
                        if (newString.Any(char.IsDigit) || newString.Length < 3)
                        {
                            Console.WriteLine("That name does not meet requirements - no changes have been made.");
                        }
                        else if (newString.ToLower().Trim() != m.MainActor.ToLower() && newString.Length > 3)
                        {
                            m.MainActor = newString;
                            Console.WriteLine($"Main Actor for {m.Title} has been updated to: {m.MainActor}");
                        }
                        else
                        {
                            Console.WriteLine($"This person is already listed as the main actor in {m.Title} - No changes have been made.");
                        }
                        break;
                    }
                case "3":
                    {
                        Console.WriteLine($"{m.Title} is currently listed as a {m.Category}.  Here are all possible genres:");
                        int i = 1;
                        foreach (Genre g in Enum.GetValues(typeof(Genre)))
                        {
                            Console.WriteLine($"[{i}] " + g);
                            i++;
                        }
                       
                        if (newString.ToLower().Trim() == "1" || newString.ToLower().Trim() == "action")
                        {
                            m.Category = Genre.Action;
                            Console.WriteLine($"{m.Title} - genre successfully updated to {m.Category}.");
                        }
                        else if (newString.ToLower().Trim() == "2" || newString.ToLower().Trim() == "animated")
                        {
                            m.Category = Genre.Animated;
                            Console.WriteLine($"{m.Title} - genre successfully updated to {m.Category}.");
                        }
                        else if (newString.ToLower().Trim() == "3" || newString.ToLower().Trim() == "comedy")
                        {
                            m.Category = Genre.Comedy;
                            Console.WriteLine($"{m.Title} - genre successfully updated to {m.Category}.");
                        }

                        else if (newString.ToLower().Trim() == "4" || newString.ToLower().Trim() == "drama")
                        {
                            m.Category = Genre.Drama;
                            Console.WriteLine($"{m.Title} - genre successfully updated to {m.Category}.");
                        }

                        else if (newString.ToLower().Trim() == "5" || newString.ToLower().Trim() == "horror")
                        {
                            m.Category = Genre.Horror;
                            Console.WriteLine($"{m.Title} - genre successfully updated to {m.Category}.");
                        }

                        else if (newString.ToLower().Trim() == "6" || newString.ToLower().Trim() == "romance")
                        {
                            m.Category = Genre.Romance;
                            Console.WriteLine($"{m.Title} - genre successfully updated to {m.Category}.");
                        }

                        else
                            Console.WriteLine($"Invalid selection. {m.Title} will remain a {m.Category.ToString().ToLower()}.");
                        break;
                    }
                case "4":
                    {
                        string newDir = GetInput($"{m.Title}'s current Director: {m.Director}\n");
                        if (newDir.Any(char.IsDigit) || newDir.Length < 3)
                        {
                            Console.WriteLine("That name does not meet requirements - no changes have been made.");
                        }
                        else if (newDir.ToLower().Trim() != m.MainActor.ToLower() && newDir.Length > 3)
                        {
                            m.Director = newDir;
                            Console.WriteLine($"Director for {m.Title} has been updated to: {m.Director}");
                        }
                        else
                        {
                            Console.WriteLine($"This person is already listed as the director for {m.Title} - No changes have been made.");
                        }
                        break;
                    }
                case "5":
                    {
                        Console.WriteLine("Exiting menu...\n");
                        break;
                    }
                default:
                    {
                        response = GetInput($"Invalid response.  \nWhat would you like to update?\n[1] Title\n[2] Main Actor\n[3] Genre\n[4] Director\n[5] Cancel\n");
                        break;
                    }

            }
        }
        public void UpdateMovieTitle(Movie m)
        {
            string newTitle = GetInput("Please enter new Title:  ");

            if (newTitle.ToLower().Trim() != m.Title.ToLower())
            {
                m.Title = newTitle;
                Console.WriteLine($"Title updated to: {m.Title}\n");
            }
            else
            {
                Console.WriteLine("This matches the existing title - No changes have been made.");
            }
        }
        public void UpdateMovieActor(Movie m)
        {
            string newActor = GetInput("Please enter new Main Actor:  ");

            if (newActor.Any(char.IsDigit) || newActor.Length < 3)
            {
                Console.WriteLine("\nThat name does not meet requirements - no changes have been made.");
            }
            else if (newActor.ToLower().Trim() != m.MainActor.ToLower() && newActor.Length > 3)
            {
                m.MainActor = newActor;
                Console.WriteLine($"\nMain Actor for {m.Title} has been updated to: {m.MainActor}");
            }
            else
            {
                Console.WriteLine($"\nThis person is already listed as the main actor in {m.Title} - No changes have been made.");
            }
        }
        public void UpdateMovieGenre(Movie m)
        {
            Console.WriteLine($"\n{m.Title} is currently listed as \"{m.Category}\".  Here are all possible genres:");
            int i = 1;
            foreach (Genre g in Enum.GetValues(typeof(Genre)))
            {
                Console.WriteLine($"[{i}] " + g);
                i++;
            }
            string userGenre = GetInput("Please select new genre from the list above:  ");

            if (userGenre.ToLower().Trim() == "1" || userGenre.ToLower().Trim() == "action")
            {
                m.Category = Genre.Action;
                Console.WriteLine($"\n{m.Title} - genre successfully updated to {m.Category}.");
            }
            else if (userGenre.ToLower().Trim() == "2" || userGenre.ToLower().Trim() == "animated")
            {
                m.Category = Genre.Animated;
                Console.WriteLine($"\n{m.Title} - genre successfully updated to {m.Category}.");
            }
            else if (userGenre.ToLower().Trim() == "3" || userGenre.ToLower().Trim() == "comedy")
            {
                m.Category = Genre.Comedy;
                Console.WriteLine($"\n{m.Title} - genre successfully updated to {m.Category}.");
            }
            else if (userGenre.ToLower().Trim() == "4" || userGenre.ToLower().Trim() == "drama")
            {
                m.Category = Genre.Drama;
                Console.WriteLine($"\n{m.Title} - genre successfully updated to {m.Category}.");
            }
            else if (userGenre.ToLower().Trim() == "5" || userGenre.ToLower().Trim() == "horror")
            {
                m.Category = Genre.Horror;
                Console.WriteLine($"\n{m.Title} - genre successfully updated to {m.Category}.");
            }

            else if (userGenre.ToLower().Trim() == "6" || userGenre.ToLower().Trim() == "romance")
            {
                m.Category = Genre.Romance;
                Console.WriteLine($"\n{m.Title} - genre successfully updated to {m.Category}.");
            }
            else
                Console.WriteLine($"Invalid selection. {m.Title} will remain a {m.Category.ToString().ToLower()}.");
        }
        public void UpdateMovieDir(Movie m)
        {
            string newDir = GetInput($"\nPlease enter new Director:  ");
            if (newDir.Any(char.IsDigit) || newDir.Length < 3)
            {
                Console.WriteLine("That name does not meet requirements - no changes have been made.");
            }
            else if (newDir.ToLower().Trim() != m.MainActor.ToLower() && newDir.Length > 3)
            {
                m.Director = newDir;
                Console.WriteLine($"\nDirector for {m.Title} has been updated to: {m.Director}");
            }
            else
            {
                Console.WriteLine($"This person is already listed as the director for {m.Title} - No changes have been made.");
            }
        }
        public void RemoveMovieFromList(Movie m) 
        {
            bool movieExists = false;

            foreach (Movie currentMovie in MovieRepo.GetMoviesList)
            {
                if (m == currentMovie) 
                {
                    movieExists = true;
                    Console.WriteLine(currentMovie);
                    Console.WriteLine($"\n{currentMovie.Title} has been removed from the list.");
                    MovieRepo.GetMoviesList.Remove(currentMovie);
                    break;
                } 
            }
            if (!movieExists)
            {
                Console.WriteLine("\nThat movie is not on the list.  Please add via the \"Add Movie\" option in the Admin menu if needed.");
            }
        }

        public void AdminMenu()
        {
            string adminSelect = GetInput("\n~~Admin Menu~~\nPlease select one of the following menu items:\n\n" +
                "[1] Add Movie to List\n[2] Edit Existing Movie Info\n[3] Re-movie Da Movie (Remove)\n[4] Exit to Main Menu\n\nSelection:  ");
            switch (adminSelect)
            {
                case "1":
                    {
                        Movie addMovie = new Movie();
                        addMovie.Title = GetInput("New Movie Title:  ");
                        AddMovieToList(addMovie);
                        if (MovieRepo.GetMoviesList.Contains(addMovie))
                        {
                            UpdateMovieActor(addMovie);
                            UpdateMovieGenre(addMovie);
                            UpdateMovieDir(addMovie);
                            Console.WriteLine($"\n{addMovie.Title} successfully added.");
                            PrintLists(MovieRepo.GetMoviesList);
                        }
                        break;
                    }
                case "2":
                    {
                        string searchByNum = GetInput("[1] Select by Number from Current Movie List\n[2] Search by Movie Title\n");

                        if (searchByNum.Trim() == "1")
                        {
                            int movieNum = -1;
                            Console.WriteLine("\nFull Movie List:");
                            List<Movie> sorted = MovieRepo.GetMoviesList.OrderBy(Movie => Movie.Title).ToList();
                            PrintLists(sorted);
                            string movieSelect = GetInput("\nMovie # to edit:  ");
                            try
                            {
                                movieNum = int.Parse(movieSelect) - 1;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("\nInvalid selection.  \n" + e.Message+"\n\nBack to the main menu.");
                                break;
                            }
                            try
                            {
                                UpdateExistingMovie(sorted[movieNum]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("\nInvalid selection.  \n" + e.Message + "\n\nBack to the main menu.");
                                break;
                            }
                        }
                        else if (searchByNum.Trim() == "2")
                        {
                            Movie movieToEdit = GetMovieByTitle(GetInput("Search Title:  "));
                            if (movieToEdit.Title != null)
                            {
                                UpdateExistingMovie(movieToEdit);
                            }
                            else
                            {
                                Console.WriteLine("\nMovie not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid Selection.  Please try again.");
                            break;
                        }
                        break;
                    }
                case "3":
                    {
                        string searchByNum = GetInput("\n[1] Select by Number from Current Movie List\n[2] Search by Movie Title\n");

                        if (searchByNum.Trim() == "1")
                        {
                            int movieNum = -1;
                            Console.WriteLine("\nFull Movie List:");
                            List<Movie> sorted = MovieRepo.GetMoviesList.OrderBy(Movie => Movie.Title).ToList();
                            PrintLists(sorted);
                            string movieSelect = GetInput("\nMovie # to remove:  ");
                            try
                            {
                                movieNum = int.Parse(movieSelect) - 1;
                                if (movieNum > MovieRepo.GetMoviesList.Count)
                                {
                                    Console.WriteLine("\nThat is not a valid selection.\nBack to the main menu.");
                                    break;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("\nInvalid selection.  \n" + e.Message+"\n\nBack to the main menu.");
                                break;
                            }
                            try
                            {
                                if (ContinueLoop("Are you sure you want to remove this movie? [y] or [n]:  ") == true)
                                {
                                    RemoveMovieFromList(sorted[movieNum]);
                                }
                                else 
                                {
                                    Console.WriteLine($"{sorted[movieNum].Title} has NOT been removed.");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("\nInvalid selection.  \n" + e.Message + "\n\nBack to the main menu.");
                                break;
                            }
                        }
                        else if (searchByNum.Trim() == "2")
                        {
                            Movie movieToRemove = GetMovieByTitle(GetInput("\nSearch Title:  "));
                            if (movieToRemove.Title != null)
                            {
                                RemoveMovieFromList(movieToRemove); 
                            }
                            else
                            {
                                Console.WriteLine("\nMovie not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid Selection.  Please try again.");
                            break;
                        }
                        break;
                    }
                case "4":
                    {
                        break;
                    }
                default:
                    {
                        Console.WriteLine("\nInvalid response.  Please enter a number between 1 and 4 to continue.");
                        break;
                    }
            }
        }
    }
}
