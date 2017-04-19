using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace Document_Clustering_Code
{
    public partial class MatrixForm : Form
    {
        SqlConnection con = new SqlConnection(DbClass.constr);
        ArrayList clusList=new ArrayList();
        DataTable dt = new DataTable();
        public MatrixForm()
        {
            InitializeComponent();
            richTextBox1.Text=AnalysisData.clusteringdata;
            DataColumn dc1 = new DataColumn("word Find");
            DataColumn dc2 = new DataColumn("word Found");
            DataColumn dc3 = new DataColumn("Type");
            DataColumn dc4 = new DataColumn("Line Matched");
            DataColumn dc5 = new DataColumn("Line Number");
            DataColumn dc6 = new DataColumn("Para Number");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList alCType = new ArrayList();
            ArrayList alCword = new ArrayList();
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select cluster_type,cluster_word from TblClusterAnalysis", con);
            SqlDataReader dr1=cmd1.ExecuteReader();
            while (dr1.Read())
            {
                alCType.Add(dr1[0].ToString());
                alCword.Add(dr1[1].ToString());
            }
            con.Close();


            //Hashtable HtTypes = new Hashtable();
            richTextBox1.Text = richTextBox1.Text.Replace("@@@","#");
            string[] paragraphs=richTextBox1.Text.Split('#');
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
                    bool linefound = false;
                    foreach (string line in lines)
                    {
                        if (line.Trim() != "" && line.Length > 5)
                        {
                            linefound = false;
                            linenum++;
                            string[] words = line.Split(' ');
                            foreach (string word in words)
                            {

                                foreach (string cword in alCword)
                                {

                                    /*con.Open();
                                    SqlCommand cmd = new SqlCommand("select cluster_type,cluster_word from TblClusterAnalysis where cluster_word like '" + word.Replace("\'", "") + "' ", con);
                                    SqlDataReader dr = cmd.ExecuteReader();
                                    if (dr.Read())
                                    {
                                        string data = word + "-" + dr[1].ToString() + "-" + dr[0].ToString() + "-" + line;
                                        object[] objvalues = data.Split('-');
                                        dt.Rows.Add(objvalues);
                                        clusList.Add(data);
                                    }
                                    con.Close();*/
                                    if (cword == word)
                                    {
                                        //if(word.Contains('-'))
                                        //word.Remove(word.IndexOf('-'), 1);

                                        string sword = word.Replace("-", "");
                                        linefound = true;
                                        string cltype = alCType[alCword.IndexOf(cword)].ToString();
                                        string data = sword + "-" + cword + "-" + cltype + "-" + line.Replace("-", " ") + "-" + linenum + "-" + paragraphNum;
                                        object[] objvalues = data.Split('-');
                                        dt.Rows.Add(objvalues);
                                        clusList.Add(data);
                                    }
                                }
                            }
                            if (linefound == false)
                            {
                                string data = "" + "-" + "" + "-" + " Unknown " + "-" + line.Replace("-", "") + "-" + linenum + "-" + paragraphNum;
                                object[] objvalues = data.Split('-');
                                dt.Rows.Add(objvalues);
                                clusList.Add(data);
                            }

                        }
                    }
                }
            }
            dataGridView1.DataSource = dt;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ArrayList alTypes = new ArrayList();
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                alTypes.Add(dataGridView1[2, i].Value.ToString());
            con.Open();
            SqlCommand cmd3 = new SqlCommand("delete from mytbl", con);
            cmd3.ExecuteNonQuery();
            foreach (string s in alTypes)
            {
                cmd3=new SqlCommand("insert into mytbl values('"+ s +"')",con);
                cmd3.ExecuteNonQuery();
            }
            con.Close();



            DataTable dtbl = new DataTable();
            /*dtbl.TableName="mytbl";
            dtbl.Columns.Add("col1");
            foreach(string s in alTypes)
            {
               object[] obj=new object[1];
                obj[0]=s;
                dtbl.Rows.Add(obj);
            }*/

            //SqlCommand cmd2 = new SqlCommand("select col1,count(col1) from mytbl");
            SqlCommand cmd2 = new SqlCommand("select col1,count(*) from mytbl group by col1",con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            da.Fill(dtbl);

            dataGridView2.DataSource = dtbl;


            ArrayList altypeCount = new ArrayList();
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                altypeCount.Add(dataGridView2[1, i].Value.ToString());


            altypeCount.Sort();
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                if (altypeCount[altypeCount.Count - 1].ToString() == dataGridView2[1, i].Value.ToString())
                {
                    if (dataGridView2[0, i].Value.ToString().Trim() != "Unknown")
                    {
                        MessageBox.Show(dataGridView2[0, i].Value.ToString());
                    }
                    
                }
            }
            
            /*DataRow[] drow = dtbl.Select("DISTINCT col1");
            foreach (DataRow dro in drow)
            {
                MessageBox.Show(dro.ToString());
            }*/

                  /*  var query =
                                from c in alTypes 
                                    group c by c into g
                                        where g.Count() > 1
                                            select new { Item = g.Key,  ItemCount = g.Count()};

                            foreach (var item in query)
                            {
                                MessageBox.Show("Country {0} has {1} cities", item.Item, item.ItemCount);
                            }
            */


            /*string type=alTypes[0].ToString();
            foreach (string typ in alTypes)
            {
                if(
            }*/
        }
    }
}
