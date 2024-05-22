﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class contact_us : Form
    {
        public contact_us()
        {
            InitializeComponent();
        }

        private void contact_us_Load(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToShortDateString();
            label2.Text = DateTime.Now.ToShortTimeString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mdi m1 = new mdi();
            m1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Want To Exit...", "Exit..", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
                this.Close();
        }
    }
}
