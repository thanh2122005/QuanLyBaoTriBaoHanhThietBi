using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;
using OfficeOpenXml;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiSanController : ControllerBase
    {
        private readonly TaiSanBUS _bus;
        private readonly LogHelper _logger;
        private readonly string _username;

        public TaiSanController(TaiSanBUS bus, LogHelper logger)
        {
            _bus = bus;
            _logger = logger;
            _username = "Admin";
        }

        // ======================= GET ALL =======================
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var list = _bus.GetAll();
            if (!list.Any())
                return NotFound(new { message = "Không có tài sản nào!" });

            return Ok(list);
        }

        // ======================= GET BY ID =======================
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var ts = _bus.GetById(id);
            if (ts == null)
                return NotFound(new { message = "Không tìm thấy tài sản!" });

            return Ok(ts);
        }

        // ======================= CREATE =======================
        [HttpPost("create")]
        public IActionResult Create([FromBody] TaiSan model)
        {
            try
            {
                if (_bus.Add(model))
                {
                    _logger.WriteLog("TaiSan", model.MaTaiSan ?? 0, "Thêm", null, model, _username);
                    return Ok(new { message = "Thêm tài sản thành công!" });
                }

                return BadRequest(new { message = "Không thể thêm tài sản!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ======================= UPDATE =======================
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] TaiSan model)
        {
            try
            {
                var oldData = _bus.GetById(id);
                if (oldData == null)
                    return NotFound(new { message = "Không tìm thấy tài sản để cập nhật!" });

                model.MaTaiSan = id;

                if (_bus.Update(model))
                {
                    _logger.WriteLog("TaiSan", id, "Sửa", oldData, model, _username);
                    return Ok(new { message = "Cập nhật thành công!" });
                }

                return BadRequest(new { message = "Cập nhật thất bại!" });
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
                    return NotFound(new { message = "Không tìm thấy tài sản để xóa!" });

                if (_bus.Delete(id))
                {
                    _logger.WriteLog("TaiSan", id, "Xóa", oldData, null, _username);
                    return Ok(new { message = "Xóa thành công!" });
                }

                return BadRequest(new { message = "Xóa thất bại!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // ======================= SEARCH =======================
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { message = "Từ khóa tìm kiếm không được để trống!" });

            var list = _bus.Search(keyword);

            if (!list.Any())
                return NotFound(new { message = "Không tìm thấy tài sản phù hợp!" });

            return Ok(list);
        }

        // ======================= PAGING =======================
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
            var ws = package.Workbook.Worksheets.Add("TaiSan");

            // ===== HEADER =====
            ws.Cells[1, 1].Value = "MaTaiSan";
            ws.Cells[1, 2].Value = "TenTaiSan";
            ws.Cells[1, 3].Value = "TenKhachHang";
            ws.Cells[1, 4].Value = "ViTri";
            ws.Cells[1, 5].Value = "TrangThai";

            // Tô màu header
            using (var range = ws.Cells[1, 1, 1, 5])
            {
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                range.Style.Font.Bold = true;
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }

            // ===== WRITE DATA =====
            int row = 2;
            foreach (var x in list)
            {
                ws.Cells[row, 1].Value = x.MaTaiSan;
                ws.Cells[row, 2].Value = x.TenTaiSan;
                ws.Cells[row, 3].Value = x.TenKhachHang;
                ws.Cells[row, 4].Value = x.ViTri;
                ws.Cells[row, 5].Value = x.TrangThai;
                row++;
            }

            // ===== AUTO FIT WIDTH — KHÔNG MẤT CHỮ =====
            ws.Cells[ws.Dimension.Address].AutoFitColumns();

            // ===== BORDER TOÀN BỘ BẢNG =====
            using (var rng = ws.Cells[1, 1, row - 1, 5])
            {
                rng.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                rng.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            }

            // Wrap text cho những ô dài
            ws.Cells[ws.Dimension.Address].Style.WrapText = true;

            // Xuất file
            var file = package.GetAsByteArray();

            return File(file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "TaiSan.xlsx");
        }
        



    }
}
