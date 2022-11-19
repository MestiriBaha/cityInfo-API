using Microsoft.EntityFrameworkCore;
using FirstApiCreated.Entities;
namespace FirstApiCreated.Database
{
    public class CityInfoContext : DbContext
    {
        
        // we won't be using this method we will invoke the constructor 
        //public Override OnConfiguring(DbContext CityInfoContext)
        //{

        //}
        //null forgiving operator 
        public DbSet<City> Cities { get; set; } = null! ;
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null! ;
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {

        }
        //seeding the database using the OnModelCreating method 
        protected override void OnModelCreating ( ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<City>().HasData(
                new City("Mahdia")
                {
                    CityId = 1,
                    Description = "The City where Bouha came from"
                },
                new City("Tozeur")
                {
                    CityId = 2,
                    Description = "The City where Boul3ez came from "
                },
                new City("Bizete")
                {
                    CityId = 3,
                    Description = "The city where the Animal Harry came from"
                });
                modelBuilder.Entity<PointOfInterest>().HasData(
                    new PointOfInterest("The beach")
                    {
                        Id = 1 ,
                        CityId = 1,
                        Description = "Blue lagoon beach"
                    },
                new PointOfInterest("Sahara")
                {
                    Id=2 ,
                    CityId = 2,
                    Description = "The most Magical sahara in Africa "
                },
                new PointOfInterest("Ras jabl")
                {
                    Id=3,
                    CityId = 3,
                    Description = "wonderful place "
                });
            base.OnModelCreating (modelBuilder);
        }

    }
}
