using Microsoft.AspNetCore.Mvc;

namespace StudentsApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        IStudentGenerator studentGenerator = new StudentGenerator();
        return Ok(await studentGenerator.GetAll());
    }

    [HttpGet("{count}")]
    [Route("api/Student/{count}")]
    public async Task<IActionResult> Get(int count)
    {
        IStudentGenerator studentGenerator = new StudentGenerator();
        return Ok(await studentGenerator.GetByCount(count));
    }
}