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
    public partial class donor_rpt : Form
    {
        public donor_rpt()
        {
            InitializeComponent();
        }
        OleDbConnection cn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\a_6\database\blood_bank.mdb");
        DataTable dt;
        OleDbCommand cmd;
        OleDbDataAdapter da;
        private void donor_rpt_Load(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToShortDateString();
            label2.Text = DateTime.Now.ToShortTimeString();
            radioButton1.Checked = false;
            da = new OleDbDataAdapter();
            cmd = new OleDbCommand("select * from donor_master", cn);
            da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            comboid.DataSource = dt;
            comboid.DisplayMember = "donor_master";
            comboid.ValueMember = "donorid";

            combonm.DataSource = dt;
            combonm.DisplayMember = "donor_master";
            combonm.ValueMember = "name";
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true || radioButton2.Checked == true || radioButton3.Checked == true)
            {
                r1.WindowShowRefreshBtn = true;
                string st;

                if (radioButton1.Checked)
                {
                    st = Application.StartupPath + "\\report\\donor1.rpt";
                    r1.SelectionFormula = "{donor_master.donorid}=" + comboid.Text + "";
                    r1.ReportFileName = st;
                }

                else if (radioButton2.Checked)
                {
                    st = Application.StartupPath + "\\report\\donor1.rpt";
                    r1.SelectionFormula = "{donor_master.name}='" + combonm.Text + "'";
                    r1.ReportFileName = st;
                }
                else
                {
                    st = Application.StartupPath + "\\report\\donor2.rpt";
                    r1.SelectionFormula = "{donor_master.donorid}>0";
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

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                comboid.Visible = false;
                combonm.Visible = false;
            }
        }
    }
}
