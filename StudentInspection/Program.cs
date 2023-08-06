using System.Text.Json;
using StudentInspection.Models;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
.AddTransient<IStudentService, StudentService>().BuildServiceProvider();

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

// Resolve the service
var studentService = serviceProvider.GetService<IStudentService>()
            ?? throw new InvalidOperationException();


var cts = new CancellationTokenSource();
List<Task<IEnumerable<Student>>> tasks = new();

tasks = new List<Task<IEnumerable<Student>>>{
  studentService.LoadStudents(1,cts.Token),
  studentService.LoadStudents(2,cts.Token),
  studentService.LoadStudents(5,cts.Token),
  studentService.LoadStudents(10,cts.Token)
};

while (tasks.Count > 0)
{
    var finishedTask = await Task.WhenAny<IEnumerable<Student>>(tasks);
    AddStudentsToConsole(finishedTask.Result);
    tasks.Remove(finishedTask);
}

Console.WriteLine("program ended");


void AddStudentsToConsole(IEnumerable<Student> students)
{
    foreach (var student in students)
        Console.WriteLine(student);
}

