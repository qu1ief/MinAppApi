using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinAppApi.Data;
using MinAppApi.Dtos.Event;
using MinAppApi.Dtos.Organizer;
using MinAppApi.Dtos.Ticket;
using MinAppApi.Entities;

namespace MinAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController(MiniAppApiDbContext dbContext ,IMapper mapper ,IWebHostEnvironment env ) : ControllerBase
    {



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await dbContext.Events.Include(e => e.Organizer).ToListAsync();
            var result = mapper.Map<List<EventGetDto>>(events);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var evt = await dbContext.Events.Include(e => e.Organizer).FirstOrDefaultAsync(e => e.Id == id);
            if (evt == null) return NotFound();

            var dto = mapper.Map<EventGetDto>(evt);
            return Ok(dto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EventCreateDto dto)
        {
            var evt = mapper.Map<Event>(dto);

            if (dto.image != null && dto.image.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/events");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.image.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.image.CopyToAsync(stream);
                }

                evt.BannerImageUrl = $"/uploads/events/{uniqueFileName}";
            }

            dbContext.Events.Add(evt);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = evt.Id }, evt);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] EventUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var evt = await dbContext.Events.FindAsync(id);
            if (evt == null) return NotFound();

            mapper.Map(dto, evt);

            if (dto.Image != null && dto.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/events");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Optional: köhnə şəkli sil
                if (!string.IsNullOrWhiteSpace(evt.BannerImageUrl))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", evt.BannerImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                evt.BannerImageUrl = $"/uploads/events/{uniqueFileName}";
            }

            await dbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var evt = await dbContext.Events.FindAsync(id);
            if (evt == null) return NotFound();

            dbContext.Events.Remove(evt);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpGet("{eventId}/tickets")]
        public async Task<IActionResult> GetTicketsForEvent(int eventId)
        {
            var tickets = await dbContext.Tickets.Where(t => t.EventId == eventId).ToListAsync();
            return Ok(mapper.Map<List<TicketGetDto>>(tickets));
        }

        [HttpGet("{eventId}/organizer")]
        public async Task<IActionResult> GetOrganizerForEvent(int eventId)
        {
            var evt = await dbContext.Events.Include(e => e.Organizer).FirstOrDefaultAsync(e => e.Id == eventId);
            if (evt == null) return NotFound();

            return Ok(mapper.Map<OrganizerGetDto>(evt.Organizer));
        }

        [HttpPost("{eventId}/tickets")]
        public async Task<IActionResult> CreateTicketForEvent(int eventId, [FromBody] TicketCreateDto dto)
        {
            if (!await dbContext.Events.AnyAsync(e => e.Id == eventId))
                return NotFound("Event not found");

            var ticket = mapper.Map<Ticket>(dto);
            ticket.EventId = eventId;

            dbContext.Tickets.Add(ticket);
            await dbContext.SaveChangesAsync();
            return Ok(mapper.Map<TicketGetDto>(ticket));
        }

        [HttpPost("{eventId}/banner")]
        public async Task<IActionResult> UploadBanner(int eventId, IFormFile file)
        {
            var evt = await dbContext.Events.FindAsync(eventId);
            if (evt == null) return NotFound();

            if (file == null || file.Length == 0) return BadRequest("No file");

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var path = Path.Combine(env.WebRootPath, "uploads", "banners", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            evt.BannerImageUrl = $"/uploads/banners/{fileName}";
            await dbContext.SaveChangesAsync();

            return Ok(new { evt.BannerImageUrl });
        }
    }
}
