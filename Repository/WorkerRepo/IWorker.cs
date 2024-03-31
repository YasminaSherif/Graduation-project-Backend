﻿namespace Graduation_project.Repository.WorkerRepo
{
    public interface IWorker
    {
        
        public Task<ResponseDto> GetCustmerById(int Id);
        public Task<GetAllReviewsDTOcs> GetAllReviews(int Id);
        public Task<GetAllCustomerRequestsDTO> GetAllRequests(int Id);
        public Task<CustomerRequestResponseWorkerDTO> AcceptRequest(int WorkerId, int requestId);
        public Task<CustomerRequestResponseWorkerDTO> DeclineRequest(int WorkerId, int requestId);
        public Task<ResponseDto> EditDetails(int id, UserDataRequestDTO worker);
    }
}
