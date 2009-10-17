namespace SMS_Gateway.FormDiagnostic
{
    partial class FrmDiagnostic
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
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSupportedStorage = new System.Windows.Forms.TextBox();
            this.txtSignal = new System.Windows.Forms.TextBox();
            this.txtSMSC = new System.Windows.Forms.TextBox();
            this.txtRevision = new System.Windows.Forms.TextBox();
            this.txtManufacturer = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtIMEI = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.txtIMSI = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.txtSupportedStorage);
            this.GroupBox1.Controls.Add(this.txtSignal);
            this.GroupBox1.Controls.Add(this.txtSMSC);
            this.GroupBox1.Controls.Add(this.txtRevision);
            this.GroupBox1.Controls.Add(this.txtManufacturer);
            this.GroupBox1.Controls.Add(this.txtModel);
            this.GroupBox1.Controls.Add(this.txtIMEI);
            this.GroupBox1.Controls.Add(this.Label13);
            this.GroupBox1.Controls.Add(this.Label12);
            this.GroupBox1.Controls.Add(this.Label11);
            this.GroupBox1.Controls.Add(this.Label10);
            this.GroupBox1.Controls.Add(this.Label9);
            this.GroupBox1.Controls.Add(this.Label8);
            this.GroupBox1.Controls.Add(this.Label7);
            this.GroupBox1.Controls.Add(this.txtIMSI);
            this.GroupBox1.Controls.Add(this.Label6);
            this.GroupBox1.Location = new System.Drawing.Point(21, 8);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(337, 239);
            this.GroupBox1.TabIndex = 12;
            this.GroupBox1.TabStop = false;
            // 
            // txtSupportedStorage
            // 
            this.txtSupportedStorage.Location = new System.Drawing.Point(123, 206);
            this.txtSupportedStorage.Name = "txtSupportedStorage";
            this.txtSupportedStorage.ReadOnly = true;
            this.txtSupportedStorage.Size = new System.Drawing.Size(193, 20);
            this.txtSupportedStorage.TabIndex = 28;
            // 
            // txtSignal
            // 
            this.txtSignal.Location = new System.Drawing.Point(123, 180);
            this.txtSignal.Name = "txtSignal";
            this.txtSignal.ReadOnly = true;
            this.txtSignal.Size = new System.Drawing.Size(193, 20);
            this.txtSignal.TabIndex = 27;
            this.txtSignal.TextChanged += new System.EventHandler(this.txtSignal_TextChanged);
            // 
            // txtSMSC
            // 
            this.txtSMSC.Location = new System.Drawing.Point(123, 154);
            this.txtSMSC.Name = "txtSMSC";
            this.txtSMSC.ReadOnly = true;
            this.txtSMSC.Size = new System.Drawing.Size(193, 20);
            this.txtSMSC.TabIndex = 26;
            // 
            // txtRevision
            // 
            this.txtRevision.Location = new System.Drawing.Point(123, 128);
            this.txtRevision.Name = "txtRevision";
            this.txtRevision.ReadOnly = true;
            this.txtRevision.Size = new System.Drawing.Size(193, 20);
            this.txtRevision.TabIndex = 25;
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.Location = new System.Drawing.Point(123, 102);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.ReadOnly = true;
            this.txtManufacturer.Size = new System.Drawing.Size(193, 20);
            this.txtManufacturer.TabIndex = 24;
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(123, 76);
            this.txtModel.Name = "txtModel";
            this.txtModel.ReadOnly = true;
            this.txtModel.Size = new System.Drawing.Size(193, 20);
            this.txtModel.TabIndex = 23;
            // 
            // txtIMEI
            // 
            this.txtIMEI.Location = new System.Drawing.Point(123, 50);
            this.txtIMEI.Name = "txtIMEI";
            this.txtIMEI.ReadOnly = true;
            this.txtIMEI.Size = new System.Drawing.Size(193, 20);
            this.txtIMEI.TabIndex = 22;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(17, 213);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(99, 13);
            this.Label13.TabIndex = 20;
            this.Label13.Text = "Supported Storage:";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(17, 187);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(82, 13);
            this.Label12.TabIndex = 19;
            this.Label12.Text = "Signal Strength:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(17, 161);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(67, 13);
            this.Label11.TabIndex = 18;
            this.Label11.Text = "SMS Center:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(17, 135);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(51, 13);
            this.Label10.TabIndex = 17;
            this.Label10.Text = "Revision:";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(17, 109);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(70, 13);
            this.Label9.TabIndex = 16;
            this.Label9.Text = "Manufacturer";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(17, 83);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(39, 13);
            this.Label8.TabIndex = 15;
            this.Label8.Text = "Model:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(17, 57);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(32, 13);
            this.Label7.TabIndex = 14;
            this.Label7.Text = "IMEI:";
            // 
            // txtIMSI
            // 
            this.txtIMSI.Location = new System.Drawing.Point(123, 24);
            this.txtIMSI.Name = "txtIMSI";
            this.txtIMSI.ReadOnly = true;
            this.txtIMSI.Size = new System.Drawing.Size(193, 20);
            this.txtIMSI.TabIndex = 13;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(17, 31);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(32, 13);
            this.Label6.TabIndex = 12;
            this.Label6.Text = "IMSI:";
            // 
            // Btn_Close
            // 
            this.Btn_Close.Location = new System.Drawing.Point(267, 263);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(91, 23);
            this.Btn_Close.TabIndex = 13;
            this.Btn_Close.Text = "&Close";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // FrmDiagnostic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 297);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDiagnostic";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modem Diagnostic";
            this.Load += new System.EventHandler(this.FrmDiagnostic_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.TextBox txtSupportedStorage;
        internal System.Windows.Forms.TextBox txtSignal;
        internal System.Windows.Forms.TextBox txtSMSC;
        internal System.Windows.Forms.TextBox txtRevision;
        internal System.Windows.Forms.TextBox txtManufacturer;
        internal System.Windows.Forms.TextBox txtModel;
        internal System.Windows.Forms.TextBox txtIMEI;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox txtIMSI;
        internal System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Button Btn_Close;
    }
}