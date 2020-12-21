using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using DataFromApi;

namespace LOTR_app
{
    public partial class Form1 : Form
    {
        private LotrProvider repo = new CashProvider(new Provider());
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            var result = list_provider();

        }

        private string list_provider()
        {
            string strurl = String.Format("https://the-one-api.dev/v2/character");
            WebRequest requestObjGet = WebRequest.Create(strurl);
            requestObjGet.Method = "GET";
            requestObjGet.Headers.Add("Authorization", " Bearer kLDaPIKZqqjECO-TGmyq");
            HttpWebResponse responseObjGet = null;

            responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();


            string json = null;
            using (Stream stream = responseObjGet.GetResponseStream())
            {
                TextReader tr = new StreamReader(stream);
                json = tr.ReadToEnd();
                tr.Close();
            }

            return json;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            var result = repo.GetMovies();
            foreach(var movie in result)
            {
                if (movie.Name.Contains("Series"))
                    continue;
                else
                {
                    var item = new ListViewItem(new[] { movie.Name, movie.RunTimeInMinutes, movie.AwardWins });
                    item.Tag = movie;
                    listView1.Items.Add(item);
                }

            }
        }

        private void Load_page(object sender, EventArgs e)
        {
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems != null && listView1.SelectedItems.Count != 0)
            {
                string movieName = listView1.FocusedItem.Text;

                Form2 quotesForm = new Form2();
                quotesForm.MovieName = movieName;
                quotesForm.ShowDialog();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            var result = repo.GetMovies();
            foreach (var movie in result)
            {
                if (movie.Name.Contains("Series") || movie.Name.Contains("Journey") || movie.Name.Contains("Smaug") || movie.Name.Contains("Five"))
                    continue;
                else
                {
                    var item = new ListViewItem(new[] { movie.Name, movie.RunTimeInMinutes, movie.AwardWins });
                    item.Tag = movie;
                    listView1.Items.Add(item);
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            var result = repo.GetMovies();
            foreach (var movie in result)
            {
                if (movie.Name.Contains("Series") || movie.Name.Contains("Two") || movie.Name.Contains("Ring") || movie.Name.Contains("King"))
                    continue;
                else
                {
                    var item = new ListViewItem(new[] { movie.Name, movie.RunTimeInMinutes, movie.AwardWins });
                    item.Tag = movie;
                    listView1.Items.Add(item);
                }

            }
        }
    }
}
