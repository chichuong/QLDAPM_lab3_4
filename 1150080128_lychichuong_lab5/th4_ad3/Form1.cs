using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace th4_ad3
{
    public partial class Form1 : Form
    {
        // ===== Chuỗi kết nối của bạn =====
        private readonly string strCon =
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\hoc\hocthayhuynh\Phuongphapphattrienphanmen\cshard\lab2\1150080128_lychichuong_lab5\th4_ad3\th4_ad3_db.mdf;Integrated Security=True";

        private string maSV_DangChon = string.Empty;

        public Form1()
        {
            InitializeComponent();
            KhoiTaoListView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EnsureTablesAndSeed();  // tạo bảng và seed dữ liệu
            HienThiDSSinhVien();    // hiển thị danh sách
            CapNhatTrangThaiNutXoa();
        }

        private SqlConnection GetConnection() => new SqlConnection(strCon);

        // ============================================================
        // CẤU HÌNH GIAO DIỆN LISTVIEW
        // ============================================================
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
            lsvDanhSach.Columns.Add("Mã lớp", 110);
        }

        // ============================================================
        // TẠO BẢNG VÀ SEED DỮ LIỆU
        // ============================================================
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

IF NOT EXISTS (SELECT 1 FROM [dbo].[Lop] WHERE MaLop = N'11ĐHCNPM2')
    INSERT INTO [dbo].[Lop](MaLop, TenLop)
    VALUES (N'11ĐHCNPM2', N'Công Nghệ Phần Mềm 2');

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

        // ============================================================
        // HIỂN THỊ DANH SÁCH SINH VIÊN
        // ============================================================
        private void HienThiDSSinhVien()
        {
            lsvDanhSach.Items.Clear();

            const string sql = "SELECT MaSV, TenSV, GioiTinh, NgaySinh, QueQuan, MaLop FROM [dbo].[SinhVien] ORDER BY MaSV";
            using (var con = GetConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var item = new ListViewItem(r.GetString(0)); // MaSV
                        item.SubItems.Add(r.GetString(1));           // TenSV
                        item.SubItems.Add(r.GetString(2));           // GioiTinh
                        item.SubItems.Add(r.GetDateTime(3).ToString("dd/MM/yyyy")); // NgaySinh
                        item.SubItems.Add(r.IsDBNull(4) ? "" : r.GetString(4));     // QueQuan
                        item.SubItems.Add(r.GetString(5));           // MaLop
                        lsvDanhSach.Items.Add(item);
                    }
                }
            }

            maSV_DangChon = string.Empty;
        }

        // ============================================================
        // CHỌN 1 SINH VIÊN TRONG LISTVIEW
        // ============================================================
        private void lsvDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvDanhSach.SelectedItems.Count == 0)
                maSV_DangChon = string.Empty;
            else
                maSV_DangChon = lsvDanhSach.SelectedItems[0].SubItems[0].Text.Trim();

            CapNhatTrangThaiNutXoa();
        }

        private void CapNhatTrangThaiNutXoa()
        {
            btnXoaSV.Enabled = !string.IsNullOrEmpty(maSV_DangChon);
        }

        // ============================================================
        // XÓA SINH VIÊN (CÓ DÙNG PARAMETER)
        // ============================================================
        private void btnXoaSV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maSV_DangChon))
            {
                MessageBox.Show("Vui lòng chọn sinh viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sinh viên có mã: {maSV_DangChon}?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            string sql = "DELETE FROM [dbo].[SinhVien] WHERE MaSV = @MaSV";

            try
            {
                using (var con = GetConnection())
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV_DangChon);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        HienThiDSSinhVien();
                        CapNhatTrangThaiNutXoa();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
