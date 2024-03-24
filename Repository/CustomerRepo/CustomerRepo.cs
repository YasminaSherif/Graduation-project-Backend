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

            List<ReviewDTO> reviews = worker.Reviews.Select(r => new ReviewDTO()
            {
                Id = r.Id,
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
    }
}
