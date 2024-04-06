using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class StudentController : AuthorizedController
{
    private readonly IStudentService _studentService;
    public StudentController(IStudentService studentService, IUserService userService) : base(userService)
    {
        _studentService = studentService;
    }
    
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<StudentDTO>>> GetById([FromRoute] Guid id) {
        return this.FromServiceResponse(await _studentService.GetStudentById(id));
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] StudentDTO student)
    {
        var currentUser = await GetCurrentUser();
       return this.FromServiceResponse(await _studentService.AddStudent(student, currentUser.Result));
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] StudentUpdateDTO student)
    {
        var currentUser = await GetCurrentUser();
        return this.FromServiceResponse(await _studentService.UpdateStudent(student, currentUser.Result));
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return this.FromServiceResponse(await _studentService.DeleteStudent(id, currentUser.Result));
    }
    
    [Authorize]
    [HttpGet("{classroomId:guid}")]
    public async Task<ActionResult<RequestResponse<List<GetStudentsByClassDTO>>>> GetStudents([FromRoute] Guid classroomId)
    {
        return this.FromServiceResponse(await _studentService.GetStudentByClassroomId(classroomId));
    }
}