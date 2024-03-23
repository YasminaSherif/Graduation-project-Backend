using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Graduation_project.Models
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } 
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
        public string Comment { get; set; }
        [Range(0, 5)]
        public float RateOFthisWork { get; set; }
    }
}
