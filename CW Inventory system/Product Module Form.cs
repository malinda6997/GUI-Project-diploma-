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
    public partial class Product_Module_Form : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-Q9L167I0;Initial Catalog=DBMS;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Product_Module_Form()
        {
            InitializeComponent();
            LoadCategory();
        }
        public void LoadCategory()
        {
            comb_category.Items.Clear();
            cm = new SqlCommand("SELECT catname From tbcategory", con);
            con.Open();
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                comb_category.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if(txt_pname.Text.Length == 0)
                {
                    labl_p_name.Text = "Please enter product name";
                    txt_pname.Focus();
                }
                else if(txt_quantity.Text.Any(char.IsLetter))
                {
                    labl_p_name.Text = "";
                    labl_quantity.Text = "Please enter numbers only";
                    txt_quantity.Focus();
                }
                else if(txt_quantity.Text.Length == 0)
                {
                    labl_quantity.Text = "";
                    labl_p_name.Text = "";
                    labl_quantity.Text = "Please enter quantity";
                    txt_quantity.Focus();
                }
                else if(txt_price.Text.Any(char.IsLetter))
                {
                    labl_quantity.Text = "";
                    labl_price.Text = "Please enter numbers only";
                    txt_price.Focus();
                }
                else if(txt_price.Text.Length == 0)
                {
                    labl_price.Text = "";
                    labl_quantity.Text = "";
                    labl_price.Text = "Please enter price";
                    txt_price.Focus();
                }
                else if(comb_category.Text.Length == 0)
                {
                    labl_price.Text = "";
                    labl_category.Text = "Please select category";
                    comb_category.Focus();
                }
                else if (MessageBox.Show("Are you suer you want to save this product", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    labl_category.Text = "";
                    labl_price.Text = "";
                    labl_quantity.Text = "";
                    labl_p_name.Text = "";
                    cm = new SqlCommand("INSERT INTO tbproduct(pname,pqty,pprice,pdescription,pcategory)values(@pname,@pqty,@pprice,@pdescription,@pcategory)", con);
                    cm.Parameters.AddWithValue("@pname", txt_pname.Text);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt32(txt_quantity.Text));
                    cm.Parameters.AddWithValue("@pprice", Convert.ToInt32(txt_price.Text));
                    cm.Parameters.AddWithValue("@pdescription", txt_description.Text);
                    cm.Parameters.AddWithValue("@pcategory", comb_category.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been successfully saved");
                    clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void clear()
        {
            labl_category.Text = "";
            labl_price.Text = "";
            labl_quantity.Text = "";
            labl_p_name.Text = "";
            txt_pname.Clear();
            txt_quantity.Clear();
            txt_price.Clear();
            txt_description.Clear();
            comb_category.Text = "";
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
            btn_save.Enabled = true;
            btn_update.Enabled = false;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (MessageBox.Show("Are you suer you want to update this product", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbproduct SET pname=@pname,pqty=@pqty,pprice=@pprice,pdescription=@pdescription,pcategory=@pcategory WHERE pid LIKE '" + labl_pid.Text + "' ", con);
                    cm.Parameters.AddWithValue("@pname", txt_pname.Text);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt32(txt_quantity.Text));
                    cm.Parameters.AddWithValue("@pprice", Convert.ToDouble(txt_price.Text));
                    cm.Parameters.AddWithValue("@pdescription", txt_description.Text);
                    cm.Parameters.AddWithValue("@pcategory", comb_category.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been successfully update");
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
