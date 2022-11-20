using FirstApiCreated.Entities;
namespace FirstApiCreated.Services

{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityByIdAsync(int cityid , bool IsPointofinterestincluded);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityid);
        Task<PointOfInterest?> GetPointOfInterestAsync(int cityid, int pointofinterestid);
    }
}
