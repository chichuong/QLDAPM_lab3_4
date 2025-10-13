using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace th3_apdung2_lab5
{
    public partial class Form1 : Form
    {
        // ===== CHUỖI KẾT NỐI BẠN CUNG CẤP =====
        private readonly string strCon =
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\hoc\hocthayhuynh\Phuongphapphattrienphanmen\cshard\lab2\1150080128_lychichuong_lab5\th3_apdung2_lab5\th3_ad2_lab5_db.mdf;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
            KhoiTaoListView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (cbGioiTinh.Items.Count == 0)
            {
                cbGioiTinh.Items.Add("Nam");
                cbGioiTinh.Items.Add("Nữ");
                cbGioiTinh.SelectedIndex = 0;
            }

            // Tạo bảng & seed dữ liệu mẫu
            EnsureTablesAndSeed();

            // Hiển thị danh sách lớp
            HienThiDSMaLop();
        }

        private SqlConnection GetConnection() => new SqlConnection(strCon);

        // ===========================================================
        // KHỞI TẠO LISTVIEW
        // ===========================================================
        private void KhoiTaoListView()
        {
            lsvDanhSach.View = View.Details;
            lsvDanhSach.FullRowSelect = true;
            lsvDanhSach.GridLines = true;

            lsvDanhSach.Columns.Clear();
            lsvDanhSach.Columns.Add("Mã SV", 90);
            lsvDanhSach.Columns.Add("Tên SV", 160);
            lsvDanhSach.Columns.Add("Giới tính", 70);
            lsvDanhSach.Columns.Add("Ngày sinh", 90);
            lsvDanhSach.Columns.Add("Quê quán", 140);
            lsvDanhSach.Columns.Add("Mã lớp", 100);
        }

        // ===========================================================
        // TẠO BẢNG & SEED DỮ LIỆU
        // ===========================================================
        private void EnsureTablesAndSeed()
        {
            string sql = @"
IF NOT EXISTS (SELECT 1 FROM sys.tables t JOIN sys.schemas s ON t.schema_id=s.schema_id WHERE t.name='Lop' AND s.name='dbo')
BEGIN
    CREATE TABLE [dbo].[Lop](
        MaLop NVARCHAR(20) NOT NULL PRIMARY KEY,
        TenLop NVARCHAR(100) NOT NULL
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables t JOIN sys.schemas s ON t.schema_id=s.schema_id WHERE t.name='SinhVien' AND s.name='dbo')
BEGIN
    CREATE TABLE [dbo].[SinhVien](
        MaSV NVARCHAR(20) NOT NULL PRIMARY KEY,
        TenSV NVARCHAR(100) NOT NULL,
        GioiTinh NVARCHAR(10) NOT NULL,
        NgaySinh DATE NOT NULL,
        QueQuan NVARCHAR(100) NULL,
        MaLop NVARCHAR(20) NOT NULL
            CONSTRAINT FK_SV_LOP REFERENCES [dbo].[Lop](MaLop)
    );
END;

-- Thêm lớp 11ĐHCNPM2 nếu chưa có
IF NOT EXISTS (SELECT 1 FROM [dbo].[Lop] WHERE MaLop = N'11ĐHCNPM2')
    INSERT INTO [dbo].[Lop](MaLop, TenLop)
    VALUES (N'11ĐHCNPM2', N'Công Nghệ Phần Mềm 2');

-- Seed sinh viên nếu chưa có
IF NOT EXISTS (SELECT 1 FROM [dbo].[SinhVien] WHERE MaSV = N'1150080128')
    INSERT INTO [dbo].[SinhVien](MaSV, TenSV, GioiTinh, NgaySinh, QueQuan, MaLop)
    VALUES (N'1150080128', N'Lý Chí Chương', N'Nam', '2004-09-03', N'Bạc Liêu', N'11ĐHCNPM2');
";
            using (var con = GetConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ===========================================================
        // HIỂN THỊ DANH SÁCH LỚP
        // ===========================================================
        private void HienThiDSMaLop()
        {
            cbMaLop.Items.Clear();
            string sql = "SELECT MaLop, TenLop FROM Lop ORDER BY MaLop";

            using (var con = GetConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        cbMaLop.Items.Add($"{r["MaLop"]} - {r["TenLop"]}");
                    }
                }
            }

            if (cbMaLop.Items.Count > 0)
                cbMaLop.SelectedIndex = 0;
        }

        // ===========================================================
        // HIỂN THỊ DANH SÁCH SINH VIÊN THEO LỚP
        // ===========================================================
        private void HienThiDSSinhVienTheoLop(string maLop)
        {
            lsvDanhSach.Items.Clear();
            string sql = "SELECT MaSV, TenSV, GioiTinh, NgaySinh, QueQuan, MaLop FROM SinhVien WHERE MaLop = @MaLop";

            using (var con = GetConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@MaLop", maLop);
                con.Open();

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var lvi = new ListViewItem(r["MaSV"].ToString());
                        lvi.SubItems.Add(r["TenSV"].ToString());
                        lvi.SubItems.Add(r["GioiTinh"].ToString());
                        lvi.SubItems.Add(Convert.ToDateTime(r["NgaySinh"]).ToString("dd/MM/yyyy"));
                        lvi.SubItems.Add(r["QueQuan"].ToString());
                        lvi.SubItems.Add(r["MaLop"].ToString());
                        lsvDanhSach.Items.Add(lvi);
                    }
                }
            }
        }

        // ===========================================================
        // SỰ KIỆN CHỌN LỚP
        // ===========================================================
        private void cbMaLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaLop.SelectedIndex == -1) return;
            string[] parts = cbMaLop.SelectedItem.ToString().Split('-');
            string maLop = parts[0].Trim();
            HienThiDSSinhVienTheoLop(maLop);
        }

        // ===========================================================
        // SỰ KIỆN CHỌN SINH VIÊN TRONG LISTVIEW
        // ===========================================================
        private void lsvDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvDanhSach.SelectedItems.Count == 0) return;

            var lvi = lsvDanhSach.SelectedItems[0];
            txtMaSV.Text = lvi.SubItems[0].Text;
            txtTenSV.Text = lvi.SubItems[1].Text;
            cbGioiTinh.Text = lvi.SubItems[2].Text;
            dtpNgaySinh.Value = DateTime.ParseExact(lvi.SubItems[3].Text, "dd/MM/yyyy", null);
            txtQueQuan.Text = lvi.SubItems[4].Text;
            txtMaLop.Text = lvi.SubItems[5].Text;
        }

        // ===========================================================
        // NÚT SỬA THÔNG TIN (DÙNG PARAMETER)
        // ===========================================================
        private void btnSuaThongTin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng chọn sinh viên để sửa!");
                return;
            }

            string sql = @"UPDATE SinhVien 
                           SET TenSV = @TenSV, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, 
                               QueQuan = @QueQuan, MaLop = @MaLop
                           WHERE MaSV = @MaSV";

            using (var con = GetConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@TenSV", txtTenSV.Text.Trim());
                cmd.Parameters.AddWithValue("@GioiTinh", cbGioiTinh.Text.Trim());
                cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value.Date);
                cmd.Parameters.AddWithValue("@QueQuan", txtQueQuan.Text.Trim());
                cmd.Parameters.AddWithValue("@MaLop", txtMaLop.Text.Trim());
                cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text.Trim());

                con.Open();
                int kq = cmd.ExecuteNonQuery();

                if (kq > 0)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    string[] parts = cbMaLop.SelectedItem.ToString().Split('-');
                    string maLop = parts[0].Trim();
                    HienThiDSSinhVienTheoLop(maLop);
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu nào được sửa.");
                }
            }
        }
    }
}
