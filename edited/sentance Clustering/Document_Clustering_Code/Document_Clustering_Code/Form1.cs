using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Document_Clustering_Code
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(DbClass.constr);
        int i;
        Random ran = new Random();
        public Form1()
        {
            InitializeComponent();
            
            loadStopWords();
        }
        private void loadStopWords()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from TblStopWords", con);
            cmd.ExecuteNonQuery();
            con.Close();

            string[] words = File.ReadAllLines(Application.StartupPath+"\\Stopwords.txt");
            foreach (string word in words)
            {
                string txt= word.Replace('\'', ' ');

                con.Open();
                cmd=new SqlCommand("insert into TblStopWords(stopword) values('"+ txt +"')",con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i+=ran.Next(10,25);
            if (i < progressBar1.Maximum)
                progressBar1.Value = i;
            else
            {
                progressBar1.Visible = false;
                timer1.Enabled = false;
                Login obj = new Login();
                obj.Show();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }
    }
}
