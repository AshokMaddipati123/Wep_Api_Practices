using ConsumeWEBAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ConsumeWEBAPI.Controllers
{
    public class StudentController1 : Controller
    {

        Uri baseAddress = new Uri("https://localhost:44305/api");

        private readonly HttpClient _client;

        public StudentController1()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<StudentViewModel> studentsList = new List<StudentViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/student/GetStudents").Result;

            
            if(response.IsSuccessStatusCode)
            {
                string data= response.Content.ReadAsStringAsync().Result;
                studentsList = JsonConvert.DeserializeObject<List<StudentViewModel>>(data)!;
            }

            return View(studentsList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/student/AddStudents", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Student Created";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            StudentViewModel student = new StudentViewModel();
            //HttpResponseMessage response = await _client.GetAsync(new Uri(_client.BaseAddress, $"/Student/GetStudentById/{id}"));
            HttpResponseMessage response = await _client.GetAsync(new Uri(_client.BaseAddress, $"/student/GetStudentById/{id}"));

            //HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/student/GetStudentById" +id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<StudentViewModel>(data)!;
            }
            return View(student);
        }


        [HttpPost]
        public IActionResult Edit(StudentViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8,"application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/student/UpdateStudent", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
