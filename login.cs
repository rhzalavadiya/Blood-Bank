using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace project
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\a_6\database\blood_bank.mdb");
            con.Open();
            OleDbCommand cmd = new OleDbCommand("select * from login where unm='"+textBox1.Text+"'AND '"+textBox2.Text+"'",con);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Login Successfully", "Login", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                mdi c1 = new mdi();
                c1.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid UserName And Password...", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void login_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            label7.Text = DateTime.Now.ToShortDateString();
            label2.Text = DateTime.Now.ToShortTimeString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Want To Exit...","Exit..",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
                System.Environment.Exit(0);
        }
    }
}
