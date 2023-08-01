using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) :base(dbContextOptions)
        {
               
        }


        //Create Tables/relation from /Models/Domain in database
        public DbSet<Difficulty> Difficulties { get; set; } 
        public DbSet<Region> Regions { get; set; } 
        public DbSet<Walk> Walks { get; set; } 

    }
}
