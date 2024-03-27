using System.ComponentModel.DataAnnotations;

namespace Graduation_project.DTO
{
    public class AuthenticationResponseDTO:ResponseDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [StringLength(128)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string UserName { get; set; }
        [MaxLength(250)]
        public string Location { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        public string Role { get; set; }
        public Byte[]? ProfilePicture { get; set; }
        public string? Bios { get; set; }
        public string Token { get; set; }
    }
}
