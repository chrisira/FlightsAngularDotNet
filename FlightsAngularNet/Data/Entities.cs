using FlightsAngularNet.Domain.Entities;
using System;

namespace FlightsAngularNet.Data
{
    public class Entities
    {
         public IList<Passenger> Passengers = new List<Passenger>();
         public List<Flights> Flights = new List<Flights>();
            
    }
}
