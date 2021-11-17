using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using midterm_abeer;

namespace TestMockbuster
{
    public class TestUserMethods
    {
        [Theory]    //Using the index of the list living in MovieRepo.Movies
                    //Testing the search movie by title conversion to still return a valid movie in different string format.
        [InlineData("predator", 0)]
        [InlineData("the DEPARTED  ", 1)]
        [InlineData("CAsaBlanca", 2)]
        [InlineData("billy madison", 3)]
        [InlineData("The Shining                                            ", 4)]
        [InlineData("Toy Story", 5)]
        public void SearchByMovieTitle_MovieFound(string searchTitle, int expected)
        {
            User u = new User();
            Movie expMovie = MovieRepo.GetMoviesList[expected];

            Movie actual = u.GetMovieByTitle(searchTitle);

            Assert.Equal(expMovie, actual);
        }

        [Theory]
        [InlineData("   Arnold Schwarzenegger","Predator")]
        [InlineData("LEO DICAPRIO", "The Departed")]
        [InlineData("ingrid BERGMAN", "Casablanca")]
        [InlineData("aDaM SaNdLeR   ", "Billy Madison")]
        [InlineData("Jack Nicholson   ", "The Shining")]
        [InlineData("Tom Hanks", "Toy Story")]
        public void TestGetMovieListByActor(string actorInput, string expected)
        {
            User u = new User();

            List<Movie> actorList = u.GetMovieListByActor(actorInput);
            string actualTitle = actorList[0].Title;    //Hard-coded the index, since we do not have a large list of movies - should return exactly 1 movie because of this.

            Assert.Equal(expected, actualTitle);
        }

        [Theory]    //Using the index of the list living in MovieRepo.Movies
                    //Testing the UserSelectedGenre, which outputs an enum.
                    //Takes in a string, but using #s as a menu selection - must pass in 1-6 (because enum index +1)
        [InlineData("   1", Genre.Action)]
        [InlineData("2  ", Genre.Animated)]
        [InlineData("3", Genre.Comedy)]
        [InlineData("4", Genre.Drama)]
        [InlineData("5", Genre.Horror)]
        [InlineData("6", Genre.Romance)]
        public void TestUserSelectedGenre(string genreChoice, Genre expGenre)
        {
            User u = new User();

            Genre actual = u.UserSelectedGenre(genreChoice);

            Assert.Equal(expGenre, actual);
        }
        [Theory]
        [InlineData(Genre.Action, 2, "Stanley Kubrick")]
        [InlineData(Genre.Animated, 0, "John McTiernan")]
        [InlineData(Genre.Comedy, 1, "Michael Curtiz")]
        [InlineData(Genre.Drama, 1, "Martin Scorsese")]
        [InlineData(Genre.Horror, 2, "Stanley Kubrick")]
        [InlineData(Genre.Romance, 1, "Michael Curtiz")]
        public void TestGetMovieListByGenre(Genre searchGenre, int index, string expDir)    //Can pass in an int for index of list, and can compare title strings
                                                                                            //Will also change up to 3 objects to that genre prior to comparing
                                                                                            //Could also combine this test with UserSelectedGenre method - I'll do that after this
        {
            User u = new User();
            List<Movie> testList = MovieRepo.GetMoviesList;
            testList[0].Category = searchGenre;
            testList[2].Category = searchGenre;
            testList[4].Category = searchGenre;
            //from inline data, be aware that total # of movies on the returned list will be anything btwn 2-4

            List<Movie> genreList = u.GetMovieListByGenre(searchGenre);
            string actualDir = genreList[index].Director;

            Assert.Equal(expDir, actualDir);
        }

        [Theory]
        [InlineData("JOHN MCTIERNAN   ", "Arnold Schwarzenegger")]
        [InlineData("Martin SCORSESE", "Leo DiCaprio")]
        [InlineData("michael curtiz", "Ingrid Bergman")]
        [InlineData("         tamra davis", "Adam Sandler")]
        [InlineData("Stanley Kubrick", "Jack Nicholson")]
        [InlineData("John Lassiter", "Tom Hanks")]
        public void TestGetMovieListByDir(string dirInput, string expected)
        {
            User u = new User();

            List<Movie> dirList = u.GetMovieListByDirector(dirInput);
            string actualActor = dirList[0].MainActor;    //Hard-coded the index, since we do not have a large list of movies - should return exactly 1 movie because of this.

            Assert.Equal(expected, actualActor);
        }
    }
}
