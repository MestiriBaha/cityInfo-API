using FirstApiCreated.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstApiCreated.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class citiesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<cityDto> Getcities()
        {
            return Ok(citiesDataStore.current.cities);

        }
        [HttpGet("{id}")]

        public ActionResult<cityDto> Getcity (int id) {
           var result = citiesDataStore.current.cities.FirstOrDefault( ptr => ptr.Id==id);
            if ( result==null) { return NotFound(); } ; 
            return Ok(result);  
        }

    }
}
