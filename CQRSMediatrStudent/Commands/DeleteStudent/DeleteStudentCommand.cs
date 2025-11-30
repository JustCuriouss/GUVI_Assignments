using MediatR;

namespace CQRSMediatrStudent.Commands.DeleteStudent
{
    public record DeleteStudentCommand(int Id) : IRequest<int>;
}
