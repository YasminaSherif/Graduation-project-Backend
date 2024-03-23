using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_project.Models
{
    public class Category
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        [MaxLength(50),MinLength(3)]
        public string Name { get; set; }

        public virtual ICollection<Worker> Workers { get; set; }=new List<Worker>();
    }
}
