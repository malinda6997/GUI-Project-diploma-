using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW_Inventory_system
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'Sales.tborder' table. You can move, or remove it, as needed.
            this.tborderTableAdapter.Fill(this.Sales.tborder);

            this.reportViewer1.RefreshReport();
        }
    }
}
