using FirstApiCreated.Models;
using Microsoft.AspNetCore.Mvc;
using FirstApiCreated.Services ;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Text.Json;

namespace FirstApiCreated.Controllers
{
    
    [ApiController]
    [Route("api/[Controller]")]
    public class citiesController : ControllerBase

    {
        int maximumpagesize = 20;
        //let 's get rid from the static data 
        // inject the Automapper Profile service 
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        public citiesController(ICityInfoRepository cityinforepository,IMapper mapper)
        {
            _cityInfoRepository = cityinforepository ;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)) ;   
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDTO>>> Getcities(String? name , String? searchQuery , int pagesize=10 , int pagenumber=1 )
        {
            if (pagesize > maximumpagesize)
            {
                pagesize = maximumpagesize ;
            }
            var (cities,metadata) = await _cityInfoRepository.FilteringCitiesAsync(name,searchQuery,pagesize,pagenumber) ;
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
           //  IList<CityWithoutPointOfInterestDTO> result = new List<CityWithoutPointOfInterestDTO>();
            //look how easy is Mapping !! 

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDTO>>(cities));
        }
        [HttpGet("{id}")]
        // Bool or Boolean !! Bool never use it !! melaa 3fattt 5éh
        // we will have to return an IAction result , rather than returning type cityDto which is not totally correct ! 
        public async Task<IActionResult>  Getcity (int id , Boolean isPointofinterestincluded=false  ) 
        {
           var result = await _cityInfoRepository.GetCityByIdAsync(id , isPointofinterestincluded);
            if ( result == null) { return NotFound(); }
            if (isPointofinterestincluded==false)
            {
                return Ok(_mapper.Map<CityWithoutPointOfInterestDTO>(result));  
            }
            return Ok(_mapper.Map<cityDto>(result));
       }

    }
}
