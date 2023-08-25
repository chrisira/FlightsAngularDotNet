using FlightsAngularNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace FlightsAngularNet.Data
{
    public class Entities:DbContext
    {
        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Flights> Flights => Set<Flights>();
        public Entities(DbContextOptions<Entities> options):base(options)
        {
                
        }
      
            
    }
}
