using LeapEventTech.Models;
using NHibernate;
using NHibernate.Linq;
using ISession = NHibernate.ISession;

namespace LeapEventTech.Infrastructure.Data;

public sealed class TicketRepository : ITicketRepository
{
    private readonly ISession _session;
    public TicketRepository(ISession session) => _session = session;

    public Task<IReadOnlyList<Ticket>> GetByEventAsync(string eventId, CancellationToken ct = default)
    {
        return _session.Query<Ticket>()
            .Where(t => t.EventId == eventId)
            .OrderBy(t => t.PurchaseDate)
            .ToListAsync(ct)
            .ContinueWith(t => (IReadOnlyList<Ticket>)t.Result, ct);
    }

    public async Task<IReadOnlyList<TopEventSales>> GetTopByCountAsync(int topN, CancellationToken ct = default)
    {
        // 1) Aggregate on TicketSales only
        var agg = await _session.Query<Ticket>()
            .GroupBy(t => t.EventId)
            .Select(g => new
            {
                EventId = g.Key,
                TicketsSold = g.Count(),
                TotalCents = g.Sum(x => (long)x.Price)
            })
            .OrderByDescending(x => x.TicketsSold)
            .ThenBy(x => x.EventId)
            .Take(topN)
            .ToListAsync(ct);

        // 2) Fetch names for those ids
        var ids = agg.Select(a => a.EventId).ToArray();
        var names = await _session.Query<Event>()
            .Where(e => ids.Contains(e.Id))
            .Select(e => new { e.Id, e.Name })
            .ToListAsync(ct);

        var nameMap = names.ToDictionary(x => x.Id, x => x.Name);

        // 3) Compose result in memory
        return agg.Select(a => new TopEventSales
        {
            EventId = a.EventId,
            EventName = nameMap.TryGetValue(a.EventId, out var n) ? n : string.Empty,
            TicketsSold = a.TicketsSold,
            TotalCents = a.TotalCents
        }).ToList();
    }

    public async Task<IReadOnlyList<TopEventSales>> GetTopByAmountAsync(int topN, CancellationToken ct = default)
    {
        var agg = await _session.Query<Ticket>()
            .GroupBy(t => t.EventId)
            .Select(g => new
            {
                EventId = g.Key,
                TicketsSold = g.Count(),
                TotalCents = g.Sum(x => (long)x.Price)
            })
            .OrderByDescending(x => x.TotalCents)
            .ThenBy(x => x.EventId)
            .Take(topN)
            .ToListAsync(ct);

        var ids = agg.Select(a => a.EventId).ToArray();
        var names = await _session.Query<Event>()
            .Where(e => ids.Contains(e.Id))
            .Select(e => new { e.Id, e.Name })
            .ToListAsync(ct);

        var nameMap = names.ToDictionary(x => x.Id, x => x.Name);

        return agg.Select(a => new TopEventSales
        {
            EventId = a.EventId,
            EventName = nameMap.TryGetValue(a.EventId, out var n) ? n : string.Empty,
            TicketsSold = a.TicketsSold,
            TotalCents = a.TotalCents
        }).ToList();
    }
}
