namespace SMS_Gateway
{
    partial class FrmDeliveryOrder
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Gb1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.deliveryReport = new SMS_Gateway.AppData.DeliveryReport();
            this.customerprofileBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.customer_profileTableAdapter = new SMS_Gateway.AppData.DeliveryReportTableAdapters.customer_profileTableAdapter();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deliveryReportDetil = new SMS_Gateway.AppData.DeliveryReportDetil();
            this.viewdeliveryorderdetilBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.viewdeliveryorderdetilTableAdapter = new SMS_Gateway.AppData.DeliveryReportDetilTableAdapters.viewdeliveryorderdetilTableAdapter();
            this.Gb1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deliveryReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerprofileBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deliveryReportDetil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewdeliveryorderdetilBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DeliveryReport_customer_profile";
            reportDataSource1.Value = this.customerprofileBindingSource;
            reportDataSource2.Name = "DeliveryReportDetil_viewdeliveryorderdetil";
            reportDataSource2.Value = this.viewdeliveryorderdetilBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SMS_Gateway.DeliveryOrder.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 79);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(514, 305);
            this.reportViewer1.TabIndex = 0;
            // 
            // Gb1
            // 
            this.Gb1.Controls.Add(this.button1);
            this.Gb1.Controls.Add(this.dateTimePicker1);
            this.Gb1.Controls.Add(this.label1);
            this.Gb1.Location = new System.Drawing.Point(12, 12);
            this.Gb1.Name = "Gb1";
            this.Gb1.Size = new System.Drawing.Size(514, 44);
            this.Gb1.TabIndex = 1;
            this.Gb1.TabStop = false;
            this.Gb1.Text = "Control";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Delivery Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(117, 13);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(153, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(286, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "PrintOut";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // deliveryReport
            // 
            this.deliveryReport.DataSetName = "DeliveryReport";
            this.deliveryReport.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // customerprofileBindingSource
            // 
            this.customerprofileBindingSource.DataMember = "customer_profile";
            this.customerprofileBindingSource.DataSource = this.deliveryReport;
            // 
            // customer_profileTableAdapter
            // 
            this.customer_profileTableAdapter.ClearBeforeFill = true;
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
            // deliveryReportDetil
            // 
            this.deliveryReportDetil.DataSetName = "DeliveryReportDetil";
            this.deliveryReportDetil.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // viewdeliveryorderdetilBindingSource
            // 
            this.viewdeliveryorderdetilBindingSource.DataMember = "viewdeliveryorderdetil";
            this.viewdeliveryorderdetilBindingSource.DataSource = this.deliveryReportDetil;
            // 
            // viewdeliveryorderdetilTableAdapter
            // 
            this.viewdeliveryorderdetilTableAdapter.ClearBeforeFill = true;
            // 
            // FrmDeliveryOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 404);
            this.Controls.Add(this.Gb1);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmDeliveryOrder";
            this.Text = "FrmDeliveryOrder";
            this.Load += new System.EventHandler(this.FrmDeliveryOrder_Load);
            this.Gb1.ResumeLayout(false);
            this.Gb1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deliveryReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerprofileBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deliveryReportDetil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewdeliveryorderdetilBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.GroupBox Gb1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private SMS_Gateway.AppData.DeliveryReport deliveryReport;
        private System.Windows.Forms.BindingSource customerprofileBindingSource;
        private SMS_Gateway.AppData.DeliveryReportTableAdapters.customer_profileTableAdapter customer_profileTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.BindingSource viewdeliveryorderdetilBindingSource;
        private SMS_Gateway.AppData.DeliveryReportDetil deliveryReportDetil;
        private SMS_Gateway.AppData.DeliveryReportDetilTableAdapters.viewdeliveryorderdetilTableAdapter viewdeliveryorderdetilTableAdapter;
    }
}