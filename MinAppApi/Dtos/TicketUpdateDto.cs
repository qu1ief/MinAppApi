namespace MinAppApi.Dtos
{
    public class TicketUpdateDto : TicketCreateDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
    }


}
