using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class BaoHanhDAL
    {
        private readonly string _connStr;
        public BaoHanhDAL(string connStr)
        {
            _connStr = connStr;
        }

        // Lấy tất cả
        public List<BaoHanh> GetAll()
        {
            var list = new List<BaoHanh>();
            string sql = @"
                SELECT b.*, t.TenTaiSan 
                FROM BaoHanh b 
                LEFT JOIN TaiSan t ON b.MaTaiSan = t.MaTaiSan";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new BaoHanh
                        {
                            MaBaoHanh = (int)reader["MaBaoHanh"],
                            NhaCungCap = reader["NhaCungCap"].ToString(),
                            NgayBatDau = (DateTime)reader["NgayBatDau"],
                            NgayKetThuc = (DateTime)reader["NgayKetThuc"],
                            DieuKhoan = reader["DieuKhoan"]?.ToString(),
                            MaTaiSan = (int)reader["MaTaiSan"],
                            TenTaiSan = reader["TenTaiSan"]?.ToString()
                        });
                    }
                }
            }
            return list;
        }

        // Lấy theo ID
        public BaoHanh? GetById(int id)
        {
            BaoHanh? bh = null;
            string sql = @"SELECT b.*, t.TenTaiSan 
                           FROM BaoHanh b 
                           LEFT JOIN TaiSan t ON b.MaTaiSan = t.MaTaiSan
                           WHERE b.MaBaoHanh = @id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bh = new BaoHanh
                            {
                                MaBaoHanh = (int)reader["MaBaoHanh"],
                                NhaCungCap = reader["NhaCungCap"].ToString(),
                                NgayBatDau = (DateTime)reader["NgayBatDau"],
                                NgayKetThuc = (DateTime)reader["NgayKetThuc"],
                                DieuKhoan = reader["DieuKhoan"]?.ToString(),
                                MaTaiSan = (int)reader["MaTaiSan"],
                                TenTaiSan = reader["TenTaiSan"]?.ToString()
                            };
                        }
                    }
                }
            }
            return bh;
        }

        // Thêm mới
        public bool Insert(BaoHanh bh)
        {
            string sql = @"INSERT INTO BaoHanh (NhaCungCap, NgayBatDau, NgayKetThuc, DieuKhoan, MaTaiSan)
                           VALUES (@NCC, @NBD, @NKT, @DK, @MTS)";
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NCC", bh.NhaCungCap);
                    cmd.Parameters.AddWithValue("@NBD", bh.NgayBatDau);
                    cmd.Parameters.AddWithValue("@NKT", bh.NgayKetThuc);
                    cmd.Parameters.AddWithValue("@DK", (object)bh.DieuKhoan ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MTS", bh.MaTaiSan);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Cập nhật
        public bool Update(BaoHanh bh)
        {
            string sql = @"UPDATE BaoHanh 
                           SET NhaCungCap=@NCC, NgayBatDau=@NBD, NgayKetThuc=@NKT, DieuKhoan=@DK, MaTaiSan=@MTS
                           WHERE MaBaoHanh=@ID";
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", bh.MaBaoHanh);
                    cmd.Parameters.AddWithValue("@NCC", bh.NhaCungCap);
                    cmd.Parameters.AddWithValue("@NBD", bh.NgayBatDau);
                    cmd.Parameters.AddWithValue("@NKT", bh.NgayKetThuc);
                    cmd.Parameters.AddWithValue("@DK", (object)bh.DieuKhoan ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MTS", bh.MaTaiSan);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Xóa
        public bool Delete(int id)
        {
            string sql = "DELETE FROM BaoHanh WHERE MaBaoHanh = @id";
            using (var conn = new SqlConnection(_connStr))
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
