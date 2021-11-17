using System;
using System.Collections.Generic;
using System.Text;

namespace midterm_abeer
{
    public class MovieRepo
    {
        public static List<Movie> Movies { get; set; } = new List<Movie>()
        {
        new Movie("Predator", "Arnold Schwarzenegger", Genre.Action, "John McTiernan"),
        new Movie("The Departed", "Leo DiCaprio", Genre.Drama, "Martin Scorsese"),
        new Movie("Casablanca", "Ingrid Bergman", Genre.Romance, "Michael Curtiz"),
        new Movie("Billy Madison", "Adam Sandler", Genre.Comedy, "Tamra Davis"),
        new Movie("The Shining", "Jack Nicholson", Genre.Horror, "Stanley Kubrick"),
        new Movie("Toy Story", "Tom Hanks", Genre.Animated, "John Lassiter")
        };

        public static List<Movie> GetMoviesList()
        {
            List<Movie> listGetter = Movies;
            return listGetter;
        }
        public static void AddToMoviesList(Movie m)
        { 

        
        }
    }
}
