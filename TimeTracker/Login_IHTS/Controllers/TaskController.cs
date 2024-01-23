using Microsoft.AspNetCore.Mvc;

namespace API_TimeTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private DataContext _context;


        public TaskController(DataContext context)
        {
            _context = context;
        }



        [HttpGet("projects")]
        public IActionResult GetProjects()
        {
            try
            {
                var projects = _context.Projects.ToList();
                return Ok(projects);
            }
            catch (Exception)
            {
                //return StatusCode(500, "Internal server error");
                return BadRequest("locations not found");
            }
        }

        [HttpGet("locations")]
        public IActionResult GetLocations()
        {
            try
            {
                var locations = _context.Locations.ToList();
                return Ok(locations);
            }
            catch (Exception)
            {
                //return StatusCode(500, "Internal server error");
                return BadRequest("locations not found");
            }
        }
        [HttpPost("SaveTask")]
        public IActionResult SaveTask([FromBody] TaskModel model)
        {

            try
            {
                _context.Tasks.Add(model);
                _context.SaveChanges();

                // Returning the list of all tasks after saving
                var tasks = _context.Tasks.ToList();
                return Ok(tasks);
            }
            catch (Exception)
            {
                return BadRequest("locations not found");
            }
        }
    }
}
