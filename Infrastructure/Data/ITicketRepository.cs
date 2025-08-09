using LeapEventTech.Models;

namespace LeapEventTech.Infrastructure.Data;

public interface ITicketRepository
{
    Task<IReadOnlyList<Ticket>> GetByEventAsync(string eventId, CancellationToken ct = default);

    // Top 5 Sales by Count or Amount
    Task<IReadOnlyList<TopEventSales>> GetTopByCountAsync(int topN, CancellationToken ct = default);
    Task<IReadOnlyList<TopEventSales>> GetTopByAmountAsync(int topN, CancellationToken ct = default);
}
