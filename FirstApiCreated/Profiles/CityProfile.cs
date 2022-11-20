using AutoMapper;
namespace FirstApiCreated.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, Models.CityWithoutPointOfInterestDTO>();
            CreateMap<Entities.City, Models.cityDto>();   
        }
    }
}
