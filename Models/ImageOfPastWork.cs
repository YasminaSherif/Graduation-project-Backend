using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Graduation_project.Models
{
    public class ImageOfPastWork
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public int WorkerId { get; set; }
        public Worker worker { get; set; }
    }
}
