using System.Text.Json;
// // See https://aka.ms/new-console-template for more information
// Console.WriteLine($"Program started");
// var cts=new CancellationTokenSource();

//   // using (var client=new HttpClient())
//   // {
//   //     try
//   //     {
//   //       var task=client.GetAsync("https://api.nuget.org/v3/index.json");
//   //       var result=await task;

//   //       Console.WriteLine(await result.Content.ReadAsStringAsync());
//   //     }
//   //     catch (System.Exception)
//   //     {

//   //         throw;
//   //     }
//   // }

// var customTask= Task.Run(() =>{
//     Thread.Sleep(2000);

//     if (cts.Token.IsCancellationRequested)
//     {
//       return "cancelled";
//     }

//     Console.WriteLine($"Hello from the custom task! {Thread.CurrentThread.ManagedThreadId}");

//     return "finished";
// },cts.Token);

// var waitForCancellationTask= Task.Run(() =>{
//   Console.ReadKey();
//   cts.Cancel();
// });


// // Console.WriteLine(customTask.Status);

// await waitForCancellationTask;
// var customTaskResult=await customTask;
// Console.WriteLine(customTaskResult);
// // Thread.Sleep(4000);

// var taskWithException = Task.Run(() =>
// {
//     // Console.WriteLine("hello from the inner task 1 ");
//     throw new Exception("exception from taskWithException");
// }).ContinueWith((t) =>
// {
//     throw new Exception("exception from taskWithException continue");
// });
// taskWithException.ContinueWith((t) =>
// {
//     throw new Exception("exception from taskWithException continue");
// }, TaskContinuationOptions.OnlyOnFaulted);


// try
// {
//   await taskWithException;
// }
// catch (AggregateException ae)
// {
//   Console.WriteLine(ae);
// }






Console.WriteLine("program started");

var cts = new CancellationTokenSource();
List<Task<IEnumerable<Student>>> tasks = new();
tasks = new List<Task<IEnumerable<Student>>>{
  Task.Run(async () => {
    Console.WriteLine("task 1 started");
    var students=await LoadStudents(2,cts.Token);
    Console.WriteLine("task 1 ended");
    return students;
  }),
  Task.Run(async () => {
    Console.WriteLine("task 2 started");
    var students=await LoadStudents(2,cts.Token);
    Console.WriteLine("task 2 ended");
    return students;
  }),
  Task.Run(async () => {
    Console.WriteLine("task 3 started");
    var students=await LoadStudents(2,cts.Token);
    Console.WriteLine("task 3 ended");
    return students;
  }),
  Task.Run(async () => {
    Console.WriteLine("task 4 started");
    var students=await LoadStudents(100,cts.Token);
    Console.WriteLine("task 4 ended");
    return students;
  })
};
while (tasks.Count > 0)
{
    var finishedTask = await Task.WhenAny<IEnumerable<Student>>(tasks);
    AddStudentsToConsole(finishedTask.Result);
    tasks.Remove(finishedTask);
}

Console.WriteLine("program ended");



const string url = "https://localhost:7251/api/Students";
async Task<IEnumerable<Student>> LoadStudents(int? count, CancellationToken ct)
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
            return students;
        }
        catch (System.Exception)
        {

            throw;
        }

    }
}

void AddStudentsToConsole(IEnumerable<Student> students)
{
    foreach (var student in students)
        Console.WriteLine(student);
}

public class Student
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public int Age { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName} {Age}";
    }
}
