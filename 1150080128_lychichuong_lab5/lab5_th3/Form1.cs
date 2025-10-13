using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace lab5_th3
{
    public partial class Form1 : Form
    {
        // DÙNG |DataDirectory| – nhớ đặt DBConnect.mdf trong thư mục project
        private readonly string strCon =
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\hoc\hocthayhuynh\Phuongphapphattrienphanmen\cshard\lab2\1150080128_lychichuong_lab5\lab5_th3\db_lab5_th3.mdf;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
            KhoiTaoListView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Combobox giới tính
            if (cbGioiTinh.Items.Count == 0)
            {
                cbGioiTinh.Items.Add("Nam");
                cbGioiTinh.Items.Add("Nữ");
                cbGioiTinh.SelectedIndex = 0;
            }

            // Đảm bảo bảng tồn tại (tạo nếu thiếu) + dữ liệu mẫu Lop
            EnsureTables();

            // Hiển thị danh sách mã lớp
            HienThiDSMaLop();
        }

        private SqlConnection GetConnection() => new SqlConnection(strCon);

        // =============== UI helpers ===============
        private void KhoiTaoListView()
        {
            lsvDanhSach.View = View.Details;
            lsvDanhSach.FullRowSelect = true;
            lsvDanhSach.GridLines = true;

            lsvDanhSach.Columns.Clear();
            lsvDanhSach.Columns.Add("Mã SV", 80);
            lsvDanhSach.Columns.Add("Tên SV", 160);
            lsvDanhSach.Columns.Add("Giới tính", 70);
            lsvDanhSach.Columns.Add("Ngày sinh", 90);
            lsvDanhSach.Columns.Add("Quê quán", 140);
            lsvDanhSach.Columns.Add("Mã lớp", 80);
        }

        // =============== DB bootstrap ===============
        private void EnsureTables()
        {
            string createSql = @"
IF NOT EXISTS (SELECT 1 FROM sys.tables t JOIN sys.schemas s ON t.schema_id=s.schema_id WHERE t.name='Lop' AND s.name='dbo')
BEGIN
    CREATE TABLE [dbo].[Lop](
        MaLop NVARCHAR(20) NOT NULL PRIMARY KEY,
        TenLop NVARCHAR(100) NOT NULL
    );
    INSERT INTO [dbo].[Lop](MaLop, TenLop) VALUES (N'11ĐHCNPM2', N'Công Nghệ Phần Mềm 2'), (N'CTK44', N'Công nghệ thông tin K44');
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables t JOIN sys.schemas s ON t.schema_id=s.schema_id WHERE t.name='SinhVien' AND s.name='dbo')
BEGIN
    CREATE TABLE [dbo].[SinhVien](
        MaSV NVARCHAR(20) NOT NULL PRIMARY KEY,
        TenSV NVARCHAR(100) NOT NULL,
        GioiTinh NVARCHAR(10) NOT NULL,
        NgaySinh DATE NOT NULL,
        QueQuan NVARCHAR(100) NULL,
        MaLop NVARCHAR(20) NOT NULL REFERENCES [dbo].[Lop](MaLop)
    );
END;";
            using (var con = GetConnection())
            using (var cmd = new SqlCommand(createSql, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // =============== Load dữ liệu ===============
        private void HienThiDSMaLop()
        {
            cbMaLop.Items.Clear();

            string sql = "SELECT MaLop, TenLop FROM [dbo].[Lop] ORDER BY MaLop";
            using (var con = GetConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        string maLop = r.GetString(0);
                        string tenLop = r.GetString(1);
                        cbMaLop.Items.Add(maLop + " - " + tenLop);
                    }
                }
            }

            if (cbMaLop.Items.Count > 0) cbMaLop.SelectedIndex = 0;
        }

        private void HienThiDSSinhVienTheoLop(string maLop)
        {
            lsvDanhSach.Items.Clear();
            string sql = "SELECT MaSV, TenSV, GioiTinh, NgaySinh, QueQuan, MaLop FROM [dbo].[SinhVien] WHERE MaLop='" + maLop.Replace("'", "''") + "' ORDER BY MaSV";

            using (var con = GetConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        string maSV = r.GetString(0);
                        string tenSV = r.GetString(1);
                        string gioiTinh = r.GetString(2);
                        string ngaySinh = r.GetDateTime(3).ToString("dd/MM/yyyy");
                        string queQuan = r.IsDBNull(4) ? "" : r.GetString(4);
                        string ma = r.GetString(5);

                        var lvi = new ListViewItem(maSV);
                        lvi.SubItems.Add(tenSV);
                        lvi.SubItems.Add(gioiTinh);
                        lvi.SubItems.Add(ngaySinh);
                        lvi.SubItems.Add(queQuan);
                        lvi.SubItems.Add(ma);

                        lsvDanhSach.Items.Add(lvi);
                    }
                }
            }
        }

        // =============== Events ===============
        private void cbMaLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaLop.SelectedIndex == -1) return;
            string[] parts = cbMaLop.SelectedItem.ToString().Split('-');
            string maLop = parts[0].Trim();
            HienThiDSSinhVienTheoLop(maLop);
        }

        private void lsvDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvDanhSach.SelectedItems.Count == 0) return;

            var lvi = lsvDanhSach.SelectedItems[0];

            txtMaSV.Text = lvi.SubItems[0].Text;
            txtTenSV.Text = lvi.SubItems[1].Text;

            cbGioiTinh.SelectedIndex = -1;
            cbGioiTinh.Text = lvi.SubItems[2].Text;

            // dd/MM/yyyy
            string[] dt = lvi.SubItems[3].Text.Split('/');
            dtpNgaySinh.Value = new DateTime(
                int.Parse(dt[2].Trim()),
                int.Parse(dt[1].Trim()),
                int.Parse(dt[0].Trim())
            );

            txtQueQuan.Text = lvi.SubItems[4].Text;
            txtMaLop.Text = lvi.SubItems[5].Text;
        }

        private void btnSuaThongTin_Click(object sender, EventArgs e)
        {
            // Validate tối thiểu
            var sb = new StringBuilder();
            if (string.IsNullOrWhiteSpace(txtMaSV.Text)) sb.AppendLine("• Chưa nhập Mã SV");
            if (string.IsNullOrWhiteSpace(txtTenSV.Text)) sb.AppendLine("• Chưa nhập Tên SV");
            if (string.IsNullOrWhiteSpace(txtMaLop.Text)) sb.AppendLine("• Chưa nhập Mã lớp");
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy dữ liệu từ control
            string maSV = txtMaSV.Text.Trim();
            string tenSV = txtTenSV.Text.Trim();
            string gioiTinh = cbGioiTinh.Text.Trim();
            string ngaySinh = dtpNgaySinh.Value.ToString("yyyy-MM-dd"); // để CAST chính xác
            string queQuan = (txtQueQuan.Text ?? "").Replace("'", "''");
            string maLop = txtMaLop.Text.Trim();

            // CẬP NHẬT KHÔNG DÙNG PARAMETER (theo yêu cầu bài)
            string sql = "UPDATE [dbo].[SinhVien] SET " +
                         "TenSV=N'" + tenSV.Replace("'", "''") + "', " +
                         "GioiTinh=N'" + gioiTinh.Replace("'", "''") + "', " +
                         "NgaySinh=CAST('" + ngaySinh + "' AS DATE), " +
                         "QueQuan=N'" + queQuan + "', " +
                         "MaLop='" + maLop.Replace("'", "''") + "' " +
                         "WHERE MaSV='" + maSV.Replace("'", "''") + "'";

            try
            {
                using (var con = GetConnection())
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    int kq = cmd.ExecuteNonQuery();
                    if (kq > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!");
                        // Refresh lại danh sách theo lớp đang chọn
                        if (cbMaLop.SelectedIndex != -1)
                        {
                            string[] parts = cbMaLop.SelectedItem.ToString().Split('-');
                            string maLopDaChon = parts[0].Trim();
                            HienThiDSSinhVienTheoLop(maLopDaChon);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật không thành công.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
        }

        private void grpRight_Enter(object sender, EventArgs e)
        {

        }

        private void labelChonLop_Click(object sender, EventArgs e)
        {

        }
    }
}
