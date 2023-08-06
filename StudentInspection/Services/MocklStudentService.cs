using System.Text.Json;
using StudentInspection.Models;

public class MockStudentService : IStudentService
{
    public Task<IEnumerable<Student>> LoadStudents(int? count, CancellationToken ct)
    {
        var result=new List<Student>(){
            new Student(){
                FirstName="John",
                LastName="Doe",
                Age=20
            },
            new Student(){
                FirstName="Jane",
                LastName="Doe",
                Age=30
            },
            new Student(){
                FirstName="John",
                LastName="Smith",
                Age=40
            },
            new Student(){
                FirstName="Jane",
                LastName="Smith",
                Age=50
            },
            new Student(){
                FirstName="John",
                LastName="Johnson",
                Age=60
            },
            new Student(){
                FirstName="Jane",
                LastName="Johnson",
                Age=70
            }
        };
        return Task.FromResult<IEnumerable<Student>>((count.HasValue) ? result.Take(count.Value) : result);
    }
}