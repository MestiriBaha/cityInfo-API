namespace FirstApiCreated.Models
{
    public class cityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
        public int points  {
            get { return PointsofInterest.Count() ; } }
        public ICollection<pointsOfInterestDto> PointsofInterest { get; set; } = new List<pointsOfInterestDto>();  
    }
}
