using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.API.Data;
using Cinema.API.Dtos;
using Cinema.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public MoviesController(ILogger<MoviesController> logger, ApplicationDbContext context, IMapper mapper,
            IWebHostEnvironment env)
        {
            _env = env;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] MovieDto dto)
        {
            var newMovie = _mapper.Map<Movie>(dto);

            if (dto.image != null)
            {
                var imagePath = Path.Combine("wwwroot", "images", $"{Guid.NewGuid()}{Path.GetExtension(dto.image.FileName)}");
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await dto.image.CopyToAsync(fileStream);
                }
                newMovie.imageUrl = imagePath.Remove(0, 7);
            }

            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            return Ok(new { Status = true, Message = "Created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] MovieDto dto)
        {
            try
            {
                var movie = await _context.Movies.FindAsync(id);
                if (movie == null) return NotFound(new { Status = false, Message = "Movie does not exist" });

                if (dto.image != null)
                {
                    if (!string.IsNullOrEmpty(movie.imageUrl))
                    {
                        var possibleFileLocation = _env.WebRootPath + movie.imageUrl;
                        if (System.IO.File.Exists(possibleFileLocation))
                        {
                            System.IO.File.Delete(possibleFileLocation);
                        }
                    }
                    var imagePath = Path.Combine("wwwroot", "images", $"{Guid.NewGuid()}{Path.GetExtension(dto.image.FileName)}");
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        await dto.image.CopyToAsync(fileStream);
                    }
                    movie.imageUrl = imagePath.Remove(0, 7);
                }

                movie.Title = dto.Title;
                movie.Description = dto.Description;
                movie.Language = dto.Language;
                movie.TicketPrice = dto.TicketPrice;
                movie.Rating = dto.Rating;
                movie.Genre = dto.Genre;
                movie.TailorUrl = dto.TailorUrl;
                await _context.SaveChangesAsync();

                return Ok(new { Status = true, Message = "Updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var movie = await _context.Movies.FindAsync(id);
                if (movie == null) throw new Exception("Movie does not exist");

                return Ok(new { Status = true, Message = "Retrieved successfully", Data = movie });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var movies = await _context.Movies.ToListAsync();
            return Ok(new
            {
                Status = true,
                Message = (movies.Count() <= 0) ? "No movies" : "Retrieved successfully",
                Data = movies
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) throw new Exception("Movie does not exist");

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok(new { Status = true, Message = "Deleted successfully", Data = movie });
        }
    }
}
