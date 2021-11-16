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
            {}
        public string Password { get; set; } = "Password123";

        public void AddMovieToList(Movie m) //I probably would have designed this to take a string, so we could verify on entry of title in Program
                                            //- we can still do this by creating a mostly empty movie obj w just a title, then run this method at that time.
                                            //also, void methods are going to kill me w unit testing 

        {
            bool newMovie = true;
            for (int i = 0; i < MovieRepo.Movies.Count; i++)
            {
                if (MovieRepo.Movies[i].Title.ToLower() == m.Title.ToLower())
                {
                    newMovie = false;
                    Console.WriteLine("That movie already exists - here are the current movies available:\n");
                    PrintLists(MovieRepo.Movies);
                    break;
                }
                else
                {
                    continue;
                }
            }
            if (newMovie)
            {
                MovieRepo.Movies.Add(m);
                Console.WriteLine($"{m.Title} does not currently exist - let's add it!");
            }
        }
        public void UpdateExistingMovie(Movie m)  //going off the assumption that the user will already see a list,
                                                  //select the movie they want to edit, allowing us to get an existing movie
                                                  //Do we also need validation if the new inputs are of a certain length?  Probably.
                                                  //Could you return a string, and then have another method update the movie object????
        {
            string response = GetInput($"\n{m}\nWhat would you like to update?\n[1] Title\n[2] Main Actor\n[3] Genre\n[4] Director\n[5] Cancel\n");
            switch (response)
            {
                case "1":
                    {
                        string newTitle = GetInput("Please enter new Title:  ");

                        if (newTitle.ToLower().Trim() != m.Title.ToLower())
                        {
                            m.Title = newTitle;
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
                        string newActor = GetInput("Please enter new Main Actor:  ");

                        if (newActor.Any(char.IsDigit) || newActor.Length < 3)
                        {
                            Console.WriteLine("That name does not meet requirements - no changes have been made.");
                        }
                        else if (newActor.ToLower().Trim() != m.MainActor.ToLower() && newActor.Length > 3)
                        {
                            m.MainActor = newActor;
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
                        string userGenre = GetInput($"Please select new genre from the list above:  ");

                        if (userGenre.ToLower().Trim() == "1" || userGenre.ToLower().Trim() == "action")
                        {
                            m.Category = Genre.Action;
                            Console.WriteLine($"{m.Title} - genre successfully updated to {m.Category}.");
                        }
                        else if (userGenre.ToLower().Trim() == "2" || userGenre.ToLower().Trim() == "animated")
                        {
                            m.Category = Genre.Animated;
                            Console.WriteLine($"{m.Title} - genre successfully updated to {m.Category}.");
                        }
                        else if (userGenre.ToLower().Trim() == "3" || userGenre.ToLower().Trim() == "comedy")
                        {   
                            m.Category = Genre.Comedy;
                            Console.WriteLine($"{m.Title} - genre successfully updated to {m.Category}.");
                        }

                        else if (userGenre.ToLower().Trim() == "4" || userGenre.ToLower().Trim() == "drama")
                        { 
                            m.Category = Genre.Drama;
                            Console.WriteLine($"{m.Title} - genre successfully updated to {m.Category}.");
                        }

                        else if (userGenre.ToLower().Trim() == "5" || userGenre.ToLower().Trim() == "horror")
                        {   
                            m.Category = Genre.Horror;
                            Console.WriteLine($"{m.Title} - genre successfully updated to {m.Category}.");
                        }

                        else if (userGenre.ToLower().Trim() == "6" || userGenre.ToLower().Trim() == "romance")
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
        public void RemoveMovieFromList(Movie m) 
        {
            bool movieExists = true;
            Movie removeMe = new Movie();
            foreach (Movie currentMovie in MovieRepo.Movies)
            {
                if (m == currentMovie) 
                {
                    Console.WriteLine($"{currentMovie.Title} has been removed from the list.");
                    removeMe = currentMovie;//gotta be a better way than this.
                    movieExists = false;
                }
            }
            if (movieExists)
            {
                Console.WriteLine("That movie is not on the list.  Please add via the \"Add Movie\" option in the Admin menu if needed.");
            }
            else if (!movieExists)
            {
                MovieRepo.Movies.Remove(removeMe);
            }
            
        }

        public void AdminMenu()
        {
            string adminSelect = GetInput("~~Admin Menu~~\nPlease select one of the following menu items:\n" +
                "[1] Add Movie to List\n[2] Edit Existing Movie Info\n[3] Re-movie Da Movie\n[4] Exit to Main Menu\n\nSelection:  ");
            switch (adminSelect)
            {
                case "1":
                    {
                        Movie addMovie = new Movie();
                        addMovie.Title = GetInput("New Movie Title:  ");
                        AddMovieToList(addMovie);
                        break;
                    }
                case "2":
                    {
                        string searchByNumOrTitle = GetInput("[1] Select by Number from Current Movie List\n[2] Search by Movie Title\n");

                        if (searchByNumOrTitle.Trim() == "1")
                        {
                            int movieNum = -1;
                            Movie movieToEdit = new Movie();
                            Console.WriteLine("Full Movie List:");
                            int i = 1;
                            foreach (Movie m in MovieRepo.Movies)
                            {
                                Console.WriteLine($"[{i}] " + m);
                                i++;
                            }
                            string movieSelect = GetInput("Movie # to edit:  ");
                            try
                            {
                                movieNum = int.Parse(movieSelect) - 1;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Invalid selection.  " + e.Message);
                            }
                            try
                            {
                                movieToEdit = MovieRepo.Movies[movieNum];
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Invalid selection.  " + e.Message);
                            }
                            UpdateExistingMovie(movieToEdit);
                        }
                        else if (searchByNumOrTitle.Trim() == "2")
                        {
                            Movie movieToEdit = GetMovieByTitle(GetInput("Search Title:  "));
                            if (movieToEdit != null)
                            {
                                UpdateExistingMovie(movieToEdit);
                            }
                        }
                        else
                        {
                            searchByNumOrTitle = GetInput("Invalid Selection.  Please try again.");
                        }
                        break;
                    }
                case "3":
                    {
                        string searchByNumOrTitle = GetInput("[1] Select by Number from Current Movie List\n[2] Search by Movie Title\n");

                        if (searchByNumOrTitle.Trim() == "1")
                        {
                            int movieNum = -1;
                            Movie movieToRemove = new Movie();
                            Console.WriteLine("Full Movie List:");
                            int i = 1;
                            foreach (Movie m in MovieRepo.Movies)
                            {
                                Console.WriteLine($"[{i}] " + m);
                                i++;
                            }
                            string movieSelect = GetInput("Movie # to remove:  ");
                            try
                            {
                                movieNum = int.Parse(movieSelect);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Invalid selection.  " + e.Message);
                            }
                            try
                            {
                                movieToRemove = MovieRepo.Movies[movieNum-1];
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Invalid selection.  " + e.Message);
                            }
                            RemoveMovieFromList(movieToRemove);
                        }
                        else if (searchByNumOrTitle.Trim() == "2")
                        {
                            Movie movieToRemove = GetMovieByTitle(GetInput("Search Title:  "));
                            if (movieToRemove != null)
                            {
                                RemoveMovieFromList(movieToRemove); 
                            }
                        }
                        else
                        {
                            searchByNumOrTitle = GetInput("Invalid Selection.  Please try again.");
                        }
                        break;
                    }
                case "4":
                    {
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid response.\n\n");
                        AdminMenu();
                        break;
                    }
            }
        }
    }
}
