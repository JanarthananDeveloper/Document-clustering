using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Document_Clustering_Code
{
    public partial class ClusterForm : Form
    {
        SqlConnection con=new SqlConnection(DbClass.constr);
        public ClusterForm()
        {
            InitializeComponent();

        }
        public void fillType()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select cluster_type from TblClusterAnalysis", con);
            SqlDataReader dr = cmd.ExecuteReader();
            comboBox1.Items.Add("--Select--");
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            con.Close();
        }

        public void fillword()
        {
            con.Open();
            SqlCommand cmd=new SqlCommand();
            try
            {
                if (comboBox1.SelectedItem.ToString() == "--Select--")
                {
                    cmd = new SqlCommand("select cluster_word from TblClusterAnalysis", con);
                }
                else
                    cmd = new SqlCommand("select cluster_word from TblClusterAnalysis where cluster_type='" + comboBox1.SelectedItem.ToString() + "' ", con);

                SqlDataReader dr = cmd.ExecuteReader();
                listBox1.Items.Clear();
                while (dr.Read())
                {
                    listBox1.Items.Add(dr[0].ToString());
                }

            }
            catch { }
            con.Close();
        }
        private void ClusterForm_Load(object sender, EventArgs e)
        {
            fillType();
            fillword();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillword();
        }
    }
}
