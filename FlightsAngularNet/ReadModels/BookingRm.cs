namespace FlightsAngularNet.ReadModels
{
    public record BookingRm
    (
        Guid FlighItd,
        string Airling,
        string Price,
        TimePlaceRm Arrival,
        TimePlaceRm Departure,
        int NumberOfBookedSeats,
        string PassengerEmail
        );
}
