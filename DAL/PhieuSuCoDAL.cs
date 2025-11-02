using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class PhieuSuCoDAL
    {
        private readonly string _conn;

        public PhieuSuCoDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        // ===================== GET ALL =====================
        public List<PhieuSuCo> GetAll()
        {
            var list = new List<PhieuSuCo>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT s.*, t.TenTaiSan, k.TenKH AS TenKhachHang, nv.HoTen AS TenNhanVien
                FROM PhieuSuCo s
                LEFT JOIN TaiSan t ON s.MaTaiSan = t.MaTaiSan
                LEFT JOIN KhachHang k ON s.MaKH = k.MaKH
                LEFT JOIN NhanVien nv ON s.MaNV_TiepNhan = nv.MaNV
                ORDER BY s.NgayTao DESC", conn);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(MapToPhieuSuCo(dr));
            }
            return list;
        }

        // ===================== GET BY ID =====================
        public PhieuSuCo? GetById(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT s.*, t.TenTaiSan, k.TenKH AS TenKhachHang, nv.HoTen AS TenNhanVien
                FROM PhieuSuCo s
                LEFT JOIN TaiSan t ON s.MaTaiSan = t.MaTaiSan
                LEFT JOIN KhachHang k ON s.MaKH = k.MaKH
                LEFT JOIN NhanVien nv ON s.MaNV_TiepNhan = nv.MaNV
                WHERE s.MaSuCo = @id", conn);

            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr.Read() ? MapToPhieuSuCo(dr) : null;
        }

        // ===================== ADD =====================
        public bool Add(PhieuSuCo s)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                INSERT INTO PhieuSuCo
                (MaTaiSan, MaKH, NguoiBao, MucUuTien, SLA_Gio, MaNV_TiepNhan,
                 NgayTao, TrangThai, MoTa, MaPhieuCV)
                VALUES
                (@MaTaiSan, @MaKH, @NguoiBao, @MucUuTien, @SLA_Gio, @MaNV_TiepNhan,
                 @NgayTao, @TrangThai, @MoTa, @MaPhieuCV)", conn);

            cmd.Parameters.AddWithValue("@MaTaiSan", s.MaTaiSan);
            cmd.Parameters.AddWithValue("@MaKH", (object?)s.MaKH ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NguoiBao", (object?)s.NguoiBao ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MucUuTien", s.MucUuTien);
            cmd.Parameters.AddWithValue("@SLA_Gio", (object?)s.SLA_Gio ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaNV_TiepNhan", (object?)s.MaNV_TiepNhan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayTao", s.NgayTao);
            cmd.Parameters.AddWithValue("@TrangThai", s.TrangThai);
            cmd.Parameters.AddWithValue("@MoTa", (object?)s.MoTa ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaPhieuCV", (object?)s.MaPhieuCV ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== UPDATE =====================
        public bool Update(PhieuSuCo s)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                UPDATE PhieuSuCo
                SET MaTaiSan=@MaTaiSan, MaKH=@MaKH, NguoiBao=@NguoiBao, 
                    MucUuTien=@MucUuTien, SLA_Gio=@SLA_Gio, MaNV_TiepNhan=@MaNV_TiepNhan,
                    TrangThai=@TrangThai, MoTa=@MoTa, MaPhieuCV=@MaPhieuCV
                WHERE MaSuCo=@MaSuCo", conn);

            cmd.Parameters.AddWithValue("@MaSuCo", s.MaSuCo);
            cmd.Parameters.AddWithValue("@MaTaiSan", s.MaTaiSan);
            cmd.Parameters.AddWithValue("@MaKH", (object?)s.MaKH ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NguoiBao", (object?)s.NguoiBao ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MucUuTien", s.MucUuTien);
            cmd.Parameters.AddWithValue("@SLA_Gio", (object?)s.SLA_Gio ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaNV_TiepNhan", (object?)s.MaNV_TiepNhan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TrangThai", s.TrangThai);
            cmd.Parameters.AddWithValue("@MoTa", (object?)s.MoTa ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaPhieuCV", (object?)s.MaPhieuCV ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== DELETE =====================
        public bool Delete(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM PhieuSuCo WHERE MaSuCo=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== GET BY TAI SAN =====================
        public List<PhieuSuCo> GetByTaiSan(int maTaiSan)
        {
            var list = new List<PhieuSuCo>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT s.*, t.TenTaiSan, nv.HoTen AS TenNhanVien
                FROM PhieuSuCo s
                LEFT JOIN TaiSan t ON s.MaTaiSan = t.MaTaiSan
                LEFT JOIN NhanVien nv ON s.MaNV_TiepNhan = nv.MaNV
                WHERE s.MaTaiSan = @maTaiSan", conn);
            cmd.Parameters.AddWithValue("@maTaiSan", maTaiSan);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(MapToPhieuSuCo(dr));
            }
            return list;
        }

        // ===================== KPI SUMMARY =====================
        public List<(string TrangThai, int SoLuong)> GetSummaryByStatus()
        {
            var list = new List<(string, int)>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT TrangThai, COUNT(*) AS SoLuong
                FROM PhieuSuCo
                GROUP BY TrangThai", conn);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add((dr["TrangThai"].ToString(), Convert.ToInt32(dr["SoLuong"])));
            }
            return list;
        }

        // ===================== MAP FUNCTION =====================
        private static PhieuSuCo MapToPhieuSuCo(SqlDataReader dr)
        {
            return new PhieuSuCo
            {
                MaSuCo = Convert.ToInt32(dr["MaSuCo"]),
                MaTaiSan = Convert.ToInt32(dr["MaTaiSan"]),
                MaKH = dr["MaKH"] == DBNull.Value ? null : Convert.ToInt32(dr["MaKH"]),
                NguoiBao = dr["NguoiBao"] == DBNull.Value ? null : dr["NguoiBao"].ToString(),
                MucUuTien = dr["MucUuTien"].ToString(),
                SLA_Gio = dr["SLA_Gio"] == DBNull.Value ? null : Convert.ToInt32(dr["SLA_Gio"]),
                MaNV_TiepNhan = dr["MaNV_TiepNhan"] == DBNull.Value ? null : Convert.ToInt32(dr["MaNV_TiepNhan"]),
                NgayTao = Convert.ToDateTime(dr["NgayTao"]),
                TrangThai = dr["TrangThai"].ToString(),
                MoTa = dr["MoTa"] == DBNull.Value ? null : dr["MoTa"].ToString(),
                MaPhieuCV = dr["MaPhieuCV"] == DBNull.Value ? null : Convert.ToInt32(dr["MaPhieuCV"]),
                TenTaiSan = dr["TenTaiSan"] == DBNull.Value ? null : dr["TenTaiSan"].ToString(),
                TenKhachHang = dr["TenKhachHang"] == DBNull.Value ? null : dr["TenKhachHang"].ToString(),
                TenNhanVien = dr["TenNhanVien"] == DBNull.Value ? null : dr["TenNhanVien"].ToString()
            };
        }
    }
}
