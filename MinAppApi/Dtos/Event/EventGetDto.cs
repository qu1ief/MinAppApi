using MinAppApi.Dtos.Organizer;

namespace MinAppApi.Dtos.Event
{
    public class EventGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string BannerImageUrl { get; set; }

        public OrganizerGetDto Organizer { get; set; }
    }


}
