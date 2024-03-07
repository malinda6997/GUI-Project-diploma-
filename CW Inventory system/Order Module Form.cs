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
    public partial class Order_Module_Form : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-Q9L167I0;Initial Catalog=DBMS;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;
        public Order_Module_Form()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            
        }
        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT cid, cname FROM tbCustomer WHERE CONCAT(cid,cname) LIKE '%"+txt_search_customer.Text+"%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();

        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbproduct WHERE CONCAT(pid, pname, pprice, pdescription, pcategory) LIKE '%" + txt_search_product.Text + "%'", con);
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

        private void txt_search_customer_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void txt_search_product_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }


        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_cid.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_cname.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        
        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_pid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_pname.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txt_price.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GetQty();
            if (Convert.ToInt32(numericUpDown1.Value) > qty)
            {
                MessageBox.Show("Insert quantity is not enough!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericUpDown1.Value = numericUpDown1.Value - 1;
                return;
            }
            if(Convert.ToInt32(numericUpDown1.Value) > 0)
            {
                int total = Convert.ToInt32(txt_price.Text) * Convert.ToInt32(numericUpDown1.Value);
                txt_total.Text = total.ToString();
            }
            
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_cid.Text == "")
                {
                    MessageBox.Show("Please select customer!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txt_pid.Text == "")
                {
                    MessageBox.Show("Please select product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you suer you want to insert this order", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tborder(odate,pid,cid,qty,price,total)VALUES(@odate,@pid,@cid,@qty,@price,@total)", con);
                    cm.Parameters.AddWithValue("@odate", dateTimePick.Value);
                    cm.Parameters.AddWithValue("@pid", Convert.ToInt32(txt_pid.Text));
                    cm.Parameters.AddWithValue("@cid", Convert.ToInt32(txt_cid.Text));
                    cm.Parameters.AddWithValue("@qty", Convert.ToInt32(numericUpDown1.Value));
                    cm.Parameters.AddWithValue("@price", Convert.ToInt32(txt_price.Text));
                    cm.Parameters.AddWithValue("@total", Convert.ToInt32(txt_total.Text));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Order has been successfully inserted");
                    

                    cm = new SqlCommand("UPDATE tbproduct SET pqty=(pqty-@pqty) WHERE pid LIKE '" + txt_pid.Text + "' ", con);
                   
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt32(numericUpDown1.Value));
                   
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    LoadProduct();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txt_cid.Clear();
            txt_cname.Clear();

            txt_pid.Clear();
            txt_pname.Clear();

            txt_price.Clear();
            numericUpDown1.Value = 0;
            txt_total.Clear();
            dateTimePick.Value = DateTime.Now;

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            Clear();
            
            
        }

        public void GetQty()
        {
            cm = new SqlCommand("SELECT pqty FROM tbproduct WHERE pid= '" + txt_pid.Text + "'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                qty =Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }


    }
}
