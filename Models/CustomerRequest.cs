using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Graduation_project.Models
{
    public class CustomerRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
         public string Details { get; set; }
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
        public Status Status { get; set; }
    }
    public enum Status
    {
        Pending,
        Accepted,
        Declined
    }
}
