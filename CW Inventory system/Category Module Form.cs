using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CW_Inventory_system
{
    public partial class Category_Module_Form : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-Q9L167I0;Initial Catalog=DBMS;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Category_Module_Form()
        {
            InitializeComponent();
           
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if(String.IsNullOrEmpty(txt_category_name.Text))
                {
                    labl_category_name.Text = "Category name cannot be blank";
                    txt_category_name.Focus();
                }
                else if (MessageBox.Show("Are you suer you want to save this category", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    labl_category_name.Text = "";
                    cm = new SqlCommand("INSERT INTO tbcategory(catname)values(@catname)", con);
                    cm.Parameters.AddWithValue("@catname", txt_category_name.Text);
                    
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Category has been successfully saved");
                    Clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            labl_category_name.Text = "";
            txt_category_name.Clear();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            Clear();
            btn_save.Enabled = true;
            btn_update.Enabled = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("Are you suer you want to update this customer", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbcategory SET catname=@catname WHERE catid LIKE '" + labl_category_id.Text + "' ", con);
                    cm.Parameters.AddWithValue("@catname", txt_category_name.Text);
                    
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Category has been successfully update");
                    this.Dispose();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
       
    }
}
