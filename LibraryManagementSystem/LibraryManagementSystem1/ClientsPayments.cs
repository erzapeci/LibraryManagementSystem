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
    public partial class ClientsPayments : Form
    {
        public ClientsPayments()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        public void GenerateClientPayments()
        {
            dtgListCP.DataSource = null;
            dtgListCP.Rows.Clear();
            dt.Clear();

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT * FROM TotalPaymentsClient
                        WHERE (Muaji BETWEEN @MonthFrom AND @MonthTo)
                        AND (Viti BETWEEN @YearFrom AND @YearTo)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MonthFrom", dtpFrom.Value.Month);
                    cmd.Parameters.AddWithValue("@MonthTo", dtpTo.Value.Month);
                    cmd.Parameters.AddWithValue("@YearFrom", dtpFrom.Value.Year);
                    cmd.Parameters.AddWithValue("@YearTo", dtpTo.Value.Year);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    dtgListCP.DataSource = dt;
                    Total();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void Total()
        {
            decimal totalP = 0;

            
            if (dtgListCP.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dtgListCP.Rows)
                {
                    
                    if (!row.IsNewRow && row.Cells["ShumaTotale"].Value != null)
                    {
                        if (decimal.TryParse(row.Cells["ShumaTotale"].Value.ToString(), out decimal amount))
                        {
                            totalP += amount;
                        }
                    }
                }
            }

            
            lblTotalP.Text = totalP.ToString("N2") + " €";
        }


        private void btnGENERATE_Click(object sender, EventArgs e)
        {
            GenerateClientPayments();
        }
    }
}
