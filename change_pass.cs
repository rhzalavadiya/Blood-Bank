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
    public partial class change_pass : Form
    {
        public change_pass()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\\a_6\\database\\blood_bank.mdb");
        OleDbCommand cmd;
        private void change_pass_Load(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToShortDateString();
            label2.Text = DateTime.Now.ToShortTimeString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            if (textBox1.Text == " " || textBox2.Text == " " || textBox3.Text == " " || textBox4.Text == " ")
            {
                MessageBox.Show("please enter value");
                MessageBox.Show("Please enter value", "change password", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {              
                cmd = new OleDbCommand("select * from login where unm='" + textBox1.Text + "' and pwd='" + textBox2.Text + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (textBox3.Text == textBox4.Text)
                    {
                        cmd = new OleDbCommand("update login set pwd='"+textBox4.Text+"' where unm='"+textBox1.Text+"'",con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Password Are Successfully Changed...");
                    }
                    else
                    {
                        MessageBox.Show("Password Are Not Changed...");
                    }
                }
            }
            con.Close();
        }
    }
}
