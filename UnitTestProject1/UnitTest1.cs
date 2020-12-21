using DataFromApi;
using LOTR_app;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    public class TestProvider : LotrProvider
    {
        public IList<Movie> Movies;
        public IList<Book> Books;
        public IList<MovieQuote> MovieQuotes;
        public IList<Character> Characters;
        public int GetMoviesCounter { get; private set; }
        public IList<Movie> GetMovies()
        {
            GetMoviesCounter++;
            return Movies;
        }
        public IList<Book> GetBooks()
        {
            return Books;
        }
        public IList<MovieQuote> GetMovieQuotes(string Id)
        {
            return MovieQuotes;
        }
        public IList<Character> GetCharacters()
        {
            return Characters;
        }
    }
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCashing()
        {
            var testProvider = new TestProvider { Movies = new List<Movie>
            {
                new Movie {Id="1", Name="Rambo 1", RunTimeInMinutes="93", AwardWins="0"},
                new Movie {Id="2", Name="Rambo 2", RunTimeInMinutes="96", AwardWins="0"},
                new Movie {Id="3", Name="Rambo 3", RunTimeInMinutes="104", AwardWins="0"}
            }
            };
            var cashProvider = new CashProvider(testProvider);
            var movies1 = cashProvider.GetMovies();
            Assert.AreEqual(1, testProvider.GetMoviesCounter);

            var movies2 = cashProvider.GetMovies();
            Assert.AreEqual(1, testProvider.GetMoviesCounter);

            Assert.AreEqual(movies1, movies2);
        }

        [TestMethod]
        public void TestMovieRetrieval()
        {
            Provider provider = new Provider();
            var movies = provider.GetMovies();
            Assert.AreEqual(8, movies.Count);

        }

        [TestMethod]
        public void TestBookRetrieval()
        {
            Provider provider = new Provider();
            var books = provider.GetBooks();
            Assert.AreEqual(3, books.Count);

        }

        [TestMethod]
        public void TestCharacterRetrieval()
        {
            Provider provider = new Provider();
            var characters = provider.GetCharacters();
            Assert.AreEqual(933, characters.Count);

        }

        [TestMethod]
        public void TestMovieQuoteRetrieval()
        {
            Provider provider = new Provider();
            var movieQuotes = provider.GetMovieQuotes("5cd95395de30eff6ebccde5d");
            Assert.AreEqual(873, movieQuotes.Count);

        }

        [TestMethod]
        public void TestIfFindRightMovieId()
        {
            Form2 form2 = new Form2();
            form2.MovieName = "The Return of the King";
            form2.GetMovieId();
            Assert.AreEqual("5cd95395de30eff6ebccde5d", form2.MovieId);

        }

        [TestMethod]
        public void TestIfRightCharacterCount()
        {
            Form2 form2 = new Form2();
            form2.MovieName = "The Return of the King";
            form2.GetMovieId();
            form2.GetData();
            Assert.AreEqual(37, form2.CharacterCount);

        }

        [TestMethod]
        public void TestIfRightQuoteCount()
        {
            Form2 form2 = new Form2();
            form2.MovieName = "The Return of the King";
            form2.GetMovieId();
            form2.GetData();
            Assert.AreEqual(873, form2.QuoteCount);

        }
    }
}
