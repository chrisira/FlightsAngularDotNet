using FlightsAngularNet.Data;
using FlightsAngularNet.Domain.Entities;
using FlightsAngularNet.Domain.Errors;
using FlightsAngularNet.DTOS;
using FlightsAngularNet.ReadModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace FlightsAngularNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightsController : ControllerBase
    {

        private readonly ILogger<FlightsController> _logger;
        private readonly Entities _entities;



        public FlightsController(ILogger<FlightsController> logger,Entities entities)
        {
            _logger = logger;
            _entities = entities;
        }


        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(IEnumerable<FlightsRm>), 200)]
        public IEnumerable<FlightsRm> Search([FromQuery]  FlightSearchParameters @params)
        {
            _logger.LogInformation("searching a flight for the destination {Destination} ", @params.Destination);

            IQueryable<Flights> flights = _entities.Flights;

            if(!string.IsNullOrWhiteSpace(@params.Destination))
            {
                flights = flights.Where(f => f.Arrival.Place.Contains(@params.Destination));
            }
            if (!string.IsNullOrWhiteSpace(@params.From))
            {
                flights = flights.Where(f => f.Departure.Place.Contains(@params.From));
            }
            if (@params.FromDate !=null)
            {
                flights = flights.Where(f => f.Departure.Time >= @params.FromDate.Value.Date);
            }
            if (@params.ToDate != null)
            {
                flights = flights.Where(f => f.Departure.Time >= @params.ToDate.Value.Date.AddDays(1).AddTicks(-1));
            }
            if (@params.NumberOfPassengers !=0 && @params.NumberOfPassengers !=null)
            {
                flights = flights.Where(f => f.RemainingNumberOfSeats >= @params.NumberOfPassengers;
            }
            else
            {
                flights = flights.Where(f=> f.RemainingNumberOfSeats >= 1);
            }



            var FlightRmList = flights

                .Select(flight => new FlightsRm(
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
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == id);

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
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == dto.FlightId);

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
            try
            {
                _entities.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                return Conflict(new { message = "An error occured while trying to book the seat, try again later" });

            }
            
            return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
        }





    }
}