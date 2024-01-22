using Login_And_Registration.Models;
using Login_IHTS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Login_And_Registration.Controllers
{
    public class UserController1 : Controller
    {

        Uri baseaddres = new Uri("https://localhost:44334/api");

        private readonly HttpClient _client;

        public UserController1()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseaddres;   
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationViewModel model)
        {
            try
            {
                string data = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/User/Register", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccesssMessage"] = "Registration Successful";
                    return RedirectToAction("Login");
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    ModelState.AddModelError(string.Empty, "User already exists.");
                    return View(model);
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Registration failed. Please try again later.");
                    return View(model);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Registration failed. Please try again later.");
                return View(model);
            }
        }





        [HttpGet]
          public IActionResult Login()
            {
                 return View();
            }







        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            try
            {
                string data = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/User/login", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccesssMessage"] = "Login Successful";
                    return RedirectToAction("UserDetails");
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    return View(model);
                }
                else
                {
                    var errorMessage = "An error occurred during login. Please try again.";
                    ModelState.AddModelError(string.Empty, errorMessage);
                    return View(model);
                }
            }
            catch (Exception)
            {
                var errorMessage = "An error occurred during login. Please try again.";
                ModelState.AddModelError(string.Empty, errorMessage);
                return View(model);
            }
        }



        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ReserPassword(ResetPasswordViewModel model)
        {
            try
            {
                string data = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/User/reset-password", content).Result;

                if (response.IsSuccessStatusCode)
                {

                    TempData["SuccesssMessage"] = " Successfull";
                    return RedirectToAction("Login");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return View(model);
                }
            }
            catch (Exception)
            {

                return View(model);
            }
        }


        //[HttpGet]
        //public IActionResult UserDetails()
        //{
        //    return View();
        //}



        [HttpGet]
        public async Task<IActionResult> UserDetails()
        {
            try
            {
                var projectsEndpoint = "https://localhost:44334/api/User/projects";
                var locationsEndpoint = "https://localhost:44334/api/User/locations";

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


        //[Route("User/SaveTask")]
        //[HttpPost]
        //public async Task<IActionResult> SaveTask(TaskModel model)
        //{
        //    try
        //    {
        //        // Call Web API to save the task
        //        var response = await _client.PostAsJsonAsync(_client.BaseAddress + "/User/SaveTask", model);


        //        if (response.IsSuccessStatusCode)
        //        {
        //            var tasks = await response.Content.ReadFromJsonAsync<List<dynamic>>();
        //            ViewBag.AllTasks = tasks;
        //            return View("UserDetails");
        //        }
        //        else
        //        {
        //            var errorMessage = await response.Content.ReadAsStringAsync();
        //            ModelState.AddModelError(string.Empty, errorMessage);
        //            return View("UserDetails");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ModelState.AddModelError(string.Empty, "Failed to save the task. Please try again later.");
        //        return View("UserDetails");
        //    }
        //}



        [Route("User/SaveTask")]
        [HttpPost]
        public async Task<IActionResult> SaveTask(TaskModel model)
        {
            try
            {
                // to  Call Web API to save the task
                var response = await _client.PostAsJsonAsync(_client.BaseAddress + "/User/SaveTask", model);

                if (response.IsSuccessStatusCode)
                {
                   
                    ViewBag.SuccessMessage = "Task created successfully!";
                    return View("UserDetails");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorMessage);
                    return View("UserDetails");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Failed to save the task. Please try again later.");
                return View("UserDetails");
            }
        }























        //[Route("User/SaveTask")]
        //[HttpPost]
        //public async Task<IActionResult> SaveTask(TaskModel model)
        //{
        //    try
        //    {
        //        var response = await _client.PostAsJsonAsync("https://localhost:44334/api/User/SaveTask", model);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Fetch and display updated tasks after saving
        //            var tasksResponse = await _client.GetAsync("https://localhost:44334/api/User/tasks");
        //            tasksResponse.EnsureSuccessStatusCode();
        //            var tasks = await tasksResponse.Content.ReadFromJsonAsync<List<TaskModel>>();
        //            ViewBag.AllTasks = tasks;

        //            return View("UserDetails");
        //        }
        //        else
        //        {
        //            var errorMessage = await response.Content.ReadAsStringAsync();
        //            ModelState.AddModelError(string.Empty, errorMessage);
        //            return View("UserDetails");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ModelState.AddModelError(string.Empty, "Failed to save the task. Please try again later.");
        //        return View("UserDetails");
        //    }
        //}















    }
}
