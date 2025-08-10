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
            try
            {
                var events = await _eventService.GetUpcomingEventsAsync(days);
                return Ok(events);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred." });
            }
        }
    }
}
