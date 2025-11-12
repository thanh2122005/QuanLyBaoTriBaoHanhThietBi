using Microsoft.Data.SqlClient;
using BaiMoiiii.Models;

namespace BaiMoiiii.DAL
{
    public class KhachHangDAL
    {
        private readonly string _connectionString;

        public KhachHangDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        // 🔹 Lấy tất cả khách hàng
        public List<KhachHang> GetAll()
        {
            var list = new List<KhachHang>();
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT * FROM KhachHang ORDER BY MaKH DESC", conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new KhachHang
                {
                    MaKH = Convert.ToInt32(reader["MaKH"]),
                    TenKH = reader["TenKH"].ToString() ?? "",
                    DiaChi = reader["DiaChi"] == DBNull.Value ? null : reader["DiaChi"].ToString(),
                    DienThoai = reader["DienThoai"] == DBNull.Value ? null : reader["DienThoai"].ToString(),
                    Email = reader["Email"] == DBNull.Value ? null : reader["Email"].ToString()
                });
            }
            return list;
        }

        // 🔹 Lấy theo ID
        public KhachHang? GetById(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT * FROM KhachHang WHERE MaKH=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new KhachHang
                {
                    MaKH = Convert.ToInt32(reader["MaKH"]),
                    TenKH = reader["TenKH"].ToString() ?? "",
                    DiaChi = reader["DiaChi"] == DBNull.Value ? null : reader["DiaChi"].ToString(),
                    DienThoai = reader["DienThoai"] == DBNull.Value ? null : reader["DienThoai"].ToString(),
                    Email = reader["Email"] == DBNull.Value ? null : reader["Email"].ToString()
                };
            }
            return null;
        }

        // 🔹 Thêm mới
        public bool Add(KhachHang kh)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"
                INSERT INTO KhachHang (TenKH, DiaChi, DienThoai, Email)
                VALUES (@TenKH, @DiaChi, @DienThoai, @Email)", conn);
            cmd.Parameters.AddWithValue("@TenKH", kh.TenKH);
            cmd.Parameters.AddWithValue("@DiaChi", (object?)kh.DiaChi ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DienThoai", (object?)kh.DienThoai ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object?)kh.Email ?? DBNull.Value);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 Cập nhật
        public bool Update(KhachHang kh)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"
                UPDATE KhachHang
                SET TenKH=@TenKH, DiaChi=@DiaChi, DienThoai=@DienThoai, Email=@Email
                WHERE MaKH=@MaKH", conn);
            cmd.Parameters.AddWithValue("@MaKH", kh.MaKH);
            cmd.Parameters.AddWithValue("@TenKH", kh.TenKH);
            cmd.Parameters.AddWithValue("@DiaChi", (object?)kh.DiaChi ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DienThoai", (object?)kh.DienThoai ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object?)kh.Email ?? DBNull.Value);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 Xóa
        public bool Delete(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("DELETE FROM KhachHang WHERE MaKH=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
