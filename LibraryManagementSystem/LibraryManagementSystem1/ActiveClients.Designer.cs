namespace LibraryManagementSystem1
{
    partial class ActiveClients
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label10 = new System.Windows.Forms.Label();
            this.dtgListAC = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEA = new System.Windows.Forms.DateTimePicker();
            this.btnGENERATE = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpSA = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dtgListAC)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label10.Location = new System.Drawing.Point(27, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(279, 54);
            this.label10.TabIndex = 80;
            this.label10.Text = "Active Clients";
            // 
            // dtgListAC
            // 
            this.dtgListAC.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(104)))), ((int)(((byte)(154)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgListAC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgListAC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgListAC.DefaultCellStyle = dataGridViewCellStyle6;
            this.dtgListAC.Location = new System.Drawing.Point(36, 252);
            this.dtgListAC.Name = "dtgListAC";
            this.dtgListAC.RowHeadersWidth = 51;
            this.dtgListAC.RowTemplate.Height = 24;
            this.dtgListAC.Size = new System.Drawing.Size(1108, 478);
            this.dtgListAC.TabIndex = 79;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(58, 195);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 28);
            this.label1.TabIndex = 84;
            this.label1.Text = "End Date:";
            // 
            // dtpEA
            // 
            this.dtpEA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEA.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEA.Location = new System.Drawing.Point(199, 195);
            this.dtpEA.Name = "dtpEA";
            this.dtpEA.Size = new System.Drawing.Size(181, 27);
            this.dtpEA.TabIndex = 83;
            // 
            // btnGENERATE
            // 
            this.btnGENERATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(104)))), ((int)(((byte)(154)))));
            this.btnGENERATE.FlatAppearance.BorderSize = 0;
            this.btnGENERATE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGENERATE.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGENERATE.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnGENERATE.Location = new System.Drawing.Point(633, 170);
            this.btnGENERATE.Name = "btnGENERATE";
            this.btnGENERATE.Size = new System.Drawing.Size(278, 45);
            this.btnGENERATE.TabIndex = 87;
            this.btnGENERATE.Text = "GENERATE";
            this.btnGENERATE.UseVisualStyleBackColor = false;
            this.btnGENERATE.Click += new System.EventHandler(this.btnGENERATE_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(58, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 28);
            this.label2.TabIndex = 89;
            this.label2.Text = "Start Date:";
            // 
            // dtpSA
            // 
            this.dtpSA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSA.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSA.Location = new System.Drawing.Point(199, 144);
            this.dtpSA.Name = "dtpSA";
            this.dtpSA.Size = new System.Drawing.Size(181, 27);
            this.dtpSA.TabIndex = 88;
            // 
            // ActiveClients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(80)))), ((int)(((byte)(138)))));
            this.ClientSize = new System.Drawing.Size(1200, 746);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpSA);
            this.Controls.Add(this.btnGENERATE);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpEA);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dtgListAC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ActiveClients";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ActiveClients";
            ((System.ComponentModel.ISupportInitialize)(this.dtgListAC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dtgListAC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEA;
        private System.Windows.Forms.Button btnGENERATE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpSA;
    }
}