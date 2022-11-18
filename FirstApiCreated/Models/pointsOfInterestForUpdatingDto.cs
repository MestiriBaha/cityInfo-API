using System.ComponentModel.DataAnnotations;

namespace FirstApiCreated.Models
{
    public class pointsOfInterestForUpdatingDto
    {

        [Required(ErrorMessage = "just provide a name dumbass ")]
        public string name { get; set; } = String.Empty;
        public string? description { get; set; }
    }
}
