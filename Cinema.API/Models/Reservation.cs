using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.API.Models
{
    [Table("Reservations")]
    public class Reservation: BaseModel
    {
        public int NoOfTickets { get; set; }

        [Phone]
        public string Phone { get; set; }

        public double Price { get; set; }
        
        public DateTime ReservationTime { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}