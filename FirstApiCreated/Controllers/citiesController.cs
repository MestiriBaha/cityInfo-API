using FirstApiCreated.Models;
using Microsoft.AspNetCore.Mvc;
using FirstApiCreated ; 

namespace FirstApiCreated.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class citiesController : ControllerBase
    {
        private readonly citiesDataStore _cityDataStore; 
        public citiesController(citiesDataStore cityDataStore)
        {
            _cityDataStore = cityDataStore;
        }
    
        [HttpGet]
        public ActionResult<cityDto> Getcities()
        {
            return Ok(_cityDataStore.cities);

        }
        [HttpGet("{id}")]

        public ActionResult<cityDto> Getcity (int id) {
           var result = _cityDataStore.cities.FirstOrDefault( ptr => ptr.Id==id);
            if ( result==null) { return NotFound(); } ; 
            return Ok(result);  
        }

    }
}
