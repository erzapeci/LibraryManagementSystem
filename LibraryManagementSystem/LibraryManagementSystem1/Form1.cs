using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 20; // Rrit me një vlerë më të vogël për lëvizje më të ngadaltë

            if (panel2.Width >= this.Width) // Përdor madhësinë e formës në vend të një vlere fikse
            {
                timer1.Stop();
                Login login = new Login();
                login.ShowDialog(); // Hap dritaren e Login
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
            // Vendos sfondin blu
            this.BackColor = Color.FromArgb(25, 69, 140);

            // Qendro PictureBox në mes
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = this.ClientSize.Height / 4;

            // Qendro Label nën PictureBox
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = pictureBox1.Bottom + 10; // 10px hapësirë poshtë logos
        }
    }
}


