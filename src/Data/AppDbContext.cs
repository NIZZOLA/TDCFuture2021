using LocationMinApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationMinApi.Data
{
    public class AppDbContext: DbContext
    {
         public AppDbContext(DbContextOptions<AppDbContext> options)
             : base(options)
         {
         }
        
        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<PlaceModel> Places { get; set; }
    }
}
