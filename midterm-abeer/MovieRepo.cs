using System;
using System.Collections.Generic;
using System.Text;

namespace midterm_abeer
{
    public class MovieRepo
    {
        public static List<Movie> Movies { get; set; } = new List<Movie>();
        public Movie predator = new Movie("Predator", "Arnold Schwarzenegger", Genre.Action, "John McTeirnan");
        public Movie theDeparted = new Movie("The Departed", "Leo DiCaprio", Genre.Drama, "Martin Scorsese");
        public Movie casablanca = new Movie("Casablanca", "Ingrid Bergman", Genre.Romance, "Michael Curtiz");
        public Movie billyMadison = new Movie("Billy Madison", "Adam Sandler", Genre.Comedy, "Tamra Davis");
        public Movie theShining = new Movie("The Shining", "Jack Nicholson", Genre.Horror, "Stanley Kubrick");
        public Movie toyStory = new Movie("Toy Story", "Tom Hanks", Genre.Animated, "John Lassiter");
        
        public MovieRepo()
        {
            Movies.Add(predator);
            Movies.Add(theDeparted);
            Movies.Add(casablanca);
            Movies.Add(billyMadison);
            Movies.Add(theShining);
            Movies.Add(toyStory);
        }
        public static List<Movie> GetMoviesList()
        {
            List<Movie> listGetter = Movies;
            return listGetter;
        }


    }
}
