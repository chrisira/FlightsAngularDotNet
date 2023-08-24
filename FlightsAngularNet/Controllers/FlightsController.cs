using FlightsAngularNet.Data;
using FlightsAngularNet.Domain.Entities;
using FlightsAngularNet.Domain.Errors;
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
        private static readonly Entities Entities = new Entities();



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
            var FlightRmList = Entities.Flights.Select(flight => new FlightsRm(
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
            var flight = Entities.Flights.SingleOrDefault(f => f.Id == id);

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
            var flight = Entities.Flights.SingleOrDefault(f => f.Id == dto.FlightId);

            if (flight == null)
            {
                return NotFound();
            }
            // calling the makeBooking method from the entity
           var error =  flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);
            if(error is OverBookError)
            {
                return Conflict(new { message = "not enough seats" });

            }
            return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
        }





    }
}