using CQRSMediatrStudent.Data;
using CQRSMediatrStudent.Exceptions;
using CQRSMediatrStudent.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSMediatrStudent.Commands.DeleteStudent
{
    public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand, int>
    {
        private readonly AppDbContext _context;
        public DeleteStudentHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
            if (student == null)
            {
                throw new StudentNotFoundException(request.Id);
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync(cancellationToken);
            return student.Id;
        }
    }
}
