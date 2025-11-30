using CQRSMediatrStudent.Data;
using CQRSMediatrStudent.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSMediatrStudent.Commands.UpdateStudent
{
    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, int>
    {
        private readonly AppDbContext _context;
        public UpdateStudentHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (student == null)
                return 0;

            student.Name = request.Name;
            student.Age = request.Age;

            await _context.SaveChangesAsync(cancellationToken);
            return student.Id;
        }
    }

}
