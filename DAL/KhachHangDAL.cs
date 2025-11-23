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

        // ========== GET ALL ==========
        public List<KhachHang> GetAll()
        {
            var list = new List<KhachHang>();
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT * FROM KhachHang ORDER BY MaKH ASC", conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new KhachHang
                {
                    MaKH = Convert.ToInt32(reader["MaKH"]),
                    TenKH = reader["TenKH"].ToString() ?? "",
                    DiaChi = reader["DiaChi"]?.ToString(),
                    DienThoai = reader["DienThoai"]?.ToString(),
                    Email = reader["Email"]?.ToString()
                });
            }
            return list;
        }

        // ========== GET BY ID ==========
        public KhachHang? GetById(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT * FROM KhachHang WHERE MaKH=@id", conn);

            cmd.Parameters.AddWithValue("@id", id);

            conn.Open();
            using var reader = cmd.ExecuteReader();

            if (!reader.Read()) return null;

            return new KhachHang
            {
                MaKH = Convert.ToInt32(reader["MaKH"]),
                TenKH = reader["TenKH"].ToString() ?? "",
                DiaChi = reader["DiaChi"]?.ToString(),
                DienThoai = reader["DienThoai"]?.ToString(),
                Email = reader["Email"]?.ToString()
            };
        }

        // ========== ADD ==========
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

        // ========== UPDATE ==========
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

        // ========== DELETE ==========
        public bool Delete(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("DELETE FROM KhachHang WHERE MaKH=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ========== PAGING ==========
        public List<KhachHang> GetPaged(int page, int size)
        {
            var list = new List<KhachHang>();

            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(@"
        SELECT *
        FROM KhachHang
        ORDER BY MaKH ASC
        OFFSET (@skip) ROWS
        FETCH NEXT @size ROWS ONLY;", conn);

            cmd.Parameters.AddWithValue("@skip", (page - 1) * size);
            cmd.Parameters.AddWithValue("@size", size);

            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new KhachHang
                {
                    MaKH = Convert.ToInt32(reader["MaKH"]),
                    TenKH = reader["TenKH"].ToString() ?? "",
                    DiaChi = reader["DiaChi"]?.ToString(),
                    DienThoai = reader["DienThoai"]?.ToString(),
                    Email = reader["Email"]?.ToString()
                });
            }

            return list;
        }

        // ========== COUNT ==========
        public int CountAll()
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT COUNT(*) FROM KhachHang", conn);
            conn.Open();
            return (int)cmd.ExecuteScalar();
        }
    }
}
