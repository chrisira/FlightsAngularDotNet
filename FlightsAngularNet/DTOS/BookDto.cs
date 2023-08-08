namespace FlightsAngularNet.DTOS
{
    public record BookDto(Guid FlightId,
        string PassengerEmail,
        byte NumberOfSeats);
    
}
