using API_TimeTracker.Models;
using Microsoft.AspNetCore.Mvc;


namespace MVC_TimeTracker.Controllers
{
    public class UserDataController : Controller
    {
        Uri baseaddres = new Uri("https://localhost:44334/api");

        private readonly HttpClient _client;

        public UserDataController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseaddres;
        }


        //[HttpGet]
        //public IActionResult UserData()
        //{
        //    return View();
        //}



        [HttpGet]
        public async Task<IActionResult> UserData()
        {
            try
            {
                var projectsEndpoint = "https://localhost:44334/api/Task/projects";
                var locationsEndpoint = "https://localhost:44334/api/Task/locations";

                var projectsResponse = await _client.GetAsync(projectsEndpoint);
                var locationsResponse = await _client.GetAsync(locationsEndpoint);

                projectsResponse.EnsureSuccessStatusCode();
                locationsResponse.EnsureSuccessStatusCode();

                // Deserialize the response content to the respective models
                var projects = await projectsResponse.Content.ReadFromJsonAsync<List<ProjectModel>>();
                var locations = await locationsResponse.Content.ReadFromJsonAsync<List<LocationModel>>();

                ViewBag.Projects = projects;
                ViewBag.Locations = locations;
                return View();
            }
            catch (HttpRequestException ex)
            {
                // Log.LogError($"Error while fetching data from Web API: {ex.Message}");
                return View("Error");
            }
        }




        [HttpGet]
        public IActionResult SaveTask()
        {
            return View();
        }


        [Route("Task/SaveTask")]
        [HttpPost]
        public async Task<IActionResult> SaveTask(TaskModel model)
        {
            try
            {
                // to  Call Web API to save the task
                var response = await _client.PostAsJsonAsync(_client.BaseAddress + "/Task/SaveTask", model);

                if (response.IsSuccessStatusCode)
                {
                    //ViewBag.SuccessMessage = "Task created successfully!";
                    return View("UserData");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    return View("UserData");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Failed to save the task. Please try again later.");
                return View("UserData");
            }
        }



    }
}
