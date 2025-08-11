using LeapEventTech.Infrastructure.Data;
using LeapEventTech.Models;

namespace LeapEventTech.Infrastructure.Services;

public sealed class TicketService : ITicketService
{
    private readonly ITicketRepository _repo;
    public TicketService(ITicketRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<Ticket>> GetTicketsForEventAsync(string eventId, CancellationToken ct = default)
    {
        //Throw exception if requried field (EventId) is not passed
        if (string.IsNullOrWhiteSpace(eventId))
            throw new ArgumentException("eventId is required", nameof(eventId));

        try
        {
            //Calling the data access layer to get the events by eventId
            return await _repo.GetByEventAsync(eventId, ct);
        }
        catch(Exception e)
        {
            throw;
        }
    }

    public async Task<IReadOnlyList<TopEventSales>> GetTop5ByCountAsync(CancellationToken ct = default)
    {
        try
        {
            //Calling data access layer to get the top 5 sales of tickets by count
            return await _repo.GetTopByCountAsync(5, ct);
        }
        catch(Exception e)
        {
            throw;
        }
    }

    public async Task<IReadOnlyList<TopEventSales>> GetTop5ByAmountAsync(CancellationToken ct = default)
    {
        try
        {
            //Calling data access layer to get the top 5 sales of tickets by amount
            return await _repo.GetTopByAmountAsync(5, ct);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}
