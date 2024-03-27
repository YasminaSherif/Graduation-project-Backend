using Graduation_project.DTO;
using Graduation_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Graduation_project.Repository.CustomerRepo
{
    public class CustomerRepo:ICustomer
    {
        private readonly ApplicationContext _db;

        public CustomerRepo(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<WorkersInCategoryDTO> GetWorkersInCategory(byte id)
        {
            WorkersInCategoryDTO result;
            if (await _db.Categories.FindAsync(id) is null)
            {
                result = new WorkersInCategoryDTO() { Message="Category is not found"};
                return result;
            }
            List<WorkerResponseDto> workers = _db.Workers.Where(w => w.Id == id)
                .Select(w => new WorkerResponseDto
                {
                    Id = w.Id,
                    City = w.City,
                    FirstName = w.FirstName,
                    LastName = w.LastName,
                    Location = w.Location,
                    PhoneNumber = w.PhoneNumber,
                    ProfilePicture = w.ProfilePicture,
                }).ToList();

            if(workers.Count == 0) {
                return result = new WorkersInCategoryDTO() { Message = "No Workers Are in this category" };
            }

            result = new WorkersInCategoryDTO()
            {
                Message = "Found",
                WorkersResponse= workers
            };

            return result;
        }

        public WorkerResponseDto GetWorkerById(int id)
        {
            var worker =  _db.Workers
                .Include(w=>w.ImagesOfPastWork)
                .Include(w=>w.Reviews)
                .SingleOrDefault(w => w.Id == id);
            if (worker is null)
            {
                return new WorkerResponseDto { Message = "Worker doesn't exist" };
            }

            List<ReviewRequestDTO> reviews = worker.Reviews.Select(r => new ReviewRequestDTO()
            {
                
                Comment = r.Comment,
                CustomerId = r.CustomerId,
                RateOFthisWork = r.RateOFthisWork
            }).ToList();

            List<ImagesOfPastworkDTO> images = worker.ImagesOfPastWork.Select(I => new ImagesOfPastworkDTO()
            {
                Id = I.Id,
                Image = I.Image,
            }).ToList();


            return new WorkerResponseDto {
                Message = "Found",
                Id = worker.Id,
                City = worker.City,
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                Location = worker.Location,
                PhoneNumber = worker.PhoneNumber,
                ProfilePicture = worker.ProfilePicture,
                ImagesOfPastWork= images,
                Reviews = reviews
            };
        }

        public async Task<ReviewResponseDTO> CreateReview(ReviewRequestDTO reviewDTO)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == reviewDTO.CustomerId);
            if (customer is null)
                return new ReviewResponseDTO { Message="Customer was not found" };
            var worker = await _db.Workers.FirstOrDefaultAsync(c => c.Id == reviewDTO.WorkerId);
            if (worker is null)
                return new ReviewResponseDTO { Message="worker was not found" };

            var review = new Review()
            {
                WorkerId = worker.Id,
                CustomerId = customer.Id,
                Comment = reviewDTO.Comment,
                Customer = customer,
                Worker = worker,
                RateOFthisWork = reviewDTO.RateOFthisWork,

            };

            await _db.Reviews.AddAsync(review);
            await _db.SaveChangesAsync();

            return new ReviewResponseDTO
            {
                Message="Created",
                Comment=review.Comment,
                CustomerName=review.Customer.FirstName +" "+ review.Customer.LastName,
                ProfilePicture=review.Customer.ProfilePicture,
                RateOFthisWork=review.RateOFthisWork
            };
        }

        public async Task<CustomerRequestResponseDTO> MakeRequest(CustomerRequestRequestDTO requestDTO)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == requestDTO.CustomerId);
            if (customer is null)
                return new CustomerRequestResponseDTO { Message = "Customer was not found" };
            var worker = await _db.Workers.FirstOrDefaultAsync(c => c.Id == requestDTO.WorkerId);
            if (worker is null)
                return new CustomerRequestResponseDTO { Message = "worker was not found" };

            var request = new CustomerRequest
            {
                CustomerId = requestDTO.CustomerId,
                Customer = customer,
                Worker = worker,
                WorkerId = requestDTO.WorkerId,
                Status = Status.Pending,
                Details = requestDTO.Details,
            };

            await _db.CustomerRequests.AddAsync(request);
            await _db.SaveChangesAsync();

            return new CustomerRequestResponseDTO
            {
                Message = "Created",
                Details = request.Details,
                Id = request.Id,
                status = request.Status.ToString(),
                WorkerId = request.WorkerId,
                WorkerName = request.Worker.FirstName + " " + request.Worker.LastName,
                WorkerProfilePicture = request.Worker.ProfilePicture
            };

        }

        public async Task<AllCustomerRequestsDTO> GetAllRequests(int CustomerId)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == CustomerId);
            
            if (customer is null)
                return new AllCustomerRequestsDTO { Message = "Customer was not found" };
            if(customer.CustomerRequest is null)
                return new AllCustomerRequestsDTO { Message = "No requests yet" };

            var requests = _db.CustomerRequests.Include(c => c.Worker).Where(r=>r.CustomerId==CustomerId).Select(c => new CustomerRequestResponseDTO
            {
                Details = c.Details,
                Id = c.Id,
                status = c.Status.ToString(),
                WorkerId = c.WorkerId,
                WorkerName = c.Worker.FirstName + " " + c.Worker.LastName,
                WorkerProfilePicture = c.Worker.ProfilePicture
            }).ToList();

            return new AllCustomerRequestsDTO
            {
                Message = "Found",
                Requests = requests
            };

        }

        public async Task<ResponseDto> DeleteRequest(int CustomerId, int RequestId)
        {
            var customer = await _db.Customers.Include(c=>c.CustomerRequest).FirstOrDefaultAsync(c => c.Id == CustomerId);
            if (customer is null)
                return new ResponseDto { Message = "Customer was not found" };
            var request=customer.CustomerRequest.SingleOrDefault(r=>r.Id==RequestId);
            if (request is null)
                return new ResponseDto { Message = "request was not found" };
            _db.CustomerRequests.Remove(request);
           await _db.SaveChangesAsync();
            return new ResponseDto { Message = "Deleted" };
        }
    }
}
