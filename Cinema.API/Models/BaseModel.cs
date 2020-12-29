using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}