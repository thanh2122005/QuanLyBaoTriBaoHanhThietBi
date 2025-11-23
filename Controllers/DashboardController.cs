using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;
using BaiMoiiii.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly TaiSanBUS _ts;
        private readonly PhieuCongViecBUS _pcv;
        private readonly PhieuSuCoBUS _ps;
        private readonly KhachHangBUS _kh;
        private readonly LichBaoTriBUS _lich;
        private readonly BaoHanhBUS _bh;

        public DashboardController(
            TaiSanBUS ts,
            PhieuCongViecBUS pcv,
            PhieuSuCoBUS ps,
            KhachHangBUS kh,
            LichBaoTriBUS lich,
            BaoHanhBUS bh
        )
        {
            _ts = ts;
            _pcv = pcv;
            _ps = ps;
            _kh = kh;
            _lich = lich;
            _bh = bh;
        }

        // ====================================================================
        // ====================      API DASHBOARD      ========================
        // ====================================================================
        [HttpGet("get-all")]
        public IActionResult GetDashboard()
        {
            try
            {
                var taiSan = _ts.GetAll()?.ToList() ?? new List<TaiSan>();
                var pcv = _pcv.GetAll()?.ToList() ?? new List<PhieuCongViec>();
                var suco = _ps.GetAll()?.ToList() ?? new List<PhieuSuCo>();
                var khach = _kh.GetAll()?.ToList() ?? new List<KhachHang>();
                var lich = _lich.GetAll()?.ToList() ?? new List<LichBaoTri>();
                var baohanh = _bh.GetAll()?.ToList() ?? new List<BaoHanh>();

                DateTime today = DateTime.Now;

                // ====================== THỐNG KÊ ======================
                var thongKe = new
                {
                    tongTaiSan = taiSan.Count,
                    dangLam = pcv.Count(x => x.TrangThai == "Đang làm"),
                    suCoChuaXL = suco.Count(x => x.TrangThai != "Đã giải quyết"),
                    tongKhachHang = khach.Count
                };

                // ====================== CẢNH BÁO ======================
                // Lịch bảo trì trong 7 ngày tới
                var lichSapToi = lich
                    .Where(x =>
                    {
                        DateTime? date = x.NgayKeTiep;
                        if (!date.HasValue)
                            return false;

                        double diff = (date.Value - today).TotalDays;
                        return diff >= 0 && diff <= 7;
                    })
                    .ToList();

                // Bảo hành sắp hết hạn (30 ngày)
                var bhSapHet = baohanh
                    .Where(x =>
                    {
                        // loại bỏ các giá trị rác
                        if (x.NgayKetThuc < new DateTime(2000, 1, 1))
                            return false;

                        double diff = (x.NgayKetThuc - today).TotalDays;
                        return diff >= 0 && diff <= 30;
                    })
                    .ToList();

                // ================= HOẠT ĐỘNG GẦN ĐÂY =================
                var hoatDong = new
                {
                    pcv = pcv
                        .Where(x => x.NgayTao != null)
                        .OrderByDescending(x => x.NgayTao)
                        .Take(3)
                        .ToList(),

                    suco = suco
                        .Where(x => x.NgayBaoCao != null)
                        .OrderByDescending(x => x.NgayBaoCao)
                        .Take(3)
                        .ToList()
                };

                // ======================== TRẢ VỀ ========================
                return Ok(new
                {
                    thongKe,
                    canhBao = new
                    {
                        lichSapToi,
                        bhSapHet
                    },
                    lichSapToi,
                    hoatDong
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Lỗi Dashboard",
                    error = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }
    }
}
