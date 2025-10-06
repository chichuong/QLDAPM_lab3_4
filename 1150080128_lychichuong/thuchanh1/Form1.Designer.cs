namespace QuanAnNhanh
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.groupDanhSach = new System.Windows.Forms.GroupBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblChonBan = new System.Windows.Forms.Label();
            this.cboBan = new System.Windows.Forms.ComboBox();
            this.btnOrder = new System.Windows.Forms.Button();
            this.lvOrder = new System.Windows.Forms.ListView();
            this.colMon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.groupDanhSach.SuspendLayout();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.ForestGreen;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(110, 8);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(538, 52);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Quán ăn nhanh Chí Chương";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupDanhSach
            // 
            this.groupDanhSach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDanhSach.Controls.Add(this.pnlButtons);
            this.groupDanhSach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupDanhSach.Location = new System.Drawing.Point(10, 65);
            this.groupDanhSach.Name = "groupDanhSach";
            this.groupDanhSach.Size = new System.Drawing.Size(638, 147);
            this.groupDanhSach.TabIndex = 2;
            this.groupDanhSach.TabStop = false;
            this.groupDanhSach.Text = "Danh sách món ăn:";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtons.Location = new System.Drawing.Point(3, 19);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(632, 125);
            this.pnlButtons.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBottom.Controls.Add(this.btnClear);
            this.panelBottom.Controls.Add(this.lblChonBan);
            this.panelBottom.Controls.Add(this.cboBan);
            this.panelBottom.Controls.Add(this.btnOrder);
            this.panelBottom.Location = new System.Drawing.Point(10, 218);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(638, 31);
            this.panelBottom.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(5, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(64, 22);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Xóa";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblChonBan
            // 
            this.lblChonBan.AutoSize = true;
            this.lblChonBan.Location = new System.Drawing.Point(206, 9);
            this.lblChonBan.Name = "lblChonBan";
            this.lblChonBan.Size = new System.Drawing.Size(56, 13);
            this.lblChonBan.TabIndex = 3;
            this.lblChonBan.Text = "Chọn bàn:";
            // 
            // cboBan
            // 
            this.cboBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBan.FormattingEnabled = true;
            this.cboBan.Location = new System.Drawing.Point(261, 5);
            this.cboBan.Name = "cboBan";
            this.cboBan.Size = new System.Drawing.Size(146, 21);
            this.cboBan.TabIndex = 1;
            // 
            // btnOrder
            // 
            this.btnOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrder.Location = new System.Drawing.Point(567, 4);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(64, 22);
            this.btnOrder.TabIndex = 2;
            this.btnOrder.Text = "Order";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // lvOrder
            // 
            this.lvOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvOrder.BackColor = System.Drawing.Color.Gainsboro;
            this.lvOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMon,
            this.colSL});
            this.lvOrder.FullRowSelect = true;
            this.lvOrder.GridLines = true;
            this.lvOrder.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvOrder.HideSelection = false;
            this.lvOrder.Location = new System.Drawing.Point(10, 254);
            this.lvOrder.Name = "lvOrder";
            this.lvOrder.Size = new System.Drawing.Size(638, 222);
            this.lvOrder.TabIndex = 4;
            this.lvOrder.UseCompatibleStateImageBehavior = false;
            this.lvOrder.View = System.Windows.Forms.View.Details;
            // 
            // colMon
            // 
            this.colMon.Text = "Món";
            this.colMon.Width = 560;
            // 
            // colSL
            // 
            this.colSL.Text = "Số lượng";
            this.colSL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colSL.Width = 120;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::thuchanh1.Properties.Resources.z6578501955207_120481c09891f1979a1a65da3269b644;
            this.pictureBoxLogo.Location = new System.Drawing.Point(34, 12);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(58, 51);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 486);
            this.Controls.Add(this.lvOrder);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.groupDanhSach);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.pictureBoxLogo);
            this.MinimumSize = new System.Drawing.Size(674, 525);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupDanhSach.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox groupDanhSach;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblChonBan;
        private System.Windows.Forms.ComboBox cboBan;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.ListView lvOrder;
        private System.Windows.Forms.ColumnHeader colMon;
        private System.Windows.Forms.ColumnHeader colSL;
    }
}
