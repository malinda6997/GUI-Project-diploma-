using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace CW_Inventory_system
{
    public partial class Customer_Module_Form : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-Q9L167I0;Initial Catalog=DBMS;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        public Customer_Module_Form()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Name.Text.Length == 0)
                {
                    labl_error_Name.Text = "Please enter customer name";
                    txt_Name.Focus();
                    
                }
                else if (txt_phone.Text.Any(char.IsLetter))
                {
                    labl_error_Name.Text = "";
                    labl_error_phone.Text = "Enter numbers only";
                    txt_phone.Focus();
                }
                
                else if (txt_phone.Text.Length !=10  )
                {
                    labl_error_Name.Text = "";
                    labl_error_phone.Text = "Phone number must have 10 numbers";
                    txt_phone.Focus();
                    
                }

                else if ((MessageBox.Show("Are you suer you want to save this customer", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    labl_error_phone.Text = "";
                    labl_error_Name.Text = "";
                    cm = new SqlCommand("INSERT INTO tbCustomer(cname,cphone)values(@cname,@cphone)", con);
                    cm.Parameters.AddWithValue("@cname", txt_Name.Text);
                    cm.Parameters.AddWithValue("@cphone", txt_phone.Text);
                    

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been successfully saved");
                    Clear();
                    labl_error_Name.Text = "";
                    labl_error_phone.Text = "";
                 

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            labl_error_phone.Text = "";
            labl_error_Name.Text = "";
            txt_Name.Clear();
            txt_phone.Clear();
            
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
                    cm = new SqlCommand("UPDATE tbCustomer SET cname=@cname,cphone=@cphone WHERE cid LIKE '" + labl_cid.Text + "' ", con);
                    cm.Parameters.AddWithValue("@cname", txt_Name.Text);
                    cm.Parameters.AddWithValue("@cphone", txt_phone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer has been successfully update");
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
