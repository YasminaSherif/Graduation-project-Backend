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
        public CustomerController(ICustomer repo)
        {
            _repo = repo;
        }



        [HttpGet("WorkersInCategory")]
        public async Task<IActionResult> GetWorkersInCategory(byte id)
        {
            WorkersInCategoryDTO result = await _repo.GetWorkersInCategory(id);

            return result.Message == "Found" ? Ok(result) : BadRequest(result.Message);
        }



        [HttpGet("GetWorkerById")]
        public IActionResult GetWorkerById(int id)
        {
            WorkerResponseDto result = _repo.GetWorkerById(id);
            return result.Message == "Found" ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost("CreateReview")]
        public async Task<IActionResult> CreateReview(ReviewRequestDTO reviewDTO)
        {
            var result =await _repo.CreateReview(reviewDTO);
            return result.Message == "Created" ? Created("Created",result) : BadRequest(result.Message);
        }
    }
}
