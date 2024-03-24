namespace Graduation_project.Repository.CustomerRepo
{
    public class CustomerRepo:ICustomer
    {
        private readonly ApplicationContext _db;

        public CustomerRepo(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<WorkersInCategoryDTO> WorkersInCategory(byte id)
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
    }
}
