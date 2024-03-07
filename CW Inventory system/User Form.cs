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
    public partial class User_Form : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-Q9L167I0;Initial Catalog=DBMS;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public User_Form()
        {
            InitializeComponent();
            LoadUser();
        }

        public void LoadUser()
        {
            int i = 0;
            dgvUser.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbuser", con);
            con.Open();
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i++;
                dgvUser.Rows.Add(i,dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();
            con.Close();


        }

        private void btn_add_user_Click(object sender, EventArgs e)
        {
            User_Module_Form user_Module = new User_Module_Form();
            user_Module.btn_save.Enabled = true;
            user_Module.btn_update.Enabled = false;
            user_Module.ShowDialog();
            LoadUser();
            
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUser.Columns[e.ColumnIndex].Name;
            if(colName == "Edit")
            {
                 User_Module_Form user_Module = new User_Module_Form();
                user_Module.txt_user_name.Text = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                user_Module.txt_full_name.Text = dgvUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                user_Module.txt_password.Text = dgvUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                user_Module.txt_phone_no.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value.ToString();

                user_Module.btn_save.Enabled = false;
                user_Module.btn_update.Enabled = true;
                user_Module.txt_user_name.Enabled = false;
                user_Module.ShowDialog();
            }
            else if(colName == "Delete")
            {
                if(MessageBox.Show("Are you suer you want to delete this user","Record",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbuser WHERE username LIKE '" + dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                }
            }
            LoadUser();
        }
    }
}
