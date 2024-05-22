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
    public partial class doctor : Form
    {
        public doctor()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\a_6\database\blood_bank.mdb");
        OleDbCommand cmd;
        DataTable dt;
       OleDbDataReader dr;
       int i,a;
        private void doctor_Load(object sender, EventArgs e)
        {
            DateTime dt = this.dateTimePicker1.Value.Date;
            label7.Text = DateTime.Now.ToShortDateString();
            label2.Text = DateTime.Now.ToShortTimeString(); dispaly();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select * from doctor_master where doctorid="+comboBox1.Text+" ", con);
            var r = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(r);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
                        try
                        {
                            con.Open();
                            cmd = new OleDbCommand("select max(doctorid) from doctor_master",con);
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
        public void dispaly()
        {
            con.Open();
            cmd = new OleDbCommand("select * from doctor_master", con);
            var reader = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            auto();
            con.Close(); 
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Are you want to Save Record...?", "insert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                try
                {
                    string em = textBox6.Text;
                    System.Text.RegularExpressions.Regex expr = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
                    if (!expr.IsMatch(em))
                    {
                        MessageBox.Show("Invalid Email Id...", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        con.Open();
                        String gen = "";
                        if (radioButton1.Checked == true)
                            gen = radioButton1.Text;
                        if (radioButton2.Checked == true)
                            gen = radioButton2.Text;
                        String str = "insert into doctor_master(doctorid,doctorname,gender,dob,mobileno,specification,address,city,emailid) values('" + textBox1.Text + "','" + textBox2.Text + "','" + gen + "','" + dateTimePicker1.Value + "','" + textBox3.Text + "','" + textBox4.Text + "','" + richTextBox1.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')";
                        cmd = new OleDbCommand(str, con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Your data is added successfully", "ADD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        dispaly();
                        clear();
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("You Want to Update Data", "asking to user", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                try
                {
                    string em = textBox6.Text;
                    System.Text.RegularExpressions.Regex expr = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
                    if (!expr.IsMatch(em))
                    {
                        MessageBox.Show("Invalid Email Id...", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        con.Open();
                        String gen = "";
                        if (radioButton1.Checked == true)
                            gen = radioButton1.Text;
                        if (radioButton2.Checked == true)
                            gen = radioButton2.Text;
                        cmd = new OleDbCommand("update doctor_master set doctorname='" + textBox2.Text + "',gender='" + gen + "',dob='" + dateTimePicker1.Value + "',mobileno='" + textBox3.Text + "',specification='" + textBox4.Text + "',address='" + richTextBox1.Text + "',city='" + textBox5.Text + "',emailid='" + textBox6.Text + "'where doctorid=" + textBox1.Text + "", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        dispaly();
                        clear();
                    }
                }
                catch (OleDbException ed)
                {
                    MessageBox.Show(ed.ToString());
                }
             }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String gen = "";
            if (radioButton1.Checked == true)
                gen = radioButton1.Text;
            if (radioButton2.Checked == true)
                gen = radioButton2.Text;
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
               gen = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
               if (gen == "Male")
                   radioButton1.Checked = true;
               else
                   radioButton2.Checked = true;
                dateTimePicker1.Text= dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            }
            catch (OleDbException obc)
            { MessageBox.Show(obc.ToString()); }
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
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            dispaly();
          //  auto();
        }

        private void button4_Click(object sender, EventArgs e)
        {
             DialogResult rus = MessageBox.Show("You Want to delete Data", "asking to user", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
             if (rus == DialogResult.Yes)
             {
                 try
                 {
                     con.Open();
                     cmd = new OleDbCommand("delete from doctor_master where doctorid="+textBox1.Text+"", con);
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

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Want To Exit...", "Exit..", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }
        public void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            dateTimePicker1.Text = "";
            textBox3.Clear();
            richTextBox1.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                comboBox1.Visible = true;
                comboBox2.Visible = false;
                con.Open();
                cmd = new OleDbCommand("select doctorid from doctor_master",con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                    comboBox1.Items.Add(dr[0].ToString());
                con.Close();
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                comboBox1.Visible = false;
                comboBox2.Visible = true;
                con.Open();
                cmd = new OleDbCommand("select doctorname from doctor_master", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                    comboBox2.Items.Add(dr[0].ToString());
                con.Close();
            }
        }
        int pos = 0;
        private void button7_Click(object sender, EventArgs e)
        {
            pos = 0;
            storedata(pos);
        }
        public void storedata(int index)
        {
            textBox1.Text = dt.Rows[index][0].ToString();
            textBox2.Text = dt.Rows[index][1].ToString();
            String gen = "";
            if (radioButton1.Checked == true)
                gen = radioButton1.Text;
            if (radioButton2.Checked == true)
                gen = radioButton2.Text;
            gen = dt.Rows[index][2].ToString();
            if (gen == "Male")
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;
            dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[index][3]);
            textBox3.Text = dt.Rows[index][4].ToString();
            textBox4.Text = dt.Rows[index][5].ToString();
            richTextBox1.Text = dt.Rows[index][6].ToString();
            textBox5.Text = dt.Rows[index][7].ToString();
            textBox6.Text = dt.Rows[index][8].ToString();
            
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand("select * from doctor_master where doctorname='" + comboBox2.Text + "' ", con);
            var r = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(r);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            mdi m1 = new mdi();
            m1.Show();
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Charcter Not  Valid For ID...", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
                MessageBox.Show("Digit Not Valid For Name...", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                e.Handled = false;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
                MessageBox.Show("Digit Not Valid For City...", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                e.Handled = false;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Charcter Not Valid For Mobile No...", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }
    }
}
