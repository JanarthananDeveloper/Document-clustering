using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Collections;

namespace Document_Clustering_Code
{
    public partial class AnalysisData : Form
    {
        SqlConnection con = new SqlConnection(DbClass.constr);
        ArrayList StopWordList = new ArrayList();
        public static string loaddata = "";
        public static string remstopwordData = "";
        public static string remTermVariData = ""; 
        public AnalysisData()
        {
            InitializeComponent();
        }

        private void AnalysisData_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select stopword from TblStopWords", con);
            SqlDataReader dr=cmd.ExecuteReader();
            while (dr.Read())
            {
                StopWordList.Add(dr[0].ToString());
            }
            con.Close();

            textBox1.Text=LoadForm.fname;
            richTextBox1.Text = File.ReadAllText(textBox1.Text);
            loaddata = File.ReadAllText(textBox1.Text);

           

        }
        public static string clusteringdata = "";
        private void button1_Click(object sender, EventArgs e)
        {
            clusteringdata = richTextBox1.Text;
            MatrixForm obj = new MatrixForm();
            obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] words=richTextBox1.Text.Split(' ');
            richTextBox1.Text = "";
            foreach (string word in words)
            {
                if(!StopWordList.Contains(word.ToLower()))
                richTextBox1.Text += word +" ";
            }
            remstopwordData = richTextBox1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] words = richTextBox1.Text.Split(' ');
            richTextBox1.Text = "";
            foreach (string word in words)
            {
                
                {
                    if (word.Length < 15 && word != Environment.NewLine)
                        richTextBox1.Text += word + " ";
                    //richTextBox1.Text = richTextBox1.Text.Replace(word, "");
                    //else
                        
                }
            }
            remTermVariData= richTextBox1.Text;

            richTextBox1.Text = richTextBox1.Text.Replace("\n", " @@@ ");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string data = richTextBox1.Text;
            richTextBox1.Text = richTextBox1.Text.Replace("@@@", "#");
            string[] paragraphs = richTextBox1.Text.Split('#');
            richTextBox1.Text="";
            int paragraphNum = 0;
            foreach (string paragra in paragraphs)
            {
                if (paragra.Trim() != "" && paragra.Length > 5)
                {
                    paragraphNum++;
                    //ArrayList =new ArrayList();
                    //string[] lines=richTextBox1.Text.Split('.');
                    string[] lines = paragra.Split('.');
                    int linenum = 0;
                    foreach (string line in lines)
                    {
                        if (line.Trim() != "" && line.Length > 5)
                        {
                            linenum++;
                            richTextBox1.Text += line + Environment.NewLine;
                        }
                    }
                }
                richTextBox1.Text += "#"+Environment.NewLine;
            }
            
        }
    }
}
