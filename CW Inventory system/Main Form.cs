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
    public partial class Main_Form : Form
    {
        public Main_Form()
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



        private void btn_user_Click_1(object sender, EventArgs e)
        {
            openChildForm(new User_Form());
        }

        private void btn_Customer_Click(object sender, EventArgs e)
        {
            openChildForm(new Customer_Form());
        }

        private void btn_categories_Click(object sender, EventArgs e)
        {
            openChildForm(new Category_Form());
        }

        private void btn_product_Click(object sender, EventArgs e)
        {
            openChildForm(new Product_Form());
        }

        private void btn_orders_Click(object sender, EventArgs e)
        {
            openChildForm(new Order_Form());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_time.Text = DateTime.Now.ToString("T");
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void btn_reports_Click(object sender, EventArgs e)
        {
            openChildForm(new Reports());
        }

        private void Log_out_Click(object sender, EventArgs e)
        {
            Hide();
            Login obj = new Login();
            obj.ShowDialog();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
