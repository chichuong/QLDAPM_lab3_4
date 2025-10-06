using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace lab4_TH2
{
    public class UCParam : UserControl
    {
        TextBox txt; Button btn; ListView lv;
        public UCParam()
        {
            Controls.Add(new Label { Text = "Tên khoa:", Left = 20, Top = 20, AutoSize = true });
            txt = new TextBox { Left = 85, Top = 16, Width = 260 };
            btn = new Button { Text = "Xem danh sách", Left = 360, Top = 14, Width = 130 };
            lv = new ListView { Left = 20, Top = 50, Width = 470, Height = 500, View = View.Details, GridLines = true, FullRowSelect = true, Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right };
            lv.Columns.Add("Tên lớp", 300); lv.Columns.Add("Mã lớp", 120);
            Controls.Add(txt); Controls.Add(btn); Controls.Add(lv);

            btn.Click += (s, e) =>
            {
                var mk = MapTenKhoaToMa((txt.Text ?? "").Trim());
                if (mk == "") { MessageBox.Show("Tên khoa hợp lệ: CNTT / Cơ khí / Điện tử / Kinh tế"); return; }

                try
                {
                    lv.Items.Clear();
                    using (var con = Db.NewConnection())
                    using (var cmd = new SqlCommand("SELECT TenLop, MaLop FROM Lop WHERE MaKhoa=@mk ORDER BY TenLop", con))
                    {
                        cmd.Parameters.Add("@mk", SqlDbType.VarChar, 10).Value = mk;
                        con.Open();
                        using (var rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                var it = new ListViewItem(rd["TenLop"].ToString());
                                it.SubItems.Add(rd["MaLop"].ToString());
                                lv.Items.Add(it);
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            };
        }

        string MapTenKhoaToMa(string ten)
        {
            ten = ten.ToLower();
            if (ten == "công nghệ thông tin" || ten == "cntt") return "CNTT";
            if (ten == "cơ khí" || ten == "ck") return "CK";
            if (ten == "điện tử" || ten == "dt" || ten == "đt") return "DT";
            if (ten == "kinh tế" || ten == "kt") return "KT";
            return "";
        }
    }
}
