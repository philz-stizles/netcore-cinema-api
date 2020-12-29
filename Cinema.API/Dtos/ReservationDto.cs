using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Dtos
{
    public class ReservationDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Movie title is required")]
        public string Title { get; set; }

        public string Language { get; set; }

        public double Ratings { get; set; }
        
        public IFormFile image { get; set; }
    }
}