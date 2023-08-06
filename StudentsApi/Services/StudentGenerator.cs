using StudentsApi.Models;

public class StudentGenerator : IStudentGenerator
{
    const string FirstnameFileAddr = "firstnames.csv";
    const string LastnameFileAddr = "lastnames.csv";
    public async Task<IEnumerable<Student>> GetAll()
    {
        var result = await Generate(null);
        Random random = new Random();
        Thread.Sleep(1000 * random.Next(2, 6));
        return result;
    }

    async Task<IEnumerable<Student>> IStudentGenerator.GetByCount(int count)
    {
        var result = await Generate(count);
        Random random = new Random();
        Thread.Sleep(1000 * random.Next(2, 6));
        return result;
    }

    async Task<IEnumerable<Student>> Generate(int? count)
    {
        string[] firstnames, lastnames;
        try
        {
            var firstnameFileReadingTask = File.ReadAllTextAsync(FirstnameFileAddr);
            var lastnameFileReadingTask = File.ReadAllTextAsync(LastnameFileAddr);
            await Task.WhenAll(firstnameFileReadingTask, lastnameFileReadingTask);

            firstnames = firstnameFileReadingTask.Result.Split(",");
            lastnames = lastnameFileReadingTask.Result.Split(",");
        }
        catch (System.Exception)
        {
            throw;
        }

        Random random = new Random();
        List<Student> students = new List<Student>();

        foreach (var firstname in firstnames)
        {
            foreach (var lastname in lastnames)
            {
                students.Add(new Student
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Age = random.Next(10, 31)
                });
                if (count.HasValue)
                    if (count.Value == students.Count)
                        break;
            }


            if (count.HasValue)
                if (count.Value == students.Count)
                    break;
        }

        Thread.Sleep(1000 * random.Next(2, 6));
        return students;
    }
}