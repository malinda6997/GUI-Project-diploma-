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
    public partial class Product_Form : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-Q9L167I0;Initial Catalog=DBMS;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Product_Form()
        {
            InitializeComponent();
            LoadProduct();
        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbproduct WHERE CONCAT(pid, pname, pprice, pdescription, pcategory) LIKE '%"+txt_search.Text+"%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();


        }

        private void btn_add_user_Click(object sender, EventArgs e)
        {
            Product_Module_Form module_Form = new Product_Module_Form();
            module_Form.btn_save.Enabled = true;
            module_Form.btn_update.Enabled = false;
            module_Form.ShowDialog();
            LoadProduct();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                Product_Module_Form Product_Module = new Product_Module_Form();
                Product_Module.labl_pid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                Product_Module.txt_pname.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                Product_Module.txt_quantity.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                Product_Module.txt_price.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                Product_Module.txt_description.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                Product_Module.comb_category.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();

                Product_Module.btn_save.Enabled = false;
                Product_Module.btn_update.Enabled = true;
                Product_Module.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you suer you want to delete this product", "Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbproduct WHERE pid LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                }
            }
            LoadProduct();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
