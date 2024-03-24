using Graduation_project.Repository.CustomerRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Graduation_project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
       private readonly ICustomer _repo;
        public CustomerController(ICustomer repo) {
            _repo = repo;
        }


    
        [HttpGet("WorkersInCategory")]
        
        public async Task<IActionResult> WorkersInCategory(byte id)
        {
            WorkersInCategoryDTO result=await _repo.WorkersInCategory(id);

            return result.Message == "Found" ? Ok(result) : BadRequest(result.Message);
        }
    }
}
