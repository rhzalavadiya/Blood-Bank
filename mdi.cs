using System;
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
    public partial class mdi : Form
    {
        public mdi()
        {
            InitializeComponent();
        }

        private void bloodTestDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            blood_test b1 = new blood_test();
            b1.Show();
            this.Hide();
        }

        private void detialToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void googleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe");
        }

        private void mdi_Load(object sender, EventArgs e)
        {

        }

        private void utilityToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void notpadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\\Windows\\notepad.exe");
        }

        private void doctorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doctor d1 = new doctor();
            d1.Show();
            this.Hide();
        }

        private void donorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            donor d2 = new donor();
            d2.Show();
            this.Hide();
        }

        private void patientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            patient p1 = new patient();
            p1.Show();
            this.Hide();
        }

        private void bloodBagDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            blood_bag b2 = new blood_bag();
            b2.Show();
            this.Hide();
        }

        private void bloodDonateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            blood_donate b3 = new blood_donate();
            b3.Show();
            this.Hide();
        }

        private void paymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            payment p1= new payment();
            p1.Show();
            this.Hide();
        }

        private void bloodDiscardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            blood_discard b4 = new blood_discard();
            b4.Show();
            this.Hide();
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stock s1 = new stock();
            s1.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You Want To Exit...", "Exit..", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
                System.Environment.Exit(0);
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\\Windows\\System32\\calc.exe");
        }

        private void wordpadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\\Program Files\\Windows NT\\Accessories\\wordpad.exe");
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_pass c2 = new change_pass();
            c2.Show();
            //this.Hide();
        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contact_us c1 = new contact_us();
            c1.Show();
            
        }

        private void doctorReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doctor_rpt dr1 = new doctor_rpt();
            dr1.Show();
            
        }

        private void donorReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            donor_rpt dr2 = new donor_rpt();
            dr2.Show();
           
        }

        private void patientReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            patient_rpt pr1 = new patient_rpt();
            pr1.Show();
            
        }

        private void bloodTestReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            blood_test_rpt btr = new blood_test_rpt();
            btr.Show();
          
        }

        private void bloodBagReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            blood_bag_rpt bbr = new blood_bag_rpt();
            bbr.Show();
            
        }

        private void bloodDonateReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            boold_donate_rpt bdr1 = new boold_donate_rpt();
            bdr1.Show();
          
        }

        private void paymentReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            payment_rpt pr = new payment_rpt();
            pr.Show();
            
        }

        private void bloodDiscardReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            blood_discard_rpt bdr2 = new blood_discard_rpt();
            bdr2.Show(); 
            
        }

        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutUsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            about a1 = new about();
            a1.Show();
        }

        private void stockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stockrpt s1 = new stockrpt();
            s1.Show();
        }

        
    }
}
