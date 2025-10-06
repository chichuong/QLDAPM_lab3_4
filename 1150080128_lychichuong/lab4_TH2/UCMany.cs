using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace lab4_TH2
{
    public class UCMany : UserControl
    {
        Button btn; ListView lv;
        public UCMany()
        {
            btn = new Button { Text = "Xem danh sách", Left = 20, Top = 15, Width = 130 };
            lv = new ListView { Left = 20, Top = 50, Width = 920, Height = 500, View = View.Details, GridLines = true, FullRowSelect = true, Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right };
            lv.Columns.Add("Mã SV", 100);
            lv.Columns.Add("Họ tên", 240);
            lv.Columns.Add("Giới tính", 80);
            lv.Columns.Add("Ngày sinh", 100);
            lv.Columns.Add("Quê quán", 240);
            lv.Columns.Add("Mã lớp", 100);
            Controls.Add(btn); Controls.Add(lv);

            btn.Click += (s, e) =>
            {
                try
                {
                    lv.Items.Clear();
                    using (var con = Db.NewConnection())
                    using (var cmd = new SqlCommand(
                        "SELECT MaSV,TenSV,GioiTinh,NgaySinh,QueQuan,MaLop FROM SinhVien ORDER BY TenSV", con))
                    {
                        con.Open();
                        using (var rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                var it = new ListViewItem(rd["MaSV"].ToString());
                                it.SubItems.Add(rd["TenSV"].ToString());
                                it.SubItems.Add(rd["GioiTinh"].ToString());
                                DateTime ns; it.SubItems.Add(DateTime.TryParse(rd["NgaySinh"].ToString(), out ns) ? ns.ToString("dd/MM/yyyy") : "");
                                it.SubItems.Add(rd["QueQuan"].ToString());
                                it.SubItems.Add(rd["MaLop"].ToString());
                                lv.Items.Add(it);
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            };
        }
    }
}
