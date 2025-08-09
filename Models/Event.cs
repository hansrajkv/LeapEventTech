namespace LeapEventTech.Models
{
    public class Event
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime StartsOn { get; set; }
        public virtual DateTime EndsOn { get; set; }
        public virtual string Location { get; set; }

    }
}
