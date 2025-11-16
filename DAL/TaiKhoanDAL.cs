using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class TaiKhoanDAL
    {
        private readonly string _conn;

        public TaiKhoanDAL(string connectionString)
        {
            _conn = connectionString;
        }

        // ===== Lấy tất cả tài khoản =====
        public List<TaiKhoan> GetAll()
        {
            var list = new List<TaiKhoan>();
            string sql = "SELECT * FROM TaiKhoan ORDER BY MaTaiKhoan DESC";

            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TaiKhoan
                        {
                            MaTaiKhoan = (int)reader["MaTaiKhoan"],
                            TenDangNhap = reader["TenDangNhap"].ToString(),
                            MatKhauHash = reader["MatKhauHash"].ToString(),
                            Role = reader["Role"].ToString(),
                            FullName = reader["FullName"]?.ToString(),
                            Email = reader["Email"]?.ToString(),
                            Phone = reader["Phone"]?.ToString(),
                            TrangThai = reader["TrangThai"].ToString(),
                            NgayTao = (DateTime)reader["NgayTao"]
                        });
                    }
                }
            }
            return list;
        }

        // ===== Lấy theo ID =====
        public TaiKhoan? GetById(int id)
        {
            TaiKhoan? tk = null;
            string sql = "SELECT * FROM TaiKhoan WHERE MaTaiKhoan = @id";

            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tk = new TaiKhoan
                            {
                                MaTaiKhoan = (int)reader["MaTaiKhoan"],
                                TenDangNhap = reader["TenDangNhap"].ToString(),
                                MatKhauHash = reader["MatKhauHash"].ToString(),
                                Role = reader["Role"].ToString(),
                                FullName = reader["FullName"]?.ToString(),
                                Email = reader["Email"]?.ToString(),
                                Phone = reader["Phone"]?.ToString(),
                                TrangThai = reader["TrangThai"].ToString(),
                                NgayTao = (DateTime)reader["NgayTao"]
                            };
                        }
                    }
                }
            }
            return tk;
        }
        // ===== Lấy theo Email =====
        public TaiKhoan? GetByEmail(string email)
        {
            TaiKhoan? tk = null;
            string sql = "SELECT * FROM TaiKhoan WHERE Email = @Email";

            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tk = new TaiKhoan
                            {
                                MaTaiKhoan = (int)reader["MaTaiKhoan"],
                                TenDangNhap = reader["TenDangNhap"].ToString(),
                                MatKhauHash = reader["MatKhauHash"].ToString(),
                                Role = reader["Role"].ToString(),
                                FullName = reader["FullName"]?.ToString(),
                                Email = reader["Email"]?.ToString(),
                                Phone = reader["Phone"]?.ToString(),
                                TrangThai = reader["TrangThai"].ToString(),
                                NgayTao = (DateTime)reader["NgayTao"]
                            };
                        }
                    }
                }
            }
            return tk;
        }


        // ===== Thêm mới =====
        public bool Insert(TaiKhoan tk)
        {
            string sql = @"INSERT INTO TaiKhoan (TenDangNhap, MatKhauHash, Role, FullName, Email, Phone, TrangThai, NgayTao)
                           VALUES (@TDN, @MK, @Role, @FN, @Email, @Phone, @TrangThai, @NgayTao)";
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TDN", tk.TenDangNhap);
                    cmd.Parameters.AddWithValue("@MK", tk.MatKhauHash);
                    cmd.Parameters.AddWithValue("@Role", tk.Role);
                    cmd.Parameters.AddWithValue("@FN", (object?)tk.FullName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object?)tk.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", (object?)tk.Phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", tk.TrangThai);
                    cmd.Parameters.AddWithValue("@NgayTao", tk.NgayTao);
                    return cmd.ExecuteNonQuery() > 0;
                    
                }
            }
        }

        // ===== Cập nhật =====
        public bool Update(TaiKhoan tk)
        {
            string sql = @"UPDATE TaiKhoan 
                           SET TenDangNhap=@TDN, MatKhauHash=@MK, Role=@Role, 
                               FullName=@FN, Email=@Email, Phone=@Phone, TrangThai=@TrangThai
                           WHERE MaTaiKhoan=@ID";
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", tk.MaTaiKhoan);
                    cmd.Parameters.AddWithValue("@TDN", tk.TenDangNhap);
                    cmd.Parameters.AddWithValue("@MK", tk.MatKhauHash);
                    cmd.Parameters.AddWithValue("@Role", tk.Role);
                    cmd.Parameters.AddWithValue("@FN", (object?)tk.FullName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object?)tk.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", (object?)tk.Phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", tk.TrangThai);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // ===== Xóa =====
        public bool Delete(int id)
        {
            string sql = "DELETE FROM TaiKhoan WHERE MaTaiKhoan = @id";
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
