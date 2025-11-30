using MediatR;

namespace CQRSMediatrStudent.Commands.UpdateStudent
{
    public record UpdateStudentCommand(int Id, string Name, int Age) : IRequest<int>;
}
