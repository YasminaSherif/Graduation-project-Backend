namespace Graduation_project.DTO
{
    public class CustomerDataResponseDTO:ResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public string PhoneNumber { get; set; }

    }
}
