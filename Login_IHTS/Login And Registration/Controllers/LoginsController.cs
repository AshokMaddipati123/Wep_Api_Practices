using API_TimeTracker.Models;
using Microsoft.AspNetCore.Mvc;
using MVC_TimeTracker.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace MVC_TimeTracker.Controllers
{
    public class LoginsController : Controller
    {

        Uri baseaddres = new Uri("https://localhost:44334/api");

        private readonly HttpClient _client;



        public LoginsController()
        {
            
            _client = new HttpClient();
            _client.BaseAddress = baseaddres;
        }

        [HttpGet]
        public IActionResult Logins()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logins(UserLoginViewModel model)
        {
            try
            {
                string data = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Login/login", content).Result;
       

                if (response.IsSuccessStatusCode)
                {

                    var Data = await _client.GetAsync(_client.BaseAddress + $"/Task/GetUserPermission?userName={model.UserName}");
                    byte Permission = await Data.Content.ReadAsAsync<byte>();
                    TempData["Permision"] = Convert.ToInt32(Permission);
                    TempData["UserName"] = model.UserName;

                    return RedirectToAction("UserData","UserData");
                   
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
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                string data = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Login/reset-password", content).Result;

                if (response.IsSuccessStatusCode)
                {

                    TempData["SuccesssMessage"] = " Successfull";
                    return RedirectToAction("Logins");
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





    }
}
