using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_project.Models
{
    public class Worker:User
    {
        public virtual ICollection<ImageOfPastWork> ImagesOfPastWork { get; set; } = new List<ImageOfPastWork>();

        public byte CategoryId { get; set; }
        public Category Category { get; set; }

        public virtual ICollection<CustomerRequest> CustomerRequest { get; set; }= new List<CustomerRequest>();

      //  public virtual ICollection<Review> Review { get; set; } = new List<Review>();
    }
}
