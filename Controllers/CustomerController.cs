using Graduation_project.Models;
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
            WorkerDataDTO result = _repo.GetWorkerById(id);
            return result.Message == "Found" ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost("CreateReview")]
        public async Task<IActionResult> CreateReview(ReviewRequestDTO reviewDTO)
        {
            var result =await _repo.CreateReview(reviewDTO);
            return result.Message == "Created" ? Created("Created",result) : BadRequest(result.Message);
        }

        [HttpPost("MakeRequest")]
        public async Task<IActionResult> MakeRequest(CustomerRequestRequestDTO requestDTO)
        {
            var result = await _repo.MakeRequest(requestDTO);
            return result.Message == "Created" ? Created("Created", result) : BadRequest(result.Message);
        }

        [HttpGet("GetAllRequests")]
        public async Task<IActionResult> GetAllRequests(int CustomerId)
        {
            var result = await _repo.GetAllRequests(CustomerId);
            return result.Message == "Found" ? Ok( result) : BadRequest(result.Message);
        }

        [HttpDelete("DeleteRequest")]
        public async Task<IActionResult> DeleteRequest(int CustomerId, int RequestId)
        {
            var result = await _repo.DeleteRequest(CustomerId,RequestId);
            return result.Message == "Deleted" ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("EditDetails")]
        public async Task<IActionResult> EditDetails([FromQuery]int id,[FromForm] UserDataRequestDTO customer)
        {
            var result = await _repo.EditDetails(id, customer);
            return result.Message == "Edited" ? Ok(result) : BadRequest(result.Message);
        }

    }
}
