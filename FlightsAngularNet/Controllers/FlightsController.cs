using FlightsAngularNet.Domain.Entities;
using FlightsAngularNet.DTOS;
using FlightsAngularNet.ReadModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FlightsAngularNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightsController : ControllerBase
    {

        private readonly ILogger<FlightsController> _logger;
        static Random random = new Random();

        private static Flights[] flights = new Flights[]
            {
                new (Guid.NewGuid(),
                "Rwandair",
                random.Next(90, 5000).ToString(),
                new TimePlace("Los Angeles", DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlace("Istanbul", DateTime.Now.AddHours(random.Next(4, 10))),
                    2),
        new (Guid.NewGuid(),
                "Deutsche BA",
                random.Next(90, 5000).ToString(),
                new TimePlace("Munchen", DateTime.Now.AddHours(random.Next(1, 10))),
                new TimePlace("Schiphol", DateTime.Now.AddHours(random.Next(4, 15))),
                random.Next(1, 853)),
        new (Guid.NewGuid(),
                "British Airways",
                random.Next(90, 5000).ToString(),
                new TimePlace("London, England", DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlace("Vizzola-Ticino", DateTime.Now.AddHours(random.Next(4, 18))),
                    random.Next(1, 853)),
        new (Guid.NewGuid(),
                "Basiq Air",
                random.Next(90, 5000).ToString(),
                new TimePlace("Amsterdam", DateTime.Now.AddHours(random.Next(1, 21))),
                new TimePlace("Glasgow, Scotland", DateTime.Now.AddHours(random.Next(4, 21))),
                    random.Next(1, 853)),
        new (Guid.NewGuid(),
                "BB Heliag",
                random.Next(90, 5000).ToString(),
                new TimePlace("Zurich", DateTime.Now.AddHours(random.Next(1, 23))),
                new TimePlace("Baku", DateTime.Now.AddHours(random.Next(4, 25))),
                    random.Next(1, 853)),
        new (Guid.NewGuid(),
                "Adria Airways",
                random.Next(90, 5000).ToString(),
                new TimePlace("Ljubljana", DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlace("Warsaw", DateTime.Now.AddHours(random.Next(4, 19))),
                    random.Next(1, 853)),
        new (Guid.NewGuid(),
                "ABA Air",
                random.Next(90, 5000).ToString(),
                new TimePlace("Praha Ruzyne", DateTime.Now.AddHours(random.Next(1, 55))),
                new TimePlace("Paris", DateTime.Now.AddHours(random.Next(4, 58))),
                    random.Next(1, 853)),
        new (Guid.NewGuid(),
                "AB Corporate Aviation",
                random.Next(90, 5000).ToString(),
                new TimePlace("Le Bourget", DateTime.Now.AddHours(random.Next(1, 58))),
                new TimePlace("Zagreb", DateTime.Now.AddHours(random.Next(4, 60))),
                    random.Next(1, 853))

            };

        

        public FlightsController(ILogger<FlightsController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(IEnumerable<FlightsRm>), 200)]
        public IEnumerable<FlightsRm> Search()
        {
            var FlightRmList = flights.Select(flight => new FlightsRm(
                flight.Id,
                flight.Price,
                flight.Airline,
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                flight.RemainingNumberOfSeats
                ));
            return FlightRmList;
        }
     
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(FlightsRm), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id")]
        public ActionResult<FlightsRm> Find(Guid id)
        {
            var flight = flights.SingleOrDefault(f => f.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            var readModel = new FlightsRm(
                flight.Id,
                flight.Price,
                flight.Airline,
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                flight.RemainingNumberOfSeats
                ) ;
            
            return Ok(flight);
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public IActionResult Book(BookDto dto)
        {
            System.Diagnostics.Debug.WriteLine($"booking  new flight {dto.FlightId}");
            var flight = flights.SingleOrDefault(f => f.Id == dto.FlightId);

            if (flight == null)
            {
                return NotFound();
            }

            //added domain validation rule to avoid overbooking
            if(flight.RemainingNumberOfSeats < dto.NumberOfSeats)
            {
                return Conflict(new {message = "The number of requested of seats exceeds the remaining seats"});  
            }
            flight.Bookings.Add(
                new Bookings(
                    dto.FlightId,
                    dto.PassengerEmail,
                    dto.NumberOfSeats
                    ));
            flight.RemainingNumberOfSeats -= dto.NumberOfSeats;
            return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
        }





    }
}