using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace LeapEventTech.Models
{
    public class Ticket
    {
        public virtual string Id { get; set; }          
        public virtual string EventId { get; set; }
        public virtual string UserId { get; set; }
        public virtual decimal Price { get; set; }    
        public virtual DateTime PurchaseDate { get; set; }
    }
}
