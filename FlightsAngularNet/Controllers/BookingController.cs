using FlightsAngularNet.Data;
using FlightsAngularNet.Domain.Errors;
using FlightsAngularNet.DTOS;
using FlightsAngularNet.ReadModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightsAngularNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly Entities _entities;
        public BookingController(Entities entities)
        {
            _entities = entities;

        }

        [HttpGet("{email}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<BookingRm>), 200)]
        public ActionResult<IEnumerable<BookingRm>> List(string email)
        {
            var bookings = _entities.Flights.ToArray()
                .SelectMany(f => f.Bookings
                .Where(b => b.PassengerEmail == email)
                .Select(b => new BookingRm(
                    f.Id,
                    f.Airline,
                    f.Price.ToString(),
                    new TimePlaceRm(f.Arrival.Place, f.Arrival.Time),
                    new TimePlaceRm(f.Departure.Place, f.Departure.Time),
                    b.NumberOfSeats,
                    email
                    )));
            return Ok(bookings);

        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]

        public IActionResult Cancel(BookDto dto)
        {
            var flight = _entities.Flights.Find(dto.FlightId);
            var error = flight?.CancelBooking(dto.PassengerEmail, dto.NumberOfSeats);
            if(error == null)
            {
                _entities.SaveChanges();
                return NoContent();
            }
            if(error is NotFoundError)
            {
                return NotFound();
            }
            throw new Exception($"The error of type : {error.GetType().Name}" +
                $" occured while canceling the booking made by {dto.PassengerEmail}");
        }
        


    }
}
