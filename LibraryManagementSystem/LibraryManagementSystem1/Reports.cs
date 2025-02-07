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
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void btnCPayment_Click(object sender, EventArgs e)
        {
            ClientsPayments clientsPayments = new ClientsPayments();
            clientsPayments.ShowDialog(this);
        }

        private void btnOLoans_Click(object sender, EventArgs e)
        {
            OverdueLoans overdueLoans = new OverdueLoans();
            overdueLoans.ShowDialog(this);
        }

        private void btnAClients_Click(object sender, EventArgs e)
        {
            ActiveClients activeClients = new ActiveClients();
            activeClients.ShowDialog(this);
        }
    }
}
