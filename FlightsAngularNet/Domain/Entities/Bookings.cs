using System.ComponentModel.DataAnnotations;

namespace FlightsAngularNet.Domain.Entities
{
    public record Bookings(
        
        
        string PassengerEmail,
        byte NumberOfSeats);
    
}
