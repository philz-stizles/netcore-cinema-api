using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.API.Models
{
    [Table("Users")]
    public class User: BaseModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Password { get; set; }
        
        public string Role { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}