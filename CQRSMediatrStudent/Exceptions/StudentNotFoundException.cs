namespace CQRSMediatrStudent.Exceptions
{
    public class StudentNotFoundException : Exception
    {
        public StudentNotFoundException(int id)
            : base($"Student with Id {id} was not found.")
        {
        }
    }
}
