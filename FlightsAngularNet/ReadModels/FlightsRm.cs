namespace FlightsAngularNet.ReadModels
{
    public record FlightsRm(
        Guid Id,
        String Airline,
        String Price,
        TimePlaceRm Departure,
        TimePlaceRm Arrival,
        int RemainingNumberOfSeats
        
        );
   
}
