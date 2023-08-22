using System.ComponentModel.DataAnnotations;

namespace FlightsAngularNet.Domain.Entities
{
    public record Bookings(
        
        Guid FlightId,
        string PassengerEmail,
        byte NumberOfSeats);
    
}
