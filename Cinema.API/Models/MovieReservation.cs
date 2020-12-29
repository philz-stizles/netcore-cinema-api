using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.API.Models
{
    [Table("MovieReservations")]
    public class MovieReservation
    {
        [Key]
        public int ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }

        [Key]
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}