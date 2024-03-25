using System.ComponentModel.DataAnnotations;

namespace Graduation_project.DTO
{
    public class ReviewResponseDTO:ResponseDto
    {
        public string CustomerName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string Comment { get; set; }
        [Range(0, 5)]
        [Required]
        public float RateOFthisWork { get; set; }

    }
}
