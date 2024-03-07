using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW_Inventory_system
{
    public partial class Main_User_Form : Form
    {
        public Main_User_Form()
        {
            InitializeComponent();
        }
        //to show subform in main form
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_main.Controls.Add(childForm);
            panel_main.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btn_Customer_Click(object sender, EventArgs e)
        {
            openChildForm(new Customer_Form());
        }

        private void btn_orders_Click(object sender, EventArgs e)
        {
            openChildForm(new Order_Form());
        }

        private void btn_product_Click(object sender, EventArgs e)
        {
            openChildForm(new Product_Form());
        }

        private void btn_reports_Click(object sender, EventArgs e)
        {
            openChildForm(new Reports());
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_time1.Text = DateTime.Now.ToString("T");
        }

        private void Main_User_Form_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Log_out_Click(object sender, EventArgs e)
        {
            Hide();
            Login obj = new Login();
            obj.ShowDialog();
        }
    }
}
