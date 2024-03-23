using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Graduation_project.Models
{
   public abstract class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Email {  get; set; }
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required, StringLength(256), DataType(DataType.Password)]
        public string Password { get; set; }
        [MaxLength(50)]
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
        public string? Bios { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
    public enum Roles
    {
        Customer = 0,
        Worker = 1
    }

}
