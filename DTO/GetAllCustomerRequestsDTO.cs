namespace Graduation_project.DTO
{
    public class GetAllCustomerRequestsDTO:ResponseDto
    {
        public List<CustomerRequestResponseWorkerDTO> requests { get; set; } = new List<CustomerRequestResponseWorkerDTO>();
    }
}
