using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using midterm_abeer;

namespace TestMockbuster
{
    public class TestAdminMethods
    //When running ALL Unit Tests, lots of them fail.  I believe this is due to the lists being updated between different tests running.
    //When run separately, they all pass.
    {
        [Fact]
        public void TestAddMovieToList() 
        {
            //Arrange
            //Act
            //Assert
            Admin a = new Admin();
            Movie youveGotMail = new Movie("You've Got Mail", "Meg Ryan", Genre.Romance, "Nora Ephron");
            //MovieRepo repo = new MovieRepo(); //fills the list with the pre-existing movies

            a.AddMovieToList(youveGotMail);
            List<Movie> actual = MovieRepo.GetMoviesList;
            Assert.Contains(youveGotMail, actual); //takes in an object, verifies it exists in the collection

        }


        [Fact]
        public void TestAddMovieToListWithDupe()    //Something breaks down when all tests are ran - I think it is due to them all referencing the same list
                                                    //Running it individually gets a pass.
        {
            Admin a = new Admin();
            Movie predator = new Movie("Predator", "Arnold Schwarzenegger", Genre.Action, "John McTeirnan");

            a.AddMovieToList(predator);
             //MovieRepo.Movies from the start only has 6 movies
             //- if this add method allows a dupe, then actual will be +1.
            int actualNumOnList = MovieRepo.GetMoviesList.Count;

            Assert.Equal(6, actualNumOnList);
        }
        [Fact]
        public void TestUpdateMovieTitle_ActuallyUpdates() 
        {
            Admin a = new Admin();

            Movie actual = a.GetMovieByTitle("predator");
            a.UpdateExistingMovie(actual, "1", "Totally Not The Predator Anymore");

            string expected = "Totally Not The Predator Anymore";

            Assert.Equal(expected, actual.Title);
        }
        [Fact]
        public void TestUpdateMovie_MatchesExistingTitle()//Something breaks down when all tests are ran - I think it is due to them all referencing the same list
                                                          //Running it individually gets a pass.
        {
            Admin a = new Admin();

            Movie actual = a.GetMovieByTitle("predator");
            a.UpdateExistingMovie(actual, "1", "PReDAToR"); //same string .ToLower, so shouldnt change.

            string expected = "Predator"; //original formatting of this string

            Assert.Equal(expected, actual.Title);//should fail if formatting is off, since we are not calling any .ToLower here
        }
        [Fact]
        public void TestUpdateMovie_MatchesExistingTitle_ShouldFail()//Something breaks down when all tests are ran - I think it is due to them all referencing the same list
                                                                     //Running it individually gets a pass.
        {
            Admin a = new Admin();

            Movie actual = a.GetMovieByTitle("predator");
            a.UpdateExistingMovie(actual, "1", "PReDAToR"); //same string.ToLower, so shouldnt change.

            string expected = "PReDAToR"; //changed original formatting of this string to match entry above.

            Assert.NotEqual(expected, actual.Title);
        }
        [Fact]
        public void TestUpdateMovieActor_ActuallyUpdates() //there is so much user interaction here that it cannot be tested as-is
        {
            Admin a = new Admin();

            Movie actual = a.GetMovieByTitle("casablanca");
            a.UpdateExistingMovie(actual, "2", "Keanu Reeves");

            string expected = "Keanu Reeves";

            Assert.Equal(expected, actual.MainActor);
        }
        [Theory]
        [InlineData("predator", "12a", "Arnold Schwarzenegger")]
        [InlineData("pREDator", "AA", "Arnold Schwarzenegger")]
        [InlineData("pREDator", "AAAA", "AAAA")]
        [InlineData("pREDator", "Arnold Schwarzenegger   ", "Arnold Schwarzenegger")]
        [InlineData("pREDator", "arNOLD schwarzenegger   ", "Arnold Schwarzenegger")]
        [InlineData("pREDATOR", "12ThisNameIsLongEnough", "Arnold Schwarzenegger")]
        [InlineData("THE DEParted", "LEROY DECAPREEO", "LEROY DECAPREEO")]
        [InlineData("the shininG", "Jack NicholSOn", "Jack Nicholson")]
        
        public void TestUpdateMovieActor_NumbersOrUnderLengthOrAlreadyExists(string movieTitle,string newActor, string expected) 
        {
            Admin a = new Admin();

            Movie actual = a.GetMovieByTitle(movieTitle);
            a.UpdateExistingMovie(actual, "2", newActor);

            Assert.Equal(expected, actual.MainActor);
        }
        [Fact]
        public void TestRemoveMovieFromList_ActuallyRemoves()
        {
            Admin a = new Admin();

            Movie actual = a.GetMovieByTitle("the departed");
            a.RemoveMovieFromList(actual);

            Assert.DoesNotContain(actual, MovieRepo.GetMoviesList);
        }
        [Fact]
        public void TestRemoveMovieFromList_DoesntExist()   //no way to test using the GetMovieByTitle method, so I will have to create a new movie that isnt on the list
                                                            
        {
            Admin a = new Admin();
            Movie actual = new Movie ("Toy Story II", "Tim Allen", Genre.Horror, "someone");
            a.RemoveMovieFromList(actual);

            Assert.Equal(6, MovieRepo.GetMoviesList.Count);
        }
    }
}
