using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace lab4_TH2

{
    public class UCApply : UserControl
    {
        ListBox lsb; ListView lv;
        public UCApply()
        {
            Controls.Add(new Label { Text = "Danh sách lớp:", Left = 20, Top = 20, AutoSize = true });
            Controls.Add(new Label { Text = "Danh sách sinh viên:", Left = 200, Top = 20, AutoSize = true });
            lsb = new ListBox { Left = 20, Top = 40, Width = 160, Height = 480 };
            lv = new ListView { Left = 200, Top = 40, Width = 740, Height = 480, View = View.Details, GridLines = true, FullRowSelect = true, Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right };
            lv.Columns.Add("Mã", 80); lv.Columns.Add("Họ tên", 260); lv.Columns.Add("NS", 120); lv.Columns.Add("Quê", 240);
            Controls.Add(lsb); Controls.Add(lv);

            Load += (s, e) => LoadClasses();
            lsb.SelectedIndexChanged += (s, e) => LoadStudentsBySelectedClass();
        }

        class LopItem { public string Ma; public string Ten; public override string ToString() => Ten; }

        void LoadClasses()
        {
            try
            {
                lsb.Items.Clear();
                using (var con = Db.NewConnection())
                using (var cmd = new SqlCommand("SELECT MaLop, TenLop FROM Lop ORDER BY TenLop", con))
                {
                    con.Open();
                    using (var rd = cmd.ExecuteReader())
                        while (rd.Read())
                            lsb.Items.Add(new LopItem { Ma = rd["MaLop"].ToString(), Ten = rd["TenLop"].ToString() });
                }
            }
            catch { /* có thể DB chưa có – bỏ qua */ }
        }

        void LoadStudentsBySelectedClass()
        {
            var it = lsb.SelectedItem as LopItem;
            if (it == null) return;
            try
            {
                lv.Items.Clear();
                using (var con = Db.NewConnection())
                using (var cmd = new SqlCommand(
                    "SELECT MaSV, TenSV, NgaySinh, QueQuan FROM SinhVien WHERE MaLop=@ml ORDER BY TenSV", con))
                {
                    cmd.Parameters.Add("@ml", SqlDbType.VarChar, 50).Value = it.Ma;
                    con.Open();
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            var vi = new ListViewItem(rd["MaSV"].ToString());
                            vi.SubItems.Add(rd["TenSV"].ToString());
                            DateTime ns; vi.SubItems.Add(DateTime.TryParse(rd["NgaySinh"].ToString(), out ns) ? ns.ToString("dd/MM/yyyy") : "");
                            vi.SubItems.Add(rd["QueQuan"].ToString());
                            lv.Items.Add(vi);
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
    }
}
