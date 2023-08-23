using FlightsAngularNet.Domain.Errors;

namespace FlightsAngularNet.Domain.Entities
{
    public record Flights(
        Guid Id,
        String Airline,
        String Price,
        TimePlace Departure,
        TimePlace Arrival,
        int RemainingNumberOfSeats
        
        )
    {
        public IList<Bookings> Bookings = new List<Bookings>();
        public int RemainingNumberOfSeats { get; set; } = RemainingNumberOfSeats;

        

         public object? MakeBooking(string passengerEmail,byte numberOfSeats)
        {
            var flight = this;
            //added domain validation rule to avoid overbooking
            if (flight.RemainingNumberOfSeats < numberOfSeats)
            {
                return new OverBookError();
                
            }
            flight.Bookings.Add(
                new Bookings(
                    passengerEmail,
                    numberOfSeats
                    ));
            flight.RemainingNumberOfSeats -= numberOfSeats;
            return null;
        }
    }
   
}
