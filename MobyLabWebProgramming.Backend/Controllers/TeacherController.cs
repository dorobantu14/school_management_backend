using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]

public class TeacherController : AuthorizationController
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService, IUserService userService) : base(userService)
    {
        _teacherService = teacherService;
    }
    
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<TeacherDTO>>> GetById([FromRoute] Guid id) {
        return this.FromServiceResponse(await _teacherService.GetTeacherById(id));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] TeacherDTO teacher)
    {
        return this.FromServiceResponse(await _teacherService.AddTeacher(teacher));
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] TeacherUpdateDTO teacher)
    {
        return this.FromServiceResponse(await _teacherService.UpdateTeacher(teacher));
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _teacherService.DeleteTeacher(id));
    }
    
    [Authorize]
    [HttpGet("{courseId:guid}")]
    public async Task<ActionResult<RequestResponse<List<GetTeachersByCourseDTO>>>> GetTeachersByCourse([FromRoute] Guid courseId)
    {
        return this.FromServiceResponse(await _teacherService.GetTeachersByCourse(courseId));
    }
}

