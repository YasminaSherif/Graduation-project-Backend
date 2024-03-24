using System.ComponentModel.DataAnnotations;

namespace Graduation_project.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Comment { get; set; }
        [Range(0, 5)]
        public float RateOFthisWork { get; set; }
    }
}
