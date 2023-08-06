using StudentInspection.Models;

interface IStudentService
{
    Task<IEnumerable<Student>> LoadStudents(int? count, CancellationToken ct);
}