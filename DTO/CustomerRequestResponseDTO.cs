using System.ComponentModel.DataAnnotations;

namespace Graduation_project.DTO
{
    public class CustomerRequestResponseDTO:ResponseDto
    {
        public int Id { get; set; }
        public String WorkerName { get; set; }
        public byte[] WorkerProfilePicture { get; set; }
        public string Details { get; set; }
        public int WorkerId { get; set; }
        public string status { get; set; }
    }
}
