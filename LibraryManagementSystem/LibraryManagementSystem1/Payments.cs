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

namespace LibraryManagementSystem1
{
    public partial class Payments : Form
    {
        public Payments()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();

        public void GetPayments()
        {
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select * from Payments", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    conn.Open();
                    dt.Clear();
                    da.Fill(dt);
                    dtgListP.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void LoadClients()
        {
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                try
                {
                    conn.Open();

                    
                    SqlCommand cmdClients = new SqlCommand("SELECT ClientID, FirstName + ' ' + LastName AS FullName FROM Clients", conn);
                    SqlDataAdapter daClients = new SqlDataAdapter(cmdClients);
                    DataTable dtClients = new DataTable();
                    daClients.Fill(dtClients);

                    cmbPClient.DataSource = dtClients;
                    cmbPClient.DisplayMember = "FullName";
                    cmbPClient.ValueMember = "ClientID";
                    cmbPClient.SelectedIndex = -1;

                   ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message, "Library System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InsertPayments()
        {
            if (cmbPClient.SelectedItem == null )
                
            {
                MessageBox.Show("Please select both Client and Payment Type!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Payments(ClientID, Amount, PaymentDate, PaymentType) VALUES(@ClientID, @Amount, @PaymentDate, @PaymentType)", conn);

                    cmd.Parameters.AddWithValue("@ClientID", Convert.ToInt32(cmbPClient.SelectedValue));
                    cmd.Parameters.AddWithValue("@Amount", nudAmount.Value);
                    cmd.Parameters.AddWithValue("@PaymentDate", dtpPDate.Value);
                    cmd.Parameters.AddWithValue("@PaymentType", cmbPType.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Payment successfully recorded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetPayments();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void ClearFields()
        {
            txtPID.Clear();
            cmbPClient.SelectedIndex = -1;
            nudAmount.Value = 0;
            dtpPDate.Value = DateTime.Now;
            cmbPType.SelectedIndex = -1;
        }

        private void Payments_Load(object sender, EventArgs e)
        {
            GetPayments();
            LoadClients();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            InsertPayments();
        }

        
    }
}
