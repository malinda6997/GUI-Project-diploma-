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
    public partial class Customer_button : PictureBox
    {
        public Customer_button()
        {
            InitializeComponent();
        }

        private Image Normalimage;
        private Image HoverImage;

        public Image ImageNormal
        {
            get { return Normalimage; }
            set { Normalimage = value; }
        }

        public Image ImageHover
        {
            get { return HoverImage; }
            set { HoverImage = value; }
        }

        private void Customer_button_MouseHover(object sender, EventArgs e)
        {
            this.Image = HoverImage;
        }

        private void Customer_button_MouseLeave(object sender, EventArgs e)
        {
            this.Image = Normalimage;
        }
    }
}
