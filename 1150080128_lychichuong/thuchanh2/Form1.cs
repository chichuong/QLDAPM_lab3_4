using System;
using System.Windows.Forms;

namespace thuchanh2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            string hoTen = (txtHoTen.Text ?? "").Trim();
            if (hoTen.Length == 0)
            {
                MessageBox.Show("Họ tên không được rỗng.", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            string ngay = dtpNgaySinh.Value.ToString("dd/MM/yyyy");
            string lop = (txtLop.Text ?? "").Trim();
            string diaChi = (txtDiaChi.Text ?? "").Trim();

            var item = new ListViewItem(hoTen);
            item.SubItems.Add(ngay);
            item.SubItems.Add(lop);
            item.SubItems.Add(diaChi);
            lvSV.Items.Add(item);

            ClearInputs();
        }

        // Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lvSV.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn 1 dòng để sửa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string hoTen = (txtHoTen.Text ?? "").Trim();
            if (hoTen.Length == 0)
            {
                MessageBox.Show("Họ tên không được rỗng.", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            var it = lvSV.SelectedItems[0];
            it.Text = hoTen;
            it.SubItems[1].Text = dtpNgaySinh.Value.ToString("dd/MM/yyyy");
            it.SubItems[2].Text = (txtLop.Text ?? "").Trim();
            it.SubItems[3].Text = (txtDiaChi.Text ?? "").Trim();
        }

        // Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lvSV.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn 1 dòng để xóa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa dòng đã chọn?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lvSV.Items.Remove(lvSV.SelectedItems[0]);
                ClearInputs();
            }
        }

        // Thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Khi chọn 1 dòng -> đổ dữ liệu lên vùng "Thông tin sinh viên"
        private void lvSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSV.SelectedItems.Count == 0) return;
            var it = lvSV.SelectedItems[0];
            txtHoTen.Text = it.Text;
            DateTime d;
            if (DateTime.TryParse(it.SubItems[1].Text, out d)) dtpNgaySinh.Value = d;
            txtLop.Text = it.SubItems[2].Text;
            txtDiaChi.Text = it.SubItems[3].Text;
        }

        private void ClearInputs()
        {
            txtHoTen.Clear();
            txtLop.Clear();
            txtDiaChi.Clear();
            txtHoTen.Focus();
        }
    }
}
