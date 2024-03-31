namespace Graduation_project.DTO
{
    public class UserDataRequestDTO
    {  // public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public string PhoneNumber { get; set; }

    }
}
