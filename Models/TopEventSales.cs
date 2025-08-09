namespace LeapEventTech.Models
{
    public sealed class TopEventSales
    {
        public string EventId { get; init; }
        public string EventName { get; init; }
        public int TicketsSold { get; init; }
        public long TotalCents { get; init; }
    }
}
