using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightsAngularNet.DTOS;
using FlightsAngularNet.ReadModels;

namespace FlightsAngularNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        static private IList<NewPassengerDto> Passengers = new List<NewPassengerDto>();
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto)
        {
            Passengers.Add(dto);
            System.Diagnostics.Debug.WriteLine(Passengers.Count);
            return CreatedAtAction(nameof(Find),new { email = dto.Email });
        }

        // adding a find action result to search for a passenger using email
        [HttpGet("{email}")]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = Passengers.FirstOrDefault(p => p.Email == email);
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
