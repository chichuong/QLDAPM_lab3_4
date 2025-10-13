using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace lab5_th1
{
    public partial class Form1 : Form
    {
        // Kết nối qua |DataDirectory| (đặt DBConnect.mdf trong thư mục project)
        private readonly string strCon =
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBConnect.mdf;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=True";

        public Form1()
        {
            InitializeComponent();
            KhoiTaoListView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (cboGioiTinh.Items.Count == 0)
            {
                cboGioiTinh.Items.Add("Nam");
                cboGioiTinh.Items.Add("Nữ");
                cboGioiTinh.SelectedIndex = 0;
            }

            EnsureSinhVienTable();
            HienThiDanhSach();
        }

        private SqlConnection GetConnection() => new SqlConnection(strCon);

        private void KhoiTaoListView()
        {
            lsvDanhSachSV.View = View.Details;
            lsvDanhSachSV.FullRowSelect = true;
            lsvDanhSachSV.GridLines = true;

            lsvDanhSachSV.Columns.Clear();
            lsvDanhSachSV.Columns.Add("Mã SV", 90);
            lsvDanhSachSV.Columns.Add("Tên SV", 160);
            lsvDanhSachSV.Columns.Add("Giới tính", 70);
            lsvDanhSachSV.Columns.Add("Ngày sinh", 90);
            lsvDanhSachSV.Columns.Add("Quê quán", 140);
            lsvDanhSachSV.Columns.Add("Mã lớp", 80);
        }

        private void EnsureSinhVienTable()
        {
            const string createSql = @"
IF NOT EXISTS (
    SELECT 1 FROM sys.tables t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'SinhVien' AND s.name = 'dbo'
)
BEGIN
    CREATE TABLE [dbo].[SinhVien](
        MaSV     NVARCHAR(20)  NOT NULL PRIMARY KEY,
        TenSV    NVARCHAR(100) NOT NULL,
        GioiTinh NVARCHAR(10)  NOT NULL,
        NgaySinh DATE          NOT NULL,
        QueQuan  NVARCHAR(100) NULL,
        MaLop    NVARCHAR(20)  NOT NULL
    );
END";
            using (var con = GetConnection())
            using (var cmd = new SqlCommand(createSql, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void HienThiDanhSach()
        {
            try
            {
                lsvDanhSachSV.Items.Clear();
                const string sql = "SELECT MaSV, TenSV, GioiTinh, NgaySinh, QueQuan, MaLop FROM [dbo].[SinhVien] ORDER BY MaSV";

                using (var con = GetConnection())
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            string maSv = r.GetString(0);
                            string tenSV = r.GetString(1);
                            string gioiTinh = r.GetString(2);
                            string ngaySinh = r.GetDateTime(3).ToString("dd/MM/yyyy");
                            string queQuan = r.IsDBNull(4) ? "" : r.GetString(4);
                            string maLop = r.GetString(5);

                            var lvi = new ListViewItem(maSv);
                            lvi.SubItems.Add(tenSV);
                            lvi.SubItems.Add(gioiTinh);
                            lvi.SubItems.Add(ngaySinh);
                            lvi.SubItems.Add(queQuan);
                            lvi.SubItems.Add(maLop);
                            lsvDanhSachSV.Items.Add(lvi);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var sbErr = new StringBuilder();
            if (string.IsNullOrWhiteSpace(txtMaSV.Text)) sbErr.AppendLine("• Vui lòng nhập Mã SV");
            if (string.IsNullOrWhiteSpace(txtTenSV.Text)) sbErr.AppendLine("• Vui lòng nhập Tên SV");
            if (string.IsNullOrWhiteSpace(txtMaLop.Text)) sbErr.AppendLine("• Vui lòng nhập Mã lớp");
            if (sbErr.Length > 0)
            {
                MessageBox.Show(sbErr.ToString(), "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maSV = txtMaSV.Text.Trim();
            string tenSV = txtTenSV.Text.Trim();
            string gioiTinh = cboGioiTinh.Text.Trim();
            string ngaySinhSql = dtpNgaySinh.Value.ToString("yyyy-MM-dd");
            string queQuan = txtQueQuan.Text.Trim().Replace("'", "''"); // hạn chế lỗi chuỗi
            string maLop = txtMaLop.Text.Trim();

            string sqlInsert =
                "INSERT INTO [dbo].[SinhVien](MaSV, TenSV, GioiTinh, NgaySinh, QueQuan, MaLop) " +
                $"VALUES ('{maSV}', N'{tenSV.Replace("'", "''")}', N'{gioiTinh}', '{ngaySinhSql}', N'{queQuan}', '{maLop}')";

            try
            {
                using (var con = GetConnection())
                using (var cmd = new SqlCommand(sqlInsert, con))
                {
                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Thêm thành công!");
                        HienThiDanhSach();
                        ClearForm();
                    }
                    else MessageBox.Show("Không có dòng nào được thêm.");
                }
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                MessageBox.Show("Mã SV đã tồn tại.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm dữ liệu: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            txtMaSV.Clear();
            txtTenSV.Clear();
            cboGioiTinh.SelectedIndex = 0;
            dtpNgaySinh.Value = DateTime.Today;
            txtQueQuan.Clear();
            txtMaLop.Clear();
            txtMaSV.Focus();
        }
    }
}
