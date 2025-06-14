using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinAppApi.Data;
using MinAppApi.Dtos.Event;
using MinAppApi.Dtos.Organizer;
using MinAppApi.Entities;

namespace MinAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizerController(MiniAppApiDbContext dbContext, IMapper mapper) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var organizers = await dbContext.Organizers.ToListAsync();
            var result = mapper.Map<List<OrganizerGetDto>>(organizers);
            return Ok(result);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var organizer = await dbContext.Organizers.FindAsync(id);
            if (organizer == null) return NotFound();
            return Ok(mapper.Map<OrganizerGetDto>(organizer));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] OrganizerCreateDto dto)
        {
            var organizer = mapper.Map<Organizer>(dto);

            if (dto.LogoUrl != null && dto.LogoUrl.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/organizers");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid() + Path.GetExtension(dto.LogoUrl.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.LogoUrl.CopyToAsync(stream);

                organizer.LogoUrl = $"/uploads/organizers/{uniqueFileName}";
            }

            dbContext.Organizers.Add(organizer);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = organizer.Id }, organizer);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] OrganizerUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var organizer = await dbContext.Organizers.FindAsync(id);
            if (organizer == null) return NotFound();

            mapper.Map(dto, organizer);

            if (dto.LogoUrl != null && dto.LogoUrl.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/organizers");
                Directory.CreateDirectory(uploadsFolder);

                if (!string.IsNullOrEmpty(organizer.LogoUrl))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", organizer.LogoUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                var uniqueFileName = Guid.NewGuid() + Path.GetExtension(dto.LogoUrl.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.LogoUrl.CopyToAsync(stream);

                organizer.LogoUrl = $"/uploads/organizers/{uniqueFileName}";
            }

            await dbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var organizer = await dbContext.Organizers.FindAsync(id);
            if (organizer == null) return NotFound();

            if (!string.IsNullOrEmpty(organizer.LogoUrl))
            {
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", organizer.LogoUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            dbContext.Organizers.Remove(organizer);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpGet("{organizerId}/events")]
        public async Task<IActionResult> GetEventsByOrganizer(int organizerId)
        {
            var events = await dbContext.Events
                .Where(e => e.OrganizerId == organizerId)
                .ToListAsync();

            if (events.Count == 0)
                return NotFound($"No events found for organizer with ID {organizerId}");

            var eventDtos = mapper.Map<List<EventGetDto>>(events);
            return Ok(eventDtos);
        }


        [HttpPost("{organizerId}/logo")]
        public async Task<IActionResult> UploadLogo(int organizerId, IFormFile logo)
        {
            var organizer = await dbContext.Organizers.FindAsync(organizerId);
            if (organizer == null) return NotFound();

            if (logo == null || logo.Length == 0)
                return BadRequest("Logo file is required");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/organizers");
            Directory.CreateDirectory(uploadsFolder);

            if (!string.IsNullOrEmpty(organizer.LogoUrl))
            {
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", organizer.LogoUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(logo.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await logo.CopyToAsync(stream);

            organizer.LogoUrl = $"/uploads/organizers/{uniqueFileName}";
            await dbContext.SaveChangesAsync();

            return Ok(new { logoUrl = organizer.LogoUrl });
        }

    }
}
