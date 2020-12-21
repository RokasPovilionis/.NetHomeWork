using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataFromApi;

namespace LOTR_app
{
    public partial class Form2 : Form
    {
        private LotrProvider repo = new CashProvider(new Provider());

        public string MovieName { get; set; }
        public int QuoteCount;
        public int CharacterCount;
        public string MovieId;

        public Form2()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void GetMovieId()
        {
            var movies = repo.GetMovies();
            foreach (var movie in movies)
            {
                if (movie.Name == MovieName)
                {
                    MovieId = movie.Id;
                }
            }
        }

        public void GetData()
        {
            var movieQuotes = repo.GetMovieQuotes(MovieId);
            var characters = repo.GetCharacters();

            CharacterCount = movieQuotes.Select(x => x.Character).Distinct().Count();
            QuoteCount = movieQuotes.Count();

            foreach (var movieQuote in movieQuotes)
            {

                foreach (var character in characters)
                {
                    if (movieQuote.Character == character.Id)
                    {
                        var item = new ListViewItem(new[] { character.Name, movieQuote.Quote });
                        item.Tag = movieQuote;
                        listView1.Items.Add(item);
                    }
                }
            }

            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("No Quotes in API :(");
                this.Close();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            GetMovieId();
            GetData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Number of Quotes in this movie: " + QuoteCount);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Number of Characters in this movie: " + CharacterCount);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            
            var movieQuotes = repo.GetMovieQuotes(MovieId);
            var characters = repo.GetCharacters();

            foreach (var movieQuote in movieQuotes)
            {
                if(movieQuote.Quote.Contains(textBox1.Text))
                {
                    foreach (var character in characters)
                    {
                        if (movieQuote.Character == character.Id)
                        {
                            var item = new ListViewItem(new[] { character.Name, movieQuote.Quote });
                            item.Tag = movieQuote;
                            listView1.Items.Add(item);
                        }
                    }
                }                
            }
        }
    }
}
