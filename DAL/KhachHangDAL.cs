using Microsoft.Data.SqlClient;
using BaiMoiiii.Models;

namespace BaiMoiiii.DAL
{
    public class KhachHangDAL
    {
<<<<<<< HEAD
        private readonly string? _conn;

        public KhachHangDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        public List<KhachHang> GetAll()
        {
            var list = new List<KhachHang>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM KhachHang", conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new KhachHang
                {
                    MaKH = Convert.ToInt32(dr["MaKH"]),
                    TenKH = dr["TenKH"].ToString() ?? "",
                    Email = dr["Email"] == DBNull.Value ? null : dr["Email"].ToString(),
                    DienThoai = dr["DienThoai"] == DBNull.Value ? null : dr["DienThoai"].ToString(),
                    DiaChi = dr["DiaChi"] == DBNull.Value ? null : dr["DiaChi"].ToString()
=======
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
>>>>>>> origin/Dung
                });
            }
            return list;
        }

<<<<<<< HEAD
        public KhachHang? GetById(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("SELECT * FROM KhachHang WHERE MaKH=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return new KhachHang
                {
                    MaKH = Convert.ToInt32(dr["MaKH"]),
                    TenKH = dr["TenKH"].ToString() ?? "",
                    Email = dr["Email"] == DBNull.Value ? null : dr["Email"].ToString(),
                    DienThoai = dr["DienThoai"] == DBNull.Value ? null : dr["DienThoai"].ToString(),
                    DiaChi = dr["DiaChi"] == DBNull.Value ? null : dr["DiaChi"].ToString()
=======
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
>>>>>>> origin/Dung
                };
            }
            return null;
        }

<<<<<<< HEAD

        public bool Add(KhachHang kh)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"INSERT INTO KhachHang (TenKH, Email, DienThoai, DiaChi)
                                   VALUES (@ten, @email, @sdt, @diachi)", conn);
            cmd.Parameters.AddWithValue("@ten", kh.TenKH);
            cmd.Parameters.AddWithValue("@email", (object?)kh.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sdt", (object?)kh.DienThoai ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@diachi", (object?)kh.DiaChi ?? DBNull.Value);
=======
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
>>>>>>> origin/Dung
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

<<<<<<< HEAD
        public bool Update(KhachHang kh)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"UPDATE KhachHang 
                                   SET TenKH=@ten, Email=@email, DienThoai=@sdt, DiaChi=@diachi 
                                   WHERE MaKH=@id", conn);
            cmd.Parameters.AddWithValue("@id", kh.MaKH);
            cmd.Parameters.AddWithValue("@ten", kh.TenKH);
            cmd.Parameters.AddWithValue("@email", (object?)kh.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sdt", (object?)kh.DienThoai ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@diachi", (object?)kh.DiaChi ?? DBNull.Value);
=======
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
>>>>>>> origin/Dung
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

<<<<<<< HEAD
        public bool Delete(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM KhachHang WHERE MaKH=@id", conn);
=======
        // 🔹 Xóa
        public bool Delete(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("DELETE FROM KhachHang WHERE MaKH=@id", conn);
>>>>>>> origin/Dung
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
<<<<<<< HEAD

=======
>>>>>>> origin/Dung
    }
}
