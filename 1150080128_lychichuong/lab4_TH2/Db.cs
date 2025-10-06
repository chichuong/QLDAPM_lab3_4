using System.Data.SqlClient;

namespace lab4_TH2
{
    static class Db
    {
        // SỬA TẠI ĐÂY CHO PHÙ HỢP MÁY BẠN
        public static readonly string ConnString =
            @"Data Source=LAPTOP-8TPGHRS8;Initial Catalog=QlSV_PTPMHDT;Integrated Security=True;";

        public static SqlConnection NewConnection() => new SqlConnection(ConnString);
    }
}
