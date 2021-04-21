using Bl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotsController : ControllerBase
    {
        private readonly ISlotService _slotService;

        public TimeSlotsController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSlots()
            => Ok(await _slotService.GetSlots());
    }
}
