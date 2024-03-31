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

        public async Task<ResponseDto> GetCustmerById(int Id)
        {
            var customer=_db.Customers.SingleOrDefault(x => x.Id == Id);
            if(customer == null)
            {
                return new ResponseDto() { Message="Mo customer Found"};
            }
            return new UserDataResponseDTO()
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

        public async Task<GetAllCustomerRequestsDTO> GetAllRequests(int Id)
        {
            var worker = await _db.Workers.SingleOrDefaultAsync(w => w.Id == Id);
            if (worker == null)
            {
                return new GetAllCustomerRequestsDTO() { Message = "Mo worker Found" };
            }
            var requests = _db.CustomerRequests.Include(r => r.Customer)
           .Select(r => new CustomerRequestResponseWorkerDTO()
           {
               Details = r.Details,
               CustomerName = r.Customer.FirstName + " " + r.Customer.LastName,
               CustomerProfilePicture = r.Customer.ProfilePicture,
               Status = r.Status.ToString()
           }).ToList();

            return new GetAllCustomerRequestsDTO
            {
                Message = "Found",
                requests = requests
            };
        }

        public async Task<CustomerRequestResponseWorkerDTO> AcceptRequest(int WorkerId, int requestId)
        {
            var worker = await _db.Workers.SingleOrDefaultAsync(w => w.Id == WorkerId);
            if (worker == null)
            {
                return new CustomerRequestResponseWorkerDTO() { Message = "Mo worker Found" };
            }

            var request = await _db.CustomerRequests.Include(s=>s.Customer).SingleOrDefaultAsync(w => w.Id == requestId);
            if (request == null)
            {
                return new CustomerRequestResponseWorkerDTO() { Message = "Mo request Found" };
            }

            request.Status = Status.Accepted;
            await _db.SaveChangesAsync();
            return new CustomerRequestResponseWorkerDTO
            {
                Details = request.Details,
                CustomerName = request.Customer.FirstName + " " + request.Customer.LastName,
                CustomerProfilePicture = request.Customer.ProfilePicture,
                Status = request.Status.ToString(),
                Message="Accepted"
            };
        }

        public async Task<CustomerRequestResponseWorkerDTO> DeclineRequest(int WorkerId, int requestId)
        {
            var worker = await _db.Workers.SingleOrDefaultAsync(w => w.Id == WorkerId);
            if (worker == null)
            {
                return new CustomerRequestResponseWorkerDTO() { Message = "Mo worker Found" };
            }

            var request = await _db.CustomerRequests.Include(s => s.Customer).SingleOrDefaultAsync(w => w.Id == requestId);
            if (request == null)
            {
                return new CustomerRequestResponseWorkerDTO() { Message = "Mo request Found" };
            }

            request.Status = Status.Declined;
            await _db.SaveChangesAsync();
            return new CustomerRequestResponseWorkerDTO
            {
                Details = request.Details,
                CustomerName = request.Customer.FirstName + " " + request.Customer.LastName,
                CustomerProfilePicture = request.Customer.ProfilePicture,
                Status = request.Status.ToString(),
                Message="Deleted"
            };
        }

        public async Task<ResponseDto> EditDetails(int id,UserDataRequestDTO worker)
        {
            var workerInDB = _db.Workers.SingleOrDefault(w => w.Id == id);
            if(workerInDB==null)
                return new ResponseDto() { Message="worker was not found in database"};
            byte[] profilePictureBytes = null;
            if (worker.ProfilePicture != null)
            {
                using var stream = new MemoryStream();
                await worker.ProfilePicture.CopyToAsync(stream);
                profilePictureBytes = stream.ToArray();
            }
            workerInDB.FirstName = worker.FirstName;
            workerInDB.LastName = worker.LastName;
            workerInDB.UserName = worker.UserName;
            workerInDB.Bios = worker.Bio;
            workerInDB.Location = worker.Location;
            workerInDB.City = worker.City;
            workerInDB.ProfilePicture = profilePictureBytes;
            workerInDB.PhoneNumber = worker.PhoneNumber;
            await _db.SaveChangesAsync();
            return new UserDataResponseDTO
            {
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                UserName = worker.UserName,
                Bio = worker.Bio,
                Location = worker.Location,
                City = worker.City,
                ProfilePicture = profilePictureBytes,
                PhoneNumber = worker.PhoneNumber,
                Message="Edited"
            };
        }
    }
}
