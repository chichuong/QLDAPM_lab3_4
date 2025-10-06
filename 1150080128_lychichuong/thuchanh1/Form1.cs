using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace QuanAnNhanh
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, int> _cart = new Dictionary<string, int>();
        private readonly string[] _monAn = new string[]
        {
            "Cơm chiên trứng","Bánh mì ốp la","Coca","Lipton",
            "Ốc rang muối","Khoai tây chiên","7 up","Cam",
            "Mỳ xào hải sản","Cá viên chiên","Pepsi","Cafe",
            "Buger bò nướng","Đùi gà rán","Bún bò Huế","Nước suối"
        };

        public Form1() { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Logo: đặt ảnh trong Properties nếu muốn, hoặc tải từ file:
            // pictureBoxLogo.Image = Image.FromFile(@"D:\path\burger.png");

            for (int i = 1; i <= 10; i++) cboBan.Items.Add("Bàn " + i);
            if (cboBan.Items.Count > 0) cboBan.SelectedIndex = 0;

            TaoNutMon_4x4();
            AutoSizeColumns();
            lvOrder.Resize += (s, ev) => AutoSizeColumns();
        }

        private void AutoSizeColumns()
        {
            colMon.Width = lvOrder.ClientSize.Width - 150;
            colSL.Width = 120;
        }

        private void TaoNutMon_4x4()
        {
            pnlButtons.Controls.Clear();

            int cols = 4, rows = 4;
            int btnW = 170, btnH = 32;
            int gapX = 18, gapY = 12;

            int startX = 12, startY = 8;
            int i = 0;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (i >= _monAn.Length) break;
                    var b = new Button();
                    b.Text = _monAn[i];
                    b.Tag = _monAn[i];
                    b.Width = btnW; b.Height = btnH;
                    b.Left = startX + c * (btnW + gapX);
                    b.Top = startY + r * (btnH + gapY);
                    b.Click += BtnMon_Click;
                    pnlButtons.Controls.Add(b);
                    i++;
                }
            }
        }

        private void BtnMon_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;
            var ten = btn.Tag as string;
            if (string.IsNullOrEmpty(ten)) return;

            int qty;
            if (_cart.TryGetValue(ten, out qty)) _cart[ten] = qty + 1; else _cart[ten] = 1;
            RefreshList();
        }

        private void RefreshList()
        {
            lvOrder.BeginUpdate();
            lvOrder.Items.Clear();
            foreach (var kv in _cart)
            {
                var it = new ListViewItem(kv.Key);
                it.SubItems.Add(kv.Value.ToString());
                lvOrder.Items.Add(it);
            }
            lvOrder.EndUpdate();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0) return;
            if (MessageBox.Show("Xóa toàn bộ món đã chọn?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _cart.Clear();
                RefreshList();
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("Chưa chọn món nào!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboBan.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn bàn.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string ban = cboBan.SelectedItem.ToString();
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "orders");
            Directory.CreateDirectory(folder);
            string file = Path.Combine(folder,
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + ban.Replace(" ", "") + ".txt");

            var sb = new StringBuilder();
            sb.AppendLine("===== PHIẾU ORDER =====");
            sb.AppendLine("Bàn: " + ban);
            sb.AppendLine("Thời gian: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            sb.AppendLine("-----------------------");
            int tong = 0;
            foreach (var kv in _cart)
            {
                sb.AppendLine($"{kv.Key} x {kv.Value}");
                tong += kv.Value;
            }
            sb.AppendLine("-----------------------");
            sb.AppendLine("Tổng số phần: " + tong);

            File.WriteAllText(file, sb.ToString(), Encoding.UTF8);
            MessageBox.Show("Đã ghi file order:\n" + file, "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            _cart.Clear();
            RefreshList();
        }
    }
}
