using CQRSMediatrStudent.Models;
using MediatR;

namespace CQRSMediatrStudent.Queries.GetAllStudents
{
    public record GetAllStudentsQuery() : IRequest<IEnumerable<Student>>;
}
