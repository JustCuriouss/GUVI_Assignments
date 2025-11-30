using CQRSMediatrStudent.Data;
using CQRSMediatrStudent.Models;
using MediatR;

namespace CQRSMediatrStudent.Commands.AddStudent
{
    public class AddStudentHandler : IRequestHandler<AddStudentCommand, int>
    {
        private readonly AppDbContext _context;
        public AddStudentHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student { Name = request.Name, Age = request.Age };
            _context.Students.Add(student);
            await _context.SaveChangesAsync(cancellationToken);
            return student.Id;
        }
    }

}
