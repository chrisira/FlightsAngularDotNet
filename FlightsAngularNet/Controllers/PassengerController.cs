﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightsAngularNet.DTOS;
using FlightsAngularNet.ReadModels;
using FlightsAngularNet.Domain.Entities;
using FlightsAngularNet.Data;

namespace FlightsAngularNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private  readonly Entities _entities;
        public PassengerController(Entities entities)
        {
            _entities = entities;
            
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto)
        {
            _entities.Passengers.Add(
                new Passenger(
                    dto.Email,
                    dto.FirstName,
                    dto.LastName,
                    dto.Gender));
            _entities.SaveChanges();
            
            return CreatedAtAction(nameof(Find),new { email = dto.Email });
        }

        // adding a find action result to search for a passenger using email
        [HttpGet("{email}")]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = _entities.Passengers.FirstOrDefault(p => p.Email == email);
            if (passenger == null)
            {
                return NotFound();
                   
            }
            var rm = new PassengerRm(
                passenger.Email,
                passenger.FirstName,
                passenger.LastName,
                passenger.Gender
                
                );
            return Ok(rm);

        }

    }
}
