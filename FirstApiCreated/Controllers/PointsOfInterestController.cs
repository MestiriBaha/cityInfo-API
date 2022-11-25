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
        private readonly ICityInfoRepository _cityInfoRepository;

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
        public async Task<ActionResult<IEnumerable<pointsOfInterestDto>>> getPoints(int cityId)
        {
            if (!await _cityInfoRepository.isCityExist(cityId))
            {
                _logger.LogInformation($"City with {cityId} wasn't found when accessing points of Interest");
                return NotFound();
            }
            var result = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId);
            return Ok(_mapper.Map<IEnumerable<pointsOfInterestDto>>(result));
        }
        [HttpGet("{pointofinterestid}", Name = "get")]
        public async Task<ActionResult<pointsOfInterestDto>> getPointsOfInterestId(int cityId, int pointofinterestid)
        {

            if (!await _cityInfoRepository.isCityExist(cityId))
            {
                _logger.LogInformation($"City with {cityId} wasn't found when accessing points of Interest");
                return NotFound();

            };
            var result = await _cityInfoRepository.GetPointOfInterestAsync(cityId, pointofinterestid);
            if (result == null) { return NotFound(); }
            return Ok(_mapper.Map<pointsOfInterestDto>(result));

        }

        //Creating a Ressource : 
        [HttpPost]
        public async Task<ActionResult<pointsOfInterestDto>> createPointOfInterest(int cityid, pointsOfInterestForCreationDto newpointofinterest)
        {
            if (!await _cityInfoRepository.isCityExist(cityid))
            {
                _logger.LogInformation($"City with {cityid} wasn't found when accessing points of Interest");
                return NotFound();
            }
            // searching for the max id in the pointsofinterest !! this is for static data ! 
            // in our database the id is auto-generated ! 
            var finalpointofinterest = _mapper.Map<Entities.PointOfInterest>(newpointofinterest);
          await _cityInfoRepository.AddPointofinterestforCityAsync(cityid, finalpointofinterest);
          await _cityInfoRepository.Savechangesasync(); 
          var result = _mapper.Map<Models.pointsOfInterestDto>(finalpointofinterest);
            return CreatedAtRoute("get", new { cityId = cityid, pointofinterestid = result.Id }, result );
        }
      [HttpPut("{pointOfInterestId}")]
        public async Task<ActionResult> updatePointOfInterest(int cityid, int pointOfInterestId, pointsOfInterestForUpdatingDto pointOfInterest)
        {
            //check for city existance !! 
            var checkcity = await _cityInfoRepository.isCityExist(cityid); 
            if (checkcity == false) { return NotFound(); }
            //check point of interest existance 
            var checkpointofinterest = _cityInfoRepository.GetPointOfInterestAsync(cityid,pointOfInterestId);
            if (checkpointofinterest == null) { return NotFound(); }
            //after checking : time for updating , we will be using AUTOMAPPER  !! 
          await _mapper.Map(pointOfInterest, checkpointofinterest); 
            //don't forget to update the database 
            await _cityInfoRepository.Savechangesasync();
            return NoContent();
        }
        [HttpPatch("{pointofinterestid}")]
        //always start by adding the task class and async keyword !
        public async Task<ActionResult> partiallyUpdatePointOfInterest(int cityid, int pointofinterestid, JsonPatchDocument<pointsOfInterestForUpdatingDto> patchDocument)
        {
            var checkcity = await _cityInfoRepository.isCityExist(cityid);
            if (checkcity== false) { return NotFound(); }
            var checkpointofinterestentity = await _cityInfoRepository.GetPointOfInterestAsync(cityid, pointofinterestid);  
            if (checkpointofinterestentity == null) { return NotFound(); }
            var pointofinteresttopatch =  _mapper.Map<Models.pointsOfInterestForUpdatingDto>(checkpointofinterestentity);
            patchDocument.ApplyTo(pointofinteresttopatch , ModelState);  
            if ( !ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }
            if (!TryValidateModel(pointofinteresttopatch))
            {
                return BadRequest(ModelState);    
            }
            _mapper.Map(pointofinteresttopatch, checkpointofinterestentity);
            await _cityInfoRepository.Savechangesasync();
            
            
            return NoContent();

        }
        // we will try our mail service here 
        [HttpDelete("{pointofinterestid}")]
        public async Task<ActionResult> deletePointOfInterest(int cityid, int pointofinterestid)
        {
            var checkcity = await _cityInfoRepository.isCityExist(cityid); 
            if (!checkcity) { return NotFound(); }
            //check point of interest Id
            var checkpointofinterest =_cityInfoRepository.GetPointOfInterestAsync(cityid,pointofinterestid);
            if (checkpointofinterest == null) { return NotFound(); }
            // i have problem here task<pointofinterest> and type pointofinterest are not the same 
            _cityInfoRepository.DeletePointofInterest(checkpointofinterest);
            _mailSerive.Send("point of interest deleted ", $"point of interest {checkpointofinterest.Name} was deleted with id {checkpointofinterest.Id}");
            return NoContent();

        }
    }
}
