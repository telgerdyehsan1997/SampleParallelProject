using StudentsApi.Models;

public interface IStudentGenerator
{
    Task<IEnumerable<Student>> GetAll();
    Task<IEnumerable<Student>> GetByCount(int count);
}