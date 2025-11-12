using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhatKyHeThongController : ControllerBase
    {
        private readonly NhatKyHeThongBUS _bus;

        public NhatKyHeThongController(NhatKyHeThongBUS bus)
        {
            _bus = bus;
        }


    }
}
