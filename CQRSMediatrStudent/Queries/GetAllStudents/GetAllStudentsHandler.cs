using CQRSMediatrStudent.Data;
using CQRSMediatrStudent.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSMediatrStudent.Queries.GetAllStudents
{
    public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, IEnumerable<Student>>
    {
        private readonly AppDbContext _context;

        public GetAllStudentsHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Students.AsNoTracking().ToListAsync(cancellationToken);
        }
    }

}
