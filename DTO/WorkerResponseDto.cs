using System.ComponentModel.DataAnnotations;

namespace Graduation_project.DTO
{
    public class WorkerResponseDto : ResponseDto
    {
       
        public int Id { get; set; }
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(250)]
        public string Location { get; set; }
        [Required, StringLength(256), DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        public Roles Role { get; set; }
        public Byte[]? ProfilePicture { get; set; }
        public List<ImagesOfPastworkDTO> ImagesOfPastWork { get; set; } = new List<ImagesOfPastworkDTO>();
        public List<ReviewRequestDTO> Reviews { get; set; } = new List<ReviewRequestDTO>();

    }
}
