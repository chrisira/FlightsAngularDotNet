﻿namespace FlightsAngularNet.Domain.Entities
{
    public record Flights(
        Guid Id,
        String Airline,
        String Price,
        TimePlace Departure,
        TimePlace Arrival,
        int RemainingNumberOfSeats
        
        );
   
}
