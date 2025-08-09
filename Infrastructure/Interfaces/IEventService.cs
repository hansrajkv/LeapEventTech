using LeapEventTech.Models;

namespace LeapEventTech.Infrastructure.Services;

public interface IEventService
{
    Task<IReadOnlyList<Event>> GetUpcomingEventsAsync(int days, CancellationToken ct = default);
}
