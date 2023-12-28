using Microsoft.AspNetCore.Mvc;
using WebProje2023.Entity;

namespace FlightReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class flightApiController : ControllerBase
    {
        private readonly DatabaseContext _databaseContex;
        public flightApiController(DatabaseContext databaseContex)
        {
            _databaseContex = databaseContex;
        }

        [HttpGet]
        public IEnumerable<KullaniciDB> Get()
        {
            return _databaseContex.Kullanici.ToList();
        }
    }
}
