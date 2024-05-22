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
    public partial class blood_discard : Form
    {
        public blood_discard()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\a_6\database\blood_bank.mdb");
        OleDbCommand cmd;
        DataTable dt;
        OleDbDataReader dr;
        int i, a;
        public void dispaly()
        {
            con.Open();
            cmd = new OleDbCommand("select * from blood_discard", con);
            var reader = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            auto();
            con.Close();
        }
        int pos = 0;
        public void storedata(int index)
        {
            comboBox1.Text = dt.Rows[index][0].ToString();
            textBox1.Text = dt.Rows[index][1].ToString();
            textBox2.Text = dt.Rows[index][2].ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[index][3]);
            textBox3.Text = dt.Rows[index][4].ToString();
            textBox4.Text = dt.Rows[index][5].ToString();           
        }
        public void auto()
        {
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd'/'MM'/'yy";
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        public void clear()
        {
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
        public void bidadd()
        {
            con.Open();
            cmd = new OleDbCommand("select bid from blood_test", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
                comboBox1.Items.Add(dr[0].ToString());
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            mdi m1 = new mdi();
            m1.Show();
            this.Close();
        }

        private void blood_discard_Load(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToShortDateString();
            label2.Text = DateTime.Now.ToShortTimeString();
            bidadd();
            dispaly();
            comboBox1.Enabled = true;
        }

        private void add_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
        }
        string s1, s2, t1, t2;
        int q;
        private void save_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("insert into blood_discard values('" + comboBox1.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + dateTimePicker1.Value + "','" + textBox3.Text + "','" + textBox4.Text + "')", con);
            cmd.ExecuteNonQuery();
            s1 = textBox4.Text;
            s2 = textBox3.Text;
            MessageBox.Show(s1);
            MessageBox.Show(s2);
            cmd = new OleDbCommand("select bloodgroup from stock where bloodgroup='" + textBox4.Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                t1 = "";
                t1 = dr[0].ToString();
                MessageBox.Show(t1);
            }
            cmd = new OleDbCommand("select bagtype from stock where bloodgroup='" + textBox4.Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                t2 = "";
                t2 = dr[0].ToString();
                MessageBox.Show(t2);
            }

            cmd = new OleDbCommand("select qty from stock where bloodgroup='" + textBox4.Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                q = Convert.ToInt32(dr[0]);
                MessageBox.Show(Convert.ToString(q));
            }
            if (s1 == t1 && s2 == t2)
            {
                q -= 1;
                MessageBox.Show(Convert.ToString(q));
                cmd = new OleDbCommand("update stock set qty=" + q + " where bloodgroup='" + textBox4.Text + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Your data is updated in stock", "ADD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please Check The Stock", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            MessageBox.Show("Your data is added successfully", "ADD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            dispaly();
            clear();
        }
        private void cancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Want To Exit...", "Exit..", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void first_Click(object sender, EventArgs e)
        {
            pos = 0;
            storedata(pos);
        }

        private void next_Click(object sender, EventArgs e)
        {

            pos++;
            if (pos < dt.Rows.Count)
                storedata(pos);
            else
            {
                MessageBox.Show("no more row exists");
                pos = dt.Rows.Count - 1;
            }
        }

        private void previous_Click(object sender, EventArgs e)
        {
            pos--;
            if (pos >= 0)
                storedata(pos);
            else
                MessageBox.Show("position at row[0]");
        }

        private void last_Click(object sender, EventArgs e)
        {
            pos = dt.Rows.Count - 1;
            storedata(pos);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                comboBox6.Items.Clear();
                comboBox6.Visible = true;
                comboBox7.Visible = false;
                con.Open();
                cmd = new OleDbCommand("select blid from blood_discard", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                    comboBox6.Items.Add(dr[0].ToString());
                con.Close();
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select * from blood_discard where blid=" + comboBox6.Text + " ", con);
            var r = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(r);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                comboBox7.Items.Clear();
                comboBox6.Visible = false;
                comboBox7.Visible = true;
                con.Open();
                cmd = new OleDbCommand("select dnm from blood_discard", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                    comboBox7.Items.Add(dr[0].ToString());
                con.Close();
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select * from blood_discard where dnm='" + comboBox7.Text + "' ", con);
            var r = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(r);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            comboBox7.Visible = false;
            comboBox6.Visible = false;
            dispaly();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select donorid from blood_bag where bloodid=" + comboBox1.Text + "", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr[0].ToString();
            }
            cmd = new OleDbCommand("select dname from blood_bag where bloodid=" + comboBox1.Text + "", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr[0].ToString();
            }
            cmd = new OleDbCommand("select bagtype from blood_bag where bloodid=" + comboBox1.Text + "", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox3.Text = dr[0].ToString();
            }
            cmd = new OleDbCommand("select bg from blood_bag where bloodid=" + comboBox1.Text + "", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox4.Text = dr[0].ToString();
            }
            con.Close();
        }
    
    }
}
