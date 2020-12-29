using Cinema.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option): base(option)
        {
        }
    }
}