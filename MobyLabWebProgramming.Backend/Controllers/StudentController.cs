using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class StudentController : AuthorizationController
{
    private readonly IStudentService _studentService;
    private readonly IClassroomService _classroomService;
    public StudentController(IStudentService studentService, IClassroomService classroomService, IUserService userService) : base(userService)
    {
        _studentService = studentService;
        _classroomService = classroomService;
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
       return this.FromServiceResponse(await _studentService.AddStudent(student));
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] StudentUpdateDTO student)
    {
        return this.FromServiceResponse(await _studentService.UpdateStudent(student));
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _studentService.DeleteStudent(id));
    }
    
    [Authorize]
    [HttpGet("{classroomId:guid}")]
    public async Task<ActionResult<RequestResponse<List<GetStudentsByClassDTO>>>> GetStudents([FromRoute] Guid classroomId)
    {
        return this.FromServiceResponse(await _studentService.GetStudentByClassroomId(classroomId));
    }
}