using LeapEventTech.Models;

namespace LeapEventTech.Infrastructure.Data;

public interface ITicketRepository
{
    Task<IReadOnlyList<Ticket>> GetByEventAsync(string eventId, CancellationToken ct = default);

    // Projections for Top 5
    Task<IReadOnlyList<TopEventSales>> GetTopByCountAsync(int topN, CancellationToken ct = default);
    Task<IReadOnlyList<TopEventSales>> GetTopByAmountAsync(int topN, CancellationToken ct = default);
}
