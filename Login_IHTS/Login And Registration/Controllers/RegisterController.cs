using Microsoft.AspNetCore.Mvc;
using MVC_TimeTracker.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace MVC_TimeTracker.Controllers
{
	public class RegisterController : Controller
	{
		Uri baseaddres = new Uri("https://localhost:44334/api");

		private readonly HttpClient _client;

        public RegisterController()
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
				HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/API_RegisterCotroller/Register", content);

				if (response.IsSuccessStatusCode)
				{
					TempData["SuccesssMessage"] = "Registration Successful";
					return RedirectToAction("Logins","Logins");
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
	}
}
