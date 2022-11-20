using FirstApiCreated.Models;
using Microsoft.AspNetCore.Mvc;
using FirstApiCreated.Services ;
using Microsoft.EntityFrameworkCore;

namespace FirstApiCreated.Controllers
{
    
    [ApiController]
    [Route("api/[Controller]")]
    public class citiesController : ControllerBase

    {
        //let 's get rid from the static data 
        private readonly ICityInfoRepository _cityInfoRepository; 
        public citiesController(ICityInfoRepository cityinforepository)
        {
            _cityInfoRepository = cityinforepository ;
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDTO>>> Getcities()
        {
            var cities = await _cityInfoRepository.GetCitiesAsync();
            IList<CityWithoutPointOfInterestDTO> result = new List<CityWithoutPointOfInterestDTO>();
            foreach ( var city in cities)
            {
                result.Add(
                    new CityWithoutPointOfInterestDTO
                    {
                        Name = city.Name,
                        Id = city.CityId ,
                        Description = city.Description 
                    }) ; 
            }
            return Ok(result);
        }
        [HttpGet("{id}")]

        public ActionResult<cityDto> Getcity (int id) {
           //var result = _cityDataStore.cities.FirstOrDefault( ptr => ptr.Id==id);
           // if ( result==null) { return NotFound(); } ; 
            return Ok();  
        }

    }
}
