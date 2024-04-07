namespace Graduation_project.DTO
{
    public class AllCustomerRequestsResponseDTO:ResponseDto
    {
        public List<CustomerRequestResponseDTO> Requests { get; set; } = new List<CustomerRequestResponseDTO>();
    }
}
