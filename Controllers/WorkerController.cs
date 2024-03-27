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
            CustomerDataResponseDTO result =await _repo.GetCustmerById(Id);
            return result.Message=="Found" ? Ok( result) : BadRequest(result.Message);
        }
    }
}
