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
    public partial class Loans : Form
    {
        public Loans()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();

        public void GetLoans()
        {
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select * from Loans", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    conn.Open();
                    dt.Clear();
                    da.Fill(dt);
                    dtgListL.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void Loans_Load(object sender, EventArgs e)
        {
            GetLoans();
            LoadClientsAndMaterials();
        }

        private void LoadClientsAndMaterials()
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

                    cmbLClient.DataSource = dtClients;
                    cmbLClient.DisplayMember = "FullName";
                    cmbLClient.ValueMember = "ClientID";
                    cmbLClient.SelectedIndex = -1;

                    
                    SqlCommand cmdMaterials = new SqlCommand("SELECT MaterialID, Title FROM Bibliographic_Materials", conn);
                    SqlDataAdapter daMaterials = new SqlDataAdapter(cmdMaterials);
                    DataTable dtMaterials = new DataTable();
                    daMaterials.Fill(dtMaterials);

                    cmbLMaterial.DataSource = dtMaterials;
                    cmbLMaterial.DisplayMember = "Title";
                    cmbLMaterial.ValueMember = "MaterialID";
                    cmbLMaterial.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message, "Library System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InsertLoans()
        {
            if (cmbLClient.SelectedItem == null ||
                cmbLMaterial.SelectedItem == null)
            {
                MessageBox.Show("Please select both ClientID and MaterialID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Loans(ClientID, MaterialID, LoanDate, ReturnDate, ActualReturnDate, PenaltyFee) VALUES(@ClientID, @MaterialID, @LoanDate, @ReturnDate, @ActualReturnDate, @PenaltyFee)", conn);

                    cmd.Parameters.AddWithValue("@ClientID", Convert.ToInt32(cmbLClient.SelectedValue));
                    cmd.Parameters.AddWithValue("@MaterialID", Convert.ToInt32(cmbLMaterial.SelectedValue));
                    cmd.Parameters.AddWithValue("@LoanDate", dtpLDate.Value);
                    cmd.Parameters.AddWithValue("@ReturnDate", dtpRDate.Value);
                    cmd.Parameters.AddWithValue("@ActualReturnDate", dtpARDate.Value);
                    cmd.Parameters.AddWithValue("@PenaltyFee", nudPFee.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Loan successfully recorded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetLoans();
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
            txtLID.Clear();
            cmbLClient.SelectedIndex = -1;
            cmbLMaterial.SelectedIndex = -1;
            dtpLDate.Value = DateTime.Now;
            dtpRDate.Value = DateTime.Now;
            dtpARDate.Value = DateTime.Now;
            nudPFee.Value = 0;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            InsertLoans();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtSearch.Text.ToLower();
            if (!string.IsNullOrEmpty(filterText))
            {
                DataView dataView = new DataView(dt);
                dataView.RowFilter = $"LoanDate like '%{filterText}%'";
                dtgListL.DataSource = dataView;
            }
            else
            {
                dtgListL.DataSource = dt;
            }
        }

        
    }
}
