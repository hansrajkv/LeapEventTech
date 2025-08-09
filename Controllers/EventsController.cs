using LeapEventTech.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeapEventTech.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUpcoming([FromQuery] int days = 30)
        {
            if (days != 30 && days != 60 && days != 180)
            {
                return BadRequest("Days must be 30, 60, or 180");
            }

            var events = await _eventService.GetUpcomingEventsAsync(days);
            return Ok(events);
        }
    }
}
