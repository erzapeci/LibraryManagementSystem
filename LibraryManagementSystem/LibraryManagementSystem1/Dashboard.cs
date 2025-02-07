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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();

            
        }

        private void btnBMaterials_Click(object sender, EventArgs e)
        {
            BibliographicMaterials bmaterial = new BibliographicMaterials();
            bmaterial.MdiParent = Dashboard.ActiveForm;
            bmaterial.Show();
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            Clients client = new Clients();
            client.MdiParent = Dashboard.ActiveForm;
            client.Show();
        }

        private void btnLoans_Click(object sender, EventArgs e)
        {
            Loans loans = new Loans();
            loans.MdiParent = Dashboard.ActiveForm;
            loans.Show();
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            Payments payments = new Payments();
            payments.MdiParent = Dashboard.ActiveForm;
            payments.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.MdiParent = Dashboard.ActiveForm;
            users.Show();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports();
            reports.MdiParent = Dashboard.ActiveForm;
            reports.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Hide();

            }
        }

    }
}
