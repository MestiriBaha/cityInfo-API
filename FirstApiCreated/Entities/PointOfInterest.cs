using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 


namespace FirstApiCreated.Entities
   
{
    public class PointOfInterest
    {
        public PointOfInterest(string name)
        {
            Name = name;    
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; } 
        //One to many RELATION !! 
        [ForeignKey("CityId")]
        public City? City { get; set; }  
        public int CityId { get; set; } 
    }
}
