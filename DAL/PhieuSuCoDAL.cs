using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class PhieuSuCoDAL
    {
        private readonly string _connStr;

        public PhieuSuCoDAL(string connStr)
        {
            _connStr = connStr;
        }

        // ========== Lấy tất cả ==========
        public List<PhieuSuCo> GetAll()
        {
            var list = new List<PhieuSuCo>();
            string sql = @"
                SELECT p.*, t.TenTaiSan 
                FROM PhieuSuCo p
                LEFT JOIN TaiSan t ON p.MaTaiSan = t.MaTaiSan
                ORDER BY p.MaSuCo DESC";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PhieuSuCo
                        {
                            MaSuCo = (int)reader["MaSuCo"],
                            MaTaiSan = (int)reader["MaTaiSan"],
                            TenTaiSan = reader["TenTaiSan"]?.ToString(),
                            MoTa = reader["MoTa"]?.ToString(),
                            MucDo = reader["MucDo"].ToString(),
                            NgayBaoCao = (DateTime)reader["NgayBaoCao"],
                            TrangThai = reader["TrangThai"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        // ========== Lấy theo ID ==========
        public PhieuSuCo? GetById(int id)
        {
            PhieuSuCo? item = null;
            string sql = @"
                SELECT p.*, t.TenTaiSan 
                FROM PhieuSuCo p
                LEFT JOIN TaiSan t ON p.MaTaiSan = t.MaTaiSan
                WHERE p.MaSuCo = @id";

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
                            item = new PhieuSuCo
                            {
                                MaSuCo = (int)reader["MaSuCo"],
                                MaTaiSan = (int)reader["MaTaiSan"],
                                TenTaiSan = reader["TenTaiSan"]?.ToString(),
                                MoTa = reader["MoTa"]?.ToString(),
                                MucDo = reader["MucDo"].ToString(),
                                NgayBaoCao = (DateTime)reader["NgayBaoCao"],
                                TrangThai = reader["TrangThai"].ToString()
                            };
                        }
                    }
                }
            }
            return item;
        }

        // ========== Thêm ==========
        public bool Insert(PhieuSuCo model)
        {
            string sql = @"INSERT INTO PhieuSuCo (MaTaiSan, MoTa, MucDo, NgayBaoCao, TrangThai)
                           VALUES (@MaTaiSan, @MoTa, @MucDo, @NgayBaoCao, @TrangThai)";
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTaiSan", model.MaTaiSan);
                    cmd.Parameters.AddWithValue("@MoTa", (object?)model.MoTa ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MucDo", model.MucDo);
                    cmd.Parameters.AddWithValue("@NgayBaoCao", model.NgayBaoCao);
                    cmd.Parameters.AddWithValue("@TrangThai", model.TrangThai);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // ========== Cập nhật ==========
        public bool Update(PhieuSuCo model)
        {
            string sql = @"UPDATE PhieuSuCo 
                           SET MaTaiSan=@MaTaiSan, MoTa=@MoTa, MucDo=@MucDo, 
                               NgayBaoCao=@NgayBaoCao, TrangThai=@TrangThai
                           WHERE MaSuCo=@ID";
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", model.MaSuCo);
                    cmd.Parameters.AddWithValue("@MaTaiSan", model.MaTaiSan);
                    cmd.Parameters.AddWithValue("@MoTa", (object?)model.MoTa ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MucDo", model.MucDo);
                    cmd.Parameters.AddWithValue("@NgayBaoCao", model.NgayBaoCao);
                    cmd.Parameters.AddWithValue("@TrangThai", model.TrangThai);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // ========== Xóa ==========
        public bool Delete(int id)
        {
            string sql = "DELETE FROM PhieuSuCo WHERE MaSuCo=@id";
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
