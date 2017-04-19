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
    public partial class ClusterAdd : Form
    {
        SqlConnection con = new SqlConnection(DbClass.constr);
        public ClusterAdd()
        {
            InitializeComponent();
            fillType();
        }
        
        public void fillType()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select distinct(cluster_type) from TblClusterAnalysis", con);
            SqlDataReader dr = cmd.ExecuteReader();
            comboBox1.Items.Add("--Select--");
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into TblClusterAnalysis (cluster_type,cluster_word) values (@cluster_type,@cluster_word)", con);
                cmd.Parameters.Add("@cluster_type", SqlDbType.VarChar).Value = textBox1.Text;
                cmd.Parameters.Add("@cluster_word", SqlDbType.VarChar).Value = textBox2.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Inserted Successfully!!!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Fill Type and Word","Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }

        }

        private void ClusterAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
