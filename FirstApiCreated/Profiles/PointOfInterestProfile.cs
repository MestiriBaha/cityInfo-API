using AutoMapper;
using FirstApiCreated.Models;
namespace FirstApiCreated.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile ()
        {
            CreateMap<Entities.PointOfInterest, Models.pointsOfInterestDto>();
            CreateMap<Models.pointsOfInterestForCreationDto, Entities.PointOfInterest> ();
            CreateMap<Models.pointsOfInterestForUpdatingDto, Entities.PointOfInterest>();
            CreateMap<Entities.PointOfInterest, Models.pointsOfInterestForUpdatingDto > ();


        }

    }
}
