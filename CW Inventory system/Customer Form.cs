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
    public partial class Customer_Form : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-Q9L167I0;Initial Catalog=DBMS;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Customer_Form()
        {
            InitializeComponent();
            LoadCustomer();
        }
        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbCustomer", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            con.Close();


        }

        private void btn_add_user_Click(object sender, EventArgs e)
        {
            Customer_Module_Form module_Form = new Customer_Module_Form();
            module_Form.btn_save.Enabled = true;
            module_Form.btn_update.Enabled = false;
            module_Form.ShowDialog();
            LoadCustomer();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                Customer_Module_Form customer_Module = new Customer_Module_Form();
                customer_Module.labl_cid.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                customer_Module.txt_Name.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                customer_Module.txt_phone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();


                customer_Module.btn_save.Enabled = false;
                customer_Module.btn_update.Enabled = true;
                customer_Module.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you suer you want to delete this customer", "Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbCustomer WHERE cid LIKE '" + dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                }
            }
            LoadCustomer();
        }
    }
}
