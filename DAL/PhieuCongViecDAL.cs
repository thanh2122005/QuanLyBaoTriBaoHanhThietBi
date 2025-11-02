using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.DAL
{
    public class PhieuCongViecDAL
    {
        private readonly string _conn;

        public PhieuCongViecDAL(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        // ===================== GET ALL =====================
        public List<PhieuCongViec> GetAll()
        {
            var list = new List<PhieuCongViec>();
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT p.*, t.TenTaiSan, nv.HoTen AS TenNhanVien
                FROM PhieuCongViec p
                LEFT JOIN TaiSan t ON p.MaTaiSan = t.MaTaiSan
                LEFT JOIN NhanVien nv ON p.MaNV_PhanCong = nv.MaNV
                ORDER BY p.NgayTao DESC", conn);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new PhieuCongViec
                {
                    MaPhieuCV = Convert.ToInt32(dr["MaPhieuCV"]),
                    Loai = dr["Loai"].ToString(),
                    MaLich = dr["MaLich"] == DBNull.Value ? null : Convert.ToInt32(dr["MaLich"]),
                    MaTaiSan = Convert.ToInt32(dr["MaTaiSan"]),
                    TieuDe = dr["TieuDe"].ToString(),
                    MucUuTien = dr["MucUuTien"].ToString(),
                    SLA_Gio = dr["SLA_Gio"] == DBNull.Value ? null : Convert.ToInt32(dr["SLA_Gio"]),
                    MoTa = dr["MoTa"] == DBNull.Value ? null : dr["MoTa"].ToString(),
                    MaNV_PhanCong = dr["MaNV_PhanCong"] == DBNull.Value ? null : Convert.ToInt32(dr["MaNV_PhanCong"]),
                    NgayTao = Convert.ToDateTime(dr["NgayTao"]),
                    TrangThai = dr["TrangThai"].ToString(),
                    NgayBatDau = dr["NgayBatDau"] == DBNull.Value ? null : Convert.ToDateTime(dr["NgayBatDau"]),
                    NgayHoanThanh = dr["NgayHoanThanh"] == DBNull.Value ? null : Convert.ToDateTime(dr["NgayHoanThanh"]),
                    GhiChu = dr["GhiChu"] == DBNull.Value ? null : dr["GhiChu"].ToString(),
                    TenTaiSan = dr["TenTaiSan"] == DBNull.Value ? null : dr["TenTaiSan"].ToString(),
                    TenNhanVien = dr["TenNhanVien"] == DBNull.Value ? null : dr["TenNhanVien"].ToString()
                });
            }

            return list;
        }

        // ===================== GET BY ID =====================
        public PhieuCongViec? GetById(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                SELECT p.*, t.TenTaiSan, nv.HoTen AS TenNhanVien
                FROM PhieuCongViec p
                LEFT JOIN TaiSan t ON p.MaTaiSan = t.MaTaiSan
                LEFT JOIN NhanVien nv ON p.MaNV_PhanCong = nv.MaNV
                WHERE p.MaPhieuCV = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return new PhieuCongViec
                {
                    MaPhieuCV = Convert.ToInt32(dr["MaPhieuCV"]),
                    Loai = dr["Loai"].ToString(),
                    MaLich = dr["MaLich"] == DBNull.Value ? null : Convert.ToInt32(dr["MaLich"]),
                    MaTaiSan = Convert.ToInt32(dr["MaTaiSan"]),
                    TieuDe = dr["TieuDe"].ToString(),
                    MucUuTien = dr["MucUuTien"].ToString(),
                    SLA_Gio = dr["SLA_Gio"] == DBNull.Value ? null : Convert.ToInt32(dr["SLA_Gio"]),
                    MoTa = dr["MoTa"] == DBNull.Value ? null : dr["MoTa"].ToString(),
                    MaNV_PhanCong = dr["MaNV_PhanCong"] == DBNull.Value ? null : Convert.ToInt32(dr["MaNV_PhanCong"]),
                    NgayTao = Convert.ToDateTime(dr["NgayTao"]),
                    TrangThai = dr["TrangThai"].ToString(),
                    NgayBatDau = dr["NgayBatDau"] == DBNull.Value ? null : Convert.ToDateTime(dr["NgayBatDau"]),
                    NgayHoanThanh = dr["NgayHoanThanh"] == DBNull.Value ? null : Convert.ToDateTime(dr["NgayHoanThanh"]),
                    GhiChu = dr["GhiChu"] == DBNull.Value ? null : dr["GhiChu"].ToString(),
                    TenTaiSan = dr["TenTaiSan"] == DBNull.Value ? null : dr["TenTaiSan"].ToString(),
                    TenNhanVien = dr["TenNhanVien"] == DBNull.Value ? null : dr["TenNhanVien"].ToString()
                };
            }
            return null;
        }

        // ===================== CREATE =====================
        public bool Add(PhieuCongViec p)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                INSERT INTO PhieuCongViec
                (Loai, MaLich, MaTaiSan, TieuDe, MucUuTien, SLA_Gio, MoTa,
                 MaNV_PhanCong, NgayTao, TrangThai, NgayBatDau, NgayHoanThanh, GhiChu)
                VALUES (@Loai, @MaLich, @MaTaiSan, @TieuDe, @MucUuTien, @SLA_Gio, @MoTa,
                        @MaNV_PhanCong, @NgayTao, @TrangThai, @NgayBatDau, @NgayHoanThanh, @GhiChu)", conn);

            cmd.Parameters.AddWithValue("@Loai", p.Loai);
            cmd.Parameters.AddWithValue("@MaLich", (object?)p.MaLich ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaTaiSan", p.MaTaiSan);
            cmd.Parameters.AddWithValue("@TieuDe", p.TieuDe);
            cmd.Parameters.AddWithValue("@MucUuTien", p.MucUuTien);
            cmd.Parameters.AddWithValue("@SLA_Gio", (object?)p.SLA_Gio ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MoTa", (object?)p.MoTa ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaNV_PhanCong", (object?)p.MaNV_PhanCong ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayTao", p.NgayTao);
            cmd.Parameters.AddWithValue("@TrangThai", p.TrangThai);
            cmd.Parameters.AddWithValue("@NgayBatDau", (object?)p.NgayBatDau ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayHoanThanh", (object?)p.NgayHoanThanh ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GhiChu", (object?)p.GhiChu ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== UPDATE =====================
        public bool Update(PhieuCongViec p)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"
                UPDATE PhieuCongViec
                SET Loai=@Loai, MaLich=@MaLich, MaTaiSan=@MaTaiSan, 
                    TieuDe=@TieuDe, MucUuTien=@MucUuTien, SLA_Gio=@SLA_Gio,
                    MoTa=@MoTa, MaNV_PhanCong=@MaNV_PhanCong, TrangThai=@TrangThai,
                    NgayBatDau=@NgayBatDau, NgayHoanThanh=@NgayHoanThanh, GhiChu=@GhiChu
                WHERE MaPhieuCV=@MaPhieuCV", conn);

            cmd.Parameters.AddWithValue("@MaPhieuCV", p.MaPhieuCV);
            cmd.Parameters.AddWithValue("@Loai", p.Loai);
            cmd.Parameters.AddWithValue("@MaLich", (object?)p.MaLich ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaTaiSan", p.MaTaiSan);
            cmd.Parameters.AddWithValue("@TieuDe", p.TieuDe);
            cmd.Parameters.AddWithValue("@MucUuTien", p.MucUuTien);
            cmd.Parameters.AddWithValue("@SLA_Gio", (object?)p.SLA_Gio ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MoTa", (object?)p.MoTa ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaNV_PhanCong", (object?)p.MaNV_PhanCong ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TrangThai", p.TrangThai);
            cmd.Parameters.AddWithValue("@NgayBatDau", (object?)p.NgayBatDau ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayHoanThanh", (object?)p.NgayHoanThanh ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GhiChu", (object?)p.GhiChu ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // ===================== DELETE =====================
        public bool Delete(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM PhieuCongViec WHERE MaPhieuCV=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
