using Microsoft.Data.SqlClient;
using System.Data;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class BaoHanhDAL
    {
        private readonly string _connectionString;

        public BaoHanhDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        // 🔹 Lấy tất cả
        public List<BaoHanh> GetAll()
        {
            var list = new List<BaoHanh>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM BaoHanh", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new BaoHanh
                        {
                            MaBaoHanh = (int)reader["MaBaoHanh"],
                            NhaCungCap = reader["NhaCungCap"].ToString() ?? "",
                            NgayBatDau = Convert.ToDateTime(reader["NgayBatDau"]),
                            NgayKetThuc = Convert.ToDateTime(reader["NgayKetThuc"]),
                            DieuKhoan = reader["DieuKhoan"]?.ToString(),
                            MaNV = reader["MaNV"] == DBNull.Value ? null : (int?)reader["MaNV"]
                        });
                    }
                }
            }
            return list;
        }

        // 🔹 Lấy theo ID
        public BaoHanh? GetById(int id)
        {
            BaoHanh? bh = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM BaoHanh WHERE MaBaoHanh=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        bh = new BaoHanh
                        {
                            MaBaoHanh = (int)reader["MaBaoHanh"],
                            NhaCungCap = reader["NhaCungCap"].ToString() ?? "",
                            NgayBatDau = Convert.ToDateTime(reader["NgayBatDau"]),
                            NgayKetThuc = Convert.ToDateTime(reader["NgayKetThuc"]),
                            DieuKhoan = reader["DieuKhoan"]?.ToString(),
                            MaNV = reader["MaNV"] == DBNull.Value ? null : (int?)reader["MaNV"]
                        };
                    }
                }
            }
            return bh;
        }

        // 🔹 Thêm mới
        public bool Create(BaoHanh bh)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var sql = @"INSERT INTO BaoHanh (NhaCungCap, NgayBatDau, NgayKetThuc, DieuKhoan, MaNV)
                            VALUES (@NhaCungCap, @NgayBatDau, @NgayKetThuc, @DieuKhoan, @MaNV)";
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NhaCungCap", bh.NhaCungCap);
                cmd.Parameters.AddWithValue("@NgayBatDau", bh.NgayBatDau);
                cmd.Parameters.AddWithValue("@NgayKetThuc", bh.NgayKetThuc);
                cmd.Parameters.AddWithValue("@DieuKhoan", (object?)bh.DieuKhoan ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaNV", (object?)bh.MaNV ?? DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 🔹 Cập nhật
        public bool Update(BaoHanh bh)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var sql = @"UPDATE BaoHanh
                            SET NhaCungCap=@NhaCungCap,
                                NgayBatDau=@NgayBatDau,
                                NgayKetThuc=@NgayKetThuc,
                                DieuKhoan=@DieuKhoan,
                                MaNV=@MaNV
                            WHERE MaBaoHanh=@MaBaoHanh";
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NhaCungCap", bh.NhaCungCap);
                cmd.Parameters.AddWithValue("@NgayBatDau", bh.NgayBatDau);
                cmd.Parameters.AddWithValue("@NgayKetThuc", bh.NgayKetThuc);
                cmd.Parameters.AddWithValue("@DieuKhoan", (object?)bh.DieuKhoan ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaNV", (object?)bh.MaNV ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaBaoHanh", bh.MaBaoHanh);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 🔹 Xóa
        public bool Delete(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM BaoHanh WHERE MaBaoHanh=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
