using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace lab4_TH2
{
    public class UCScalar : UserControl
    {
        Button btn; Label lbl;
        public UCScalar()
        {
            btn = new Button { Text = "Số lượng sinh viên", Left = 20, Top = 20, Width = 160, Height = 30 };
            lbl = new Label { Text = "(chưa chạy)", Left = 200, Top = 26, AutoSize = true };
            Controls.Add(btn); Controls.Add(lbl);
            btn.Click += (s, e) =>
            {
                try
                {
                    using (var con = Db.NewConnection())
                    using (var cmd = new SqlCommand("SELECT COUNT(*) FROM SinhVien", con))
                    {
                        con.Open();
                        int n = Convert.ToInt32(cmd.ExecuteScalar());
                        lbl.Text = n.ToString();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            };
        }
    }
}
