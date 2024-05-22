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
    public partial class blood_test : Form
    {
        public blood_test()
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
            cmd = new OleDbCommand("select * from blood_test", con);
            var reader = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            auto();
            con.Close();
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
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        public void clear()
        {
            textBox1.Clear();
            comboBox1.Text = "";
            textBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            comboBox7.Text = "";
            comboBox8.Text = "";
            comboBox9.Text = "";
            comboBox10.Text = "";
        }
        public void didadd()
        {
            con.Open();
            cmd = new OleDbCommand("select donorid from donor_master", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
                comboBox1.Items.Add(dr[0].ToString());
            con.Close();
        }
        int pos = 0;
        public void storedata(int index)
        {
            textBox1.Text = dt.Rows[index][0].ToString();
            comboBox1.Text = dt.Rows[index][1].ToString();
            textBox2.Text = dt.Rows[index][2].ToString();
            comboBox3.Text = dt.Rows[index][3].ToString();
            comboBox4.Text = dt.Rows[index][4].ToString();
            comboBox5.Text = dt.Rows[index][5].ToString();
            comboBox6.Text = dt.Rows[index][6].ToString();
            comboBox7.Text = dt.Rows[index][7].ToString();
            comboBox8.Text = dt.Rows[index][8].ToString();
            comboBox9.Text = dt.Rows[index][9].ToString();
            comboBox10.Text = dt.Rows[index][10].ToString(); 
        }
        private void blood_test_Load(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToShortDateString();
            label2.Text = DateTime.Now.ToShortTimeString();
            didadd();
            dispaly();
        }

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new OleDbCommand("select max(bid) from blood_test", con);
                String o = Convert.ToString(cmd.ExecuteScalar());
                if (o == "")
                    i = 1;
                else
                    i = Convert.ToInt32(o) + 1;
                textBox1.Text = i.ToString();
                con.Close();
            }
            catch (OleDbException ab)
            {
                MessageBox.Show(ab.ToString());
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            con.Open();
            String str = "insert into blood_test(bid,donorid,donorname,bg,hb,vdrl,hbsag,hiv,hcv,maleriya,thalessemia) values('" + textBox1.Text + "','" + comboBox1.Text + "','" + textBox2.Text + "','" + comboBox3.Text + "','" + comboBox4.Text + "','" + comboBox5.Text + "','" + comboBox6.Text + "','" + comboBox7.Text + "','" + comboBox8.Text + "','" + comboBox9.Text + "','" + comboBox10.Text + "')";
            cmd = new OleDbCommand(str, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Your data is added successfully", "ADD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            dispaly();
            clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                comboBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                comboBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                comboBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                comboBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                comboBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                comboBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                comboBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                comboBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
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
                    cmd = new OleDbCommand("update blood_test set donorid='" + comboBox1.Text + "',donorname='" + textBox2.Text + "',bg='" + comboBox3.Text + "',hb='" + comboBox4.Text + "',vdrl='" + comboBox5.Text + "',hbsag='" + comboBox6.Text + "',hiv='" + comboBox7.Text + "',hcv='" + comboBox8.Text + "',maleriya='" + comboBox9.Text + "',thalessemia='" + comboBox10.Text + "'where bid=" + textBox1.Text + "", con);
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
                    cmd = new OleDbCommand("delete from blood_test where bid=" + textBox1.Text + "", con);
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
                comboBox11.Visible = true;
                comboBox12.Visible = false;
                con.Open();
                cmd = new OleDbCommand("select bid from blood_test", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                    comboBox11.Items.Add(dr[0].ToString());
                con.Close();
            }
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select * from blood_test where bid=" + comboBox11.Text + " ", con);
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
                comboBox11.Visible = false;
                comboBox12.Visible = true;
                con.Open();
                cmd = new OleDbCommand("select donorname from blood_test", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                    comboBox12.Items.Add(dr[0].ToString());
                con.Close();
            }
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select * from blood_test where donorname='" + comboBox12.Text + "' ", con);
            var r = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(r);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            comboBox11.Visible = false;
            comboBox12.Visible = false;
            dispaly();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mdi m1 = new mdi();
            m1.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select name from donor_master where donorid="+comboBox1.Text+"" ,con);
           dr= cmd.ExecuteReader();
           if (dr.Read())
           {
               textBox2.Text = dr[0].ToString();
           }
            con.Close();
        }
    }
}
