using Graduation_project.DTO;
using Graduation_project.Models;
using Graduation_project.Repository.WorkerRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorker _repo;
        public WorkerController(IWorker repo)
        {
            _repo=repo;
        }
        [HttpGet("GetCustmerById")]
        public async Task<IActionResult> GetCustmerById(int Id)
        {
            ResponseDto result =await _repo.GetCustmerById(Id);
            return result.Message=="Found" ? Ok( result) : BadRequest(result.Message);
        }
        [HttpGet("GetAllReviews")]
        public async Task<IActionResult> GetAllReviews(int Id)
        {
            GetAllReviewsDTOcs result=await _repo.GetAllReviews(Id); 
            return result.Message=="Found"?Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("GetAllRequests")]
        public async Task<IActionResult> GetAllRequests(int Id)
        {
            GetAllCustomerRequestsDTO result = await _repo.GetAllRequests(Id);
            return result.Message == "Found" ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("AcceptRequest")]
        public async Task<IActionResult> AcceptRequest(int WorkerId, int requestId)
        {
            CustomerRequestResponseWorkerDTO result = await _repo.AcceptRequest(WorkerId, requestId);
            return result.Message == "Accepted" ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("DeclineRequest")]
        public async Task<IActionResult> DeclineRequest(int WorkerId, int requestId)
        {
            CustomerRequestResponseWorkerDTO result = await _repo.DeclineRequest(WorkerId, requestId);
            return result.Message == "Deleted" ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPut("EditDetails")]
        public async Task<IActionResult> EditDetails([FromQuery]int id, [FromForm] UserDataRequestDTO worker)
        {
            ResponseDto result = await _repo.EditDetails(id, worker);
            return result.Message == "Edited" ? Ok(result) : BadRequest(result.Message);
        }
    }
}
