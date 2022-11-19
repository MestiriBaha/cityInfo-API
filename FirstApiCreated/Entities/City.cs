using FirstApiCreated.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace FirstApiCreated.Entities
{
    public class City
    {
        //we want to make sure that each object city has a name ! 
        // override the constructor !
        public City(string name )
        {
            this.Name = name; 
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int CityId { get; set; }
        [Required]
        [MaxLength(50)]
        // we removed the by default string.empty not logical when it is required column !
        public string Name { get; set; }
        [MaxLength(200)] 
        public string? Description { get; set; }
      
   //Many to one Relation !! 
        public ICollection<pointsOfInterestDto> PointsofInterest { get; set; } = new List<pointsOfInterestDto>();
    }
}
