using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class TaiSanDAL
    {
        private readonly string _conn;

        // ✅ Constructor mới: nhận trực tiếp chuỗi kết nối
        public TaiSanDAL(string connectionString)
        {
            _conn = connectionString;
        }

        // ===================== GET ALL =====================
        public List<TaiSan> GetAll()
        {
            var list = new List<TaiSan>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT ts.*, bh.DieuKhoan AS TenBaoHanh, kh.TenKH AS TenKhachHang
                FROM TaiSan ts
                LEFT JOIN BaoHanh bh ON ts.MaBaoHanh = bh.MaBaoHanh
                LEFT JOIN KhachHang kh ON ts.MaKH = kh.MaKH
                ORDER BY ts.MaTaiSan DESC", conn);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                list.Add(MapToTaiSan(dr));
            return list;
        }

        // ===================== GET BY ID =====================
        public TaiSan? GetById(int id)
            {
                using SqlConnection conn = new(_conn);
                SqlCommand cmd = new(@"
                    SELECT ts.*, bh.DieuKhoan AS TenBaoHanh, kh.TenKH AS TenKhachHang
                    FROM TaiSan ts
                    LEFT JOIN BaoHanh bh ON ts.MaBaoHanh = bh.MaBaoHanh
                    LEFT JOIN KhachHang kh ON ts.MaKH = kh.MaKH
                    WHERE ts.MaTaiSan = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                return dr.Read() ? MapToTaiSan(dr) : null;
            }

            // ===================== ADD =====================
            public bool Add(TaiSan ts)
            {
                using SqlConnection conn = new(_conn);
                SqlCommand cmd = new(@"
                    INSERT INTO TaiSan (TenTaiSan, ViTri, NgayMua, MaBaoHanh, MaKH, TrangThai, GhiChu)
                    VALUES (@TenTaiSan, @ViTri, @NgayMua, @MaBaoHanh, @MaKH, @TrangThai, @GhiChu)", conn);

                cmd.Parameters.AddWithValue("@TenTaiSan", ts.TenTaiSan);
                cmd.Parameters.AddWithValue("@ViTri", (object?)ts.ViTri ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayMua", (object?)ts.NgayMua ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaBaoHanh", (object?)ts.MaBaoHanh ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaKH", (object?)ts.MaKH ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TrangThai", ts.TrangThai);
                cmd.Parameters.AddWithValue("@GhiChu", (object?)ts.GhiChu ?? DBNull.Value);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }

            // ===================== UPDATE =====================
            public bool Update(TaiSan ts)
            {
                using SqlConnection conn = new(_conn);
                SqlCommand cmd = new(@"
                    UPDATE TaiSan
                    SET TenTaiSan=@TenTaiSan, ViTri=@ViTri, NgayMua=@NgayMua,
                        MaBaoHanh=@MaBaoHanh, MaKH=@MaKH, TrangThai=@TrangThai, GhiChu=@GhiChu
                    WHERE MaTaiSan=@MaTaiSan", conn);

                cmd.Parameters.AddWithValue("@MaTaiSan", ts.MaTaiSan);
                cmd.Parameters.AddWithValue("@TenTaiSan", ts.TenTaiSan);
                cmd.Parameters.AddWithValue("@ViTri", (object?)ts.ViTri ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayMua", (object?)ts.NgayMua ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaBaoHanh", (object?)ts.MaBaoHanh ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaKH", (object?)ts.MaKH ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TrangThai", ts.TrangThai);
                cmd.Parameters.AddWithValue("@GhiChu", (object?)ts.GhiChu ?? DBNull.Value);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }

            // ===================== DELETE =====================
            public bool Delete(int id)
            {
                using SqlConnection conn = new(_conn);
                SqlCommand cmd = new("DELETE FROM TaiSan WHERE MaTaiSan=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }

            // ===================== SEARCH =====================
            public List<TaiSan> Search(string keyword)
            {
                var list = new List<TaiSan>();
                using SqlConnection conn = new(_conn);
                SqlCommand cmd = new(@"
                    SELECT ts.*, bh.DieuKhoan AS TenBaoHanh, kh.TenKH AS TenKhachHang
                    FROM TaiSan ts
                    LEFT JOIN BaoHanh bh ON ts.MaBaoHanh = bh.MaBaoHanh
                    LEFT JOIN KhachHang kh ON ts.MaKH = kh.MaKH
                    WHERE ts.TenTaiSan LIKE @kw OR ts.ViTri LIKE @kw", conn);

                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    list.Add(MapToTaiSan(dr));
                return list;
            }

            // ===================== MAP FUNCTION =====================
            private static TaiSan MapToTaiSan(SqlDataReader dr)
            {
                return new TaiSan
                {
                    MaTaiSan = Convert.ToInt32(dr["MaTaiSan"]),
                    TenTaiSan = dr["TenTaiSan"].ToString(),
                    ViTri = dr["ViTri"] == DBNull.Value ? null : dr["ViTri"].ToString(),
                    NgayMua = dr["NgayMua"] == DBNull.Value ? null : Convert.ToDateTime(dr["NgayMua"]),
                    MaBaoHanh = dr["MaBaoHanh"] == DBNull.Value ? null : Convert.ToInt32(dr["MaBaoHanh"]),
                    MaKH = dr["MaKH"] == DBNull.Value ? null : Convert.ToInt32(dr["MaKH"]),
                    TrangThai = dr["TrangThai"].ToString(),
                    GhiChu = dr["GhiChu"] == DBNull.Value ? null : dr["GhiChu"].ToString(),
                    TenBaoHanh = dr["TenBaoHanh"] == DBNull.Value ? null : dr["TenBaoHanh"].ToString(),
                    TenKhachHang = dr["TenKhachHang"] == DBNull.Value ? null : dr["TenKhachHang"].ToString()
                };
            }
        }
    }
