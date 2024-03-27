using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_project.DTO
{
    public class CustomerRequestRequestDTO
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public string Details { get; set; }
        
    }
 
}


       

