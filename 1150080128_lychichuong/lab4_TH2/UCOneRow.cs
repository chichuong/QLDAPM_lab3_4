using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace lab4_TH2
{
    public class UCOneRow : UserControl
    {
        TextBox txtMa, txtTen, txtGT, txtNS, txtQQ, txtML;
        Button btn;
        public UCOneRow()
        {
            int y = 20;
            Controls.Add(new Label { Text = "Nhập mã:", Left = 20, Top = y + 5, AutoSize = true });
            txtMa = new TextBox { Left = 90, Top = y, Width = 140 };
            btn = new Button { Text = "Xem thông tin", Left = 240, Top = y - 1, Width = 120 };
            Controls.Add(txtMa); Controls.Add(btn);
            y += 40;
            Controls.Add(new Label { Text = "Họ tên:", Left = 20, Top = y + 5, AutoSize = true });
            txtTen = new TextBox { Left = 90, Top = y, Width = 240, ReadOnly = true }; Controls.Add(txtTen);
            Controls.Add(new Label { Text = "Giới tính:", Left = 350, Top = y + 5, AutoSize = true });
            txtGT = new TextBox { Left = 410, Top = y, Width = 80, ReadOnly = true }; Controls.Add(txtGT);
            Controls.Add(new Label { Text = "Ngày sinh:", Left = 500, Top = y + 5, AutoSize = true });
            txtNS = new TextBox { Left = 570, Top = y, Width = 90, ReadOnly = true }; Controls.Add(txtNS);
            y += 40;
            Controls.Add(new Label { Text = "Quê quán:", Left = 20, Top = y + 5, AutoSize = true });
            txtQQ = new TextBox { Left = 90, Top = y, Width = 240, ReadOnly = true }; Controls.Add(txtQQ);
            Controls.Add(new Label { Text = "Mã lớp:", Left = 350, Top = y + 5, AutoSize = true });
            txtML = new TextBox { Left = 410, Top = y, Width = 80, ReadOnly = true }; Controls.Add(txtML);

            btn.Click += (s, e) =>
            {
                var ma = (txtMa.Text ?? "").Trim();
                if (ma == "") { MessageBox.Show("Nhập mã SV!"); return; }
                try
                {
                    using (var con = Db.NewConnection())
                    using (var cmd = new SqlCommand(
                        "SELECT MaSV,TenSV,GioiTinh,NgaySinh,QueQuan,MaLop FROM SinhVien WHERE MaSV=@ma", con))
                    {
                        cmd.Parameters.Add("@ma", SqlDbType.VarChar, 50).Value = ma;
                        con.Open();
                        using (var rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                txtTen.Text = rd["TenSV"].ToString();
                                txtGT.Text = rd["GioiTinh"].ToString();
                                DateTime ns; txtNS.Text = DateTime.TryParse(rd["NgaySinh"].ToString(), out ns) ? ns.ToString("dd/MM/yyyy") : "";
                                txtQQ.Text = rd["QueQuan"].ToString();
                                txtML.Text = rd["MaLop"].ToString();
                            }
                            else MessageBox.Show("Không tìm thấy!");
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            };
        }
    }
}
