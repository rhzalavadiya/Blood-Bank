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
    public partial class payment : Form
    {
        public payment()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\a_6\database\blood_bank.mdb");
        OleDbCommand cmd;
        DataTable dt;
        OleDbDataReader dr;
        int  a;
        public void dispaly()
        {
            con.Open();
            cmd = new OleDbCommand("select * from payment", con);
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
            comboBox2.Text = dt.Rows[index][1].ToString();
            textBox4.Text = dt.Rows[index][2].ToString();
            textBox5.Text = dt.Rows[index][3].ToString();
            textBox6.Text = dt.Rows[index][4].ToString();
            textBox7.Text = dt.Rows[index][5].ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[index][6]);
            textBox8.Text = dt.Rows[index][7].ToString();
            textBox1.Text = dt.Rows[index][8].ToString();
            textBox2.Text = dt.Rows[index][9].ToString();
            textBox3.Text = dt.Rows[index][10].ToString();
        }
        public void auto()
        {
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[6].DefaultCellStyle.Format = "dd'/'MM'/'yy";
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[7].DefaultCellStyle.Format = "dd'/'MM'/'yy";
            dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        public void clear()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            dateTimePicker1.Text = "";
            textBox8.Text = "";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
        public void pidadd()
        {
            con.Open();
            cmd = new OleDbCommand("select pid from patient_master", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
                comboBox1.Items.Add(dr[0].ToString());
            con.Close();
        }
        public void bagidadd()
        {
            con.Open();
            cmd = new OleDbCommand("select bagid from blood_bag", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
                comboBox2.Items.Add(dr[0].ToString());
            con.Close();
        }
        private void payment_Load(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToShortDateString();
            label2.Text = DateTime.Now.ToShortTimeString();  
            pidadd();
            bagidadd();
            dispaly();
        }

        private void add_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
        }

        private void save_Click(object sender, EventArgs e)
        {
            con.Open();
            String str = "insert into payment values('" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + dateTimePicker1.Value + "'," + textBox1.Text + "," + textBox2.Text + "," + textBox3.Text + ")";
            cmd = new OleDbCommand(str, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Your data is added successfully", "ADD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            dispaly();
            clear();
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.Text = Convert.ToString(Convert.ToInt32(textBox1.Text)*Convert.ToInt32(textBox2.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mdi m1 = new mdi();
            m1.Show();
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            }
            catch (OleDbException obc)
            {
                MessageBox.Show(obc.ToString());
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("You Want to Update Data", "asking to user", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    cmd = new OleDbCommand("update payment set bagid='" + comboBox2.Text + "',pnm='" + textBox4.Text + "',city='" + textBox5.Text + "',mobileno='" + textBox6.Text + "',bg='" + textBox7.Text + "',donordt='" + textBox8.Text + "',paydt='" + dateTimePicker1.Value + "',qty=" + textBox1.Text + ",amt=" + textBox2.Text + ",namt=" + textBox3.Text + " where pid=" + comboBox1.Text + "", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    dispaly();
                    clear();
                }
                catch (OleDbException ed)
                {
                    MessageBox.Show(ed.ToString());
                }
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            DialogResult rus = MessageBox.Show("You Want to delete Data", "asking to user", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rus == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    cmd = new OleDbCommand("delete from payment where pid=" + comboBox1.Text + "", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (OleDbException o)
                {
                    MessageBox.Show(o.ToString());
                }
                dispaly();
                clear();
            }
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
                comboBox8.Visible = true;
                comboBox9.Visible = false;
                con.Open();
                cmd = new OleDbCommand("select pid from payment", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                    comboBox8.Items.Add(dr[0].ToString());
                con.Close();
            }
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select * from payment where pid=" + comboBox8.Text + " ", con);
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
                comboBox8.Visible = false;
                comboBox9.Visible = true;
                con.Open();
                cmd = new OleDbCommand("select pnm from payment", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                    comboBox9.Items.Add(dr[0].ToString());
                con.Close();
            }
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select * from payment where pnm='" + comboBox9.Text + "' ", con);
            var r = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(r);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            comboBox9.Visible = false;
            comboBox8.Visible = false;
            dispaly();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select pname from patient_master where pid=" + comboBox1.Text + "", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox4.Text = dr[0].ToString();
            }
            cmd = new OleDbCommand("select city from patient_master where pid=" + comboBox1.Text + "", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox5.Text = dr[0].ToString();
            }
            cmd = new OleDbCommand("select bg from patient_master where pid=" + comboBox1.Text + "", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox7.Text = dr[0].ToString();
            }
            cmd = new OleDbCommand("select mobileno from patient_master where pid=" + comboBox1.Text + "", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox6.Text = dr[0].ToString();
            }
            con.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select dob from blood_bag where bagid=" + comboBox2.Text + "", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox8.Text = dr[0].ToString();
            }
            con.Close();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            String st = "";
            st = Application.StartupPath + "\\report\\bill.rpt";
            r1.SelectionFormula = "{payment.pid}=" +comboBox1.Text+ "";
            r1.ReportFileName = st;
            r1.Connect = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\a_6\database\blood_bank.mdb";
            r1.WindowState = Crystal.WindowStateConstants.crptMaximized;
            r1.WindowShowRefreshBtn = true;
            r1.Refresh();
            r1.Action = 1;
        }
    }
}
