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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().HasKey(p => p.Email);
            modelBuilder.Entity<Flights>().Property(p=> p.RemainingNumberOfSeats).IsConcurrencyToken(); 
            modelBuilder.Entity<Flights>().OwnsOne(f => f.Departure);
            modelBuilder.Entity<Flights>().OwnsOne(f => f.Arrival);
            modelBuilder.Entity<Flights>().OwnsMany(f => f.Bookings);

        }


    }
}
