using System.ComponentModel.DataAnnotations;

namespace MinAppApi.Dtos.Ticket
{
    public class TicketUpdateDto
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Type { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int QuantityAvailable { get; set; }
        public int EventId { get; set; }
    }


}
