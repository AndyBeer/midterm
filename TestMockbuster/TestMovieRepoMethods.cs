using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using midterm_abeer;

namespace TestMockbuster
{
    public class TestMovieRepoMethods
    {
        [Fact]
        public void TestGetMoviesMethod()
        {
            var expected = MovieRepo.GetMoviesList;
            var actual = MovieRepo.GetMoviesList;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TestGetMoviesMethodAfterAddingMovie()
        {
            var expected = MovieRepo.GetMoviesList;
            Movie m = new Movie("You've Got Mail", "Meg Ryan", Genre.Romance, "Nora Ephron");
            MovieRepo.GetMoviesList.Add(m);
            var actual = MovieRepo.GetMoviesList;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TestGetMoviesMethodAfterAddingAndRemovingMovieViaMethod() //this is where this gets tricky -
                                                                   //I am updating the properties of the MovieRepo class directly w my admin methods
                                                                   //Checking w Riley to see if I should refactor to avoid calling list directly
        {
            Admin a = new Admin();
            var expected = MovieRepo.GetMoviesList;
            Movie m = new Movie("You've Got Mail", "Meg Ryan", Genre.Romance, "Nora Ephron");
            a.AddMovieToList(m);
            a.RemoveMovieFromList(expected[1]);

            var actual = MovieRepo.GetMoviesList;

            Assert.Equal(expected, actual);
        }
    }
}
