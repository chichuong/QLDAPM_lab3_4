using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        string strCon = @"Data Source=LAPTOP-8TPGHRS8;Initial Catalog=QuanlyBanhang_ptpmhdt;Integrated Security=True;";
        SqlConnection sqlCon = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMoKetNoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlCon == null)
                {
                    sqlCon = new SqlConnection(strCon);
                }

                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                    MessageBox.Show("Kết nối thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở kết nối: " + ex.Message);
            }
        }

        // ======= SỰ KIỆN NÚT ĐÓNG KẾT NỐI =======
        private void btnDongKetNoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                    MessageBox.Show("Đóng kết nối thành công!");
                }
                else
                {
                    MessageBox.Show("Chưa tạo kết nối hoặc kết nối đã đóng!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đóng kết nối: " + ex.Message);
            }
        }
    }
}