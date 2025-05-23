namespace MinAppApi.Dtos
{
    public class TicketReadDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }

        public int EventId { get; set; }
    }


}
