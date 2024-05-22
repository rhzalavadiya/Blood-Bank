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
    public partial class blood_bag : Form
    {
        public blood_bag()
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
            cmd = new OleDbCommand("select * from blood_bag", con);
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
            textBox1.Text = dt.Rows[index][0].ToString();
            comboBox1.Text = dt.Rows[index][1].ToString();
            comboBox2.Text = dt.Rows[index][2].ToString();
            textBox2.Text = dt.Rows[index][3].ToString();
            textBox3.Text = dt.Rows[index][4].ToString();
            comboBox5.Text = dt.Rows[index][5].ToString();
            comboBox6.Text = dt.Rows[index][6].ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[index][7]);

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
            dataGridView1.Columns[7].DefaultCellStyle.Format = "dd'/'MM'/'yy";
        }
        public void clear()
        {
            textBox1.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            dateTimePicker1.Text = "";
        }
        public void didadd()
        {
            con.Open();
            cmd = new OleDbCommand("select donorid from blood_test", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
                comboBox2.Items.Add(dr[0].ToString());
            con.Close();
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
        private void blood_bag_Load(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToShortDateString();
            label2.Text = DateTime.Now.ToShortTimeString();
            bidadd();
            didadd();
            dispaly();
        }

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new OleDbCommand("select max(bagid) from blood_bag", con);
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
        string s1 = "", s2 = "", t1 = "", t2 = "";
        int q=1;
       
        private void save_Click(object sender, EventArgs e)
        {
            con.Open();
            String str = "insert into blood_bag(bagid,bloodid,donorid,dname,bg,rhfact,bagtype,dob) values('" + textBox1.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox5.Text + "','" + comboBox6.Text + "','" + dateTimePicker1.Value + "')";
            cmd = new OleDbCommand(str, con);
            cmd.ExecuteNonQuery();
            s1 = textBox3.Text;
            s2 = comboBox6.Text;
            MessageBox.Show(s1);
            MessageBox.Show(s2);
            cmd = new OleDbCommand("select bloodgroup from stock where bloodgroup='" + textBox3.Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                t1="";
                t1 = dr[0].ToString();
                MessageBox.Show(t1);
            }
            cmd = new OleDbCommand("select bagtype from stock where bloodgroup='" + textBox3.Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                t2 = "";
                t2 = dr[0].ToString();
                MessageBox.Show(t2);
            }

            cmd = new OleDbCommand("select qty from stock where bloodgroup='" + textBox3.Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                q = Convert.ToInt32(dr[0]);
                MessageBox.Show(Convert.ToString(q));
            }
            if (s1 == t1 && s2 == t2)
            {
                q +=1;
                MessageBox.Show(Convert.ToString(q));
                cmd = new OleDbCommand("update stock set qty=" + q + " where bloodgroup='" + textBox3.Text + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Your data is updated in stock", "ADD", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            else
            {
                cmd = new OleDbCommand("insert into stock values(" + textBox1.Text + ",'" + textBox3.Text + "','" + comboBox6.Text + "'," + q + ")", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Your data is added", "ADD", MessageBoxButtons.OK, MessageBoxIcon.Information);  
            }
            MessageBox.Show("Your data is added successfully", "ADD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            dispaly();
            clear();
        }
        String t3, t4;
        private void edit_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("You Want to Update Data", "asking to user", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    MessageBox.Show("Old  "+a1);
                    MessageBox.Show("Old  "+b);
                    cmd = new OleDbCommand("select bloodgroup from stock where bloodgroup='" + a1 + "'", con);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        t1 = "";
                        t1 = dr[0].ToString();
                        MessageBox.Show("old   " + t1);
                    }
                    cmd = new OleDbCommand("select bagtype from stock where bloodgroup='" + a1 + "'", con);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        t2 = "";
                        t2 = dr[0].ToString();
                        MessageBox.Show("old   " + t2);
                    }
                    cmd = new OleDbCommand("select qty from stock where bloodgroup='" + a1+ "'", con);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        q = Convert.ToInt32(dr[0]);
                        MessageBox.Show("old   "+Convert.ToString(q));
                    }
                    if (a1 == t1 && b == t2)
                    {
                        q -= 1;
                        MessageBox.Show(Convert.ToString(q));
                        cmd = new OleDbCommand("update stock set qty=" + q + " where bloodgroup='" +a1+ "'", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Your data is updated in stock", "ADD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    cmd = new OleDbCommand("update blood_bag set bloodid='" + comboBox1.Text + "',donorid='" + comboBox2.Text + "',dname='" + textBox2.Text + "',bg='" + textBox3.Text + "',rhfact='" + comboBox5.Text + "',bagtype='" + comboBox6.Text + "',dob='" + dateTimePicker1.Value + "'where bagid=" + textBox1.Text + "", con);
                    cmd.ExecuteNonQuery();
                    String s3 = textBox3.Text;
                    String s4 = comboBox6.Text;
                    MessageBox.Show("New   " + s3);
                    MessageBox.Show("New   " + s4);

                    cmd = new OleDbCommand("select bloodgroup from stock where bloodgroup='" + textBox3.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        t3 = dr[0].ToString();
                        MessageBox.Show("New   " + t3);
                    }
                    cmd = new OleDbCommand("select bagtype from stock where bloodgroup='" + textBox3.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        t4 = dr[0].ToString();
                        MessageBox.Show("New   "+t4);
                    }
                    cmd = new OleDbCommand("select qty from stock where bloodgroup='" + textBox3.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        q = Convert.ToInt32(dr[0]);
                        MessageBox.Show("New    " + Convert.ToString(q));
                    }
                    if (s3 == t3 && s4 == t4)
                    {
                        q += 1;
                        MessageBox.Show(Convert.ToString(q));
                        cmd = new OleDbCommand("update stock set qty=" + q + " where bloodgroup='" + textBox3.Text + "'", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Your data is updated in stock", "ADD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        cmd = new OleDbCommand("insert into stock values(" + textBox1.Text + ",'" + textBox3.Text + "','" + comboBox6.Text + "'," + q + ")", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Your data is added", "ADD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
        String a1, b;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                comboBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                comboBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                a1 = textBox3.Text;
                b = comboBox6.Text;
            }
            catch (OleDbException obc)
            {
                MessageBox.Show(obc.ToString());
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
                    MessageBox.Show("Old  " + a1);
                    MessageBox.Show("Old  " + b);
                    cmd = new OleDbCommand("select bloodgroup from stock where bloodgroup='" + a1 + "'", con);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        t1 = "";
                        t1 = dr[0].ToString();
                        MessageBox.Show("old   " + t1);
                    }
                    cmd = new OleDbCommand("select bagtype from stock where bloodgroup='" + a1 + "'", con);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        t2 = "";
                        t2 = dr[0].ToString();
                        MessageBox.Show("old   " + t2);
                    }
                    cmd = new OleDbCommand("select qty from stock where bloodgroup='" + a1 + "'", con);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        q = Convert.ToInt32(dr[0]);
                        MessageBox.Show("old   " + Convert.ToString(q));
                    }
                    if (a1 == t1 && b == t2)
                    {
                        q -= 1;
                        MessageBox.Show(Convert.ToString(q));
                        cmd = new OleDbCommand("update stock set qty=" + q + " where bloodgroup='" + a1 + "'", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Your data is updated in stock", "ADD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    cmd = new OleDbCommand("delete from blood_bag where bagid=" + textBox1.Text + "", con);
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
            comboBox7.Items.Clear();
            if (radioButton3.Checked)
            {
                comboBox7.Visible = true;
                comboBox8.Visible = false;
                con.Open();
                cmd = new OleDbCommand("select bagid from blood_bag", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                    comboBox7.Items.Add(dr[0].ToString());
                con.Close();
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select * from blood_bag where bagid=" + comboBox7.Text + " ", con);
            var r = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(r);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            comboBox8.Items.Clear();
            if (radioButton4.Checked)
            {
                comboBox7.Visible = false;
                comboBox8.Visible = true;
                con.Open();
                cmd = new OleDbCommand("select dname from blood_bag", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                    comboBox8.Items.Add(dr[0].ToString());
                con.Close();
            }
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select * from blood_bag where dname='" + comboBox8.Text + "' ", con);
            var r = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(r);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            comboBox7.Visible = false;
            comboBox8.Visible = false;
            dispaly();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mdi m1 = new mdi();
            m1.Show();
            this.Close();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select name from donor_master where donorid=" + comboBox2.Text + "", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr[0].ToString();
            }
            cmd = new OleDbCommand("select bg from blood_test where donorid=" + comboBox2.Text + "", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox3.Text = dr[0].ToString();
            }
            con.Close();
        }
    }
}
