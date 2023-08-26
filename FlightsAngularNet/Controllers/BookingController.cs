using FlightsAngularNet.Data;
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


        }


    }
}
