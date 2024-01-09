using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Datacontext;
using StudentAPI.Model;

namespace StudentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]


    public class StudentController : Controller
    {
        private StudentContext dbContext;

        public StudentController(StudentContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await dbContext.Student.ToListAsync();
            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudents(AddStudents addStudents)
        {
            var student = new Students()
            {
                Email = addStudents.Email,
                Name = addStudents.Name,
                Phone = addStudents.Phone,
            };
            await dbContext.Student.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return Ok(student);

        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, AddStudents updatedStudentData)
        {
            var existingStudent = await dbContext.Student.FindAsync(id);
            if (existingStudent == null)
            {
                return NotFound();
            }
            // Updating the existing student with the new data

            existingStudent.Email = updatedStudentData.Email;
            existingStudent.Name = updatedStudentData.Name;
            existingStudent.Phone = updatedStudentData.Phone;
            dbContext.Entry(existingStudent).State = EntityState.Modified;

            try
            {

                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return StatusCode(500, "Concurrency issue occurred.");
            }
            return Ok(existingStudent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
           
            var student = await dbContext.Student.FindAsync(id);

            if (student == null)
            {
                return NotFound(); 
            }          
            dbContext.Student.Remove(student);         
            await dbContext.SaveChangesAsync();          
            return NoContent();
        }


        [HttpGet]

        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var student = await dbContext.Student.FindAsync(id);

                if (student == null)
                {
                    return NotFound();
                }

                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
