using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureStudentApi.Data;
using SecureStudentApi.Models;

namespace SecureStudentApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsDbContext _ctx;

        public StudentsController(StudentsDbContext ctx)
        {
            _ctx = ctx;

            // Seeding default data if DB is empty
            if (!_ctx.Students.Any())
            {
                _ctx.Students.AddRange(
                    new Student { Name = "Vid", Class = "7", Section = "A" },
                    new Student { Name = "div", Class = "8", Section = "B" }
                );
                _ctx.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _ctx.Students.ToListAsync();
            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            _ctx.Students.Add(student);
            await _ctx.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Student updated)
        {
            var existing = await _ctx.Students.FindAsync(id);
            if (existing is null) return NotFound();

            existing.Name = updated.Name;
            existing.Class = updated.Class;
            existing.Section = updated.Section;

            await _ctx.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var student = await _ctx.Students.FindAsync(id);
            if (student is null) return NotFound();

            _ctx.Students.Remove(student);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
