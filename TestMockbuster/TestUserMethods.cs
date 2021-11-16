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
            Movie expMovie = MovieRepo.Movies[expected];

            Movie actual = u.GetMovieByTitle(searchTitle);

            Assert.Equal(expMovie, actual);
        }

        [Fact]
        public void SearchByMovieTitle_MovieNotOnList()
        {

        }
    }
}
