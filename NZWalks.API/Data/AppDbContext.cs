using Microsoft.EntityFrameworkCore;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        {

        }

        public DbSet<Difficulty>Difficulties { get; set; }  
        public DbSet<Region>Regions { get; set; }   
        public DbSet<Walk>Walks { get; set; }





    }
}
