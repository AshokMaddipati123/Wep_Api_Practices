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
                var projects = _context.PROJECTNAMES.ToList();
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
                var locations = _context.LOCATIONS.ToList();
                return Ok(locations);
            }
            catch (Exception)
            {
               
                return BadRequest("locations not found");
            }
        }

        [HttpGet("GetUsernames")]
        public IActionResult GetUsernames()
        {
            try
            {
                var usernames = _context.USERDETAILS.Select(u => u.UserName).ToList();
                return Ok(usernames);
            }
            catch (Exception)
            {
                return BadRequest("Usernames not found");
            }
        }

        [HttpPost("SaveTask")]
        public IActionResult SaveTask([FromBody] TaskModel model)
        {
            try
            {
                _context.TASKDETAILS.Add(model);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Failed to save the task.");
            }
        }


        [HttpGet("GetAllTasks")]
        public IActionResult GetAllTasks()
        {
            try
            {
                var tasks = _context.TASKDETAILS.ToList();
                return Ok(tasks);
            }
            catch (Exception)
            {
                return BadRequest("Error fetching all tasks");
            }
        }


        [HttpGet("GetUserPermission")]
        public IActionResult GetUserPermission(string userName)
        {
            try
            {
            
                var user = _context.USERDETAILS.FirstOrDefault(u => u.UserName == userName);

                if (user == null)
                {
                    return NotFound("User not found");
                }

          
                return Ok(user.permission);
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving user permission");
            }
        }


        [HttpGet("GetUserId")]
        public IActionResult GetUserId(string userName)
        {
            try
            {
                var user = _context.USERDETAILS.FirstOrDefault(u => u.UserName == userName);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                return Ok(user.UserId);
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving user ID");
            }
        }

        [HttpGet("GetUserNameById")]
        public IActionResult GetUserNameById(int userid)
        {
            var UserName = _context.USERDETAILS.FirstOrDefault(u => u.UserId == userid);

            if(UserName == null)
            {
                return NotFound("Username not found");
            }
            return Ok(UserName.UserName);

        }


        [HttpGet("GetLoactionNameById")]
        public IActionResult GetLoactionNameById(int locationid)
        {
            var location=_context.LOCATIONS.FirstOrDefault(l=>l.LocationId == locationid);

            if(location == null)
            {
                return NotFound("Locations not found");
            }
            return Ok(location.LocationName);
        }


        [HttpGet("GetProjectNameById")]
        public IActionResult GetProjectNameById(int projectid)
        {
            var project=_context.PROJECTNAMES.FirstOrDefault(p=>p.ProjectId == projectid);
            if(project == null)
            {
                return NotFound("project not found");
            }
            return Ok(project.ProjectName);
        }


        [HttpGet("GetTasksByUserId")]
        public IActionResult GetTasksByUserId(int userId, DateTime selectDate)
        {
            try
            {
                
                var tasks = _context.TASKDETAILS.Where(t => t.UserId == userId && t.CREATIONDATE == DateOnly.FromDateTime(selectDate.Date)).ToList();

                return Ok(tasks);
            }
            catch (Exception)
            {
                return BadRequest("Error fetching tasks by user ID");
            }
        }


    }
}

