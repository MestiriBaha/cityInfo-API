using FirstApiCreated.Entities;
using FirstApiCreated.Database;
using Microsoft.EntityFrameworkCore;

namespace FirstApiCreated.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        //injecting the database into the Repository constructor
        private readonly CityInfoContext _cityInfoContext;
        public CityInfoRepository(CityInfoContext cityinfocontext)
        {
            // beware of the nullable and the DI implementation
            // when we added the check exception for nullable database , the green error was gone ! 
            _cityInfoContext = cityinfocontext ?? throw new ArgumentNullException(nameof(cityinfocontext));  
        }
       public  async Task<IEnumerable<City>> GetCitiesAsync()
        {
            // throw new NotImplementedException();    
            return await _cityInfoContext.Cities.OrderBy(rank => rank.Name).ToListAsync(); 
        }
        public async Task<City?> GetCityByIdAsync(int cityid, Boolean IsPointofinterestincluded)
        {
            //throw new NotImplementedException();
            if (IsPointofinterestincluded)
            {
                return await _cityInfoContext.Cities.Include(x=> x.PointsofInterest).Where(search => search.CityId==cityid).FirstOrDefaultAsync();
            }
            return await _cityInfoContext.Cities.Where(search => search.CityId == cityid).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityid)
        {
           // throw new NotImplementedException();
           return await _cityInfoContext.PointsOfInterest.Where(search => search.CityId==cityid).ToListAsync();   
        }
        public async Task<PointOfInterest?> GetPointOfInterestAsync(int cityid, int pointofinterestid)
        {
            //throw new NotImplementedException();
            return await _cityInfoContext.PointsOfInterest.Where(search => search.CityId == cityid && search.Id == pointofinterestid).FirstOrDefaultAsync();
        }
        //method to check if a city exists ( needed for searching points of interest ) 
        public async Task<Boolean> isCityExist ( int cityid)
        {
            return await _cityInfoContext.Cities.AnyAsync(searching => searching.CityId==cityid);    
        }
       public  async Task AddPointofinterestforCityAsync(int cityid, PointOfInterest pointofinterestid)
        {
            // we need to get the city from  which we have the cityid 
            var city = await GetCityByIdAsync(cityid, false);
            if (city != null)
            {
                city.PointsofInterest.Add(pointofinterestid);
            }

        }
        public async Task<Boolean> Savechangesasync()
        {
            return (await _cityInfoContext.SaveChangesAsync() >=0 ); 
        }
       public  void DeletePointofInterest(PointOfInterest pointOfInterest)
        {
            // easy hein ! 
            _cityInfoContext.PointsOfInterest.Remove(pointOfInterest);  
        }



        //let's register our service in program.cs class 
    }
}
