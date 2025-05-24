using System.ComponentModel.DataAnnotations;

namespace MinAppApi.Dtos.Event
{
    public class EventUpdateDto
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, MaxLength(200)]
        public string Location { get; set; }

        [Required]
        public int OrganizerId { get; set; }
        public IFormFile Image { get; set; }
    }

}
