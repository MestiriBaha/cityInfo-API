using Microsoft.EntityFrameworkCore;
using FirstApiCreated.Entities;
namespace FirstApiCreated.Database
{
    public class CityInfoContext : DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)   
        {

        }
        // we won't be using this method we will invoke the constructor 
        //public Override OnConfiguring(DbContext CityInfoContext)
        //{

        //}
        //null forgiving operator 
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!; 

    }
}
