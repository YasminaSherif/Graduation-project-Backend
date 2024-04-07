using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_project.DTO
{
    public class ImagesOfPastworkRequestDTO
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
    }
}
