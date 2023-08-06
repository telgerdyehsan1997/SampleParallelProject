using StudentInspection.Models;

public interface IStudentService
{
    Task<IEnumerable<Student>> LoadStudents(int? count, CancellationToken ct);
}