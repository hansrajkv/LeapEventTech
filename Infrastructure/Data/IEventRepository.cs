using LeapEventTech.Models;

namespace LeapEventTech.Infrastructure.Data;

public interface IEventRepository
{
    Task<IReadOnlyList<Event>> GetUpcomingAsync(int days, CancellationToken ct = default);
}
