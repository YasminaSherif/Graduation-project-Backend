namespace Graduation_project.Repository.WorkerRepo
{
    public interface IWorker
    {
        
        public Task<CustomerDataResponseDTO> GetCustmerById(int Id);
        public Task<GetAllReviewsDTOcs> GetAllReviews(int Id);
       
    }
}
