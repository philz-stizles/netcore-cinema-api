using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Dtos
{
    public class MovieDto
    {
        [Required(ErrorMessage = "Movie title is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }

        public TimeSpan? Duration { get; set; }

        public double TicketPrice { get; set; }

        public double Rating { get; set; }

        public string Genre { get; set; }

        public string TailorUrl { get; set; }

        public DateTime? PlayingAt { get; set; }
        
        public IFormFile image { get; set; }
    }
}