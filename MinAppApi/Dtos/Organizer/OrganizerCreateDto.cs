using System.ComponentModel.DataAnnotations;

namespace MinAppApi.Dtos.Organizer
{
    public class OrganizerCreateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }
        public IFormFile LogoUrl { get; set; }
    }


}
