namespace Graduation_project.DTO
{
    public class AllCustomerRequestsDTO:ResponseDto
    {
        public List<CustomerRequestResponseDTO> Requests { get; set; } = new List<CustomerRequestResponseDTO>();
    }
}
