using FirstApiCreated.Entities;
namespace FirstApiCreated.Services

{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityByIdAsync(int cityid , Boolean IsPointofinterestincluded);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityid);
        Task<PointOfInterest?> GetPointOfInterestAsync(int cityid, int pointofinterestid);
        Task<Boolean> isCityExist(int cityid);

    }
}
