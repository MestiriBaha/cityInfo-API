using FirstApiCreated.Entities;
namespace FirstApiCreated.Services

{
    public interface ICityInfoRepository
    {
        //get methods 
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityByIdAsync(int cityid , Boolean IsPointofinterestincluded);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityid);
        Task<PointOfInterest?> GetPointOfInterestAsync(int cityid, int pointofinterestid);
        Task<Boolean> isCityExist(int cityid);
        //post methods 
        Task AddPointofinterestforCityAsync ( int cityid, PointOfInterest pointofinterestid);
        Task<Boolean> Savechangesasync();
        //delete method 
        void DeletePointofInterest(PointOfInterest pointOfInterest);
        //filter 
        Task<IEnumerable<City>> FilteringCitiesAsync(String? name , string? searchquery);


    }
}
