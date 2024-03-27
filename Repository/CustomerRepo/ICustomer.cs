using Graduation_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_project.Repository.CustomerRepo
{
    public interface ICustomer
    {
        public Task<WorkersInCategoryDTO> GetWorkersInCategory(byte id);
        public WorkerResponseDto GetWorkerById(int id);
        public Task<ReviewResponseDTO> CreateReview(ReviewRequestDTO reviewDTO);
        public Task<CustomerRequestResponseDTO> MakeRequest(CustomerRequestRequestDTO requestDTO);
        public Task<AllCustomerRequestsDTO> GetAllRequests(int CustomerId);
        public Task<ResponseDto> DeleteRequest(int CustomerId, int RequestId);

    }
}
