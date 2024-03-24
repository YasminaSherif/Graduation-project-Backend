using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_project.DTO
{
    public class ImagesOfPastworkDTO
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
    }
}
