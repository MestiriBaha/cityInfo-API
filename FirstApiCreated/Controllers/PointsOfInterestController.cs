using FirstApiCreated.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FirstApiCreated.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<pointsOfInterestDto>> getPoints (int cityId)
        {
            var result = citiesDataStore.current.cities.FirstOrDefault(ptr => ptr.Id == cityId);
            if ( result == null ) { return NotFound(); };
            return Ok(result.points); 
        }
        [HttpGet("{pointofinterestid}",Name ="get")]
        public ActionResult<pointsOfInterestDto> getPointsOfInterestId ( int cityId , int pointofinterestid)
        {
            var result = citiesDataStore.current.cities.FirstOrDefault(ptr => ptr.Id == cityId);
            if (result == null) { return NotFound(); };

            var city = result.PointsofInterest.FirstOrDefault(ptr => ptr.Id == pointofinterestid);  
            if (city ==null) { return NotFound();  };
            return Ok(city);
        }
        [HttpPost]
        public ActionResult<pointsOfInterestDto> createPointOfInterest(int cityid , pointsOfInterestForCreationDto newpointofinterest)
        {
            var checkcity = citiesDataStore.current.cities.FirstOrDefault(t => t.Id == cityid);
            if (checkcity==null) {  return NotFound(); } 
            // searching for the max id in the pointsofinterest !! 
            var nextId = citiesDataStore.current.cities.SelectMany(c => c.PointsofInterest).Max(p => p.Id);
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
            var checkcity = citiesDataStore.current.cities.FirstOrDefault(t => t.Id == cityid);
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
            var checkcity = citiesDataStore.current.cities.FirstOrDefault(t => t.Id == cityid);
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
        [HttpDelete("{pointofinterestid}")]
        public ActionResult deletePointOfInterest ( int cityid , int pointofinterestid )
        {
            var checkcity = citiesDataStore.current.cities.FirstOrDefault(t => t.Id == cityid);
            if (checkcity == null) { return NotFound(); }
            //check point of interest Id
            var checkpointofinterest = checkcity.PointsofInterest.FirstOrDefault(c => c.Id == pointofinterestid);
            if (checkpointofinterest == null) { return NotFound(); }

            checkcity.PointsofInterest.Remove(checkpointofinterest);
            return NoContent(); 

        }
        }
}
