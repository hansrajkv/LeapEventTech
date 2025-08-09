using LeapEventTech.Infrastructure.Data;
using LeapEventTech.Models;

namespace LeapEventTech.Infrastructure.Services;

public interface ITicketService
{
    Task<IReadOnlyList<Ticket>> GetTicketsForEventAsync(string eventId, CancellationToken ct = default);

    Task<IReadOnlyList<TopEventSales>> GetTop5ByCountAsync(CancellationToken ct = default);
    Task<IReadOnlyList<TopEventSales>> GetTop5ByAmountAsync(CancellationToken ct = default);
}
