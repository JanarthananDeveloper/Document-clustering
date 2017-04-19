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
    public partial class Clustering : Form
    {
        public Clustering()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClusterAdd obj = new ClusterAdd();
            obj.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClusterForm obj = new ClusterForm();
            obj.Show();
        }
    }
}
