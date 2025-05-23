using System.ComponentModel.DataAnnotations;

namespace MinAppApi.Entities
{
    public class Organizer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LogoUrl { get; set; }
        public List<Event> Events { get; set; }
    }
}
