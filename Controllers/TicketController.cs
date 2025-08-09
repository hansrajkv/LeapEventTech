using LeapEventTech.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public sealed class TicketsController : ControllerBase
{
    private readonly ITicketService _svc;
    public TicketsController(ITicketService svc) => _svc = svc;

    // Tickets for a specific event
    [HttpGet("events/{eventId}/tickets")]
    public async Task<IActionResult> GetForEvent(string eventId, CancellationToken ct)
        => Ok(await _svc.GetTicketsForEventAsync(eventId, ct));

    // Top 5 Sales (count or amount)
    [HttpGet("tickets/top5")]
    public async Task<IActionResult> Top5([FromQuery] string by = "count", CancellationToken ct = default)
    {
        if (by.Equals("count", StringComparison.OrdinalIgnoreCase))
            return Ok(await _svc.GetTop5ByCountAsync(ct));
        if (by.Equals("amount", StringComparison.OrdinalIgnoreCase))
            return Ok(await _svc.GetTop5ByAmountAsync(ct));

        return BadRequest("Query param 'by' must be 'count' or 'amount'.");
    }
}
