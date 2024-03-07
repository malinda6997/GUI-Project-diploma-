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
    public partial class User_Module_Form : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-Q9L167I0;Initial Catalog=DBMS;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        public User_Module_Form()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if(String.IsNullOrEmpty(txt_user_name.Text))
                {
                    labl_username.Text = "User name cannot be blank";
                    txt_user_name.Focus();
                }
                else if(String.IsNullOrEmpty(txt_full_name.Text))
                {
                    labl_username.Text = "";
                    labl_full_name.Text = "Fulll name cannot be blank";
                    txt_full_name.Focus();
                }
                else if(txt_password.Text.Length == 0)
                {
                    labl_full_name.Text = "";
                    labl_password.Text = "Please enter your password";
                    txt_password.Focus();
                }
                else if (txt_retype_password.Text.Length == 0)
                {
                    labl_password.Text = "";
                    labl_c_password.Text = "Please enter your confirm password";
                    txt_retype_password.Focus();
                }
                else if (txt_password.Text != txt_retype_password.Text)
                {
                    labl_c_password.Text = "";
                    labl_c_password.Text = "Confirm Password must same as the Password";
                    txt_retype_password.Focus();
                }
                else if (txt_phone_no.Text.Any(char.IsLetter))
                {
                    labl_c_password.Text = "";
                    labl_phoneno.Text = "Enter numbers only";
                    txt_phone_no.Focus();
                }

                else if (txt_phone_no.Text.Length != 10)
                {
                    labl_phoneno.Text = "";
                    labl_phoneno.Text = "Phone number must have 10 numbers";
                    txt_phone_no.Focus();
                }


                else if (MessageBox.Show("Are you suer you want to save this user?","Saving Record",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    labl_phoneno.Text = "";
                    labl_c_password.Text = "";
                    labl_password.Text = "";
                    labl_username.Text = "";
                    labl_full_name.Text = "";
                    cm = new SqlCommand("INSERT INTO tbuser(username,fullname,password,phone)values(@username,@fullname,@password,@phone)", con);
                    cm.Parameters.AddWithValue("@username", txt_user_name.Text);
                    cm.Parameters.AddWithValue("@fullname", txt_full_name.Text);
                    cm.Parameters.AddWithValue("@password", txt_password.Text);
                    cm.Parameters.AddWithValue("@phone", txt_phone_no.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been successfully saved");
                    clear();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
            btn_save.Enabled = true;
            btn_update.Enabled = false;
        }

        public void clear()
        {
            labl_phoneno.Text = "";
            labl_c_password.Text = "";
            labl_password.Text = "";
            labl_username.Text = "";
            labl_full_name.Text = "";
            txt_user_name.Clear();
            txt_full_name.Clear();
            txt_password.Clear();
            txt_retype_password.Clear();
            txt_phone_no.Clear();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_password.Text != txt_retype_password.Text)
                {
                    MessageBox.Show("Password did not Match!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you suer you want to update this user", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbuser SET fullname=@fullname,password=@password,phone=@phone WHERE username LIKE '"+txt_user_name.Text+"' ", con);
                    cm.Parameters.AddWithValue("@fullname", txt_full_name.Text);
                    cm.Parameters.AddWithValue("@password", txt_password.Text);
                    cm.Parameters.AddWithValue("@phone", txt_phone_no.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been successfully update");
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
