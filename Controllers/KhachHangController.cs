using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.Models;
using OfficeOpenXml;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly KhachHangBUS _bus;
        private readonly LogHelper _logger;
        private readonly string _username;

        public KhachHangController(KhachHangBUS bus, LogHelper logger)
        {
            _bus = bus;
            _logger = logger;
            _username = "Admin";
        }

        // ======================= GET ALL =======================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var data = _bus.GetAll();
            if (!data.Any()) return NotFound(new { message = "Không có khách hàng nào!" });
            return Ok(data);
        }

        // ======================= GET BY ID =======================
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var item = _bus.GetById(id);
            if (item == null) return NotFound(new { message = "Không tìm thấy khách hàng!" });
            return Ok(item);
        }

        // ======================= CREATE =======================
        [HttpPost("create")]
        public IActionResult Create([FromBody] KhachHang kh)
        {
            try
            {
                if (_bus.Add(kh))
                {
                    _logger.WriteLog("KhachHang", kh.MaKH, "Thêm", null, kh, _username);
                    return Ok(new { message = "Thêm khách hàng thành công!" });
                }

                return BadRequest(new { message = "Không thể thêm khách hàng!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ======================= UPDATE =======================
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] KhachHang kh)
        {
            try
            {
                var oldData = _bus.GetById(id);
                if (oldData == null)
                    return NotFound(new { message = "Không tìm thấy khách hàng để cập nhật!" });

                kh.MaKH = id;

                if (_bus.Update(kh))
                {
                    _logger.WriteLog("KhachHang", id, "Sửa", oldData, kh, _username);
                    return Ok(new { message = "Cập nhật thành công!" });
                }

                return BadRequest(new { message = "Không thể cập nhật khách hàng!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ======================= DELETE =======================
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var oldData = _bus.GetById(id);
                if (oldData == null)
                    return NotFound(new { message = "Không tìm thấy khách hàng để xóa!" });

                if (_bus.Delete(id))
                {
                    _logger.WriteLog("KhachHang", id, "Xóa", oldData, null, _username);
                    return Ok(new { message = "Xóa khách hàng thành công!" });
                }

                return BadRequest(new { message = "Không thể xóa khách hàng!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ======================= PHÂN TRANG =======================
        [HttpGet("paging")]
        public IActionResult Paging(int page = 1, int pageSize = 10)
        {
            var data = _bus.GetPaged(page, pageSize);
            var total = _bus.CountAll();

            return Ok(new
            {
                data,
                totalItems = total,
                totalPages = (int)Math.Ceiling((double)total / pageSize)
            });
        }

        // ======================= EXPORT EXCEL =======================
        [HttpGet("export")]
        public IActionResult Export()
        {
            var list = _bus.GetAll();

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("KhachHang");

            // Header
            ws.Cells[1, 1].Value = "MaKH";
            ws.Cells[1, 2].Value = "TenKH";
            ws.Cells[1, 3].Value = "DiaChi";
            ws.Cells[1, 4].Value = "SoDienThoai";
            ws.Cells[1, 5].Value = "Email";

            int row = 2;
            foreach (var x in list)
            {
                ws.Cells[row, 1].Value = x.MaKH;
                ws.Cells[row, 2].Value = x.TenKH;
                ws.Cells[row, 3].Value = x.DiaChi;
                ws.Cells[row, 4].Value = x.DienThoai;
                ws.Cells[row, 5].Value = x.Email;
                row++;
            }

            ws.Cells.AutoFitColumns();

            return File(package.GetAsByteArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "KhachHang.xlsx");
        }

        // ======================= IMPORT EXCEL =======================
        [HttpPost("import")]
        public IActionResult Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "Vui lòng chọn file Excel!" });

            using var ms = new MemoryStream();
            file.CopyTo(ms);

            using var package = new ExcelPackage(ms);
            var ws = package.Workbook.Worksheets[0];

            int rows = ws.Dimension.Rows;
            int added = 0, skipped = 0;

            var all = _bus.GetAll();

            for (int row = 2; row <= rows; row++)
            {
                string ten = ws.Cells[row, 2].Text.Trim();
                if (string.IsNullOrWhiteSpace(ten)) continue;

                string diachi = ws.Cells[row, 3].Text.Trim();
                string sdt = ws.Cells[row, 4].Text.Trim();
                string email = ws.Cells[row, 5].Text.Trim();

                var exist = all.FirstOrDefault(x => x.TenKH == ten && x.DienThoai == sdt);
                if (exist != null) { skipped++; continue; }

                var kh = new KhachHang
                {
                    TenKH = ten,
                    DiaChi = diachi,
                    DienThoai = sdt,
                    Email = email
                };

                _bus.Add(kh);
                added++;
            }

            return Ok(new
            {
                message = "Import hoàn tất!",
                added,
                skipped
            });
        }
    }
}
