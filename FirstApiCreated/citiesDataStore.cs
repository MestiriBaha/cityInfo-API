using FirstApiCreated.Models;

namespace FirstApiCreated
{
    public class citiesDataStore
    {
        public List<cityDto> cities { get; set; } 

        //public static citiesDataStore current { get; }   = new citiesDataStore(); 
       public citiesDataStore()
        {
            cities = new List<cityDto>() { new cityDto() { Id = 1, Name = "Sousse", Description = "La perle du sahel", PointsofInterest = new List<pointsOfInterestDto>()
            { new pointsOfInterestDto() { Id = 1, Name = "stade olympique du sousse ", Description = "مقبرة الغزاة" } ,
            new pointsOfInterestDto() { Id = 2, Name = "plage boujaaafer", Description = "un des plus belles plages en tunisie " } ,} } ,
                                                             new cityDto() {Id=2,Name="Monastir",Description="La belle vie",PointsofInterest = new List<pointsOfInterestDto>()
            { new pointsOfInterestDto() { Id = 1, Name = "stade Mustapha ben Jannet ", Description = "l'un des meilleurs stades en Tunisie" } ,
            new pointsOfInterestDto() { Id = 2, Name = "Mix Max ", Description = "un top Restaurant" } , }, },
                                                             new cityDto() {Id=3,Name="Mahdia",Description="ville du calme" } };

        }
}
}
