using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinAppApi.Data;
using MinAppApi.Dtos.Ticket;
using MinAppApi.Entities;

namespace MinAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController(MiniAppApiDbContext dbContext, IMapper mapper) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await dbContext.Tickets.ToListAsync();
            var dtos = mapper.Map<List<TicketGetDto>>(tickets);
            return Ok(dtos);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TicketCreateDto dto)
        {
            if(dto is null)
            {
                return BadRequest();
            }

            var currentevent = await dbContext.Events.FirstOrDefaultAsync(x=>x.Id ==dto.EventId);

            if(currentevent is null)
            {
                return BadRequest();
            }
            var ticket = mapper.Map<Ticket>(dto);
            dbContext.Tickets.Add(ticket);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await dbContext.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();
            var dto = mapper.Map<TicketGetDto>(ticket);
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] TicketUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var ticket = await dbContext.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            mapper.Map(dto, ticket);

            await dbContext.SaveChangesAsync();
            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await dbContext.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            dbContext.Tickets.Remove(ticket);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpGet("/api/events/{eventId}/tickets")]
        public async Task<IActionResult> GetTicketsByEvent(int eventId)
        {

            var currentEvent = await dbContext.Events.FindAsync(eventId);

            if(currentEvent is  null)
            {
                return NotFound();
            }

            var tickets = await dbContext.Tickets
                .Where(t => t.EventId == eventId)
                .ToListAsync();

            var dtos = mapper.Map<List<TicketGetDto>>(tickets);
            return Ok(dtos);
        }

    }
}
