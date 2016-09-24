using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Entities
{
    public class OdeToFoodDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
