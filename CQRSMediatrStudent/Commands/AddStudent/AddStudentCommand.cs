using MediatR;

namespace CQRSMediatrStudent.Commands.AddStudent
{
    public record AddStudentCommand(string Name, int Age) : IRequest<int>;
}
