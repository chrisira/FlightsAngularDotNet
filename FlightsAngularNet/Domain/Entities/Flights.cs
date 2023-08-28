using FlightsAngularNet.Domain.Errors;

namespace FlightsAngularNet.Domain.Entities
{
    public class Flights
    {
        
        public Guid Id { get; set; }
        public String Airline { get; set; } 
        public String Price { get; set; }
        public TimePlace Departure { get; set; }
        public TimePlace Arrival { get; set; }   
        public int RemainingNumberOfSeats { get; set; }

        public IList<Bookings> Bookings = new List<Bookings>();

        // constructor with no parameters
        public Flights()
        {
            
        }

        // constructor with parameters
        public Flights(
        Guid id,
        String airline,
        String price,
        TimePlace departure,
        TimePlace arrival,
        int remainingNumberOfSeats

        )
        {
            Id = id;    
            Airline = airline;  
            Price = price;  
            Departure = departure;  
            Arrival = arrival;  
            RemainingNumberOfSeats = remainingNumberOfSeats;
            

            
        }
        
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

        public object? CancelBooking(string passengerEmail,byte numberOfSeats) { 
            var booking = Bookings.FirstOrDefault(b =>  numberOfSeats == b.NumberOfSeats
            && passengerEmail.ToLower()==b.PassengerEmail.ToLower());
            if(booking == null)
            {
                return new NotFoundError();
            }
            Bookings.Remove(booking);
            RemainingNumberOfSeats += booking.NumberOfSeats;
            return null;
        
        }
    }
   
}
