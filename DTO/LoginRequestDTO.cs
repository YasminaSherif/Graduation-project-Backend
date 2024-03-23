using System.ComponentModel.DataAnnotations;

namespace Graduation_project.DTO
{
    public class LoginRequestDto
    {
        public String Email { get; set; }
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
