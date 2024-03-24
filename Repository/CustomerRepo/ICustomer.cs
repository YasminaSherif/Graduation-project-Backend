using Graduation_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_project.Repository.CustomerRepo
{
    public interface ICustomer
    {
        public Task<WorkersInCategoryDTO> WorkersInCategory(byte id);

    }
}
