using Microsoft.AspNetCore.Mvc;
using BaiMoiiii.BUS;
using BaiMoiiii.MODEL;

namespace BaiMoiiii.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinhKienController : ControllerBase
    {
        private readonly LinhKienBUS _bus;

        public LinhKienController(LinhKienBUS bus)
        {
            _bus = bus;
        }


    }
}
