using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW_Inventory_system
{
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-Q9L167I0;Initial Catalog=DBMS;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            txt_password.Text = "";
            txt_username.Text = "";

        }

        private void checkBox_pass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_pass.Checked == false)
                txt_password.UseSystemPasswordChar = true;
            else
                txt_password.UseSystemPasswordChar = false;
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application", "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_username.Text == "admin")
            {
                try
                {
                    
                    if (txt_username.Text.Length == 0)
                    {
                        labl_username.Text = "Please enter user name";
                        txt_username.Focus();
                    }
                    else if (txt_password.Text.Length == 0)
                    {
                        labl_username.Text = "";
                        labl_password.Text = "Please enter password";
                        txt_password.Focus();
                    }
                    else 
                    {
                        labl_password.Text = "";
                        labl_username.Text = "";
                        cm = new SqlCommand("SELECT * FROM tbuser WHERE username=@username AND password=@password", con);
                        cm.Parameters.AddWithValue("@username", txt_username.Text);
                        cm.Parameters.AddWithValue("@password", txt_password.Text);
                        con.Open();
                        dr = cm.ExecuteReader();
                        dr.Read();
                        if (dr.HasRows)
                        {
                            MessageBox.Show("Welcome " + dr["fullname"].ToString() + " | ", "ACCESS GENERATED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Main_Form main = new Main_Form();
                            this.Hide();
                            main.ShowDialog();

                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password!", "ACCESS DENITED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        con.Close();
                    }

                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    
                    if (txt_username.Text.Length == 0)
                    {
                        labl_username.Text = "Please enter user name";
                        txt_username.Focus();
                    }
                    else if (txt_password.Text.Length == 0)
                    {
                        labl_username.Text = "";
                        labl_password.Text = "Please enter password";
                        txt_password.Focus();
                    }
                    else
                    {
                        labl_password.Text = "";
                        labl_username.Text = "";
                        cm = new SqlCommand("SELECT * FROM tbuser WHERE username=@username AND password=@password", con);
                        cm.Parameters.AddWithValue("@username", txt_username.Text);
                        cm.Parameters.AddWithValue("@password", txt_password.Text);
                        con.Open();
                        dr = cm.ExecuteReader();
                        dr.Read();
                        if (dr.HasRows)
                        {
                            MessageBox.Show("Welcome " + dr["fullname"].ToString() + " | ", "ACCESS GENERATED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Main_User_Form main = new Main_User_Form();
                            this.Hide();
                            main.ShowDialog();

                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password!", "ACCESS DENITED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
