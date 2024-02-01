using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_TimeTracker.Models;
using OfficeOpenXml;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Text;


namespace MVC_TimeTracker.Controllers
{
    public class ReportsController : Controller
    {
        Uri baseaddres = new Uri("https://localhost:44334/api");

        private readonly HttpClient _client;


        public ReportsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseaddres;
        }

        [HttpGet]
        public async Task<IActionResult> Reports()
        {
            try
            {
                var response = await _client.GetAsync(_client.BaseAddress+ "/Task/GetUsernames");

                if (response.IsSuccessStatusCode)
                {
                    var usernames = await response.Content.ReadAsAsync<List<string>>();
                    ViewBag.Usernames = usernames;
                }
                else
                {
                    ViewBag.Usernames = new List<string>();
                }
            }
            catch (Exception)
            {
                ViewBag.Usernames = new List<string>();
            }

            return View();
        }





        [Route("Reports/DownloadExcel")]
        [HttpPost("DownloadExcel")]
        public async Task<IActionResult> DownloadExcel(string selectUser, DateOnly? selectDate)
        {
            try
            {
                var userIdResponse = await _client.GetAsync(_client.BaseAddress + $"/Task/GetUserId?userName={selectUser}");

                if (!userIdResponse.IsSuccessStatusCode)
                {
                    return BadRequest("Error fetching user ID from the API");
                }

                int userId = await userIdResponse.Content.ReadAsAsync<int>();


                var allTasksResponse = await _client.GetAsync(_client.BaseAddress + $"/Task/GetTasksByUserId?userId={userId}&selectDate={selectDate}");

                if (!allTasksResponse.IsSuccessStatusCode)
                {
                    return BadRequest("Error fetching all tasks from the API");
                }

                List<TaskViewModel> allTasks = await allTasksResponse.Content.ReadAsAsync<List<TaskViewModel>>();


               


                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Tasks");

                   // worksheet.Cells["A1"].Value = "Task ID";
                    worksheet.Cells["B1"].Value = "UserName";
                    worksheet.Cells["C1"].Value = "Project Name";
                    worksheet.Cells["D1"].Value = "Location";
                    worksheet.Cells["E1"].Value = "Task Date";
                    worksheet.Cells["F1"].Value = "User Story/Bug Number";
                    worksheet.Cells["G1"].Value = "Task Description";
                    worksheet.Cells["H1"].Value = "Start Time";
                    worksheet.Cells["I1"].Value = "End Time";


                    int row = 2;
                    foreach (var task in allTasks)
                    {


                       // worksheet.Cells[$"A{row}"].Value = task.TaskId;
              
                        var userNameResult =await _client.GetAsync (_client.BaseAddress + $"/Task/GetUserNameById?userid={task.UserId}");
                        if (userNameResult.IsSuccessStatusCode)
                        {
                            worksheet.Cells[$"B{row}"].Value = userNameResult.Content.ReadAsStringAsync().Result;
                        }
                        else
                        {
                            worksheet.Cells[$"B{row}"].Value = $"Error: {userNameResult.StatusCode}";
                        }

                      
                        var projectNameResult =await _client.GetAsync (_client.BaseAddress + $"/Task/GetProjectNameById?projectid={task.ProjectId}");
                        if (projectNameResult.IsSuccessStatusCode)
                        {
                            worksheet.Cells[$"C{row}"].Value = projectNameResult.Content.ReadAsStringAsync().Result;
                        }
                        else
                        {
                            worksheet.Cells[$"C{row}"].Value = $"Error: {projectNameResult.StatusCode}";
                        }

                    
                        var locationNameResult =await _client.GetAsync (_client.BaseAddress + $"/Task/GetLoactionNameById?locationid={task.LocationId}");
                        if (locationNameResult.IsSuccessStatusCode)
                        {
                            worksheet.Cells[$"D{row}"].Value = locationNameResult.Content.ReadAsStringAsync().Result;
                        }
                        else
                        {
                            worksheet.Cells[$"D{row}"].Value = $"Error: {locationNameResult.StatusCode}";
                        }
                        worksheet.Cells[$"E{row}"].Value = task.CREATIONDATE;
                        worksheet.Cells[$"F{row}"].Value = task.UserStoryBugNumber;
                        worksheet.Cells[$"G{row}"].Value = task.TaskDescription;
                        worksheet.Cells[$"H{row}"].Value = task.StartTime;
                        worksheet.Cells[$"I{row}"].Value = task.EndTime;

                        row++;
                    }


                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();


                    var fileBytes = package.GetAsByteArray();
                    return new FileContentResult(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"{selectUser}.xlsx"
                    };
                }
            }
            catch (Exception)
            {

                return BadRequest("Error generating Excel file");
            }
        }





        [Route("Reports/DownloadPdf")]
        [HttpPost("DownloadPdf")]
        public async Task<IActionResult> DownloadPdfAsync(string selectUser, DateOnly? selectDate)
        {
            try
            {
                var userIdResponse = await _client.GetAsync(_client.BaseAddress + $"/Task/GetUserId?userName={selectUser}");

                if (!userIdResponse.IsSuccessStatusCode)
                {
                    return BadRequest("Error fetching user ID from the API");
                }

                int userId = await userIdResponse.Content.ReadAsAsync<int>();

                 var allTasksResponse = await _client.GetAsync(_client.BaseAddress + $"/Task/GetTasksByUserId?userId={userId}&selectDate={selectDate}");
               
                if (!allTasksResponse.IsSuccessStatusCode)
                {
                    var errorMessage = await allTasksResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error fetching all tasks from the API. Status Code: {allTasksResponse.StatusCode}, Error: {errorMessage}");

                    return BadRequest("Error fetching all tasks from the API");
                }

                List<TaskViewModel> allTasks = await allTasksResponse.Content.ReadAsAsync<List<TaskViewModel>>();


                var pdfDocument = new Aspose.Pdf.Document();
                var pdfPage = pdfDocument.Pages.Add();

                var table = new Aspose.Pdf.Table
                {
                    ColumnWidths = " 80 40 60 60 50 90 50 50",
                    DefaultCellPadding = new MarginInfo(5, 2, 5, 2),
                    Border = new BorderInfo(BorderSide.All, .5f, Aspose.Pdf.Color.Black),
                    DefaultCellBorder = new BorderInfo(BorderSide.All, .2f, Aspose.Pdf.Color.Black),
                };

                table.Top = 100;
                table.Left = 30;

                pdfPage.Paragraphs.Add(table);
                var headerRow = table.Rows.Add();
               // AddCell(headerRow, "Task ID");
                AddCell(headerRow, "UserName");
                AddCell(headerRow, "Project Name");
                AddCell(headerRow, "Location");
                AddCell(headerRow, "Task Date");
                AddCell(headerRow, "User Story/Bug Number");
                AddCell(headerRow, "Task Description");
                AddCell(headerRow, "Start Time");
                AddCell(headerRow, "End Time");


                foreach (var task in allTasks)
                {
                    var dataRow = table.Rows.Add();
                   // AddCell(dataRow, task.TaskId.ToString());

                    var userNameResult = await _client.GetAsync(_client.BaseAddress + $"/Task/GetUserNameById?userid={task.UserId}");
                    if (userNameResult.IsSuccessStatusCode)
                    {
                        AddCell(dataRow, userNameResult.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        AddCell(dataRow, $"Error: {userNameResult.StatusCode}");
                    }

                    var projectNameResult = await _client.GetAsync(_client.BaseAddress + $"/Task/GetProjectNameById?projectid={task.ProjectId}");
                    if (projectNameResult.IsSuccessStatusCode)
                    {
                        AddCell(dataRow, projectNameResult.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        AddCell(dataRow, $"Error: {projectNameResult.StatusCode}");
                    }

                    var locationNameResult = await _client.GetAsync(_client.BaseAddress + $"/Task/GetLoactionNameById?locationid={task.LocationId}");
                    if (locationNameResult.IsSuccessStatusCode)
                    {
                        AddCell(dataRow, locationNameResult.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        AddCell(dataRow, $"Error: {locationNameResult.StatusCode}");
                    }

                    AddCell(dataRow, task.CREATIONDATE.ToString("yyyy-MM-dd"));
                    AddCell(dataRow, task.UserStoryBugNumber);
                    AddCell(dataRow, task.TaskDescription);
                    AddCell(dataRow, task.StartTime.ToString("HH:mm:ss"));
                    AddCell(dataRow, task.EndTime.ToString("HH:mm:ss"));
                }

                var stream = new MemoryStream();
                pdfDocument.Save(stream);

                stream.Position = 0;
                // Return the PDF with the username as the file name
                return new FileContentResult(stream.ToArray(), "application/pdf")
                {
                    FileDownloadName = $"{selectUser}.pdf"
                };
            }
            catch (Exception)
            {
                return BadRequest("Error generating PDF file");
            }
        }

        private void AddCell(Row row, string text)
        {
            var cell = row.Cells.Add();
            cell.Paragraphs.Add(new TextFragment(text));
            cell.Border = new BorderInfo(BorderSide.All, .2f, Aspose.Pdf.Color.Black);
        }











    }
}
