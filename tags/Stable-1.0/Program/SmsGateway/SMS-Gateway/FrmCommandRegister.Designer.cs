namespace SMS_Gateway.FormCommandRegister
{
    partial class FrmCommandRegister
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Txt_AssemblyName = new System.Windows.Forms.TextBox();
            this.Txt_ClassName = new System.Windows.Forms.TextBox();
            this.Txt_CmdName = new System.Windows.Forms.TextBox();
            this.Txt_CmdType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Txt_AssemblyName);
            this.groupBox1.Controls.Add(this.Txt_ClassName);
            this.groupBox1.Controls.Add(this.Txt_CmdName);
            this.groupBox1.Controls.Add(this.Txt_CmdType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 154);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // Txt_AssemblyName
            // 
            this.Txt_AssemblyName.Location = new System.Drawing.Point(126, 110);
            this.Txt_AssemblyName.MaxLength = 255;
            this.Txt_AssemblyName.Name = "Txt_AssemblyName";
            this.Txt_AssemblyName.Size = new System.Drawing.Size(191, 20);
            this.Txt_AssemblyName.TabIndex = 7;
            // 
            // Txt_ClassName
            // 
            this.Txt_ClassName.Location = new System.Drawing.Point(126, 79);
            this.Txt_ClassName.MaxLength = 255;
            this.Txt_ClassName.Name = "Txt_ClassName";
            this.Txt_ClassName.Size = new System.Drawing.Size(191, 20);
            this.Txt_ClassName.TabIndex = 6;
            // 
            // Txt_CmdName
            // 
            this.Txt_CmdName.Location = new System.Drawing.Point(126, 51);
            this.Txt_CmdName.MaxLength = 15;
            this.Txt_CmdName.Name = "Txt_CmdName";
            this.Txt_CmdName.Size = new System.Drawing.Size(96, 20);
            this.Txt_CmdName.TabIndex = 5;
            // 
            // Txt_CmdType
            // 
            this.Txt_CmdType.Location = new System.Drawing.Point(126, 24);
            this.Txt_CmdType.MaxLength = 5;
            this.Txt_CmdType.Name = "Txt_CmdType";
            this.Txt_CmdType.Size = new System.Drawing.Size(61, 20);
            this.Txt_CmdType.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Assembly Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Class Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Command Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Command Type";
            // 
            // Btn_Save
            // 
            this.Btn_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_Save.Location = new System.Drawing.Point(274, 172);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(75, 23);
            this.Btn_Save.TabIndex = 1;
            this.Btn_Save.Text = "&Add / Save";
            this.Btn_Save.UseVisualStyleBackColor = true;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(193, 172);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "&Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FrmCommandRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 211);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Btn_Save);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmCommandRegister";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form Command Register";
            this.Load += new System.EventHandler(this.FrmCommandRegister_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Txt_AssemblyName;
        private System.Windows.Forms.TextBox Txt_ClassName;
        private System.Windows.Forms.TextBox Txt_CmdName;
        private System.Windows.Forms.TextBox Txt_CmdType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_Save;
        private System.Windows.Forms.Button button2;
    }
}