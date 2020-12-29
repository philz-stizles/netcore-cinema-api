using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.API.Data;
using Cinema.API.Dtos;
using Cinema.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly ILogger<ReservationsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReservationsController(ILogger<ReservationsController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]ReservationDto dto)
        {
            await _context.Reservations.AddAsync(_mapper.Map<Reservation>(dto));
            await _context.SaveChangesAsync();

            return Ok(new { Status = true, Message = "Created successfully"});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm]ReservationDto dto)
        {
            try
            {
                var reservation = await _context.Reservations.FindAsync(id);
                if(reservation == null) throw new Exception("Reservation does not exist");

                await _context.SaveChangesAsync();

                return Ok(new { Status = true, Message = "Updated successfully"});
            }
            catch(Exception ex)
            {
                return BadRequest(new { Status = false, Message = ex.Message});
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if(reservation == null) throw new Exception("Reservation does not exist");
            
            return Ok(new { Status = true, Message = "Retrieved successfully", Data = reservation});
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reservations = await _context.Reservations.ToListAsync();
            return Ok(new { 
                Status = true, 
                Message = (reservations.Count() <= 0) ? "No reservations" : "Retrieved successfully", 
                Data = reservations});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if(reservation == null) throw new Exception("Reservation does not exist");
            
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return Ok(new { Status = true, Message = "Deleted successfully", Data = reservation});
        }
    }
}
