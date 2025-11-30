using CQRSMediatrStudent.Commands.AddStudent;
using CQRSMediatrStudent.Commands.DeleteStudent;
using CQRSMediatrStudent.Commands.UpdateStudent;
using CQRSMediatrStudent.Models;
using CQRSMediatrStudent.Queries.GetAllStudents;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQRSMediatrStudent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddStudentCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { StudentId = id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _mediator.Send(new GetAllStudentsQuery());
            return Ok(students);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _mediator.Send(new DeleteStudentCommand(id));
            return Ok(student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateStudentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok("Student updated successfully");
        }

    }

}
