using LeapEventTech.Models;
using NHibernate;
using NHibernate.Linq;
using ISession = NHibernate.ISession;

namespace LeapEventTech.Infrastructure.Data;

public sealed class EventRepository : IEventRepository
{
    private readonly ISession _session;
    public EventRepository(ISession session) => _session = session;

    public async Task<IReadOnlyList<Event>> GetUpcomingAsync(int days, CancellationToken ct = default)
    {
        var today = DateTime.UtcNow;
        var end = today.AddDays(days);

        return await _session.Query<Event>()
            .Where(e => e.StartsOn >= today && e.StartsOn < end) 
            .OrderBy(e => e.StartsOn)
            .ToListAsync(ct);
    }
}
