using System.ComponentModel.DataAnnotations;

namespace Graduation_project.DTO
{
    public class RegisterRequestDTO
    {
        public string Email { get; set; }
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required, StringLength(256), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, Compare("Password"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(250)]
        public string Location { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        public Roles Role { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public string? Bios { get; set; }
        public byte? CategoryId { get; set; }
        [Required, StringLength(256), DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

    }

}
public enum Categorys
{
    Carpenter,
    Painter,
    Electrition
}

