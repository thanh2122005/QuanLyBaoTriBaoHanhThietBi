using Microsoft.Data.SqlClient;
using BaiMoiiii.Models;

namespace BaiMoiiii.DAL
{
    public class KhachHangDAL
    {
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
                });
            }
            return list;
        }

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
                };
            }
            return null;
        }


        public bool Add(KhachHang kh)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new(@"INSERT INTO KhachHang (TenKH, Email, DienThoai, DiaChi)
                                   VALUES (@ten, @email, @sdt, @diachi)", conn);
            cmd.Parameters.AddWithValue("@ten", kh.TenKH);
            cmd.Parameters.AddWithValue("@email", (object?)kh.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sdt", (object?)kh.DienThoai ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@diachi", (object?)kh.DiaChi ?? DBNull.Value);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

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
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            using SqlConnection conn = new(_conn);
            SqlCommand cmd = new("DELETE FROM KhachHang WHERE MaKH=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

    }
}
