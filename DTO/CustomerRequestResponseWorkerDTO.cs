namespace Graduation_project.DTO
{
    public class CustomerRequestStatusResponseWorkerDTO:ResponseDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Details { get; set; }
        public byte[] CustomerProfilePicture { get; set; }
        public string Status { get; set; }
    }

    public class CustomerRequestResponseWorkerDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Details { get; set; }
        public byte[] CustomerProfilePicture { get; set; }
        public string Status { get; set; }
    }
}
