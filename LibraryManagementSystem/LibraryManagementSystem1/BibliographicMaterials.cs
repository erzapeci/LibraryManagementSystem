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
    public partial class BibliographicMaterials : Form
    {
        public BibliographicMaterials()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        public void GetBibliographicMaterials()
        {
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT bm.MaterialID,bm.Title, bm.Author, bm.CoAuthors, bm.PublicationDate, bm.ISBN, bm.DOI, bm.MaterialType, bm.AvailableCopies, bm.Publisher\r\nFROM \r\n    Bibliographic_Materials bm;", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    conn.Open();
                    dt.Clear();
                    da.Fill(dt);
                    dtgListB.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message);
                }
            }
        }

        private void InsertNewBibliographicMaterials()
        {
            if (txtTitle.Text == "")
            {
                MessageBox.Show("Please fill in the Title field", "Library System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtAuthor.Text == "")
            {
                MessageBox.Show("Please fill in the Author field", "Library System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbMaterialType.Text == "")
            {
                MessageBox.Show("Please select a Material Type", "Library System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                dtgListB.DataSource = null;
                dt.Clear();

                using (SqlConnection connection = ConnectDB.GetConnection()) 
                {
                    try
                    {
                        connection.Open();

                        
                        string insertQuery = @"INSERT INTO Bibliographic_Materials 
                                       (Title, Author, CoAuthors, PublicationDate, ISBN, DOI, MaterialType, AvailableCopies, Publisher)
                                       VALUES (@Title, @Author, @CoAuthors, @PublicationDate, @ISBN, @DOI, @MaterialType, @AvailableCopies, @Publisher)";

                        using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                        {
                            
                            cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                            cmd.Parameters.AddWithValue("@Author", txtAuthor.Text);
                            cmd.Parameters.AddWithValue("@CoAuthors", txtCoAuthor.Text);
                            cmd.Parameters.AddWithValue("@PublicationDate", dtpPublDate.Value); 
                            cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                            cmd.Parameters.AddWithValue("@DOI", txtDOI.Text);
                            cmd.Parameters.AddWithValue("@MaterialType", cmbMaterialType.Text);
                            cmd.Parameters.AddWithValue("@AvailableCopies", int.Parse(numAvaCopies.Text));
                            cmd.Parameters.AddWithValue("@Publisher", txtPublisher.Text);

                            
                            int rowsAffected = cmd.ExecuteNonQuery();

                            
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("New Bibliographic Material inserted successfully", "Library System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                GetBibliographicMaterials(); 
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
        }
        private void ClearFields()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtCoAuthor.Clear();
            txtISBN.Clear();
            txtDOI.Clear();
            numAvaCopies.Value = 0;
            txtPublisher.Clear();
            cmbMaterialType.SelectedIndex = -1;
            dtpPublDate.Value = DateTime.Now;
        }

        private void UpdateBibliographyMaterials()
        {
            if (txtTitle.Text == "")
            {
                MessageBox.Show("Fill necessary fills", "HMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtAuthor.Text == "")
            {
                MessageBox.Show("Fill necessary fills", "HMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbMaterialType.Text == "")
            {
                MessageBox.Show("Fill necessary fills", "HMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtMID.Text == "")
            {
                MessageBox.Show("Fill necessary fills", "HMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                dtgListB.DataSource = null;
                dt.Clear();

                using (SqlConnection connection = ConnectDB.GetConnection())
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE Bibliographic_Materials SET Title = @Title, Author = @Author, CoAuthors = @CoAuthors, PublicationDate = @PublicationDate, ISBN = @ISBN, DOI = @DOI, MaterialType = @MaterialType, AvailableCopies = @AvailableCopies, Publisher = @Publisher WHERE MaterialID = @MaterialID", connection);

                        cmd.Parameters.AddWithValue("@MaterialID", txtMID.Text);
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                        cmd.Parameters.AddWithValue("@Author", txtAuthor.Text);
                        cmd.Parameters.AddWithValue("@CoAuthors", txtCoAuthor.Text);
                        cmd.Parameters.AddWithValue("@PublicationDate", dtpPublDate.Value);
                        cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                        cmd.Parameters.AddWithValue("@DOI", txtDOI.Text);
                        cmd.Parameters.AddWithValue("@MaterialType", cmbMaterialType.Text);
                        cmd.Parameters.AddWithValue("@AvailableCopies", numAvaCopies.Value);
                        cmd.Parameters.AddWithValue("@Publisher", txtPublisher.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Bibliography material updated successfully", "HMS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        GetBibliographicMaterials();  
                        ClearFields();  
                        btnAddSave.Enabled = true;
                        btnEdit.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void dtgListB_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAddSave.Enabled = false;
            btnEdit.Enabled = true;

            txtMID.Text = dtgListB.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTitle.Text = dtgListB.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtAuthor.Text = dtgListB.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtCoAuthor.Text = dtgListB.Rows[e.RowIndex].Cells[3].Value.ToString();
            dtpPublDate.Value = Convert.ToDateTime(dtgListB.Rows[e.RowIndex].Cells[4].Value.ToString());
            txtISBN.Text = dtgListB.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtDOI.Text = dtgListB.Rows[e.RowIndex].Cells[6].Value.ToString();
            cmbMaterialType.Text = dtgListB.Rows[e.RowIndex].Cells[7].Value.ToString();
            numAvaCopies.Value = Convert.ToDecimal(dtgListB.Rows[e.RowIndex].Cells[8].Value.ToString());
            txtPublisher.Text = dtgListB.Rows[e.RowIndex].Cells[9].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtSearch.Text.ToLower();
            if (!string.IsNullOrEmpty(filterText))
            {
                DataView dataView = new DataView(dt);
                dataView.RowFilter = $"Title like '%{filterText}%'";
                dtgListB.DataSource = dataView;
            }
            else
            {
                dtgListB.DataSource = dt;
            }
        }

        private void BibliographicMaterials_Load(object sender, EventArgs e)
        {
            GetBibliographicMaterials();
        }

        private void btnAddSave_Click(object sender, EventArgs e)
        {
            InsertNewBibliographicMaterials();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            UpdateBibliographyMaterials();
        }
    }
}
