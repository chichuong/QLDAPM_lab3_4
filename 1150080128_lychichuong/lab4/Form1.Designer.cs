namespace lab4
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
            this.btnMoKetNoi = new System.Windows.Forms.Button();
            this.btnDongKetNoi = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnMoKetNoi
            // 
            this.btnMoKetNoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMoKetNoi.Location = new System.Drawing.Point(80, 120);
            this.btnMoKetNoi.Name = "btnMoKetNoi";
            this.btnMoKetNoi.Size = new System.Drawing.Size(150, 40);
            this.btnMoKetNoi.TabIndex = 0;
            this.btnMoKetNoi.Text = "Mở kết nối";
            this.btnMoKetNoi.UseVisualStyleBackColor = true;
            this.btnMoKetNoi.Click += new System.EventHandler(this.btnMoKetNoi_Click);
            // 
            // btnDongKetNoi
            // 
            this.btnDongKetNoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDongKetNoi.Location = new System.Drawing.Point(270, 120);
            this.btnDongKetNoi.Name = "btnDongKetNoi";
            this.btnDongKetNoi.Size = new System.Drawing.Size(150, 40);
            this.btnDongKetNoi.TabIndex = 1;
            this.btnDongKetNoi.Text = "Đóng kết nối";
            this.btnDongKetNoi.UseVisualStyleBackColor = true;
            this.btnDongKetNoi.Click += new System.EventHandler(this.btnDongKetNoi_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(500, 60);
            this.label1.TabIndex = 2;
            this.label1.Text = "KẾT NỐI SQL SERVER";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 250);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDongKetNoi);
            this.Controls.Add(this.btnMoKetNoi);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kết nối CSDL";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnMoKetNoi;
        private System.Windows.Forms.Button btnDongKetNoi;
        private System.Windows.Forms.Label label1;
    }
}
