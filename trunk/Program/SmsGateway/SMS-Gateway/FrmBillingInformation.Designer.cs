namespace SMS_Gateway
{
    partial class FrmBillingInformation
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.Gb1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.billingInfo = new SMS_Gateway.AppData.BillingInfo();
            this.viewbillingBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.viewbillingTableAdapter = new SMS_Gateway.AppData.BillingInfoTableAdapters.viewbillingTableAdapter();
            this.viewbillingBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.Gb1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.billingInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewbillingBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewbillingBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // Gb1
            // 
            this.Gb1.Controls.Add(this.comboBox3);
            this.Gb1.Controls.Add(this.label3);
            this.Gb1.Controls.Add(this.comboBox2);
            this.Gb1.Controls.Add(this.comboBox1);
            this.Gb1.Controls.Add(this.label2);
            this.Gb1.Controls.Add(this.button1);
            this.Gb1.Controls.Add(this.label1);
            this.Gb1.Location = new System.Drawing.Point(2, 6);
            this.Gb1.Name = "Gb1";
            this.Gb1.Size = new System.Drawing.Size(504, 91);
            this.Gb1.TabIndex = 3;
            this.Gb1.TabStop = false;
            this.Gb1.Text = "Control";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(243, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "PrintOut";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "menu";
            this.dataGridViewTextBoxColumn1.HeaderText = "menu";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.DataPropertyName = "price";
            this.dataGridViewImageColumn1.HeaderText = "price";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn2.HeaderText = "id";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "BillingInfo_viewbilling";
            reportDataSource1.Value = this.viewbillingBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SMS_Gateway.BillingInformation.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(2, 103);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(504, 305);
            this.reportViewer1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Month";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(87, 55);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "01",
            "02",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.comboBox2.Location = new System.Drawing.Point(87, 23);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(51, 21);
            this.comboBox2.TabIndex = 5;
            this.comboBox2.Text = "01";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "2008",
            "2009",
            "2010",
            "2011"});
            this.comboBox3.Location = new System.Drawing.Point(211, 23);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(67, 21);
            this.comboBox3.TabIndex = 7;
            this.comboBox3.Text = "2010";
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(158, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Year";
            // 
            // billingInfo
            // 
            this.billingInfo.DataSetName = "BillingInfo";
            this.billingInfo.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // viewbillingBindingSource
            // 
            this.viewbillingBindingSource.DataMember = "viewbilling";
            this.viewbillingBindingSource.DataSource = this.billingInfo;
            // 
            // viewbillingTableAdapter
            // 
            this.viewbillingTableAdapter.ClearBeforeFill = true;
            // 
            // viewbillingBindingSource1
            // 
            this.viewbillingBindingSource1.DataMember = "viewbilling";
            this.viewbillingBindingSource1.DataSource = this.billingInfo;
            // 
            // FrmBillingInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 413);
            this.Controls.Add(this.Gb1);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmBillingInformation";
            this.Text = "FrmBillingInformation";
            this.Load += new System.EventHandler(this.FrmBillingInformation_Load);
            this.Gb1.ResumeLayout(false);
            this.Gb1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.billingInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewbillingBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewbillingBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Gb1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource viewbillingBindingSource;
        private SMS_Gateway.AppData.BillingInfo billingInfo;
        private SMS_Gateway.AppData.BillingInfoTableAdapters.viewbillingTableAdapter viewbillingTableAdapter;
        private System.Windows.Forms.BindingSource viewbillingBindingSource1;
    }
}