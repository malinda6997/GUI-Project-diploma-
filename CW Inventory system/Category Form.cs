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
    
    public partial class Category_Form : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-Q9L167I0;Initial Catalog=DBMS;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Category_Form()
        {
            InitializeComponent();
            LoadCategory();
        }
        public void LoadCategory()
        {
            int i = 0;
            dgvCategory.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbcategory", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCategory.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();


        }

        private void btn_add_user_Click(object sender, EventArgs e)
        {
            Category_Module_Form module_Form = new Category_Module_Form();
            module_Form.btn_save.Enabled = true;
            module_Form.btn_update.Enabled = false;
            module_Form.ShowDialog();
            LoadCategory();
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                Category_Module_Form module_Form = new Category_Module_Form();
                module_Form.labl_category_id.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                module_Form.txt_category_name.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();



                module_Form.btn_save.Enabled = false;
                module_Form.btn_update.Enabled = true;
                module_Form.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you suer you want to delete this category", "Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbcategory WHERE catid LIKE '" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                }
            }
            LoadCategory();
        }
    }
}
