using System.ComponentModel.DataAnnotations;

namespace Graduation_project.DTO
{
    public class ReviewRequestDTO
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public string Comment { get; set; }
        [Range(0, 5)]
        [Required]
        public float RateOFthisWork { get; set; }
    }
}
