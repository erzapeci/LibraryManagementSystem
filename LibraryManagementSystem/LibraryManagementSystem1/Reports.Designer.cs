namespace LibraryManagementSystem1
{
    partial class Reports
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reports));
            this.label10 = new System.Windows.Forms.Label();
            this.btnAClients = new System.Windows.Forms.Button();
            this.btnOLoans = new System.Windows.Forms.Button();
            this.btnCPayment = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label10.Location = new System.Drawing.Point(107, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(463, 45);
            this.label10.TabIndex = 68;
            this.label10.Text = "Report Management System";
            // 
            // btnAClients
            // 
            this.btnAClients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(80)))), ((int)(((byte)(138)))));
            this.btnAClients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAClients.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAClients.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAClients.Image = ((System.Drawing.Image)(resources.GetObject("btnAClients.Image")));
            this.btnAClients.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAClients.Location = new System.Drawing.Point(133, 660);
            this.btnAClients.Name = "btnAClients";
            this.btnAClients.Size = new System.Drawing.Size(422, 89);
            this.btnAClients.TabIndex = 71;
            this.btnAClients.Text = "Active \r\nClients";
            this.btnAClients.UseVisualStyleBackColor = false;
            this.btnAClients.Click += new System.EventHandler(this.btnAClients_Click);
            // 
            // btnOLoans
            // 
            this.btnOLoans.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(80)))), ((int)(((byte)(138)))));
            this.btnOLoans.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOLoans.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOLoans.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOLoans.Image = ((System.Drawing.Image)(resources.GetObject("btnOLoans.Image")));
            this.btnOLoans.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOLoans.Location = new System.Drawing.Point(133, 431);
            this.btnOLoans.Name = "btnOLoans";
            this.btnOLoans.Size = new System.Drawing.Size(422, 89);
            this.btnOLoans.TabIndex = 70;
            this.btnOLoans.Text = "Overdue \r\nLoans";
            this.btnOLoans.UseVisualStyleBackColor = false;
            this.btnOLoans.Click += new System.EventHandler(this.btnOLoans_Click);
            // 
            // btnCPayment
            // 
            this.btnCPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(80)))), ((int)(((byte)(138)))));
            this.btnCPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCPayment.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCPayment.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCPayment.Image = ((System.Drawing.Image)(resources.GetObject("btnCPayment.Image")));
            this.btnCPayment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCPayment.Location = new System.Drawing.Point(133, 202);
            this.btnCPayment.Name = "btnCPayment";
            this.btnCPayment.Size = new System.Drawing.Size(422, 89);
            this.btnCPayment.TabIndex = 69;
            this.btnCPayment.Text = "Clients  \r\nPayments\r\n";
            this.btnCPayment.UseVisualStyleBackColor = false;
            this.btnCPayment.Click += new System.EventHandler(this.btnCPayment_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(649, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(926, 900);
            this.panel1.TabIndex = 72;
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(80)))), ((int)(((byte)(138)))));
            this.ClientSize = new System.Drawing.Size(1575, 900);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAClients);
            this.Controls.Add(this.btnOLoans);
            this.Controls.Add(this.btnCPayment);
            this.Controls.Add(this.label10);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Reports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reports";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnAClients;
        private System.Windows.Forms.Button btnOLoans;
        private System.Windows.Forms.Button btnCPayment;
        private System.Windows.Forms.Panel panel1;
    }
}