namespace MinAppApi.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
