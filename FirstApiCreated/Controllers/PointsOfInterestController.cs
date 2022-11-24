using FirstApiCreated.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using FirstApiCreated.Services;
using AutoMapper;

namespace FirstApiCreated.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        // we will be injecting the ILOGGER service in Our controller ! 
        // injecting the mail service !! 
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailSerive; 
        // no more static data 
       // private readonly citiesDataStore _citiesDataStore;
        private readonly IMapper _mapper;
        private readonly ICityInfoRepository _cityInfoRepository ;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailSerive, IMapper mapper, ICityInfoRepository cityInfoRepository)
        {
            //the dependancy injection pattern ! 
            // let's give more control about our dependancy 
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailSerive = mailSerive ?? throw new ArgumentNullException(nameof(mailSerive));    
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
             _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            // we can request service from the container directly as : 
            //   HttpContext.RequestServices.GetService(ILogger<PointsOfInterestController>); 
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<pointsOfInterestDto>>> getPoints (int cityId)
        {
            if (!await _cityInfoRepository.isCityExist(cityId))
            {
                _logger.LogInformation($"City with {cityId} wasn't found when accessing points of Interest");
                return NotFound();
            }
            var result = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId);
            return Ok(_mapper.Map<IEnumerable<pointsOfInterestDto>>(result)) ;
        }
        [HttpGet("{pointofinterestid}",Name ="get")]
        public async Task<ActionResult<pointsOfInterestDto>> getPointsOfInterestId ( int cityId , int pointofinterestid)
        {
                           
                if (!await _cityInfoRepository.isCityExist(cityId))
                {
                    _logger.LogInformation($"City with {cityId} wasn't found when accessing points of Interest");
                    return NotFound();

                };
                var result = await _cityInfoRepository.GetPointOfInterestAsync(cityId, pointofinterestid);    
            if (result==null) { return NotFound(); }
                return Ok(_mapper.Map<pointsOfInterestDto>(result));

            }
        }
       /* [HttpPost]
        public ActionResult<pointsOfInterestDto> createPointOfInterest(int cityid , pointsOfInterestForCreationDto newpointofinterest)
        {
            var checkcity = _citiesDataStore.cities.FirstOrDefault(t => t.Id == cityid);
            if (checkcity==null) {  return NotFound(); } 
            // searching for the max id in the pointsofinterest !! 
            var nextId =_citiesDataStore.cities.SelectMany(c => c.PointsofInterest).Max(p => p.Id);
            var finalPointOfInterest = new pointsOfInterestDto()
            {
                Id = ++nextId,
                Name = newpointofinterest.name,
                Description = newpointofinterest.description
            };
            checkcity.PointsofInterest.Add(finalPointOfInterest);
            return CreatedAtRoute("get",  new { cityId=cityid , pointofinterestid = finalPointOfInterest.Id }, finalPointOfInterest);
           }
        [HttpPut("{pointOfInterestId}")]
        public ActionResult updatePointOfInterest ( int cityid , int pointOfInterestId , pointsOfInterestForUpdatingDto pointOfInterest)
        {
            var checkcity = _citiesDataStore.cities.FirstOrDefault(t => t.Id == cityid);
            if (checkcity == null) { return NotFound(); }
            //check point of interest Id
            var checkpointofinterest = checkcity.PointsofInterest.FirstOrDefault( c => c.Id==pointOfInterestId);
            if ( checkpointofinterest == null) { return NotFound(); }
            checkpointofinterest.Name = pointOfInterest.name;
            checkpointofinterest.Description = pointOfInterest.description; 
            return NoContent(); 
        }
        [HttpPatch("{pointofinterestid}")]
        public ActionResult partiallyUpdatePointOfInterest ( int cityid , int pointofinterestid , JsonPatchDocument<pointsOfInterestForUpdatingDto> patchDocument)
        {
            var checkcity =_citiesDataStore.cities.FirstOrDefault(t => t.Id == cityid);
            if (checkcity == null) { return NotFound(); }
            var checkpointofinterestforpatch = checkcity.PointsofInterest.FirstOrDefault(c => c.Id == pointofinterestid);
            if (checkpointofinterestforpatch == null) { return NotFound(); }
                var pointofinteresttopatch = new pointsOfInterestForUpdatingDto()
                {
                    name = checkpointofinterestforpatch.Name,
                    description = checkpointofinterestforpatch.Description
                };
                patchDocument.ApplyTo(pointofinteresttopatch,ModelState);

                if (!ModelState.IsValid) { return BadRequest(); }
                if (!TryValidateModel(pointofinteresttopatch)) { return BadRequest(ModelState); };

                checkpointofinterestforpatch.Name = pointofinteresttopatch.name;
                checkpointofinterestforpatch.Description = pointofinteresttopatch.description;
                return NoContent();

            }
        // we will try our mail service here 
        [HttpDelete("{pointofinterestid}")]
        public ActionResult deletePointOfInterest ( int cityid , int pointofinterestid )
        {
            var checkcity = _citiesDataStore.cities.FirstOrDefault(t => t.Id == cityid);
            if (checkcity == null) { return NotFound(); }
            //check point of interest Id
            var checkpointofinterest = checkcity.PointsofInterest.FirstOrDefault(c => c.Id == pointofinterestid);
            if (checkpointofinterest == null) { return NotFound(); }

            checkcity.PointsofInterest.Remove(checkpointofinterest);
            _mailSerive.Send("point of interest deleted ", $"point of interest {checkpointofinterest.Name} was deleted with id {checkpointofinterest.Id}");
            return NoContent(); 

        }
        }*/
}
