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
    public partial class stockrpt : Form
    {
        public stockrpt()
        {
            InitializeComponent();
        }
        OleDbConnection cn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\a_6\database\blood_bank.mdb");
        DataTable dt;
        OleDbCommand cmd;
        OleDbDataAdapter da;
        private void stockrpt_Load(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToShortDateString();
            label2.Text = DateTime.Now.ToShortTimeString();
            radioButton1.Checked = false;
            da = new OleDbDataAdapter();
            cmd = new OleDbCommand("select * from stock", cn);
            da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            comboid.DataSource = dt;
            comboid.DisplayMember = "stock";
            comboid.ValueMember = "bagid";

            combonm.DataSource = dt;
            combonm.DisplayMember = "stock";
            combonm.ValueMember = "bloodgroup";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                comboid.Visible = true;
                combonm.Visible = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                comboid.Visible = false;
                combonm.Visible = true;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                comboid.Visible = false;
                combonm.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true || radioButton2.Checked == true || radioButton3.Checked == true)
            {
                r1.WindowShowRefreshBtn = true;
                string st;

                if (radioButton1.Checked)
                {
                    st = Application.StartupPath + "\\report\\stock1.rpt";
                    r1.SelectionFormula = "{stock.bagid}=" + comboid.Text + "";
                    r1.ReportFileName = st;
                }

                else if (radioButton2.Checked)
                {
                    st = Application.StartupPath + "\\report\\stock1.rpt";
                    r1.SelectionFormula = "{stock.bloodgroup}='" + combonm.Text + "'";
                    r1.ReportFileName = st;
                }
                else
                {
                    st = Application.StartupPath + "\\report\\stock2.rpt";
                    r1.SelectionFormula = "{stock.bagid}>0";
                    r1.ReportFileName = st;
                }
                r1.Connect = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\a_6\database\blood_bank.mdb";
                r1.WindowState = Crystal.WindowStateConstants.crptMaximized;
                r1.WindowShowRefreshBtn = true;
                r1.Refresh();
                r1.Action = 1;
                combonm.Visible = false;
                comboid.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Want To Exit...", "Exit..", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

    }
}
