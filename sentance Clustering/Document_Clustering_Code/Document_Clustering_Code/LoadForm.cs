using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Document_Clustering_Code
{
    public partial class LoadForm : Form
    {
        public LoadForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
                DirectoryInfo dir = new DirectoryInfo(fbd.SelectedPath);
                foreach (FileInfo f in dir.GetFiles("*.txt"))
                {
                    /*ListViewItem lSingleItem = listView1.Items.Add(f.Name);
                    //SUB ITEMS 
                    lSingleItem.SubItems.Add(Convert.ToString(f.Length));
                    lSingleItem.SubItems.Add(f.Extension);
                     */
                    listBox1.Show();
                    listBox1.Items.Add(f.ToString());
                }
            }


        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listBox1.Hide();
        }
        public static string fname;
        private void button2_Click(object sender, EventArgs e)
        {
           
            if (listBox1.SelectedItem.ToString() != "")
            {
                fname=textBox1.Text+"\\"+listBox1.SelectedItem.ToString();
                AnalysisData obj=new AnalysisData();
                obj.Show();
            }
            else
            {
                MessageBox.Show("Select a File!!!", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }

        }
    }
}
