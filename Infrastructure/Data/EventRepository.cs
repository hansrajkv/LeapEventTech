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
        try
        {
            var today = DateTime.UtcNow; //get today's date
            var end = today.AddDays(days); //get the date after passed days

            //Querying through database using Nhibernate session to get list of events by days
            return await _session.Query<Event>()
                .Where(e => e.StartsOn >= today && e.StartsOn < end)
                .OrderBy(e => e.StartsOn)
                .ToListAsync(ct);
        }
        catch (Exception ex)
        {
            throw; 
        }

    }
}
