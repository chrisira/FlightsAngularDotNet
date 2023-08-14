using System.ComponentModel.DataAnnotations;

namespace FlightsAngularNet.DTOS
{
    public record BookDto(
        [Required]
        Guid FlightId,
        [EmailAddress]
        [Required]
        [StringLength(100,MinimumLength =3)]
        string PassengerEmail,
        [Required]
        [Range(1,254)]
        byte NumberOfSeats);
    
}
