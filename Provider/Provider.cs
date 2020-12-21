using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DataFromApi
{
    public interface LotrProvider
    {
        IList<Movie> GetMovies();
        IList<Book> GetBooks();
        IList<MovieQuote> GetMovieQuotes(string Id);
        IList<Character> GetCharacters();
    }

    class Response<T>
    {
        public IList<T> Docs { get; set; }
    }
    public class Provider : LotrProvider
    {
        public IList<Movie> GetMovies()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "kLDaPIKZqqjECO-TGmyq");
                client.BaseAddress = new Uri("https://the-one-api.dev/v2/");
                string s = client.GetStringAsync("movie").Result;
                var list = JsonConvert.DeserializeObject<Response<Movie>>(s);
                return list.Docs;
            }
        }
        public IList<Book> GetBooks()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "kLDaPIKZqqjECO-TGmyq");
                client.BaseAddress = new Uri("https://the-one-api.dev/v2/");
                string s = client.GetStringAsync("book").Result;
                var list = JsonConvert.DeserializeObject<Response<Book>>(s);
                return list.Docs;
            }
        }
        public IList<MovieQuote> GetMovieQuotes(string movieId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "kLDaPIKZqqjECO-TGmyq");
                client.BaseAddress = new Uri("https://the-one-api.dev/v2/movie/"+ movieId + "/quote");
                string s = client.GetStringAsync("quote").Result;
                var list = JsonConvert.DeserializeObject<Response<MovieQuote>>(s);
                return list.Docs;
            }
        }

        public IList<Character> GetCharacters()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "kLDaPIKZqqjECO-TGmyq");
                client.BaseAddress = new Uri("https://the-one-api.dev/v2/character");
                string s = client.GetStringAsync("character").Result;
                var list = JsonConvert.DeserializeObject<Response<Character>>(s);
                return list.Docs;
            }
        }
    }

    public class CashProvider : LotrProvider
    {
        private readonly LotrProvider m_provider;
        private IList<Book> m_books = null;
        private IList<Movie> m_movies = null;
        private IList<MovieQuote> m_movieQuotes = null;
        private IList<Character> m_characters = null;
        public CashProvider(LotrProvider provider)
        {
            m_provider = provider;
        }

        public IList<Book> GetBooks()
        {
            if (m_books != null)
                return m_books;
            return m_books = m_provider.GetBooks();
        }

        public IList<Movie> GetMovies()
        {
            if (m_movies != null)
                return m_movies;
            return m_movies = m_provider.GetMovies();
        }

        public IList<MovieQuote> GetMovieQuotes(string Id)
        {
            if (m_movieQuotes != null)
                return m_movieQuotes;
            return m_movieQuotes = m_provider.GetMovieQuotes(Id);
        }
        public IList<Character> GetCharacters()
        {
            if (m_characters != null)
                return m_characters;
            return m_characters = m_provider.GetCharacters();
        }
    }
    public class Character
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("race")]
        public string Race { get; set; }
        [JsonProperty("_id")]
        public string Id { get; set; }

    }
    public class MovieQuote
    {
        [JsonProperty("character")]
        public string Character { get; set; }
        [JsonProperty("dialog")]
        public string Quote { get; set; }

    }

    public class Book
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Movie
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }        
        [JsonProperty("runtimeInMinutes")]
        public string RunTimeInMinutes { get; set; }
        [JsonProperty("academyAwardWins")]
        public string AwardWins { get; set; }

    }
}
