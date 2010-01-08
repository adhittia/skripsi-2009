namespace SMS_Gateway
{
    partial class FrmLookupMenu
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
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvMenu = new System.Windows.Forms.ListView();
            this.menuid = new System.Windows.Forms.ColumnHeader();
            this.menuname = new System.Windows.Forms.ColumnHeader();
            this.txtSearchMenu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(260, 238);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvMenu);
            this.groupBox1.Controls.Add(this.txtSearchMenu);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 211);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // lvMenu
            // 
            this.lvMenu.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.menuid,
            this.menuname});
            this.lvMenu.FullRowSelect = true;
            this.lvMenu.GridLines = true;
            this.lvMenu.HideSelection = false;
            this.lvMenu.LabelWrap = false;
            this.lvMenu.Location = new System.Drawing.Point(23, 52);
            this.lvMenu.Name = "lvMenu";
            this.lvMenu.Size = new System.Drawing.Size(300, 141);
            this.lvMenu.TabIndex = 20;
            this.lvMenu.UseCompatibleStateImageBehavior = false;
            this.lvMenu.View = System.Windows.Forms.View.Details;
            this.lvMenu.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMenu_MouseDoubleClick);
            // 
            // menuid
            // 
            this.menuid.Text = "Menu ID";
            this.menuid.Width = 74;
            // 
            // menuname
            // 
            this.menuname.Text = "Menu";
            this.menuname.Width = 235;
            // 
            // txtSearchMenu
            // 
            this.txtSearchMenu.AcceptsReturn = true;
            this.txtSearchMenu.Location = new System.Drawing.Point(97, 26);
            this.txtSearchMenu.MaxLength = 50;
            this.txtSearchMenu.Name = "txtSearchMenu";
            this.txtSearchMenu.Size = new System.Drawing.Size(201, 20);
            this.txtSearchMenu.TabIndex = 19;
            this.txtSearchMenu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchMenu_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search Menu";
            // 
            // FrmLookupMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 273);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmLookupMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Activated += new System.EventHandler(this.FrmLookupMenu_Activated);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSearchMenu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvMenu;
        private System.Windows.Forms.ColumnHeader menuid;
        private System.Windows.Forms.ColumnHeader menuname;
    }
}