using LeapEventTech.Infrastructure.Data;
using LeapEventTech.Models;

namespace LeapEventTech.Infrastructure.Services;

public sealed class EventService : IEventService
{
    private static readonly HashSet<int> Allowed = new() { 30, 60, 180 };
    private readonly IEventRepository _repo;

    public EventService(IEventRepository repo) => _repo = repo;

    public Task<IReadOnlyList<Event>> GetUpcomingEventsAsync(int days, CancellationToken ct = default)
    {
        if (!Allowed.Contains(days))
            throw new ArgumentOutOfRangeException(nameof(days), "Must be 30, 60, or 180");

        return _repo.GetUpcomingAsync(days, ct);
    }
}
