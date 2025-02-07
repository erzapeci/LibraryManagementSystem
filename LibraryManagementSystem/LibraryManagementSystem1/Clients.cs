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
    public partial class Clients : Form
    {
        public Clients()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();

        public void GetClients()
        {
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT ClientID, FirstName, LastName, Email, Phone, Address, DateOfBirth, MembershipActive, RegistrationDate FROM Clients;", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    conn.Open();
                    dt.Clear();
                    da.Fill(dt);
                    dtgListC.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            GetClients();
        }

        private void InsertNewClient()
        {
            if (txtFName.Text == "")
            {
                MessageBox.Show("Fill necessary fills", "HMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtLName.Text == "")
            {
                MessageBox.Show("Fill necessary fills", "HMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbMemberActive.Text == "")
            {
                MessageBox.Show("Fill necessary fills", "HMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            dtgListC.DataSource = null;
            dt.Clear();

            using (SqlConnection connection = ConnectDB.GetConnection())
            {
                try
                {
                    connection.Open();

                    string insertQuery = @"INSERT INTO Clients 
                                   (FirstName, LastName, Email, Phone, Address, DateOfBirth, MembershipActive, RegistrationDate) 
                                   VALUES (@FirstName, @LastName, @Email, @Phone, @Address, @DateOfBirth, @MembershipActive, @RegistrationDate)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", txtFName.Text);
                        cmd.Parameters.AddWithValue("@LastName", txtLName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dtpCBirth.Value);
                        cmd.Parameters.AddWithValue("@MembershipActive", cmbMemberActive.Text);
                        cmd.Parameters.AddWithValue("@RegistrationDate", dtpRDate.Value);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("New Client inserted successfully", "Library System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GetClients();
                            ClearFields();
                        }
                        else
                        {
                            MessageBox.Show("No rows were inserted. Please check your data.", "Library System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Library System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        

        

        private void UpdateClients()
        {
            if (txtFName.Text == "")
            {
                MessageBox.Show("Fill necessary fills", "HMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtLName.Text == "")
            {
                MessageBox.Show("Fill necessary fills", "HMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbMemberActive.Text == "")
            {
                MessageBox.Show("Fill necessary fills", "HMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            dtgListC.DataSource = null;
            dt.Clear();

            using (SqlConnection connection = ConnectDB.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(@"UPDATE Clients  
                                                    SET  FirstName = @FirstName, LastName = @LastName, Email = @Email,  
                                                        Phone = @Phone, Address = @Address, DateOfBirth = @DateOfBirth, 
                                                        MembershipActive = @MembershipActive, RegistrationDate = @RegistrationDate  
                                                    WHERE ClientID = @ClientID", connection);

                    cmd.Parameters.AddWithValue("@ClientID", txtCID.Text);
                    cmd.Parameters.AddWithValue("@FirstName", txtFName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", dtpCBirth.Value);
                    cmd.Parameters.AddWithValue("@MembershipActive", cmbMemberActive.SelectedItem);
                    cmd.Parameters.AddWithValue("@RegistrationDate", dtpRDate.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Client information updated successfully", "Library System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetClients();
                        ClearFields();
                        btnCADD.Enabled = true;
                        btnCEdit.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("No rows were updated. Please check the Client ID.", "Library System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Library System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearFields()
        {
            txtCID.Clear();
            txtFName.Clear();
            txtLName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            cmbMemberActive.SelectedIndex = -1;
            dtpRDate.Value = DateTime.Now;
            dtpCBirth.Value = DateTime.Now;
        }

        

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtSearch.Text.ToLower();
            if (!string.IsNullOrEmpty(filterText))
            {
                DataView dataView = new DataView(dt);
                dataView.RowFilter = $"FirstName like '%{filterText}%'";
                dtgListC.DataSource = dataView;
            }
            else
            {
                dtgListC.DataSource = dt;
            }
        }

        private void dtgListC_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnCADD.Enabled = false;
                btnCEdit.Enabled = true;

                txtCID.Text = dtgListC.Rows[e.RowIndex].Cells[0].Value?.ToString();
                txtFName.Text = dtgListC.Rows[e.RowIndex].Cells[1].Value?.ToString();
                txtLName.Text = dtgListC.Rows[e.RowIndex].Cells[2].Value?.ToString();
                txtEmail.Text = dtgListC.Rows[e.RowIndex].Cells[3].Value?.ToString();
                txtPhone.Text = dtgListC.Rows[e.RowIndex].Cells[4].Value?.ToString();
                txtAddress.Text = dtgListC.Rows[e.RowIndex].Cells[5].Value?.ToString();
                dtpCBirth.Value = DateTime.TryParse(dtgListC.Rows[e.RowIndex].Cells[6].Value?.ToString(), out var date) ? date : DateTime.Now;
                cmbMemberActive.Text = dtgListC.Rows[e.RowIndex].Cells[7].Value?.ToString();
            }
        }

        private void btnCADD_Click(object sender, EventArgs e)
        {
            InsertNewClient();
        }

        private void btnCEdit_Click(object sender, EventArgs e)
        {
            UpdateClients();
        }
    }
}
