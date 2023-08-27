namespace FlightsAngularNet.ReadModels
{
    public record BookingRm
    (
        Guid FlighItd,
        string Airline,
        string Price,
        TimePlaceRm Arrival,
        TimePlaceRm Departure,
        int NumberOfBookedSeats,
        string PassengerEmail
        );
}
