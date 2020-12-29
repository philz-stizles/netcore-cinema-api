using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.API.Models
{
    [Table("Movies")]
    public class Movie: BaseModel
    {
        [Required(ErrorMessage = "Movie title is required")]
        public string Title { get; set; }

        public string Description { get; set; }
        public string Language { get; set; }

        public TimeSpan Duration { get; set; }

        public double TicketPrice { get; set; }

        public double Rating { get; set; }

        public string Genre { get; set; }

        public string TailorUrl { get; set; }
        
        public string imageUrl { get; set; }

        public DateTime PlayingAt { get; set; }

        [NotMapped]
        public string PlayingDate { 
            get
            {
                return PlayingAt.ToString("yyyy-MM-dd");
            }
        }

        [NotMapped]
        public string PlayingTime { 
            get
            {
                return PlayingAt.ToString("HH:mm:ss");
            }
        }
    }
}