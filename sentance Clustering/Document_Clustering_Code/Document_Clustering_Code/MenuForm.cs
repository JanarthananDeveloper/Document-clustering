using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Document_Clustering_Code
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadForm obj = new LoadForm();
            obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clustering obj = new Clustering();
            obj.Show();
        }

     
    }
}
