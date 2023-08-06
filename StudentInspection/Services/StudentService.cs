using System.Text.Json;
using StudentInspection.Models;

public class StudentService : IStudentService
{
    const string url = "https://localhost:7251/api/Students";
    public async Task<IEnumerable<Student>> LoadStudents(int? count, CancellationToken ct)
    {
        using (var client = new HttpClient())
        {
            var customUrl = url;
            if (count.HasValue)
                customUrl += $"/{count.Value}";
            try
            {
                var task = client.GetAsync(customUrl, ct);
                var result = await task;

                var students = JsonSerializer.Deserialize<List<Student>>(await result.Content.ReadAsStringAsync());
                var test = await result.Content.ReadAsStringAsync();
                var test2 = await result.Content.ReadAsByteArrayAsync();
                return students ?? new List<Student>();
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}