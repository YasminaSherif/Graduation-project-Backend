using Graduation_project.Models;
using Graduation_project.Repository.CustomerRepo;
using Microsoft.EntityFrameworkCore;

namespace Graduation_project.Repository.WorkerRepo
{
    public class WorkerRepo:IWorker
    {
        private readonly ApplicationContext _db;
        public WorkerRepo(ApplicationContext db) {
            _db = db;
        }

        public async Task<CustomerDataResponseDTO> GetCustmerById(int Id)
        {
            var customer=_db.Customers.SingleOrDefault(x => x.Id == Id);
            if(customer == null)
            {
                return new CustomerDataResponseDTO() { Message="Mo customer Found"};
            }
            return new CustomerDataResponseDTO()
            {
                Message = "Found",
                Bio = customer.Bios,
                City = customer.City,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Location = customer.Location,
                PhoneNumber = customer.PhoneNumber,
                ProfilePicture = customer.ProfilePicture,
            };
        }

        public async Task<GetAllReviewsDTOcs> GetAllReviews(int Id)
        {
            var worker =await _db.Workers.SingleOrDefaultAsync(w => w.Id == Id);
            if (worker == null)
            {
                return new GetAllReviewsDTOcs() { Message = "Mo worker Found" };
            }
            var reviews = _db.Reviews.Include(r => r.Customer)
            .Select(r => new ReviewResponseDTO()
            {
                Comment = r.Comment,
                CustomerName = r.Customer.FirstName + " " + r.Customer.LastName,
                ProfilePicture = r.Customer.ProfilePicture,
                RateOFthisWork = r.RateOFthisWork
            }).ToList();
            return new GetAllReviewsDTOcs
            {
                Message = "Found",
                reviews = reviews
            };
        }


    }
}
