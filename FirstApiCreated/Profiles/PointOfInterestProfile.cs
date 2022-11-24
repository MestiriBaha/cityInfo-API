using AutoMapper;
using FirstApiCreated.Models;
namespace FirstApiCreated.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile ()
        {
            CreateMap<Entities.PointOfInterest, Models.pointsOfInterestDto>();

        }

    }
}
