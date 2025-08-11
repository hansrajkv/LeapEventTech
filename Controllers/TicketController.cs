using LeapEventTech.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api")]
public sealed class TicketsController : ControllerBase
{
    private readonly ITicketService _svc;
    public TicketsController(ITicketService svc) => _svc = svc;

    // Tickets for a specific event
    [HttpGet("events/{eventId}/tickets")]
    public async Task<IActionResult> GetForEvent(string eventId, CancellationToken ct)
    {
        try
        {
            //calling the service layer to get the events by eventId
            var evnt = await _svc.GetTicketsForEventAsync(eventId);
            if (evnt.Count == 0)
            {
                //throwing exception if eventId is not found
                return NotFound(new { message = $"Event with ID {eventId} not found." });
            }

            return Ok(evnt);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }

    // Top 5 Sales (count or amount)
    [HttpGet("tickets/top5")]
    public async Task<IActionResult> Top5([FromQuery] string by = "count", CancellationToken ct = default)
    {
        try
        {
            //calling service layer to get top 5 sales by count or amount
            if (by.Equals("count", StringComparison.OrdinalIgnoreCase))
                return Ok(await _svc.GetTop5ByCountAsync(ct));
            if (by.Equals("amount", StringComparison.OrdinalIgnoreCase))
                return Ok(await _svc.GetTop5ByAmountAsync(ct));

            //bad request if the param passed is not count or amount
            return BadRequest("Query param 'by' must be 'count' or 'amount'.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }
}
